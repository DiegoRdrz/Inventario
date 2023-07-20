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

                //nada de momento
            
        }

        private void FormularioHistorial_Load(object sender, EventArgs e)
        {

        }
    }
}
