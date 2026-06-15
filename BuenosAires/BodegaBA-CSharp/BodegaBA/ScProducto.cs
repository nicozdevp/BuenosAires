using System.Collections.Generic;
using BuenosAires.Model.Utiles;
using BuenosAires.Model;
using System;
using BodegaBA.WsProductoReference;


namespace BuenosAires.ServiceProxy
{
    public class ScProducto
    {
        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public Producto Producto = null;
        public List<Producto> Lista = null;
        public List<ItemStockProducto> ListaStockProducto = null;

        public void CopiarPropiedades(Respuesta resp)
        {
            this.Accion = resp.Accion;
            this.Mensaje = resp.Mensaje;
            this.HayErrores = resp.HayErrores;
            this.Producto = Util.DeserializarXML<Producto>(resp.XmlProducto);
            this.ListaStockProducto = Util.DeserializarXML<List<ItemStockProducto>>(resp.XmlListaStockProducto);
        }

        public WsProductoClient getWs()
        {
            var ws = new WsProductoClient();
            ws.InnerChannel.OperationTimeout = new TimeSpan(1, 0, 0);
            return ws;
        }

        public void Crear(Producto producto)
        {
            CopiarPropiedades(getWs().Crear(producto));
        }

        public void LeerTodos()
        {
            CopiarPropiedades(getWs().LeerTodos());
        }

        public void Leer(int id)
        {
            CopiarPropiedades(getWs().Leer(id));
        }

        public void Actualizar(Producto producto)
        {
            CopiarPropiedades(getWs().Actualizar(producto));
        }

        public void Eliminar(int id)
        {
            CopiarPropiedades(getWs().Eliminar(id));
        }

        public void ObtenerEquiposEnBodega()
        {
            CopiarPropiedades(getWs().ObtenerEquiposEnBodega());
        }

    }
}
