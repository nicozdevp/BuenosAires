using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BuenosAires.Model;
using BuenosAires.Model.Utiles;
using System.Net.Http;
using Newtonsoft.Json;

namespace BuenosAires.ServiceLayer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WsAnwo" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WsAnwo.svc o WsAnwo.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WsAnwo : IWsAnwo
    {


        public Respuesta ObtenerAnwoListaProducto()
        {
            var resp = new Respuesta();
            resp.Accion = "obtener equipos en anwo";
            resp.Mensaje = "";
            resp.HayErrores = false;
            resp.XmlListaAnwoStockProducto = "";

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/Obtener_Anwo_Lista_Producto";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        List<AnwoStockProducto> lista = JsonConvert.DeserializeObject<List<AnwoStockProducto>>(json);
                        resp.XmlListaAnwoStockProducto = Util.SerializarXML(lista);
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
                resp.Mensaje = Util.MensajeError(resp.Accion, "WsAutenticacion.ObtenerAnwoListaProducto", ex);
                return resp;
            }
        }


        public Respuesta ReservarProducto(string nroserieanwo)
        {
            var resp = new Respuesta();
            resp.Accion = "reservar producto";
            resp.Mensaje = "";
            resp.HayErrores = false;

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/reservar_equipo_anwo";


            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new { nroserieanwo = nroserieanwo, reservado = "S" };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;

                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                        if (dict != null && dict.ContainsKey("error"))
                        {
                            resp.Mensaje = dict["error"];
                        }
                        else
                        {
                            resp.Mensaje = "No fue posible " + resp.Accion;
                        }
                        resp.HayErrores = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.HayErrores = true;
                resp.Mensaje = "Error en servicio: " + ex.Message;
            }

            return resp;
        }


    }
}
