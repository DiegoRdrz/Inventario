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

namespace Inventario.Vistas
{
    public partial class FormularioAgregarCategoria : Form
    {
        private ControladorCategorias Ccategoria;
        private ModeloCategorias categoria;
        Form formularioAnterior;
        public FormularioAgregarCategoria(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            Ccategoria = new ControladorCategorias();
            categoria = new ModeloCategorias();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            categoria.Nombre = txtNombre.Text;
            categoria.Descripcion = txtDescripcion.Text;

            Ccategoria.CrearCategorias(categoria);

            formularioAnterior.Show();
            this.Close();
        }
    }
}
