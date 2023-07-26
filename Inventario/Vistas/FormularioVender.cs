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
    public partial class FormularioVender : Form
    {
        public PrincipalFormulario PrincipalFormulario { get; set; }
        ControladorProductos Cproducto;
        ModeloProductos producto;
        List<ModeloProductos> listaProductos = new List<ModeloProductos>();

        ControladorClientes Ccliente;

        ControladorVentas Cventa;
        ModeloVentas venta;

        public FormularioVender()
        {
            InitializeComponent();
            Cproducto = new ControladorProductos();
            producto = new ModeloProductos();
            Ccliente = new ControladorClientes();
            Cventa = new ControladorVentas();
            venta = new ModeloVentas();
        }

        private void FormularioVender_Load(object sender, EventArgs e)
        {

            // Configurar el ComboBox de Categorías
            cbxClientes.DataSource = Ccliente.obtenerNombres();
            cbxClientes.DropDownStyle = ComboBoxStyle.DropDownList; // Establecer el estilo de lista desplegable

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            PrincipalFormulario.restaurarColorBtns(1);
            this.Close();
        }

        public void cargarTablaBuscar(string nombre)
        {

            DataTable resultados = Cproducto.buscarPorNombre(nombre);

            dgvBuscar.DataSource = resultados;
            foreach (DataGridViewColumn columna in dgvBuscar.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvBuscar.Columns["ProveedorID"].Visible = false;
            dgvBuscar.Columns["CategoriaID"].Visible = false;

            dgvBuscar.ReadOnly = true;
            dgvBuscar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBuscar.ClearSelection();
            dgvBuscar.AllowUserToAddRows = false;
        }

        public void cargarTablaLista()
        {
            dgvLista.DataSource = null; // Limpiar el DataSource para evitar problemas de actualización
            dgvLista.DataSource = listaProductos;

            foreach (DataGridViewColumn columna in dgvLista.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvLista.Columns["ID"].Visible = false;
            dgvLista.Columns["ProveedorID"].Visible = false;
            dgvLista.Columns["CategoriaID"].Visible = false;
            dgvLista.Columns["Cantidad"].Visible = false;


            dgvLista.ReadOnly = true;
            dgvLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLista.ClearSelection();
            dgvLista.AllowUserToAddRows = false;

            int totalPrecio = 0;

            foreach (DataGridViewRow fila in dgvLista.Rows)
            {
                // Verificar si la celda tiene un valor válido en formato numérico entero
                if (int.TryParse(fila.Cells["Precio"].Value.ToString(), out int precio))
                {
                    totalPrecio += precio;
                }
            }
            lblTotal.Text = totalPrecio.ToString();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvBuscar.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvBuscar.SelectedRows[0];

                // Obtener los valores de las celdas de la fila seleccionada
                int idProducto = Convert.ToInt32(filaSeleccionada.Cells["ID"].Value);
                string nombreProducto = filaSeleccionada.Cells["Nombre"].Value.ToString();
                int precioProducto = Convert.ToInt32(filaSeleccionada.Cells["Precio"].Value);
                int cantidad = Convert.ToInt32(filaSeleccionada.Cells["Cantidad"].Value);
                string codigoBarras = filaSeleccionada.Cells["CodigoBarras"].Value.ToString();
                int categoriaID = Convert.ToInt32(filaSeleccionada.Cells["CategoriaID"].Value);
                int proveedorID = Convert.ToInt32(filaSeleccionada.Cells["ProveedorID"].Value);

                producto = new ModeloProductos
                {
                    ID = idProducto,
                    Nombre = nombreProducto,
                    Precio = precioProducto,
                    Cantidad = cantidad,
                    CodigoBarras= codigoBarras,
                    CategoriaID = categoriaID,
                    ProveedorID = proveedorID
                    // Agrega aquí las demás propiedades según tu clase Producto
                };


                listaProductos.Add(producto);
                
                cargarTablaLista();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string textoBuscado = txtBuscar.Text.Trim();

            bool esCodigoDeBarras = textoBuscado.All(char.IsDigit);
            if (string.IsNullOrEmpty(textoBuscado)) // Verificar si la cadena es vacía o nula
            {
                dgvBuscar.DataSource = null; // Limpiar el DataGridView
            }
            else if (esCodigoDeBarras)
            {
                cargarTablaLista();
            }
            else
            {
                cargarTablaBuscar(textoBuscado);
            }
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            int totalPrecio = 0;

            // Diccionario para almacenar el conteo de productos por su ID
            Dictionary<int, int> conteoProductos = new Dictionary<int, int>();

            foreach (DataGridViewRow fila in dgvLista.Rows)
            {
                // Verificar si la celda tiene un valor válido en formato numérico entero
                if (int.TryParse(fila.Cells["Precio"].Value.ToString(), out int precio))
                {
                    totalPrecio += precio;
                }

                // Obtener el ID del producto
                if (int.TryParse(fila.Cells["ID"].Value.ToString(), out int id))
                {
                    // Verificar si el ID del producto ya existe en el diccionario
                    if (conteoProductos.ContainsKey(id))
                    {
                        // Si existe, incrementar el conteo en 1
                        conteoProductos[id]++;
                    }
                    else
                    {
                        // Si no existe, agregar el ID al diccionario con conteo inicial de 1
                        conteoProductos.Add(id, 1);
                    }
                }
            }

            venta.ClienteID = Ccliente.BuscarIdPorNombre(cbxClientes.Text);
            venta.Fecha = DateTime.Now;
            venta.Total = totalPrecio;

            Cventa.CrearVenta(venta);
            int IDVenta = Cventa.obtenerUltimoId();

            // Recorrer el diccionario para crear los registros de productos vendidos
            foreach (var kvp in conteoProductos)
            {
                int idProducto = kvp.Key;
                int cantidad = kvp.Value;
                Cproducto.CrearProductoVendido(IDVenta, idProducto, cantidad);
                Cproducto.restarCantidad(idProducto, cantidad);
            }

            Close();
        }



    }
}
