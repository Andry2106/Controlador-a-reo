using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class DespegueData
    {
        private readonly Conexion conexion;
        string connectionString = "Server=tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Database=ControlAereoGeneral;User Id=ControlAereo;Password=ControlAereo;";

        public DespegueData()
        {
            conexion = new Conexion();
        }
        public DataTable ObtenerAvionPorID(int avionID)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_Obtener_AvionesID", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@AvionID", avionID);

                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el avión por ID: " + ex.Message);
                }
            }

            return dataTable;
        }
        public DataTable ObtenerVueloPorID(int vueloID)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_Obtener_Vuelo_Por_ID", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@VueloID", vueloID);

                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el vuelo por ID: " + ex.Message);
                }
            }

            return dataTable;
        }
        public void InsertarDespegue(string horaDespegue, string origen, string destino, int numeroVuelo, int idPista, int idPuerta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_Insertar_Despegue", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@HoraDespegue", horaDespegue);
                command.Parameters.AddWithValue("@Origen", origen);
                command.Parameters.AddWithValue("@Destino", destino);
                command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);
                command.Parameters.AddWithValue("@IDPista", idPista);
                command.Parameters.AddWithValue("@IDPuerta", idPuerta);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void CambiarEstadoVuelo(int numeroVuelo, int nuevoEstado)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SP_Cambiar_Estado_Vuelo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);
                command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void PasarDespegue(int numeroVuelo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_PasarDespegue", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("El vuelo se ha pasado a la tabla VuelosDespegue.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al pasar el despegue: " + ex.Message);
            }
        }
        public void BorrarVueloPorNumero(int numeroVuelo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_BorrarVueloPorNumero", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("El vuelo con número " + numeroVuelo + " ha sido eliminado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar el vuelo: " + ex.Message);
            }
        }
        public void SP_Insertar_ValidacionesDespegue02(int NumeroVuelo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ControlAereo.SP_Insertar_ValidacionesDespegue02", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NumeroVuelo", NumeroVuelo);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Las validaciones para el vuelo " + NumeroVuelo + " se han insertado correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar las validaciones: " + ex.Message);
                }
            }
        }
        public bool ValidarValidacionesDespegue(int numeroVuelo)
        {
            bool resultado = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ControlAereo.SP_Validar_Validaciones_Despegue", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);
                SqlParameter outputParameter = new SqlParameter("@Resultado", SqlDbType.Bit);
                outputParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(outputParameter);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(outputParameter.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al validar las validaciones: " + ex.Message);
                }
            }
            return resultado;
        }
        public DataTable BuscarValidacionesFallidas(int numeroVuelo)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ControlAereo.SP_Buscar_Validaciones_Fallidas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NumeroVuelo", numeroVuelo);

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al buscar validaciones fallidas: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }
    }
}
