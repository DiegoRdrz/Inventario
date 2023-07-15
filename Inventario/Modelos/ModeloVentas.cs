using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Modelos
{
    internal class ModeloVentas
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
    }
}
