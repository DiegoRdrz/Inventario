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
    public partial class FormularioHistorial : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioHistorial()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PrincipalFormulario.restaurarColorBtns(6);
            this.Close();

        }

        private void FormularioHistorial_Shown(object sender, EventArgs e)
        {
            CargarTabla();
        }

        public void CargarTabla()
        {
            ControladorVentas Cventa = new ControladorVentas();

            DataTable Tventa = Cventa.MostrarVentas();
            dgvVentas.DataSource = Tventa;

            foreach (DataGridViewColumn columna in dgvVentas.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvVentas.Columns["CLienteID"].Visible = false;

            dgvVentas.ReadOnly = true;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.ClearSelection();
            dgvVentas.AllowUserToAddRows = false;

        }

        private void FormularioHistorial_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvVentas.SelectedRows[0];

                // Obtener los valores de las celdas
                int ID = int.Parse(filaSeleccionada.Cells["ID"].Value.ToString());

                // Obtener los demás valores de las celdas

                {
                    PrincipalFormulario.abrirFormularioCrear(new FormularioVerVenta(this, ID));
                }

            }
        }
    }
}
