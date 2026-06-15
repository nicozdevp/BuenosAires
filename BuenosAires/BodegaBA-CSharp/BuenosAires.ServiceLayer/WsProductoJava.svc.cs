using BuenosAires.BusinessLayer;
using BuenosAires.Model.Utiles;
using BuenosAires.Model;
using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuenosAires.ServiceLayer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WsProductoJava" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WsProductoJava.svc o WsProductoJava.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WsProductoJava : IWsProductoJava
    {
        public Respuesta ProductosJava()
        {
            var resp = new Respuesta();
            resp.Accion = "Obtener equipos en bodega";
            resp.Mensaje = "";
            resp.HayErrores = false;
            resp.JsonStockProducto = "";

            //if (username.Trim() == "") username = "no-fue-digitado-el-username";
            //if (password.Trim() == "") password = "no-fue-digitada-la-password";

            string apiUrl = "http://127.0.0.1:8001/BuenosAiresApiRest/obtener_equipos_en_bodega";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        resp.JsonStockProducto = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        resp.HayErrores = true;
                        resp.Mensaje = "No fue posible " + resp.Accion +", intente nuevamente más tarde "
                            + "o comuníquese con el Administrador del Sistema";
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.HayErrores = true;
                resp.Mensaje = Util.MensajeError(resp.Accion, "WsAutenticacion.ProductosJava", ex);
                return resp;
            }
        }
    }
}
