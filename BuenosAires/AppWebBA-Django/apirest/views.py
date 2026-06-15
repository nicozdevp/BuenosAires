from requests import Response
from rest_framework.decorators import api_view
from django.views.decorators.csrf import csrf_exempt
from django.http import JsonResponse
from django.contrib.auth import authenticate
from core.models import PerfilUsuario
from django.db import connection
from django.db import connection
from django.http import JsonResponse
from rest_framework.decorators import api_view
import json

@csrf_exempt
@api_view(['GET'])
def autenticar(request, tipousu, username, password):
    user = authenticate(username=username, password=password)
    if user:
        perfil = PerfilUsuario.objects.get(user=user)
        nombre = f'{user.first_name} {user.last_name}'
        tipo = perfil.tipousu
        if tipo in [tipousu, 'Administrador']:
            return JsonResponse({'Autenticado': True, 'NombreUsuario': nombre, 'TipoUsuario': tipo, 'Mensaje': ''})
        msg = f'La cuenta de usuario {nombre} es del perfil {tipo}, por lo que no puede ingresar al sistema'
    else:
        nombre, tipo, msg = '', '', 'La cuenta o la contraseña no coinciden con un usuario válido'
    return JsonResponse({'Autenticado': False, 'NombreUsuario': nombre, 'TipoUsuario': tipo, 'Mensaje': msg})

@csrf_exempt
@api_view(['GET'])
def obtener_equipos_en_bodega(request):
    try:
        with connection.cursor() as cursor:
            cursor.execute("EXEC SP_OBTENER_EQUIPOS_EN_BODEGA")
            resultados = cursor.fetchall()

        if resultados:
            productos = []
            for row in resultados:
                productos.append({
                    'idprod': row[0],
                    'nomprod': row[1],
                    'descprod': row[2],
                    'precio': row[3],
                    'imagen': row[4],
                    'cantidad': row[5],
                    'disponibilidad': row[6],
                })
                
            return JsonResponse(productos, safe=False, status=200)
        else:
            return JsonResponse({'mensaje': 'No se encontraron productos'}, status=404)

    except Exception as e:
        return JsonResponse({'error': str(e)}, status=500)
    
@csrf_exempt
@api_view(['GET'])
def consultar_guias_despacho(request):
    try:
        with connection.cursor() as cursor:
            # Ejecuta el stored procedure o consulta que te devuelve las guías
            cursor.execute("EXEC SP_CONSULTAR_GUIAS_DESPACHO")
            resultados = cursor.fetchall()

        if resultados:
            guias = []
            for row in resultados:
                guias.append({
                    'nrogd': row[0],          # Número de guía despacho
                    'idprod': row[1],  
                    'nomprod': row[2], # Nombre del producto
                    'estadogd': row[3],       # Estado de la guía despacho
                    'nrofac': row[4], 
                    'rutcli': row[5],# Número de factura
                    'first_name': row[6],
                    'last_name': row[7],
                    # Nombre del cliente     
                })
            return JsonResponse(guias, safe=False, status=200)
        else:
            return JsonResponse({'mensaje': 'No se encontraron guías de despacho'}, status=404)

    except Exception as e:
        return JsonResponse({'error': str(e)}, status=500)


@csrf_exempt
@api_view(['GET', 'POST'])
def Obtener_Anwo_Lista_Producto(request):
    if request.method == 'GET':
        # Obtener productos
        try:
            with connection.cursor() as cursor:
                cursor.execute("""
                    SELECT nroserieanwo, nomprodanwo, precioanwo, reservado
                    FROM AnwoStockProducto
                    ORDER BY reservado DESC
                """)
                resultados = cursor.fetchall()

            if resultados:
                equipos_reservados = []
                for row in resultados:
                    equipos_reservados.append({
                        'nroserieanwo': row[0],
                        'nomprodanwo': row[1],
                        'precioanwo': row[2],
                        'reservado': row[3],
                    })

                return JsonResponse(equipos_reservados, safe=False, status=200)
            else:
                return JsonResponse({'mensaje': 'No hay equipos reservados'}, status=404)

        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    elif request.method == 'POST':
        # Reservar producto
        try:
            data = json.loads(request.body)
            nroserie = data.get("nroserieanwo")
            reservado = data.get("reservado", "S").upper()

            if not nroserie:
                return JsonResponse({'error': 'Falta nroserieanwo'}, status=400)

            with connection.cursor() as cursor:
                cursor.execute("""
                    UPDATE AnwoStockProducto
                    SET reservado = %s
                    WHERE nroserieanwo = %s AND reservado = 'N'
                """, [reservado, nroserie])

            if cursor.rowcount == 0:
                return JsonResponse({'error': 'No fue posible reservar el producto'}, status=400)

            return JsonResponse({'mensaje': 'Producto reservado correctamente'}, status=200)

        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)


@csrf_exempt
@api_view(['POST'])
def reservar_equipo_anwo(request):
    try:
        data = json.loads(request.body)
        nroserie = data.get('nroserieanwo')
        reservado = data.get('reservado', 'S')  # Por defecto lo deja como reservado = 'S'

        if not nroserie:
            return JsonResponse({'error': 'nroserieanwo es requerido'}, status=400)

        with connection.cursor() as cursor:
            cursor.execute("EXEC SP_RESERVAR_EQUIPO_ANWO @nroserieanwo = %s, @reservado = %s", [nroserie, reservado])

        return JsonResponse({'mensaje': 'Equipo reservado correctamente'}, status=200)

    except Exception as e:
        return JsonResponse({'error': str(e)}, status=500)
    
@csrf_exempt
@api_view(['POST'])
def actualizar_estado_guia(request):
    try:
        data = json.loads(request.body)
        nro = data.get('numero_guia')
        estado = data.get('nuevo_estado')

        if not nro or not estado:
            return JsonResponse({'error': 'Faltan parámetros'}, status=400)

        with connection.cursor() as cursor:
            cursor.execute("EXEC SP_ACTUALIZAR_ESTADO_GUIA @nrogd = %s, @estado = %s", [nro, estado])

        return JsonResponse({'mensaje': 'Estado actualizado correctamente'})
    except Exception as e:
        return JsonResponse({'error': str(e)}, status=500)

    