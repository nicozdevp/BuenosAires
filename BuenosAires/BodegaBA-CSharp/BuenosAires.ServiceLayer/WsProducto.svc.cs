using BuenosAires.BusinessLayer;
using BuenosAires.Model.Utiles;
using BuenosAires.Model;
using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuenosAires.ServiceLayer
{
    public class WsProducto : IWsProducto
    {
        private Respuesta ObtenerRespuesta(BcProducto bc)
        {
            var respuesta = new Respuesta();
            respuesta.Accion = bc.Accion;
            respuesta.Mensaje = bc.Mensaje;
            respuesta.HayErrores = bc.HayErrores;
            respuesta.XmlProducto = Util.SerializarXML(bc.Producto);
            respuesta.XmlListaProducto = Util.SerializarXML(bc.Lista);
            return respuesta;
        }

        private Respuesta ObtenerAnwoListaProducto(BcAnwoListaProductos bc)
        {
            var respuesta = new Respuesta();
            respuesta.Accion = bc.Accion;
            respuesta.Mensaje = bc.Mensaje;
            respuesta.HayErrores = bc.HayErrores;
            respuesta.XmlAnwoStockProducto = Util.SerializarXML(bc.AnwoStockProducto);
            respuesta.XmlListaAnwoStockProducto = Util.SerializarXML(bc.AnwoStockProducto);
            return respuesta;
        }   

        public Respuesta Crear(Producto producto)
        {
            var bc = new BcProducto();
            bc.Crear(producto);
            return ObtenerRespuesta(bc);
        }

        public Respuesta LeerTodos()
        {
            var bc = new BcProducto();
            bc.LeerTodos();
            return ObtenerRespuesta(bc);
        }

        public Respuesta Reservar(string nroserieanwo)
        {
            var bc = new BcAnwoListaProductos();
            bc.Reservar(nroserieanwo);
            return ObtenerAnwoListaProducto(bc);
        }

        public Respuesta Leer(int id)
        {
            var bc = new BcProducto();
            bc.Leer(id);
            return ObtenerRespuesta(bc);
        }

        public Respuesta Actualizar(Producto producto)
        {
            var bc = new BcProducto();
            bc.Actualizar(producto);
            return ObtenerRespuesta(bc);
        }

        public Respuesta Eliminar(int id)
        {
            var bc = new BcProducto();
            bc.Eliminar(id);
            return ObtenerRespuesta(bc);
        }

        public Respuesta ObtenerEquiposEnBodega()
        {
            var resp = new Respuesta();
            resp.Accion = "obtener equipos en bodega";
            resp.Mensaje = "";
            resp.HayErrores = false;
            resp.XmlListaStockProducto = "";

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/obtener_equipos_en_bodega";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        List<ItemStockProducto> lista = JsonConvert.DeserializeObject<List<ItemStockProducto>>(json);
                        resp.XmlListaStockProducto = Util.SerializarXML(lista);
                    }
                    else
                    {
                        resp.HayErrores = true;
                        resp.Mensaje = "No fue posible " + resp.Accion;
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.HayErrores = true;
                resp.Mensaje = Util.MensajeError(resp.Accion, "WsAutenticacion.ObtenerEquiposEnBodega", ex);
                return resp;
            }
        }
    }
}
