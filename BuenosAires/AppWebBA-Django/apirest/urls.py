from django.urls import path
from .views import autenticar, obtener_equipos_en_bodega, consultar_guias_despacho,Obtener_Anwo_Lista_Producto,reservar_equipo_anwo,actualizar_estado_guia

urlpatterns = [
    path('autenticar/<tipousu>/<username>/<password>', autenticar, name="autenticar"),
    path('obtener_equipos_en_bodega', obtener_equipos_en_bodega, name='obtener_equipos_en_bodega'),
    path('consultar_guias_despacho/', consultar_guias_despacho, name='consultar_guias_despacho'),
    path('Obtener_Anwo_Lista_Producto', Obtener_Anwo_Lista_Producto, name='Obtener_Anwo_Lista_Producto'),
    path('reservar_equipo_anwo', reservar_equipo_anwo, name='reservar_equipo_anwo'),
    path('actualizar_estado_guia', actualizar_estado_guia, name='actualizar_estado_guia'),
]