﻿using Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Controladores
{
    internal class ControladorProductos
    {
        private BD db;

        public ControladorProductos()
        {
            db = new BD("localhost", "3306", "inventario", "root", "1234");
        }

        public void CrearProducto(ModeloProductos producto)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", producto.Nombre);
            valores.Add("Precio", producto.Precio);
            valores.Add("Cantidad", producto.Cantidad);
            valores.Add("CodigoBarras" , producto.CodigoBarras);
            valores.Add("CategoriaID", producto.CategoriaID);
            valores.Add("ProveedorID", producto.ProveedorID);

            db.CrearRegistro("productos", valores);
        }

        public DataTable MostrarProductos()
        {
            return db.LeerRegistros("productos");
        }

        public void ActualizarProducto(ModeloProductos producto)
        {
            Dictionary<string, object> valores = new Dictionary<string, object>();
            valores.Add("Nombre", producto.Nombre);
            valores.Add("Precio", producto.Precio);
            valores.Add("Cantidad", producto.Cantidad);
            valores.Add("CodigoBarras", producto.CodigoBarras);
            valores.Add("CategoriaID", producto.CategoriaID);
            valores.Add("ProveedorID", producto.ProveedorID);

            db.ActualizarRegistro("productos", producto.ID, valores);
        }

        public void EliminarProducto(int ID)
        {
            db.EliminarRegistro("producto", ID);
        }
    }
}