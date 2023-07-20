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
    public partial class FormularioVender : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        public FormularioVender()
        {
            InitializeComponent();
        }

        private void FormularioVender_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PrincipalFormulario.restaurarColorBtns(1);
            this.Close();
        }

        private void FormularioVender_Shown(object sender, EventArgs e)
        {
            CargarTabla();
        }
        public void CargarTabla()
        {
            //No hace nada de momento
        }

    }
}
