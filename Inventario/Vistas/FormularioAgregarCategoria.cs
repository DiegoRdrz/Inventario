using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventario.Controladores;
using Inventario.Modelos;
using MySqlX.XDevAPI;

namespace Inventario.Vistas
{
    public partial class FormularioAgregarCategoria : Form
    {
        private ControladorCategorias Ccategoria;
        private ModeloCategorias categoria;
        Form formularioAnterior;
        FormularioCategorias formularioCategorias;

        public FormularioAgregarCategoria(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            Ccategoria = new ControladorCategorias();
            categoria = new ModeloCategorias();
            formularioCategorias = formularioAnterior as FormularioCategorias;


        }

        public FormularioAgregarCategoria(Form formularioAnterior, int ID, string nombre, string descripcion)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            formularioCategorias = formularioAnterior as FormularioCategorias;

            categoria = new ModeloCategorias();
            categoria.ID = ID;
            categoria.Nombre = nombre;
            categoria.Descripcion = descripcion;
            Ccategoria = new ControladorCategorias();

            txtNombre.Text = nombre;
            txtDescripcion.Text = descripcion;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            formularioCategorias.CargarTabla();
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            categoria.Nombre = txtNombre.Text;
            categoria.Descripcion = txtDescripcion.Text;
            if (categoria.ID == 0)
                Ccategoria.CrearCategorias(categoria);
            else
                Ccategoria.ActualizarCategorias(categoria);

            formularioAnterior.Show();
            formularioCategorias.CargarTabla();
            this.Close();
        }
    }
}
