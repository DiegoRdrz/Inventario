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

    public partial class FormularioAgregarCliente : Form
    {
        ControladorClientes Ccliente;
        ModeloClientes cliente;
        Form formularioAnterior;
        public FormularioAgregarCliente(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            cliente = new ModeloClientes();
            Ccliente = new ControladorClientes();
        }
        public FormularioAgregarCliente(Form formularioAnterior, int ID, string nombre, long telefono)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            cliente = new ModeloClientes();
            cliente.ID = ID;
            cliente.Nombre = nombre;
            cliente.Telefono = telefono;
            Ccliente = new ControladorClientes();

            txtNombre.Text = nombre;
            txtTelefono.Text = telefono.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            cliente.Nombre = txtNombre.Text;
            cliente.Telefono = long.Parse(txtTelefono.Text);
            if (cliente.ID == 0)
                Ccliente.CrearCliente(cliente);
            else
                Ccliente.ActualizarCliente(cliente);

            formularioAnterior.Show();
            this.Close();
        }


    }
}
