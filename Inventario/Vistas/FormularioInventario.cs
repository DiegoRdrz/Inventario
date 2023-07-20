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
    public partial class FormularioInventario : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioInventario()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PrincipalFormulario.restaurarColorBtns(2);
            this.Close();
 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (PrincipalFormulario != null)
            {
                PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarProducto(this));
            }
        }

        private void FormularioInventario_Shown(object sender, EventArgs e)
        {
            CargarTabla();
        }

        public void CargarTabla()
        {
            ControladorProductos Cproducto = new ControladorProductos();

            DataTable Tproductos = Cproducto.MostrarProductos();
            dgvProductos.DataSource = Tproductos;

            foreach (DataGridViewColumn columna in dgvProductos.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ClearSelection();
            dgvProductos.AllowUserToAddRows = false;

        }

        private void FormularioInventario_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvProductos.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                int precio = int.Parse(filaSeleccionada.Cells["Precio"].Value.ToString());
                int cantidad = int.Parse(filaSeleccionada.Cells["Cantidad"].Value.ToString());
                string codigoBarras = filaSeleccionada.Cells["CodigoBarras"].Value.ToString();
                int categoriaID = int.Parse(filaSeleccionada.Cells["CategoriaID"].Value.ToString());
                int ProveedorID = int.Parse(filaSeleccionada.Cells["ProveedorID"].Value.ToString());
                // Obtener los demás valores de las celdas

                {
                    PrincipalFormulario.abrirFormularioCrear(new FormularioAgregarProducto(this, ID, nombre, precio, cantidad,
                        codigoBarras, categoriaID, ProveedorID));
                }

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvProductos.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());
                ControladorProductos Cproductos = new ControladorProductos();
                Cproductos.EliminarProducto(ID);
            }
            CargarTabla();
        }
    }
}
