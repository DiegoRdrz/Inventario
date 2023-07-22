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
    public partial class FormularioAgregarProducto : Form
    {
        Form formularioAnterior;
        ControladorProductos Cproducto;
        ModeloProductos producto;
        FormularioInventario formularioInventario;

        ControladorProveedores Cproveedores;
        ControladorCategorias Ccategoria;
        public FormularioAgregarProducto(Form formularioAnterior)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            formularioInventario = formularioAnterior as FormularioInventario;
            producto = new ModeloProductos();
            Cproducto = new ControladorProductos();

        }

        public FormularioAgregarProducto(Form formularioAnterior, int ID, string nombre, int precio, int cantidad, string codigoBarras, int categoriaID, int ProveedorID)
        {
            InitializeComponent();
            this.formularioAnterior = formularioAnterior;
            formularioInventario = formularioAnterior as FormularioInventario;

            producto = new ModeloProductos();
            producto.ID = ID;
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Cantidad = cantidad;
            producto.CodigoBarras = codigoBarras;
            producto.CategoriaID = categoriaID;
            producto.ProveedorID = ProveedorID;

            Cproducto = new ControladorProductos();

            txtNombre.Text = nombre;
            txtPrecio.Text = precio.ToString() ;
            txtCantidad.Text = cantidad.ToString();
            txtCodigoBarras.Text = codigoBarras;

            cbxCategoria.Text = categoriaID.ToString();
            cbxProveedor.Text = ProveedorID.ToString();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            formularioAnterior.Show();
            formularioInventario.CargarTabla();
            this.Close();
        }

        private void FormularioAgregarProducto_Load(object sender, EventArgs e)
        {
            // ...
            Ccategoria = new ControladorCategorias();
            Cproveedores = new ControladorProveedores();

            // Configurar el ComboBox de Categorías
            cbxCategoria.DataSource = Ccategoria.obtenerNombres();
            cbxCategoria.DropDownStyle = ComboBoxStyle.DropDownList; // Establecer el estilo de lista desplegable

            // Configurar el ComboBox de Proveedores
            cbxProveedor.DataSource = Cproveedores.obtenerNombres();
            cbxProveedor.DropDownStyle = ComboBoxStyle.DropDownList; // Establecer el estilo de lista desplegable
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            producto.Nombre = txtNombre.Text;
            producto.Precio = int.Parse(txtPrecio.Text);
            producto.Cantidad = int.Parse(txtCantidad.Text);
            producto.CodigoBarras = txtCodigoBarras.Text;
            
            
            producto.CategoriaID = Ccategoria.buscarPorNombre(cbxCategoria.Text);
            producto.ProveedorID = Cproveedores.buscarPorNombre(cbxProveedor.Text);

            if (producto.ID == 0)
                Cproducto.CrearProducto(producto);
            else
                Cproducto.ActualizarProducto(producto);

            formularioAnterior.Show();
            formularioInventario.CargarTabla();
            this.Close();
        }
    }
}
