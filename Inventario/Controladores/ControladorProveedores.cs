using Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controladores
{
    internal class ControladorProveedores
    {
        private BD db;

        public ControladorProveedores()
        {
            db = new BD("localhost", "inventario", "admin", "");
        }

        public void CrearProveedor(ModeloProveedores proveedor)
        {   
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", proveedor.Nombre);
            valores.Add("Dirreccion", proveedor.Direccion);
            valores.Add("Telefono",proveedor.Telefono);
            valores.Add("Correo",proveedor.correo);

            db.CrearRegistro("proveedores", valores);
        }

        public DataTable MostrarProveedores()
        {
            return db.LeerRegistros("proveedores");
        }

        public void ActualizarProveedor(ModeloProveedores proveedor)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", proveedor.Nombre);
            valores.Add("Dirreccion", proveedor.Direccion);
            valores.Add("Telefono", proveedor.Telefono);
            valores.Add("Correo", proveedor.correo);

            db.ActualizarRegistro("proveedores", proveedor.ID, valores);
        }

        public void EliminarCliente(int ID)
        {
            db.EliminarRegistro("proveedores", ID);
        }
    }
}
