using Inventario.Controladores;
using Inventario.Modelos;
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
    public partial class FormularioVerVenta : Form
    {
        private Form formularioAnterior;
        private int id;

        public FormularioVerVenta(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
        }
        public FormularioVerVenta(Form formularioAnterior, int id)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            this.id = id;

        }

        public void CargarTabla()
        {
            ControladorProductos Cproducto = new ControladorProductos();

            DataTable Tcategorias = Cproducto.mostrarProductosVendidos(id);
            dgvProductosVendidos.DataSource = Tcategorias;

            foreach (DataGridViewColumn columna in dgvProductosVendidos.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvProductosVendidos.Columns["ProductoID"].Visible = false;
            dgvProductosVendidos.Columns["VentaID"].Visible = false;

            dgvProductosVendidos.ReadOnly = true;
            dgvProductosVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosVendidos.ClearSelection();
            dgvProductosVendidos.AllowUserToAddRows = false;
        }

        private void FormularioVerVenta_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            this.Close();
        }
    }
}
