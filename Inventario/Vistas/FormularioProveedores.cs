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
    public partial class FormularioProveedores : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioProveedores()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (PrincipalFormulario != null)
            {
                PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarProveedor(this));
                
            }
        }

        private void FormularioProveedores_Load(object sender, EventArgs e)
        {
            ControladorProveedores Cproveedores = new ControladorProveedores();

            DataTable Tproveedores = Cproveedores.MostrarProveedores();
            dgvProveedores.DataSource = Tproveedores;

            foreach (DataGridViewColumn columna in dgvProveedores.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvProveedores.ReadOnly = true;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.ClearSelection();
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvProveedores.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                long telefono = long.Parse(filaSeleccionada.Cells["Telefono"].Value.ToString());
                string direccion  = filaSeleccionada.Cells["Direccion"].Value.ToString();
                string correo = filaSeleccionada.Cells["Correo"].Value.ToString();
                // Obtener los demás valores de las celdas

                {
                    PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarProveedor(this, ID, nombre, telefono, direccion, correo));
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataGridViewRow filaSeleccionada = dgvProveedores.SelectedRows[0];
            int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
            ControladorProveedores Cproveedor = new ControladorProveedores();
            Cproveedor.EliminarCliente(ID);
        }
    }
}
