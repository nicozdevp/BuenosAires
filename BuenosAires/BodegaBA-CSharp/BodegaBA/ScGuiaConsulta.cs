using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuenosAires.Model.Utiles;
using BuenosAires.Model;
using BodegaBA.WsConsultaDespachoReference;
using System.Net.Http;
using Newtonsoft.Json;

namespace BuenosAires.ServiceProxy
{
    public class ScGuiaConsulta
    {
        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public List<ItemGuiaDespacho> ListaGuiasDespacho = null;

        public void CopiarPropiedades(Respuesta resp)
        {
            this.Accion = resp.Accion;
            this.Mensaje = resp.Mensaje;
            this.HayErrores = resp.HayErrores;
            this.ListaGuiasDespacho = Util.DeserializarXML<List<ItemGuiaDespacho>>(resp.XmlGuiaDespacho);
        }
       

        public WsGuiaDespachoClient getWs()
        {
            var ws = new WsGuiaDespachoClient();
            ws.InnerChannel.OperationTimeout = new TimeSpan(1, 0, 0);
            return ws;
        }

        public void ConsultarGuiasDespacho()
        {
            CopiarPropiedades(getWs().ConsultarGuiasDespacho());
        }
    }
}
