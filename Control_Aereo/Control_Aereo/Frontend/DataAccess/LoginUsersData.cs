using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Frontend.DataAccess
{
    public class LoginUsersData
    {
        private readonly Conexion conexion;
        string connectionString = "Server=tiusr9pl.cuc-carrera-ti.ac.cr\\MSSQLSERVER2019;Database=ControlAereoGeneral;User Id=ControlAereo;Password=ControlAereo;";

        public LoginUsersData()
        {
            conexion = new Conexion();
        }

        public string ValidarLogin(string nombreUsuario, string contraseña)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_LoginUusarios]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", contraseña);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetValue(0).ToString();
                    }
                }
            }

            return "Error no especificado en el login";
        }

        public string RegistrarUsuario(string nombre, string apellidos, int cedula, string nombreUsuario, string correo, string contraseña, string rol)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("[ControlAereo].[SP_RegistrarUsuario_Equipo2]", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellidos", apellidos);
                command.Parameters.AddWithValue("@Cedula", cedula);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@Correo", correo);
                command.Parameters.AddWithValue("@Contraseña", contraseña);
                command.Parameters.AddWithValue("@Rol", rol);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetValue(0).ToString();
                    }
                }
            }

            return "Error al registrar el usuario";
        }
    }
}