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
    }
}
