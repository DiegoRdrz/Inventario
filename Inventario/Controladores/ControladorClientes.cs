using Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controladores
{
    internal class ControladorClientes
    {
        private BD db;

        public ControladorClientes()
        {
            db = new BD("localhost", "3306", "inventario", "root", "");
        }

        public void CrearCliente(ModeloClientes cliente)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", cliente.Nombre);
            valores.Add("Telefono", cliente.Telefono);

            db.CrearRegistro("clientes", valores);
        }

        public DataTable MostrarClientes()
        {
            return db.LeerRegistros("clientes");
        }

        public void ActualizarCliente(ModeloClientes clientes)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", clientes.Nombre);
            valores.Add("Telefono", clientes.Telefono);


            db.ActualizarRegistro("clientes", clientes.ID, valores);
        }

        public void EliminarCliente(int ID)
        {
            db.EliminarRegistro("clientes", ID);
        }

        public List<string> obtenerNombres()
        {
            return db.ObtenerNombres("clientes");
        }

        public int BuscarIdPorNombre(string nombre)
        {
            return db.ObtenerIdPorNombre("clientes", nombre);
        }

    }
}
