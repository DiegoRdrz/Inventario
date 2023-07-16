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
    public partial class FormularioClientes : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioClientes()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (PrincipalFormulario != null)
            {
                PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarCliente(this));
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormularioClientes_Load(object sender, EventArgs e)
        {
            ControladorClientes Cclientes = new ControladorClientes();

            DataTable Tclientes = Cclientes.MostrarClientes();
            dgvClientes.DataSource = Tclientes;

            foreach (DataGridViewColumn columna in dgvClientes.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.ClearSelection();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvClientes.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                long telefono = long.Parse(filaSeleccionada.Cells["Telefono"].Value.ToString());
                // Obtener los demás valores de las celdas

                {
                    PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarCliente(this, ID, nombre, telefono));
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvClientes.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                ControladorClientes Ccliente = new ControladorClientes();
                Ccliente.EliminarCliente(ID);
            }
        }
    }
}
