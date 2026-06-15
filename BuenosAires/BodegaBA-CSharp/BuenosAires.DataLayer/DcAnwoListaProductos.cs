using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuenosAires.Model;
using BuenosAires.Model.Utiles;

namespace BuenosAires.DataLayer
{
    public class DcAnwoListaProductos
    {
        public string Accion = "";
        public string Mensaje = "";
        public bool HayErrores = false;
        public AnwoStockProducto AnwoStockProducto = null;
        public List<AnwoStockProducto> Lista = null;

        public DcAnwoListaProductos()
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
            this.Inicializar($"Reservar  el producto con el numero de serie '{nroserieanwo}'");
            try
            {
                using (var bd = new base_datosEntities())
                {
                    var encontrado = bd.AnwoStockProducto.FirstOrDefault(p => p.nroserieanwo == nroserieanwo);
                    if (encontrado == null)
                    {
                        this.Mensaje = $"No fue posible {this.Accion} pues no existe en la base de datos";
                    }
                    else
                    {
                        encontrado.reservado = "S";
                        bd.SaveChanges();
                        this.AnwoStockProducto = new AnwoStockProducto();
                        Util.CopiarPropiedades(encontrado, this.AnwoStockProducto);
                        this.Mensaje = this.Mensaje = $"El producto {AnwoStockProducto.nroserieanwo} fue Reservado correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                this.HayErrores = true;
                this.Mensaje = Util.MensajeError(this.Accion, "DcProducto.Actualizar", ex);

            }
        }
    }
}
