using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Modelos
{
    internal class ModeloProductos
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public string CodigoBarras { get; set; }
        public int CategoriaID { get; set; }
        public int ProveedorID { get; set; }

    }
}
