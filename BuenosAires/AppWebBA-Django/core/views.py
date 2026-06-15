from django.contrib.auth import login, logout, authenticate
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User
from django.shortcuts import redirect, render
from django.views.decorators.csrf import csrf_exempt
from .models import Producto, PerfilUsuario
from urllib.parse import urlencode
from .forms import ProductoForm, IniciarSesionForm
from .forms import RegistrarUsuarioForm, PerfilUsuarioForm
#from .error.transbank_error import TransbankError
from transbank.webpay.webpay_plus.transaction import Transaction, WebpayOptions
from django.db import connection
import random
import requests

# --- NUEVOS IMPORTS PARA LA API DEL BANCO CENTRAL ---
from django.conf import settings # Para leer las credenciales desde settings.py
import bcchapi
import pandas as pd # bcchapi devuelve DataFrames de Pandas
from datetime import date, timedelta
# ----------------------------------------------------
# Intenta leer desde settings.py
BCENTRAL_API_USER = getattr(settings, 'BCENTRAL_API_USER', "TU_EMAIL_POR_DEFECTO_O_ERROR")
BCENTRAL_API_PASS = getattr(settings, 'BCENTRAL_API_PASS', "TU_CONTRASENA_POR_DEFECTO_O_ERROR")
DOLAR_OBSERVADO_SERIE_ID = "F073.TCO.PRE.Z.D"

def obtener_valor_dolar_actualizado():
    if BCENTRAL_API_USER == "TU_EMAIL_POR_DEFECTO_O_ERROR" or BCENTRAL_API_PASS == "TU_CONTRASENA_POR_DEFECTO_O_ERROR":
        print("ADVERTENCIA: Credenciales de la API del Banco Central no configuradas en settings.py.")
        return None
    try:
        siete = bcchapi.Siete(BCENTRAL_API_USER, BCENTRAL_API_PASS)
        fecha_hoy = date.today()
        fecha_hasta_str = fecha_hoy.strftime("%Y-%m-%d")
        fecha_desde_str = (fecha_hoy - timedelta(days=7)).strftime("%Y-%m-%d")
        df_dolar = siete.cuadro(
            series=[DOLAR_OBSERVADO_SERIE_ID],
            nombres=['dolar_observado'],
            desde=fecha_desde_str,
            hasta=fecha_hasta_str
        )
        if df_dolar is not None and not df_dolar.empty:
            valores_validos = df_dolar['dolar_observado'].dropna()
            if not valores_validos.empty:
                return float(valores_validos.iloc[-1])
            print("No se encontraron valores válidos de dólar en el DataFrame.")
        else:
            print("Respuesta vacía o None del DataFrame de bcchapi para el dólar.")
        return None
    except Exception as e:
        print(f"Error al obtener el valor del dólar con bcchapi: {e}")
        return None

def home(request):
    return render(request, "core/home.html")

def iniciar_sesion(request):
    data = {"mesg": "", "form": IniciarSesionForm()}
    if request.method == "POST":
        form = IniciarSesionForm(request.POST)
        if form.is_valid(): # CORREGIDO
            username = request.POST.get("username")
            password = request.POST.get("password")
            user = authenticate(username=username, password=password)
            if user is not None:
                if user.is_active:
                    login(request, user)
                    try:
                        tipousu = PerfilUsuario.objects.get(user=user).tipousu
                        if tipousu != 'Bodeguero':
                            return redirect(home)
                        else:
                            data["mesg"] = "¡Acceso restringido para este tipo de usuario por esta vía!" 
                    except PerfilUsuario.DoesNotExist:
                        data["mesg"] = "Perfil de usuario no encontrado."
                else:
                    data["mesg"] = "¡La cuenta está desactivada!"
            else:
                data["mesg"] = "¡La cuenta o la password no son correctos!"
        else: 
            data["mesg"] = "Por favor, corrige los errores en el formulario."
            data["form"] = form 
    return render(request, "core/iniciar_sesion.html", data)

def cerrar_sesion(request):
    logout(request)
    return redirect(home)

def tienda(request):
    # Esta vista necesita llamar al SP_OBTENER_EQUIPOS_EN_BODEGA
    # si quieres mostrar disponibilidad desde la BD como discutimos.
    # Por ahora, la dejo como estaba en tu código original para minimizar cambios no solicitados.
    # Si quieres actualizarla para usar el SP, avísame.
    data = {"list": Producto.objects.all().order_by('idprod')}
    return render(request, "core/tienda.html", data)

@csrf_exempt
def ficha(request, id):
    data = {
        "mesg": "", 
        "producto": None, 
        "precio_usd": None, 
        "mesg_dolar": "" 
    }
    if request.method == "POST":
        if request.user.is_authenticated and not request.user.is_staff:
            return redirect(iniciar_pago, id=id) 
        else:
            data["mesg"] = "¡Para poder comprar debe iniciar sesión como cliente!"
    try:
        producto_obj = Producto.objects.get(idprod=id)
        data["producto"] = producto_obj
        if producto_obj.precio is not None: 
            valor_dolar = obtener_valor_dolar_actualizado() 
            if valor_dolar and valor_dolar > 0:
                try:
                    precio_clp = float(producto_obj.precio) 
                    data["precio_usd"] = round(precio_clp / valor_dolar, 2)
                except (ValueError, TypeError):
                    error_msg = f"Error: El precio del producto (ID: {id}) no es un número válido: '{producto_obj.precio}'."
                    print(error_msg)
                    data["mesg_dolar"] = "Precio base del producto no es numérico."
            else:
                data["mesg_dolar"] = "Valor del dólar no disponible en este momento." 
        else:
            data["mesg_dolar"] = "Producto sin precio definido en CLP."
    except Producto.DoesNotExist:
        data["mesg"] = "Producto no encontrado."
    return render(request, "core/ficha.html", data)

# =====================================================================
# INICIO DE MODIFICACIONES PARA GUARDAR FACTURA DE PRODUCTOS
# =====================================================================
@csrf_exempt
def iniciar_pago(request, id): # id aquí es idprod
    print("Iniciando pago para producto...")
    buy_order = str(random.randrange(1000000, 99999999))
    session_id = request.user.username # session_id para Transbank

    try:
        producto_a_pagar = Producto.objects.get(idprod=id)
        perfil_cliente = PerfilUsuario.objects.get(user=request.user)
        
        amount_producto = producto_a_pagar.precio # Renombrado para claridad
        rut_cliente_actual = perfil_cliente.rut
        nombre_producto = producto_a_pagar.nomprod
        id_producto_actual = producto_a_pagar.idprod

        # Guardar información de la compra en la sesión
        request.session[f'compra_producto_{buy_order}'] = {
            'idprod': id_producto_actual,
            'rutcli': rut_cliente_actual,
            'nomprod': nombre_producto,
            'precio_original': str(amount_producto) 
        }
        print(f"Datos de compra para PRODUCTO guardados en sesión (buy_order: {buy_order}): {request.session[f'compra_producto_{buy_order}']}")

    except Producto.DoesNotExist:
        print(f"Error: Producto con id {id} no encontrado en iniciar_pago.")
        return redirect('tienda') 
    except PerfilUsuario.DoesNotExist:
        print(f"Error: PerfilUsuario no encontrado para el usuario {request.user.username} en iniciar_pago.")
        return redirect('home') 

    # return_url para Transbank
    # Es mejor usar reverse para generar URLs, pero mantengo tu estructura por ahora
    # from django.urls import reverse
    # return_url = request.build_absolute_uri(reverse('pago_exitoso_url_name')) 
    return_url = request.build_absolute_uri('/pago_exitoso/') 

    # Credenciales de Transbank (idealmente desde settings.py)
    commercecode = getattr(settings, 'TRANSBANK_COMMERCE_CODE', '597055555532') 
    apikey = getattr(settings, 'TRANSBANK_API_KEY', '579B532A7440BB0C9079DED94D31EA1615BACEB56610332264630D42D0A36B1C')
    integration_type = getattr(settings, 'TRANSBANK_INTEGRATION_TYPE', 'TEST')

    tx = Transaction(options=WebpayOptions(commerce_code=commercecode, api_key=apikey, integration_type=integration_type))
    
    try:
        # Transbank espera el monto como entero (en pesos, sin decimales)
        response = tx.create(buy_order, session_id, int(amount_producto), return_url)
        print(f"Respuesta de Transbank tx.create (producto): {response}")
    except Exception as e:
        print(f"Error al crear transacción Transbank para producto: {e}")
        return redirect('ficha', id=id_producto_actual) 

    token_transbank = response.get('token')
    url_transbank = response.get('url')

    if not token_transbank or not url_transbank:
        print("Token o URL de Transbank faltantes en la respuesta para producto.")
        return redirect('ficha', id=id_producto_actual)

    context = {
        "buy_order": buy_order,
        "session_id": session_id,
        "amount": amount_producto,
        "return_url": return_url,
        "token_ws": token_transbank,
        "url_tbk": url_transbank,
        "first_name": request.user.first_name,
        "last_name": request.user.last_name,
        "email": request.user.email,
        "rut": rut_cliente_actual,
        "dirusu": perfil_cliente.dirusu,
    }
    return render(request, "core/iniciar_pago.html", context)

@csrf_exempt
@csrf_exempt

def pago_exitoso(request): 
    if request.method == "GET":
        token_tbk = request.GET.get("token_ws") or request.GET.get("TBK_TOKEN")
        
        if not token_tbk:
            print("Token de Transbank no encontrado en pago_exitoso.")
            return redirect('home') 

        print(f"Commit para token_ws/TBK_TOKEN: {token_tbk}")
        
        commercecode = getattr(settings, 'TRANSBANK_COMMERCE_CODE', '597055555532')
        apikey_transbank = getattr(settings, 'TRANSBANK_API_KEY', '579B532A7440BB0C9079DED94D31EA1615BACEB56610332264630D42D0A36B1C')
        integration_type = getattr(settings, 'TRANSBANK_INTEGRATION_TYPE', 'TEST')
        
        tx = Transaction(options=WebpayOptions(commerce_code=commercecode, api_key=apikey_transbank, integration_type=integration_type))
        
        try:
            response_commit = tx.commit(token=token_tbk)
            print(f"Respuesta de Transbank commit: {response_commit}")
        except Exception as e: 
            print(f"Error en Transbank commit: {e}")
            return redirect('tienda') 

        if response_commit and response_commit.get('status') == 'AUTHORIZED' and response_commit.get('response_code') == 0:
            print("Pago AUTORIZADO por Transbank.")
            
            buy_order_confirmada = response_commit.get('buy_order')

            # Intentamos obtener datos guardados de producto o servicio
            datos_compra_producto = request.session.pop(f'compra_producto_{buy_order_confirmada}', None)
            datos_compra_servicio = request.session.pop(f'compra_servicio_{buy_order_confirmada}', None)

            datos_compra = datos_compra_producto or datos_compra_servicio

            if datos_compra:
                print(f"Datos de compra recuperados de sesión: {datos_compra}")
                try:
                    with connection.cursor() as cursor:
                        monto_confirmado_tbk = int(response_commit.get('amount', 0))

                        # Si es producto, idprod viene; si es servicio, idprod será None
                        idprod_valor = datos_compra.get('idprod') if datos_compra_producto else None

                        desc_texto = f"Producto: {datos_compra.get('nomprod', '')}" if datos_compra_producto else f"Servicio: {datos_compra.get('descripcion', '')}"
                        desc_texto += f" (Orden TBK: {buy_order_confirmada})"

                        print("Llamando a SP_CREAR_FACTURA...")
                        cursor.execute(
                            "EXEC SP_CREAR_FACTURA @rutcli=%s, @idprod=%s, @fechafac=%s, @descfac=%s, @monto=%s",
                            [
                                datos_compra['rutcli'],
                                idprod_valor, 
                                date.today().strftime('%Y-%m-%d'),
                                desc_texto,
                                monto_confirmado_tbk
                            ]
                        )
                        res_sp = cursor.fetchone()
                        connection.commit()  # Muy importante
                        if res_sp and res_sp[0]:
                            nrofac_creada = res_sp[0]
                            print(f"Factura Nro. {nrofac_creada} CREADA.")
                        else:
                            print("ERROR: SP_CREAR_FACTURA no devolvió nrofac.")
                except Exception as e:
                    print(f"Error crítico al crear factura después de pago autorizado: {e}")
                    return render(request, "core/error_pago.html", {"mensaje": "Error al registrar la factura. Contacte soporte."})
            else:
                print(f"ALERTA: No se encontraron datos de compra en sesión para buy_order: {buy_order_confirmada}")
                return redirect('tienda')

            try:
                user_obj = User.objects.get(username=response_commit.get('session_id'))
                perfil_obj = PerfilUsuario.objects.get(user=user_obj)
            except (User.DoesNotExist, PerfilUsuario.DoesNotExist):
                user_obj = request.user 
                perfil_obj = getattr(user_obj, 'perfilusuario', None)

            context = {
                "buy_order": buy_order_confirmada,
                "session_id": response_commit.get('session_id'),
                "amount": response_commit.get('amount'),
                "token_ws": token_tbk, 
                "first_name": user_obj.first_name if user_obj and user_obj.is_authenticated else '',
                "last_name": user_obj.last_name if user_obj and user_obj.is_authenticated else '',
                "email": user_obj.email if user_obj and user_obj.is_authenticated else '',
                "rut": perfil_obj.rut if perfil_obj else '',
                "dirusu": perfil_obj.dirusu if perfil_obj else '',
                "response_code": response_commit.get('response_code')
            }
            return render(request, "core/pago_exitoso.html", context)
        else:
            error_msg_tbk = f"Pago rechazado o no autorizado. Estado: {response_commit.get('status')}, Código: {response_commit.get('response_code')}"
            print(error_msg_tbk)
            return render(request, "core/error_pago.html", {
                "mensaje": error_msg_tbk
            })
    else: 
        return redirect('home')

# =====================================================================
# FIN DE MODIFICACIONES PARA GUARDAR FACTURA DE PRODUCTOS
# =====================================================================

@csrf_exempt
def administrar_productos(request, action, id):
    if not (request.user.is_authenticated and request.user.is_staff):
        return redirect(home)
    data = {"mesg": "", "form": None, "action": action, "id": id} 
    if action == 'ins':
        if request.method == "POST":
            form = ProductoForm(request.POST, request.FILES)
            if form.is_valid(): 
                try:
                    form.save()
                    data["mesg"] = "¡El producto fue creado correctamente!"
                    form = ProductoForm() 
                except Exception as e: 
                    data["mesg"] = f"¡No se puede crear el producto!: {str(e)}"
        else: 
            form = ProductoForm()
        data["form"] = form
    elif action == 'upd':
        try:
            objeto = Producto.objects.get(idprod=id)
            if request.method == "POST":
                form = ProductoForm(data=request.POST, files=request.FILES, instance=objeto)
                if form.is_valid(): 
                    form.save()
                    data["mesg"] = "¡El producto fue actualizado correctamente!"
            else: 
                form = ProductoForm(instance=objeto)
            data["form"] = form
            data["objeto"] = objeto 
        except Producto.DoesNotExist:
            data["mesg"] = "Producto no encontrado para actualizar."
            data["action"] = 'ins' 
            data["form"] = ProductoForm()
    elif action == 'del':
        try:
            Producto.objects.get(idprod=id).delete()
            data["mesg"] = "¡El producto fue eliminado correctamente!"
        except Producto.DoesNotExist:
            data["mesg"] = "¡El producto ya estaba eliminado o no existe!"
        except Exception as e:
            data["mesg"] = f"Ocurrió un error al eliminar: {str(e)}"
        return redirect('administrar_productos', action='ins', id = '-1') 
    data["list"] = Producto.objects.all().order_by('idprod')
    return render(request, "core/administrar_productos.html", data)

def registrar_usuario(request):
    if request.method == 'POST':
        form = RegistrarUsuarioForm(request.POST)
        if form.is_valid():
            try:
                user = form.save()  # Guardar usuario primero
                
                rut = form.cleaned_data.get("rut")
                dirusu = form.cleaned_data.get("dirusu")
                
                # Crear o actualizar perfil con tipousu fijo
                PerfilUsuario.objects.update_or_create(
                    user=user,
                    defaults={'rut': rut, 'tipousu': 'cliente', 'dirusu': dirusu}
                )
                
                return redirect(iniciar_sesion)
            except Exception as e:
                print(f"Error en registro: {e}")
                form.add_error(None, "Ocurrió un error inesperado durante el registro.")
    else:
        form = RegistrarUsuarioForm()
    return render(request, "core/registrar_usuario.html", context={'form': form})


@login_required 
def perfil_usuario(request):
    try:
        perfil, created = PerfilUsuario.objects.get_or_create(user=request.user)
    except AttributeError: 
        return redirect('iniciar_sesion') 
    mesg = "" 
    if request.method == 'POST':
        form = PerfilUsuarioForm(request.POST) 
        if form.is_valid(): 
            request.user.first_name = form.cleaned_data.get('first_name', request.user.first_name)
            request.user.last_name = form.cleaned_data.get('last_name', request.user.last_name)
            request.user.email = form.cleaned_data.get('email', request.user.email)
            request.user.save()
            perfil.rut = form.cleaned_data.get('rut', perfil.rut)
            perfil.tipousu = form.cleaned_data.get('tipousu', perfil.tipousu)
            perfil.dirusu = form.cleaned_data.get('dirusu', perfil.dirusu)
            perfil.save()
            mesg = "¡Sus datos fueron actualizados correctamente!"
        else:
            mesg = "Por favor, corrija los errores en el formulario."
    else: 
        initial_data = {
            'first_name': request.user.first_name,
            'last_name': request.user.last_name,
            'email': request.user.email,
            'rut': perfil.rut,
            'tipousu': perfil.tipousu,
            'dirusu': perfil.dirusu,
        }
        form = PerfilUsuarioForm(initial=initial_data)
    data = {"mesg": mesg, "form": form}
    return render(request, "core/perfil_usuario.html", data)



@login_required
def obtener_solicitudes_de_servicio(request):
    solicitudes_lista = []
    tipousu_para_sp = None  # Lo que se pasará al SP
    rut_para_sp = ""
    display_tipousu = "Usuario"  # Lo que se mostrará en el título

    try:
        perfil = PerfilUsuario.objects.get(user=request.user)
        display_tipousu = perfil.tipousu  # Para mostrar en el título

        # Determinar qué tipousu y rut pasar al SP
        if request.user.is_staff or request.user.is_superuser:
            tipousu_para_sp = 'Todos'  # El SP debería tener una rama para 'Todos' que muestre todo
            rut_para_sp = ''           # Para el caso 'Todos', el SP podría no necesitar rut
            display_tipousu = f"{perfil.tipousu if perfil.tipousu else 'Admin'} (Vista General)"
        else:
            tipousu_para_sp = perfil.tipousu  # El tipo de usuario real del perfil
            rut_para_sp = perfil.rut

    except PerfilUsuario.DoesNotExist:
        print(f"ADVERTENCIA: PerfilUsuario no encontrado para {request.user.username}.")
        if request.user.is_staff or request.user.is_superuser:
            tipousu_para_sp = 'Todos'
            rut_para_sp = ''
            display_tipousu = "Administrador (Vista General)"
        else:
            contexto_error = {
                'tipousu': 'Desconocido',
                'lista': [],
                'error_message': 'No se encontró tu perfil de usuario para cargar las solicitudes.'
            }
            return render(request, "core/obtener_solicitudes_de_servicio.html", contexto_error)

    db_error_message = None  # Para errores específicos de la BD
    if tipousu_para_sp:  # Solo si tenemos un tipo de usuario válido para consultar
        try:
            with connection.cursor() as cursor:
                print(f"Ejecutando SP_OBTENER_SOLICITUDES_DE_SERVICIO con tipousu='{tipousu_para_sp}', rut='{rut_para_sp}'")
                cursor.execute("EXEC SP_OBTENER_SOLICITUDES_DE_SERVICIO @tipousu=%s, @rut=%s", [tipousu_para_sp, rut_para_sp])

                if cursor.description:  # ¡VERIFICACIÓN IMPORTANTE!
                    columnas = [col[0] for col in cursor.description]
                    for fila in cursor.fetchall():
                        solicitudes_lista.append(dict(zip(columnas, fila)))
                    print(f"Número de solicitudes encontradas: {len(solicitudes_lista)}")
                else:
                    print("SP_OBTENER_SOLICITUDES_DE_SERVICIO no devolvió un conjunto de resultados (cursor.description es None).")
        except Exception as e:
            error_original = str(e)
            print(f"Error al ejecutar SP_OBTENER_SOLICITUDES_DE_SERVICIO: {error_original}")
            db_error_message = f"Error al cargar solicitudes: {error_original}"
    else:
        print("No se pudo determinar un tipousu válido para la consulta al SP.")

    es_admin = request.user.is_superuser or request.user.is_staff

    contexto_final = {
        'tipousu': display_tipousu,
        'lista': solicitudes_lista,
        'es_admin': es_admin,
    }
    if db_error_message:
        contexto_final['db_error'] = db_error_message

    return render(request, "core/obtener_solicitudes_de_servicio.html", contexto_final)

@login_required
@csrf_exempt 
def ingresar_solicitud_servicio(request):
    if request.method == 'POST':
        if not request.user.is_staff:
            data_solicitud = { 
                'tipo': request.POST.get('tipo'),
                'descripcion': request.POST.get('descripcion'),
                'fecha': request.POST.get('fecha'),
                'hora': request.POST.get('hora'),
                'precio': 25000 
            }
            request.session['solicitud_data'] = data_solicitud
            return redirect('iniciar_pago_servicio')
        else:
            return render(request, "core/ingresar_solicitud_servicio.html", {
                "mesg": "¡Debe iniciar sesión como cliente!"
            })
    return render(request, "core/ingresar_solicitud_servicio.html")

@csrf_exempt 
@login_required
def iniciar_pago_servicio(request):
    solicitud_data_actual = request.session.get('solicitud_data')
    if not solicitud_data_actual:
        return redirect('ingresar_solicitud_servicio') 
    try:
        rut_cliente = request.user.perfilusuario.rut
        perfil_cliente_obj = request.user.perfilusuario 
    except (AttributeError, PerfilUsuario.DoesNotExist): 
        return redirect('iniciar_sesion') 
    datos_para_transbank = {
        'rutcli': rut_cliente,
        'tipo': solicitud_data_actual.get('tipo'),
        'descripcion': solicitud_data_actual.get('descripcion'),
        'fecha': solicitud_data_actual.get('fecha'),
        'hora': solicitud_data_actual.get('hora'),
        'precio': int(solicitud_data_actual.get('precio', 25000)) 
    }
    buy_order = str(random.randint(100000, 999999))
    session_id = request.user.username
    return_url = request.build_absolute_uri('/retorno_pago_servicio/')
    commerce_code_tbk = getattr(settings, 'TRANSBANK_COMMERCE_CODE', '597055555532')
    api_key_tbk = getattr(settings, 'TRANSBANK_API_KEY', '579B532A7440BB0C9079DED94D31EA1615BACEB56610332264630D42D0A36B1C')
    integration_type_tbk = getattr(settings, 'TRANSBANK_INTEGRATION_TYPE', 'TEST')
    tx = Transaction(WebpayOptions(
        commerce_code=commerce_code_tbk,
        api_key=api_key_tbk,
        integration_type=integration_type_tbk
    ))
    try:
        response = tx.create(buy_order, session_id, datos_para_transbank['precio'], return_url)
    except Exception as e: 
        print(f"Error al crear transacción Transbank: {e}")
        return redirect('ingresar_solicitud_servicio') 
    token_transbank = response.get('token')
    url_transbank = response.get('url')
    if not token_transbank or not url_transbank:
        return redirect('ingresar_solicitud_servicio')
    context = {
        "buy_order": buy_order,
        "session_id": session_id,
        "amount": datos_para_transbank['precio'],
        "return_url": return_url, 
        "token_ws": token_transbank, 
        "url_tbk": url_transbank,    
        "first_name": request.user.first_name,
        "last_name": request.user.last_name,
        "email": request.user.email,
        "rut": perfil_cliente_obj.rut, 
        "dirusu": perfil_cliente_obj.dirusu, 
        "tipo_servicio": datos_para_transbank['tipo'],
        "descripcion_servicio": datos_para_transbank['descripcion'],
        "fecha_servicio": datos_para_transbank['fecha'],
        "hora_servicio": datos_para_transbank['hora'],
    }
    return render(request, "core/iniciar_pago_servicio.html", context)

# =====================================================================
# INICIO DE MODIFICACIÓN EN 'retorno_pago_servicio' PARA GUARDAR FACTURA DE SERVICIO
# Y ASEGURAR QUE USA %s PARA SPs Y MANEJA 'idprod' como NULL para servicios
# =====================================================================
@csrf_exempt 
@login_required 
def retorno_pago_servicio(request):
    tbk_token = request.POST.get('TBK_TOKEN') or request.GET.get('TBK_TOKEN') 
    token_ws = request.POST.get('token_ws') or request.GET.get('token_ws') 
    final_token = tbk_token if tbk_token else token_ws

    if not final_token:
        print("Retorno Transbank sin token final.")
        return render(request, "core/error_pago.html", {"mensaje": "Token de transacción no encontrado."})

    commerce_code_tbk = getattr(settings, 'TRANSBANK_COMMERCE_CODE', '597055555532')
    api_key_tbk = getattr(settings, 'TRANSBANK_API_KEY', '579B532A7440BB0C9079DED94D31EA1615BACEB56610332264630D42D0A36B1C')
    integration_type_tbk = getattr(settings, 'TRANSBANK_INTEGRATION_TYPE', 'TEST')

    tx = Transaction(WebpayOptions(
        commerce_code=commerce_code_tbk,
        api_key=api_key_tbk,
        integration_type=integration_type_tbk
    ))

    response_commit = None
    try:
        response_commit = tx.commit(token=final_token) 
        print(f"DEBUG: Tipo de response_commit (servicio): {type(response_commit)}")
        print(f"DEBUG: Contenido de response_commit (servicio): {response_commit}")
    except Exception as e:
        print(f"Error en commit de Transbank (servicio): {str(e)}")
        return render(request, "core/error_pago.html", {"mensaje": f"Error al confirmar el pago del servicio: {str(e)}"})

    if response_commit and response_commit.get('status') == 'AUTHORIZED' and response_commit.get('response_code') == 0:
        print("Pago de SERVICIO AUTORIZADO por Transbank.")
        solicitud_data_guardada = request.session.pop('solicitud_data', None)

        if not solicitud_data_guardada:
            print("ALERTA: No se encontró solicitud_data en sesión después de un pago de servicio autorizado.")
            return render(request, "core/error_pago.html", {"mensaje": "Error interno: no se encontraron los detalles de la solicitud original."})

        session_id_transbank = response_commit.get('session_id')
        if request.user.is_authenticated and request.user.username != session_id_transbank:
            print(f"ALERTA DE SEGURIDAD (servicio): Discrepancia de sesión. Usuario Django: {request.user.username}, Sesión Transbank: {session_id_transbank}")
            return render(request, "core/error_pago.html", {"mensaje": "Error de validación de sesión."})
        
        try:
            usuario_actual = User.objects.get(username=session_id_transbank)
            perfil_usuario_actual = PerfilUsuario.objects.get(user=usuario_actual)
            rut_cliente_actual = perfil_usuario_actual.rut
        except (User.DoesNotExist, PerfilUsuario.DoesNotExist, AttributeError): 
            print(f"Error obteniendo usuario/perfil para session_id_transbank: {session_id_transbank}")
            return render(request, "core/error_pago.html", {"mensaje": "Error al obtener datos del usuario del pago."})

        try:
            with connection.cursor() as cursor:
                monto_confirmado = int(response_commit.get('amount', 0))
                buy_order_transbank = response_commit.get('buy_order', 'N/A')

                fecha_servicio = solicitud_data_guardada.get('fecha', date.today().strftime('%Y-%m-%d'))
                tipo_servicio = solicitud_data_guardada.get('tipo', 'Servicio General')
                descripcion_servicio = solicitud_data_guardada.get('descripcion', 'Servicio pagado vía Webpay')
                
                # Asegurar fecha en formato YYYY-MM-DD para evitar errores
                if isinstance(fecha_servicio, str):
                    fecha_servicio_formateada = fecha_servicio
                else:
                    fecha_servicio_formateada = fecha_servicio.strftime('%Y-%m-%d')

                print("Llamando a SP_CREAR_FACTURA para SERVICIO...")
                cursor.execute(
                    "EXEC SP_CREAR_FACTURA @rutcli=%s, @idprod=%s, @fechafac=%s, @descfac=%s, @monto=%s",
                    [
                        rut_cliente_actual, 
                        None,  
                        date.today().strftime('%Y-%m-%d'), 
                        f"Servicio: {tipo_servicio} (Orden TBK: {buy_order_transbank})", 
                        monto_confirmado
                    ]
                )
                res_sp_factura = cursor.fetchone()
                if not res_sp_factura or not res_sp_factura[0]:
                    print("ERROR: SP_CREAR_FACTURA no devolvió nrofac para servicio.")
                    raise Exception("SP_CREAR_FACTURA no devolvió nrofac.")
                nrofac_creado = res_sp_factura[0]
                print(f"Factura Nro. {nrofac_creado} CREADA para el servicio.")

                print("Llamando a SP_CREAR_SOLICITUD_SERVICIO...")
                cursor.execute("SELECT rut FROM PerfilUsuario WHERE tipousu = 'Técnico'")
                tecnicos_ruts = [row[0] for row in cursor.fetchall()]
                if not tecnicos_ruts:
                    print("ERROR: No hay técnicos disponibles para asignar la solicitud.")
                    raise Exception("No hay técnicos disponibles para asignar la solicitud.")
                
                rut_tecnico_asignado = random.choice(tecnicos_ruts)

                cursor.execute(
                    "EXEC SP_CREAR_SOLICITUD_SERVICIO %s, %s, %s, %s, %s, %s", 
                    [
                        nrofac_creado,
                        tipo_servicio,
                        fecha_servicio_formateada,
                        rut_tecnico_asignado,
                        descripcion_servicio,
                        'Pendiente'  # Estado inicial
                    ]
                )
                print(f"Solicitud de servicio creada para factura Nro. {nrofac_creado}.")
            
            contexto_exito = {
                "buy_order": buy_order_transbank,
                "authorization_code": response_commit.get('authorization_code'),
                "amount": monto_confirmado,
                "transaction_date": response_commit.get('transaction_date'),
                "nro_factura": nrofac_creado,
                "tipo_servicio": tipo_servicio,
            }
            return render(request, "core/solicitud_exitosa.html", contexto_exito)

        except Exception as e:
            print(f"Error crítico después de pago de servicio autorizado: {str(e)}")
            buy_order_error = response_commit.get('buy_order', 'No disponible') if response_commit else 'No disponible'
            return render(request, "core/error_pago.html", {
                "mensaje": f"Pago AUTORIZADO (Orden TBK: {buy_order_error}), pero ocurrió un error al registrar su servicio: {str(e)}. Por favor, contáctenos con su número de orden de compra."
            })
    else: 
        status_tbk = response_commit.get('status', 'DESCONOCIDO') if response_commit else 'ERROR EN RESPUESTA'
        response_code_tbk = response_commit.get('response_code', 'N/A') if response_commit else 'N/A'
        error_detalle = ""
        print(f"Pago de servicio NO AUTORIZADO. Estado: {status_tbk}, Código: {response_code_tbk}")
        return render(request, "core/error_pago.html", {
            "mensaje": f"Transacción rechazada por Webpay. Estado: {status_tbk}, Código: {response_code_tbk}. {error_detalle}"
        })
# =====================================================================
# FIN DE MODIFICACIÓN EN 'retorno_pago_servicio'
# =====================================================================

@login_required
def ver_facturas(request, rut=None):
    with connection.cursor() as cursor:
        # Si es superusuario o staff, pasar "admin" para obtener todas las facturas
        if request.user.is_superuser or request.user.is_staff:
            cursor.execute("EXEC SP_OBTENER_FACTURAS %s", ['admin'])
        else:
            # Si no es admin, obtener su rut real para filtrar
            if rut is None:
                try:
                    perfil = PerfilUsuario.objects.get(user=request.user)
                    rut = perfil.rut
                except PerfilUsuario.DoesNotExist:
                    rut = ''
            cursor.execute("EXEC SP_OBTENER_FACTURAS %s", [rut])

        columnas = [col[0] for col in cursor.description]
        facturas = [dict(zip(columnas, fila)) for fila in cursor.fetchall()]

        cursor.execute("EXEC SP_OBTENER_GUIAS_DE_DESPACHO")
        columnas_guias = [col[0] for col in cursor.description]
        guias = [dict(zip(columnas_guias, fila)) for fila in cursor.fetchall()]

    return render(request, "core/facturas.html", {
        "facturas": facturas,
        "guias": guias,
        "es_admin": request.user.is_superuser or request.user.is_staff
    })
