using Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controladores
{
    internal class ControladorCategorias
    {
        private BD db;

        public ControladorCategorias()
        {
            db = new BD("localhost", "inventario", "admin", "");
        }

        public void CrearCategorias(ModeloCategorias categoria)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", categoria.Nombre);
            valores.Add("Descripcion", categoria.Descripcion);

            db.CrearRegistro("categorias", valores);
        }

        public DataTable MostrarCategorias()
        {
            return db.LeerRegistros("categorias");
        }

        public void ActualizarCategorias(ModeloCategorias categoria)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", categoria.Nombre);
            valores.Add("Descripcion", categoria.Descripcion);


            db.ActualizarRegistro("categorias", categoria.ID, valores);
        }

        public void EliminarCategoria(int ID)
        {
            db.EliminarRegistro("categorias", ID);
        }
    }
}
