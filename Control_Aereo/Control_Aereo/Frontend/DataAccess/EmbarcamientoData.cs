using System;
using System.Data;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class EmbarcamientoData
    {
        private readonly Conexion conexion;
        string connectionString = "Server=tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Database=ControlAereoGeneral;User Id=ControlAereo;Password=ControlAereo;";

        public EmbarcamientoData()
        {
            conexion = new Conexion();
        }

        public DataTable ListadoPasajeros()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_ObtenerPasajeros", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ListadoPasajeros: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable ListadoEquipamiento()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_ObtenerEquipamiento", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ListadoEquipamiento: " + ex.Message);
            }
            return dataTable;
        }

        public void InsertarEmbarque(int IDEquipaje, int IDPasajero)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_AgregarEmbarque]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDEquipaje", IDEquipaje);
                command.Parameters.AddWithValue("@IDPasajero", IDPasajero);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable ObtenerDatosEmbarque()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("[ControlAereo].[SP_ObtenerDatosEmbarque]", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerDatosEmbarque: " + ex.Message);
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
                    SqlCommand command = new SqlCommand("[ControlAereo].[SP_ObtenerPuertasActivas]", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerPuertasActivas: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable ObtenerDatosAvionPorID(int IDAvion)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("[ControlAereo].[SP_ObtenerDatosAvionesPorID]", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IDAvion", IDAvion);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerDatosAvionPorID: " + ex.Message);
            }
            return dataTable;
        }

        public string InsertarEmbarcamiento(int IDAvion, int IDEmbarque, int IDPuerta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_InsertarEmbarcamiento]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDAvion", IDAvion);
                command.Parameters.AddWithValue("@IDEmbarque", IDEmbarque);
                command.Parameters.AddWithValue("@IDPuerta", IDPuerta);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensajeParam);

                connection.Open();
                command.ExecuteNonQuery();

                return mensajeParam.Value.ToString();
            }
        }

        public string InsertarEmbarcamientoMasivo(int IDAvion, int IDPuerta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_InsertarEmbarcamientoMasivo]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDAvion", IDAvion);
                command.Parameters.AddWithValue("@IDPuerta", IDPuerta);

                SqlParameter mensajeParam = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(mensajeParam);

                connection.Open();
                command.ExecuteNonQuery();

                return mensajeParam.Value.ToString();
            }
        }

        public void InsertarEmbarqueMasivo()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_AgregarEmbarqueMasivo]", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();

            }
        }

        public DataTable ObtenerEmbarcamientosPorAvion(int IDAvion)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("[ControlAereo].[SP_ObtenerEmbarcamientosPorAvion]", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IDAvion", IDAvion);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ObtenerEmbarcamientosPorAvion: " + ex.Message);
            }

            return dataTable;
        }

        public void EliminarEmbarcamientoPorID(int IDEmbarque)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_EliminarEmbarcamientoPorIDEmbarque]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDEmbarque", IDEmbarque);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarEmbarcamientosPorIDAvion(int IDAvion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_EliminarEmbarcamientosPorIDAvion]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDAvion", IDAvion);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarEmbarquePorIDEmbarque(int IDEmbarque)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_EliminarEmbarquePorIDEmbarque]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@IDEmbarque", IDEmbarque);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EliminarEmbarquesNoVinculados()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_EliminarEmbarquesNoVinculados]", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();

            }
        }

    }

}