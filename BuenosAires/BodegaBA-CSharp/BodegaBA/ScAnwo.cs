using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodegaBA.WsObtenerAnwoReference;
using BuenosAires.Model;
using BuenosAires.Model.Utiles;

namespace BuenosAires.ServiceProxy
{
    class ScAnwo
    {

        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public AnwoStockProducto Producto = null;
        public List<AnwoStockProducto> Lista = null;
        public List<AnwoStockProducto> ListaAnwoStockProducto = null;

        public void CopiarPropiedades(Respuesta resp)
        {
            this.Accion = resp.Accion;
            this.Mensaje = resp.Mensaje;
            this.HayErrores = resp.HayErrores;
            this.Producto = Util.DeserializarXML<AnwoStockProducto>(resp.XmlAnwoStockProducto);
            this.ListaAnwoStockProducto = Util.DeserializarXML<List<AnwoStockProducto>>(resp.XmlListaAnwoStockProducto);
        }

        public WsAnwoClient getWs()
        {
            var ws = new WsAnwoClient();
            ws.InnerChannel.OperationTimeout = new TimeSpan(1, 0, 0);
            return ws;
        }

        public void ObtenerAnwoListaProducto()
        {
            CopiarPropiedades(getWs().ObtenerAnwoListaProducto());
        }
    }
}
