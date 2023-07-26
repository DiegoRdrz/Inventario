using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventario.Modelos;
using Inventario.Controladores;

namespace Inventario.Vistas
{
    public partial class FormularioAgregarProveedor : Form
    {
        ModeloProveedores proveedor;
        ControladorProveedores Cproveedor;
        private Form formularioAnterior;
        FormularioProveedores formularioProveedores;
        public FormularioAgregarProveedor(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            proveedor = new ModeloProveedores();
            Cproveedor = new ControladorProveedores();
            formularioProveedores = formularioAnterior as FormularioProveedores;
        }
        public FormularioAgregarProveedor(Form formularioAnterior, int ID, string nombre, long telefono, string direccion, string correo)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            formularioProveedores = formularioAnterior as FormularioProveedores;
            proveedor = new ModeloProveedores();
            proveedor.ID = ID;
            proveedor.Nombre = nombre;
            proveedor.Telefono = telefono;
            proveedor.Direccion = direccion;
            proveedor.correo = correo;
            Cproveedor = new ControladorProveedores();

            txtNombre.Text = nombre;
            txtTelefono.Text = telefono.ToString();
            txtDireccion.Text = direccion;
            txtCorreo.Text = correo;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            formularioProveedores.CargarTabla();
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            proveedor.Nombre = txtNombre.Text;
            proveedor.Telefono = long.Parse(txtTelefono.Text);
            proveedor.Direccion = txtDireccion.Text;
            proveedor.correo = txtCorreo.Text;
            if (proveedor.ID == 0)
                Cproveedor.CrearProveedor(proveedor);
            else
                Cproveedor.ActualizarProveedor(proveedor);

            formularioAnterior.Show();
            formularioProveedores.CargarTabla();
            this.Close();
        }

        private void FormularioAgregarProveedor_Load(object sender, EventArgs e)
        {

        }
    }
}
