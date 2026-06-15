using BuenosAires.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BuenosAires.BusinessLayer;
using BuenosAires.Model.Utiles;
using System.Net.Http;
using Newtonsoft.Json;

namespace BuenosAires.ServiceLayer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WsGuiaDespacho" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WsGuiaDespacho.svc o WsGuiaDespacho.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WsGuiaDespacho : IWsGuiaDespacho
    {
        public Respuesta ConsultarGuiasDespacho()
        {
            var resp = new Respuesta();
            resp.Accion = "consultar guías de despacho";
            resp.Mensaje = "";
            resp.HayErrores = false;
            resp.XmlGuiaDespacho = ""; // Aquí se guarda el XML serializado con las guías

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/consultar_guias_despacho";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        List<ItemGuiaDespacho> lista = JsonConvert.DeserializeObject<List<ItemGuiaDespacho>>(json);
                        resp.XmlGuiaDespacho = Util.SerializarXML(lista);
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
                resp.Mensaje = Util.MensajeError(resp.Accion, "WsGuiaDespacho.ConsultarGuiasDespacho", ex);
                return resp;
            }
        }

        public Respuesta ActualizarEstadoGuia(string nroGd, string estadoGd)
        {
            var resp = new Respuesta();
            resp.Accion = "actualizar estado guía";
            resp.Mensaje = "";
            resp.HayErrores = false;

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/actualizar_estado_guia";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var data = new
                    {
                        numero_guia = nroGd,
                        nuevo_estado = estadoGd
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    var result = client.PostAsync(apiUrl, content).Result;
                    string respuestaTexto = result.Content.ReadAsStringAsync().Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        resp.HayErrores = true;
                        resp.Mensaje = $"No fue posible {resp.Accion}. {respuestaTexto}";
                    }
                    else
                    {
                        dynamic obj = JsonConvert.DeserializeObject(respuestaTexto);
                        resp.Mensaje = obj.mensaje != null ? obj.mensaje.ToString() : "Estado actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.HayErrores = true;
                resp.Mensaje = $"Error en servicio: {ex.Message}";
            }

            return resp;
        }
    }
}
