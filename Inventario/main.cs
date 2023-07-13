using Inventario.Vistas;
using System;
using System.Windows.Forms;

namespace Inventario
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PrincipalFormulario formularioPrincipal = new PrincipalFormulario();

            // Establece la posición de inicio del formulario en el centro de la pantalla
            formularioPrincipal.StartPosition = FormStartPosition.CenterScreen;

            Application.Run(formularioPrincipal);
        }
    }
}

