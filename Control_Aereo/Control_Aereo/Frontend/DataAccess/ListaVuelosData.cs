using System;
using System.Data;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class ListaVuelosData
    {
        private readonly Conexion conexion;
        string connectionString = "Server=tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Database=ControlAereoGeneral;User Id=ControlAereo;Password=ControlAereo;";

        public ListaVuelosData()
        {
            conexion = new Conexion();
        }
        public DataTable ListadoVuelos(int operacion)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_ListadoVuelos", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operacion", operacion);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ListadoVuelos: " + ex.Message);
            }
            return dataTable;
        }
        public DataTable FiltrarVuelos(int op, string valor)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_Filtrar_Vuelos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@op", op);
                    command.Parameters.AddWithValue("@valor", valor);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
        public DataTable FiltrarVuelosVarios(string aeropuerto, string destino, string fecha)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ControlAereo.SP_Filtrar_Vuelos_Multiple", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@aeropuerto", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(aeropuerto) ? DBNull.Value : (object)aeropuerto;
                    command.Parameters.Add("@destino", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(destino) ? DBNull.Value : (object)destino;
                    command.Parameters.Add("@fecha", SqlDbType.NVarChar, 50).Value = string.IsNullOrEmpty(fecha) ? DBNull.Value : (object)fecha;
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
        public DataTable CargarDestinosAeropuerto(string nombreAeropuerto)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string storedProcedure = "SP_Cargar_Destinos_Aereopuerto";

                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NombreAeropuerto", nombreAeropuerto);

                try
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return dt;
        }
    }
}
