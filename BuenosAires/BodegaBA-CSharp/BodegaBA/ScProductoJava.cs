using System.Collections.Generic;
using BuenosAires.Model.Utiles;
using BuenosAires.Model;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using BodegaBA.WsProductoReference;
using BodegaBA.WsProductoJavaReference;
using Newtonsoft.Json;
using BuenosAires.ServiceLayer;
using System;


namespace BodegaBA
{
    class ScProductoJava
    {
        public int idprod { get; set; }
        public string nomprod { get; set; }
        public string descprod { get; set; }
        public int precio { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public string disponibilidad { get; set; }
    

        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public string JsonStockProducto = "";
        public List<ItemStockProducto> Lista = new List<ItemStockProducto>();

        public void CopiarPropiedades(Respuesta resp)
        {
            this.Accion = resp.Accion;
            this.Mensaje = resp.Mensaje;
            this.HayErrores = resp.HayErrores;
            this.JsonStockProducto = resp.JsonStockProducto;

            if (resp.JsonAutenticado != "")
            {
                
                this.Lista =
                    JsonConvert.DeserializeObject<List<ItemStockProducto>>(resp.JsonStockProducto);
                this.Mensaje = resp.Mensaje;
            }
        }


        public WsProductoJavaClient getWs()
        {
            var ws = new WsProductoJavaClient();
            ws.InnerChannel.OperationTimeout = new TimeSpan(1, 0, 0);
            return ws;
        }

        public void ProductoJava()
        {
            CopiarPropiedades(getWs().ProductosJava());
        }

    }
}
