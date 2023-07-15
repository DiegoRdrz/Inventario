using Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Inventario.Controladores
{
    internal class ControladorVentas
    {
        private BD db;

        public ControladorVentas()
        {
            db = new BD("localhost", "3306", "inventario", "root", "1234");
        }

        public void CrearVenta(ModeloVentas venta)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("ClienteID", venta.ClienteID);
            valores.Add("Fecha", venta.Fecha);
            valores.Add("Total", venta.Total);

            db.CrearRegistro("Venta", valores);
        }

        public DataTable MostrarVentas()
        {
            return db.LeerRegistros("Venta");
        }

        public void ActualizarVenta(ModeloVentas venta)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("ClienteID", venta.ClienteID);
            valores.Add("Fecha", venta.Fecha);
            valores.Add("Total", venta.Total);

            db.ActualizarRegistro("Venta", venta.ID, valores);
        }

        public void EliminarVenta(int ventaID)
        {
            db.EliminarRegistro("Venta", ventaID);
        }
    }
}
