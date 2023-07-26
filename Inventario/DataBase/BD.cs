using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;
using Inventario.Modelos;

namespace Inventario
{
    public class BD
    {
        private string cadenaConexion;
        private MySqlConnection conexion;

        public BD(string servidor, string puerto, string baseDeDatos, string usuario, string contraseña)
        {
            cadenaConexion = $"Server={servidor};Port={puerto};Database={baseDeDatos};Uid={usuario};Pwd={contraseña};";
            conexion = new MySqlConnection(cadenaConexion);
        }


        public void CrearRegistro(string tabla, Dictionary<string, object> valores)
        {
            try
            {
                string consulta = $"INSERT INTO {tabla} (";

                // Agregar los nombres de las columnas
                foreach (var columna in valores.Keys)
                {
                    consulta += columna + ", ";
                }

                consulta = consulta.TrimEnd(',', ' ') + ") VALUES (";

                // Agregar los parámetros de los valores
                int contador = 0;
                foreach (var valor in valores.Values)
                {
                    string parametro = $"@param{contador}";
                    consulta += parametro + ", ";
                    contador++;
                }

                consulta = consulta.TrimEnd(',', ' ') + ")";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    conexion.Open();

                    int i = 0;
                    foreach (var valor in valores.Values)
                    {
                        comando.Parameters.AddWithValue($"@param{i}", valor);
                        i++;
                    }

                    comando.ExecuteNonQuery();

                    conexion.Close();

                    MessageBox.Show("Registro creado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error al crear el registro: {ex.Message}");
            }
        }

        public DataTable LeerRegistros(string tabla)
        {
            try
            {
                string consulta = "";

                if (tabla == "productos")
                {
                    consulta = @"
                    SELECT p.* , pro.Nombre as Proveedor, cat.Nombre as Categoria
                    FROM productos p
                    JOIN proveedores pro ON pro.ID = p.ProveedorID
                    JOIN categorias cat ON cat.ID = p.CategoriaID";
                }
                else if(tabla == "ventas")
                {
                    consulta = @"
                    SELECT v.* , cli.Nombre as Cliente
                    FROM ventas v
                    JOIN clientes cli ON cli.ID = v.ClienteID";
                }
                else
                {
                    consulta = $"SELECT * FROM {tabla}";
                }

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tablaRegistros = new DataTable();
                    adaptador.Fill(tablaRegistros);

                    return tablaRegistros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los registros: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public void ActualizarRegistro(string tabla, int id, Dictionary<string, object> valores)
        {
            try
            {
                string consulta = $"UPDATE {tabla} SET ";

                foreach (var columna in valores.Keys)
                {
                    consulta += $"{columna} = @{columna}, ";
                }

                consulta = consulta.TrimEnd(',', ' ') + $" WHERE id = @id";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    foreach (var valor in valores)
                    {
                        comando.Parameters.AddWithValue($"@{valor.Key}", valor.Value);
                    }

                    conexion.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Registro actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
        }

        public void EliminarRegistro(string tabla, int id)
        {
            try
            {
                string consulta = $"DELETE FROM {tabla} WHERE id = @id";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexion.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Registro eliminado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
        }


        #region Consultas Personalizadas

        public List<string> ObtenerNombres(string nombreTabla)
        {
            List<string> nombres = new List<string>();

            try
            {
                string consulta = $"SELECT Nombre FROM {nombreTabla}";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        nombres.Insert(0, "Seleccionar...");
                        while (lector.Read())
                        {
                            string nombre = lector["Nombre"].ToString();
                            nombres.Add(nombre);
                        }
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al obtener nombres de la tabla {nombreTabla}: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombres;
        }
        public int ObtenerIdPorNombre(string nombreTabla, string valorNombre)
        {
            int idValor = -1;

            try
            {
                string consulta = $"SELECT ID FROM {nombreTabla} WHERE Nombre = @valorNombre";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    comando.Parameters.AddWithValue("@valorNombre", valorNombre);

                    object result = comando.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idValor = Convert.ToInt32(result);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al obtener ID por nombre: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return idValor;
        }

        public string ObtenerNombrePorID(string tabla, int id)
        {
            string nombre = string.Empty;

            try
            {
                string consulta = $"SELECT Nombre FROM {tabla} WHERE ID = @id";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexion.Open();
                    object resultado = comando.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        nombre = resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al obtener el nombre por ID en la tabla {tabla}: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }

            return nombre;
        }

        public DataTable BuscarPorNombre(string tabla, string nombre)
        {
            DataTable resultado = new DataTable();
            string consulta;
            try
            {
                consulta = $@"
                SELECT p.*, pro.Nombre as Proveedor, cat.Nombre as Categoria
                FROM {tabla} p 
                JOIN proveedores pro ON pro.ID = p.ProveedorID
                JOIN categorias cat ON cat.ID = p.CategoriaID
                WHERE p.Nombre LIKE @nombre";



                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

                    conexion.Open();
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(resultado);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al realizar la búsqueda: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultado;
        }

        public int ObtenerUltimoID(string tabla)
        {
            int ultimoID = -1; // Valor por defecto en caso de que no se pueda obtener el ID

            try
            {
                string consulta = $"SELECT MAX(ID) FROM {tabla}";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        ultimoID = Convert.ToInt32(resultado);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el último ID de la tabla {tabla}: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ultimoID;
        }

        public void RestarCantidad(int idProducto, int cantidadVendida)
        {
            try
            {
                string consulta = "UPDATE productos SET Cantidad = Cantidad - @cantidadVendida WHERE ID = @idProducto";

                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@cantidadVendida", cantidadVendida);
                    comando.Parameters.AddWithValue("@idProducto", idProducto);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al restar la cantidad en stock: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable mostrarProductosVendidos(int idVenta)
        {
            DataTable productosVendidos = new DataTable();

            try
            {
                string consulta = @"
                SELECT pv.*, pro.Nombre AS Producto
                FROM productosvendidos pv
                JOIN productos pro ON pro.ID = pv.ProductoID
                WHERE VentaID = @idVenta";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@idVenta", idVenta);

                    conexion.Open();

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    adaptador.Fill(productosVendidos);

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, puedes mostrar un mensaje de error, log, etc.
                MessageBox.Show($"Error al obtener productos vendidos por idVenta: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productosVendidos;
        }


    }
    #endregion
}

