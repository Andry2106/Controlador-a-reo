using System;
using System.Data.SqlClient;

namespace Frontend.DataAccess
{
    public class Conexion
    {
        private readonly string connectionString;

        public Conexion()
        {
            string serverName = "tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019";
            string username = "ControlAereo";
            string password = "ControlAereo";
            connectionString = $"Server={serverName};Database=ControlAereoGeneral;User Id={username};Password={password};";
        }

        public SqlConnection AbrirConexion()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void CerrarConexion(SqlConnection connection)
        {
            connection.Close();
        }
    }
}
