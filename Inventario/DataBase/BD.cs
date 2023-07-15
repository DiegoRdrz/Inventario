using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Inventario
{
    public class BD
    {
        private string cadenaConexion;
        private SqlConnection conexion;

        public BD(string servidor, string baseDeDatos, string usuario, string contraseña)
        {
            cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDeDatos};User ID={usuario};Password={contraseña}";
            conexion = new SqlConnection(cadenaConexion);
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
                foreach (var valor in valores.Values)
                {
                    consulta += "@" + Guid.NewGuid().ToString() + ", ";
                }

                consulta = consulta.TrimEnd(',', ' ') + ")";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    int i = 0;
                    foreach (var valor in valores.Values)
                    {
                        comando.Parameters.AddWithValue($"@{i}", valor);
                        i++;
                    }

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("Registro creado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine($"Error al crear el registro: {ex.Message}");
            }
        }

        public DataTable LeerRegistros(string tabla)
        {
            try
            {
                string consulta = $"SELECT * FROM {tabla}";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tablaRegistros = new DataTable();
                    adaptador.Fill(tablaRegistros);

                    return tablaRegistros;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer los registros: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    foreach (var valor in valores)
                    {
                        comando.Parameters.AddWithValue($"@{valor.Key}", valor.Value);
                    }

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("Registro actualizado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void EliminarRegistro(string tabla, int id)
        {
            try
            {
                string consulta = $"DELETE FROM {tabla} WHERE id = @id";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("Registro eliminado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el registro: {ex.Message}", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
