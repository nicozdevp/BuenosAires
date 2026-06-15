using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuenosAires.Model;
using BuenosAires.DataLayer;
using BuenosAires.Model.Utiles;

namespace BuenosAires.BusinessLayer
{
    public class BcAnwoListaProductos
    {
        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public AnwoStockProducto AnwoStockProducto = null;
        public List<AnwoStockProducto> Lista = null;

        public BcAnwoListaProductos()
        {
            Inicializar("");
        }

        public void Inicializar(string accion)
        {
            this.Accion = accion;
            this.Mensaje = "";
            this.HayErrores = false;
            this.AnwoStockProducto = null;
            this.Lista = null;
        }

        public void Reservar(string nroserieanwo)
        {
            
            var dc = new DcAnwoListaProductos();
            dc.Reservar(nroserieanwo);
            Util.CopiarPropiedades(dc, this);
        }
    }
}
