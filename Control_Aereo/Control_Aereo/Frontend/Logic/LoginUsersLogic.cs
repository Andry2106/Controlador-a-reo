using Frontend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Logic
{
    public class LoginUsersLogic
    {
        private LoginUsersData loginUsersData;


        public LoginUsersLogic()
        {
            loginUsersData = new LoginUsersData();
        }

        public string ValidarUsuario(string user, string password)
        {
            string resultado = loginUsersData.ValidarLogin(user, password);

            return resultado;
        }

        public string RegistarUsuario(string nombre, string apellidos, int cedula, string nombreUsuario, string correo, string contraseña, string rol)
        {
            string resultado = loginUsersData.RegistrarUsuario(nombre, apellidos, cedula, nombreUsuario, correo, contraseña, rol);

            return resultado;
        }
    }
}