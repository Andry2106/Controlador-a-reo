using System;
using System.Data;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class DesembarqueData
    {
        private string connectionString = "Data Source=tiusr3pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Initial Catalog=ControlAereoGeneral;User ID=ControlAereo;Password=ControlAereo";

        public DataTable ObtenerHangares()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_Obtener_Hangares_Mini", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los hangares: " + ex.Message);
            }

            return dataTable;
        }
        public DataTable CargarHangaresConAviones()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_CargarHangaresConAviones", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar los hangares con aviones: " + ex.Message);
            }

            return dataTable;
        }

        public DataTable CargarHangaresSinAviones()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_CargarHangaresSinAviones", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar los hangares sin aviones: " + ex.Message);
            }

            return dataTable;
        }
        public DataTable CargarAvionesPorHangar(int idHangar)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_CargarAvionesPorHangar", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdHangar", idHangar);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar los aviones por hangar: " + ex.Message);
            }

            return dataTable;
        }

        public string AgregarAvionEnHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_AgregarAvionEnHangar", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreHangar", nombreHangar);
                    command.Parameters.AddWithValue("@NumeroDeRegistro", numeroDeRegistroAvion);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Capturar los mensajes de impresión del procedimiento almacenado
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mensaje += reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al agregar el avión al hangar: " + ex.Message;
            }
            return mensaje;
        }

        public string EliminarAvionDeHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_EliminarAvionDeHangar", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreHangar", nombreHangar);
                    command.Parameters.AddWithValue("@NumeroDeRegistro", numeroDeRegistroAvion);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Capturar los mensajes de impresión del procedimiento almacenado
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mensaje += reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al eliminar el avión del hangar: " + ex.Message;
            }
            return mensaje;
        }

        public DataTable CargarTodosLosAviones()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_CargarTodosLosAviones", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar todos los aviones: " + ex.Message);
            }

            return dataTable;
        }
        public DataTable BuscarModelosAvionesPorHangar(string nombreHangar)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_BuscarModelosAvionesPorHangar", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreHangar", nombreHangar);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar modelos de aviones por hangar: " + ex.Message);
            }

            return dataTable;
        }

        public string DesembarcarPasajero(int idEmbarque, int idPuerta)
        {
            string mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_Desembarque", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmbarque", idEmbarque);
                    command.Parameters.AddWithValue("@IdPuerta", idPuerta);

                    connection.Open();
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mensaje += reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al desembarcar : " + ex.Message;
            }
            return mensaje;
        }
        public DataTable CargarEmbarque()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_CargarEmbarque", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el embarque: " + ex.Message);
            }

            return dataTable;
        }




        public DataTable ObtenerPuertasActivas()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_ObtenerPuertasDesembarque", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las puertas activas: " + ex.Message);
            }

            return dataTable;
        }
    }
}
