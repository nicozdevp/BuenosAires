using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuenosAires.Model
{
    public class ItemGuiaDespacho
    {
        public string nrogd { get; set; }
        public string nomprod { get; set; }
        public string estadogd { get; set; }
        public string nrofac { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        public string Cliente => $"{first_name} {last_name}";


    }
}
