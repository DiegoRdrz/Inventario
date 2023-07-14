using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Inventario.Vistas
{
    public partial class PrincipalFormulario : Form
    {
        public PrincipalFormulario()
        {
            InitializeComponent();
        }

        private void PrincipalFormulario_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true; ;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private Form formularioActivo = null;
        public void abrirFormulario(Form formulario)
        {   
            if(formularioActivo != null)
            {
                formularioActivo.Close();
            }
            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            PanelPrincipal.Controls.Add(formulario);
            PanelPrincipal.Tag = formulario;
            formulario.BringToFront();
            formulario.Show();
        }
        public void abrirFormularioCrear(Form formulario)
        {
            formularioActivo.Hide();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            PanelPrincipal.Controls.Add(formulario);
            PanelPrincipal.Tag = formulario;
            formulario.BringToFront();
            formulario.Show();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormularioVender());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            FormularioInventario formularioInventario = new FormularioInventario();
            formularioInventario.PrincipalFormulario = this; // Pasar la instancia existente de PrincipalFormulario

            abrirFormulario(formularioInventario);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FormularioProveedores formularioProveedores = new FormularioProveedores();
            formularioProveedores.PrincipalFormulario = this; // Pasar la instancia existente de PrincipalFormulario

            abrirFormulario(formularioProveedores);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FormularioClientes formularioClientes = new FormularioClientes();
            formularioClientes.PrincipalFormulario = this; // Pasar la instancia existente de PrincipalFormulario

            abrirFormulario(formularioClientes);
        }

        private void Historial_Click(object sender, EventArgs e)
        {
            abrirFormulario(new FormularioHistorial());
        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
