using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BuenosAires.Model
{

    public class ItemStockProducto
    {
        public int idprod { get; set; }
        public string nomprod { get; set; }
        public string descprod { get; set; }
        public int precio { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public string disponibilidad { get; set; }
    }
}
