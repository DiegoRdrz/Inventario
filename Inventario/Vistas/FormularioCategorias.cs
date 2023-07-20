using Inventario.Controladores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario.Vistas
{
    public partial class FormularioCategorias : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioCategorias()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (PrincipalFormulario != null)
            {
                PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarCategoria(this));
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvCategorias.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                string descripcion = filaSeleccionada.Cells["Descripcion"].Value.ToString();
                // Obtener los demás valores de las celdas

                {
                    PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarCategoria(this, ID, nombre, descripcion));
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvCategorias.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                ControladorCategorias Ccategoria = new ControladorCategorias();
                Ccategoria.EliminarCategoria(ID);
            }
            CargarTabla();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PrincipalFormulario.restaurarColorBtns(5);
            this.Close();
        }

        private void FormularioCategorias_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void FormularioCategorias_Shown(object sender, EventArgs e)
        {
            CargarTabla();
        }


        public void CargarTabla()
        {
                ControladorCategorias Ccategoria = new ControladorCategorias();

                DataTable Tcategorias = Ccategoria.MostrarCategorias();
                dgvCategorias.DataSource = Tcategorias;

                foreach (DataGridViewColumn columna in dgvCategorias.Columns)
                {
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                dgvCategorias.ReadOnly = true;
                dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCategorias.ClearSelection();
                dgvCategorias.AllowUserToAddRows = false;
        }
    }
}
