# Sistema de Integración BuenosAires

Proyecto académico de integración de sistemas heterogéneos. La empresa ficticia **BuenosAires** comercializa equipos de climatización y automatización, y centraliza sus operaciones en cuatro sistemas desarrollados en distintas tecnologías que se comunican entre sí a través de servicios REST y SOAP.

---

## Arquitectura general

```
┌─────────────────────────────────────────────────────────────┐
│                   CLIENTES EXTERNOS                         │
│         Transbank WebPay          Banco Central de Chile    │
│          (pagos en línea)           (tipo de cambio USD)    │
└───────────────────┬─────────────────────────┬───────────────┘
                    │ HTTPS                   │ HTTPS
                    ▼                         ▼
┌─────────────────────────────────────────────────────────────┐
│               AppWebBA-Django  (puerto 8001)                │
│        App web para clientes  +  REST API JSON              │
│                   Base de datos: SQL Server                 │
└──────────────────────────┬──────────────────────────────────┘
                           │ HTTP REST JSON
          ┌────────────────┼────────────────┐
          ▼                ▼                ▼
┌──────────────────────────────────────────────────────────────┐
│              BodegaBA-CSharp  (puerto 54964)                 │
│     App escritorio WinForms  +  Servicios WCF/SOAP           │
└──────────────────────────────┬───────────────────────────────┘
                               │ SOAP / JAX-WS
                               ▼
              ┌────────────────────────────────┐
              │      VentaBA-Java              │
              │  App escritorio Java Swing     │
              └────────────────────────────────┘
```

---

## Sistemas del proyecto

### 1. AppWebBA-Django — App web y REST API

**Directorio:** `BuenosAires/AppWebBA-Django/`  
**Tecnologías:** Python 3, Django 3.2, Django REST Framework, SQL Server (mssql-django), Transbank SDK, bcchapi

El sistema central del proyecto. Cumple dos roles:

- **App web** para clientes finales: tienda, pagos, solicitudes de servicio, perfil de usuario.
- **REST API** consumida por los otros sistemas (C# y Java) para autenticación e inventario.

#### Funcionalidades web (app `core`)

| Funcionalidad | Descripción |
|---|---|
| Tienda de productos | Catálogo con imágenes y precios en CLP y USD |
| Ficha de producto | Precio en tiempo real en dólares vía API Banco Central |
| Registro de usuarios | Clientes se registran con RUT y dirección |
| Inicio de sesión | Autenticación por tipo de usuario |
| Pago de productos | Integración con Transbank WebPay Plus (modo TEST) |
| Solicitud de servicio | Instalación, mantención o reparación (pago vía Transbank) |
| Mis facturas | Historial de compras y servicios del cliente |
| Guías de despacho | Vista del estado del despacho de productos |
| Administración | CRUD de productos para usuarios Staff |

#### Tipos de usuario

| Tipo | Acceso |
|---|---|
| Cliente | Tienda, pagos, solicitudes, facturas propias |
| Técnico | Solicitudes de servicio asignadas |
| Bodeguero | Solo puede acceder vía app C# (acceso web restringido) |
| Vendedor | Solo puede acceder vía app Java |
| Administrador / Superusuario | Panel completo, todas las facturas y guías |

#### REST API (app `apirest`, prefijo `/BuenosAiresApiRest/`)

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `autenticar/<tipousu>/<username>/<password>` | Autentica un usuario por tipo |
| GET | `obtener_equipos_en_bodega` | Stock de productos disponibles en bodega |
| GET | `consultar_guias_despacho/` | Lista de guías de despacho |
| GET/POST | `Obtener_Anwo_Lista_Producto` | Productos del proveedor Anwo |
| POST | `reservar_equipo_anwo` | Reservar un equipo Anwo |
| POST | `actualizar_estado_guia` | Actualizar estado de una guía de despacho |

#### Stored Procedures SQL Server utilizados

- `SP_OBTENER_EQUIPOS_EN_BODEGA`
- `SP_CREAR_FACTURA`
- `SP_CREAR_SOLICITUD_SERVICIO`
- `SP_OBTENER_SOLICITUDES_DE_SERVICIO`
- `SP_OBTENER_FACTURAS`
- `SP_OBTENER_GUIAS_DE_DESPACHO`
- `SP_RESERVAR_EQUIPO_ANWO`
- `SP_ACTUALIZAR_ESTADO_GUIA`
- `SP_CONSULTAR_GUIAS_DESPACHO`

#### Modelos de base de datos

- `PerfilUsuario` — extiende el usuario Django con RUT, tipo y dirección
- `Producto` — catálogo de productos (ID, nombre, descripción, precio, imagen)
- `Factura` — registro de compras (productos y servicios)
- `SolicitudServicio` — solicitudes de instalación/mantención/reparación
- `GuiaDespacho` — seguimiento del despacho de equipos vendidos
- `StockProducto` — unidades físicas de cada producto en bodega
- `AnwoStockProducto` — equipos disponibles del proveedor externo Anwo

---

### 2. BodegaBA-CSharp — App escritorio de bodega y capa de servicios

**Directorio:** `BuenosAires/BodegaBA-CSharp/`  
**Tecnologías:** C# .NET Framework, Windows Forms, WCF (SOAP), Entity Framework, Newtonsoft.Json

Aplicación de escritorio para el personal de **Bodega**. Se divide en cinco proyectos de Visual Studio:

| Proyecto | Rol |
|---|---|
| `BodegaBA` | Interfaz Windows Forms (cliente de escritorio) |
| `BuenosAires.ServiceLayer` | Servicios WCF/SOAP expuestos a otros sistemas |
| `BuenosAires.BusinessLayer` | Lógica de negocio |
| `BuenosAires.DataLayer` | Acceso a datos con Entity Framework |
| `BuenosAires.Model` | Clases de dominio compartidas |

#### Formularios de la app (BodegaBA)

- **LoginBodegaBA** — autenticación del bodeguero vía WCF → REST API Django
- **VentanaPrincipal** — menú principal
- **ConsultarBodega** — consulta de stock disponible en bodega
- **ReservarAnwo** — reserva de equipos del proveedor Anwo
- **GuiaDespacho** — consulta y actualización de guías de despacho

#### Servicios WCF expuestos (puerto 54964)

| Servicio | Operaciones |
|---|---|
| `WsAutenticacion` | `Autenticar(tipousu, username, password)` |
| `WsProducto` | `Crear`, `Leer`, `LeerTodos`, `Actualizar`, `Eliminar`, `Reservar`, `ObtenerEquiposEnBodega` |
| `WsProductoJava` | `ProductosJava` (subconjunto para el cliente Java) |
| `WsAnwo` | `ObtenerAnwoListaProducto`, `ReservarProducto` |
| `WsGuiaDespacho` | `ConsultarGuiasDespacho`, `ActualizarEstadoGuia` |

> Todos los servicios de la capa WCF delegan a la REST API de Django en `http://127.0.0.1:8001` para las operaciones de inventario, autenticación y guías.

---

### 3. VentaBA-Java — App escritorio de ventas

**Directorio:** `BuenosAires/VentaBA-Java/`  
**Tecnologías:** Java 8, Java Swing, JAX-WS (cliente SOAP), NetBeans, Gson, json-simple

Aplicación de escritorio para el personal de **Ventas**. Consume los servicios WCF del sistema C# mediante proxies JAX-WS generados automáticamente desde los WSDLs.

#### Ventanas

- **VentanaLogin** — autentica al vendedor contra `WsAutenticacion` (C#)
- **ConsultarBodega** — muestra el stock disponible consultando `WsProductoJava` (C#)

#### Flujo de uso

1. El vendedor ingresa usuario y contraseña
2. VentanaLogin llama a `WsAutenticacion.Autenticar("Vendedor", ...)` vía SOAP
3. `WsAutenticacion` llama a la REST API de Django para validar
4. Si es correcto, abre `ConsultarBodega` con el stock actual

#### Proxies SOAP generados

| Paquete | Servicio consumido |
|---|---|
| `buenosaires.proxy` | `WsAutenticacion` |
| `buenosaires.proxy2` | `WsProductoJava` |

---

### 4. Anwo-SpringBoot — Sistema proveedor externo

**Directorio:** `BuenosAires/Anwo-SpringBoot/`  
**Tecnologías:** Spring Boot (Java)

Representa el sistema del proveedor externo **Anwo**, que provee equipos al sistema BuenosAires. El stock de Anwo se gestiona en la tabla `AnwoStockProducto` de la base de datos compartida.

---

### 5. PlandePruebasBuenosAiresJmeter — Pruebas de carga

**Directorio:** `BuenosAires/PlandePruebasBuenosAiresJmeter/`  
**Archivo:** `obtener_equipos_en_bodega.jmx`

Plan de pruebas de rendimiento con Apache JMeter sobre el endpoint REST `obtener_equipos_en_bodega`.

---

## Requisitos previos

### Para AppWebBA-Django

- Python 3.11+
- SQL Server con ODBC Driver 17
- Librerías: `pip install django djangorestframework mssql-django transbank bcchapi pandas django-extensions pillow`

### Para BodegaBA-CSharp

- Visual Studio 2019 o superior
- .NET Framework 4.7+
- SQL Server (misma instancia que Django)
- NuGet: Newtonsoft.Json (se restaura automáticamente)

### Para VentaBA-Java

- JDK 1.8 (Java 8)
- NetBeans IDE
- Librerías en `Libraries/`: `gson-2.13.1.jar`, `json-simple-1.1.1.jar`

---

## Configuración

### Variables de entorno (AppWebBA-Django)

Copiar `.env.example` a `.env` y completar con los valores reales:

```bash
cp BuenosAires/AppWebBA-Django/.env.example BuenosAires/AppWebBA-Django/.env
```

```env
DJANGO_SECRET_KEY=...
DB_NAME=base_datos
DB_USER=sa
DB_PASSWORD=...
DB_HOST=nombre-del-servidor
BCENTRAL_API_USER=tu-email@ejemplo.cl
BCENTRAL_API_PASS=tu-password
```

### Iniciar AppWebBA-Django

```bash
cd BuenosAires/AppWebBA-Django
python -m venv .venv
.venv/Scripts/activate        # Windows
source .venv/bin/activate     # Linux/Mac
pip install -r requirements.txt
python manage.py migrate
python manage.py runserver 8001
```

### Poblar la base de datos

```bash
python scripts/1-eliminar-tablas.py
python scripts/2-crear-usuarios.py
python scripts/3-poblar-base-datos.py
```

### Iniciar BodegaBA-CSharp

1. Abrir `BodegaBA.sln` en Visual Studio
2. Compilar la solución (F6)
3. Ejecutar el proyecto `BuenosAires.ServiceLayer` (expone los WCF en puerto 54964)
4. Ejecutar el proyecto `BodegaBA` (interfaz gráfica)

### Iniciar VentaBA-Java

1. Abrir el proyecto en NetBeans
2. Asegurarse de que los servicios WCF de C# estén corriendo en puerto 54964
3. Ejecutar `VentanaLogin.java`

---

## Flujo completo de integración

```
[Cliente en web]
  → compra producto o solicita servicio
  → paga con Transbank WebPay
  → Django ejecuta SP_CREAR_FACTURA en SQL Server
  → se genera GuiaDespacho (si es producto)

[Bodeguero en BodegaBA-CSharp]
  → consulta stock via WsProducto → Django REST API
  → reserva equipos Anwo via WsAnwo → Django REST API
  → actualiza estado de guías via WsGuiaDespacho → Django REST API

[Vendedor en VentaBA-Java]
  → se autentica via WsAutenticacion (C#) → Django REST API
  → consulta productos via WsProductoJava (C#) → Django REST API
```

---

## Documentación del proyecto

| Archivo | Contenido |
|---|---|
| `ERS.pdf` | Especificación de Requerimientos del Sistema |
| `Plan de Integracion.pdf` | Plan de integración de los sistemas |
| `Plan de Pruebas de Software.pdf` | Estrategia y casos de prueba |
| `Casos de Prueba de Integracion.xlsx` | Casos de prueba detallados con resultados |
| `Registro de Defectos.xlsx` | Registro de bugs encontrados durante las pruebas |
| `Checklist de Ambientes.pdf` | Lista de verificación para configuración de ambientes |
| `Plan de Implantación.pdf` | Plan de despliegue del sistema |


---

## Integraciones externas

| Servicio | Uso |
|---|---|
| **Transbank WebPay Plus** | Pago en línea de productos y servicios (ambiente TEST) |
| **API Banco Central de Chile** | Tipo de cambio USD para mostrar precio en dólares en la ficha del producto |
