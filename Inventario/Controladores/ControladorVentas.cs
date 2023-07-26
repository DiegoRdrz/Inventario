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
            db = new BD("localhost", "3306", "inventario", "root", "");
        }

        public void CrearVenta(ModeloVentas venta)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("ClienteID", venta.ClienteID);
            valores.Add("Fecha", venta.Fecha);
            valores.Add("Total", venta.Total);

            db.CrearRegistro("ventas", valores);
        }

        public DataTable MostrarVentas()
        {
            return db.LeerRegistros("ventas");
        }

        public void ActualizarVenta(ModeloVentas venta)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("ClienteID", venta.ClienteID);
            valores.Add("Fecha", venta.Fecha);
            valores.Add("Total", venta.Total);

            db.ActualizarRegistro("ventas", venta.ID, valores);
        }

        public void EliminarVenta(int ventaID)
        {
            db.EliminarRegistro("ventas", ventaID);
        }

        public int obtenerUltimoId()
        {
            return db.ObtenerUltimoID("ventas");
        }
    }
}
