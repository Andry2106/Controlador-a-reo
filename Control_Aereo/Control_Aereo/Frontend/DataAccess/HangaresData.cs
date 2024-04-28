using System;
using System.Data;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class HangaresData
    {
        private readonly Conexion conexion;
        string connectionString = "Server=tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Database=ControlAereoGeneral;User Id=ControlAereo;Password=ControlAereo;";

        public HangaresData()
        {
            conexion = new Conexion();
        }
        public DataTable MantenimientoHangares(int opcion, int idHangar, int idtipo, string nombre, string ubicacion, int capacidad, string estado, decimal tamaño, decimal altura, int idAeropuerto)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = conexion.AbrirConexion())
            {
                SqlCommand command = new SqlCommand("SP_Mantenimiento_Hangares", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Opcion", opcion);
                command.Parameters.AddWithValue("@IdHangar", idHangar);
                command.Parameters.AddWithValue("@IdTipoHangar", idtipo);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Ubicacion", ubicacion);
                command.Parameters.AddWithValue("@Capacidad", capacidad);
                command.Parameters.AddWithValue("@Estado", estado);
                command.Parameters.AddWithValue("@Tamaño", tamaño);
                command.Parameters.AddWithValue("@Altura", altura);
                command.Parameters.AddWithValue("@IdAeropuerto", idAeropuerto);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public int ObtenerIdHangarPorNombre(string nombreHangar)
        {
            int idHangar = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_ObtenerID_Nombre", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", nombreHangar);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idHangar = Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún ID para el hangar con el nombre: " + nombreHangar);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ID del hangar por nombre: " + ex.Message);
            }

            return idHangar;
        }
        public DataTable ObtenerAeropuertos()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_Obtener_Aeropuertos", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los aeropuertos: " + ex.Message);
            }

            return dataTable;
        }
        public int ObtenerIdAeropuertoPorNombre(string nombreAeropuerto)
        {
            int idAeropuerto = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_ObtenerId_Aeropuerto_Nombre", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreAeropuerto", nombreAeropuerto);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idAeropuerto = Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún ID para el aeropuerto con el nombre: " + nombreAeropuerto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ID del aeropuerto por nombre: " + ex.Message);
            }

            return idAeropuerto;
        }
        public int ObtenerIdTipoHangarPorNombre(string nombreTipoHangar)
        {
            int idTipoHangar = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_ObtenerId_TipoHangar_Nombre", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreTipoHangar", nombreTipoHangar);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idTipoHangar = Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún ID para el tipo de hangar con el nombre: " + nombreTipoHangar);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ID del tipo de hangar por nombre: " + ex.Message);
            }

            return idTipoHangar;
        }
        public DataTable ObtenerTiposHangares()
        {
            DataTable tiposHangares = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ControlAereo.SP_Obtener_Tipos_Hangares", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    tiposHangares.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los tipos de hangares: " + ex.Message);
            }

            return tiposHangares;
        }
        public bool HangarEnUso(int idHangar)
        {
            string connectionString = "tu_cadena_de_conexion";

            string query = "SELECT COUNT(*) FROM ControlAereo.AvionesEstacionados02 WHERE IdHangar = @IdHangar";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdHangar", idHangar);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}
