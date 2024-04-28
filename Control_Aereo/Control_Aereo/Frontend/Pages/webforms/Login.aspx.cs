using Frontend.DataAccess;
using Frontend.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frontend.Pages.webforms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LabelUser.Visible = true;
                LabelPassword.Visible = true;
                txbUser.Visible = true;
                txbPassword.Visible = true;
                btnsignIn.Visible = true;

                LabelNombre.Visible = false;
                txbNombre.Visible = false;
                LabelApellidos.Visible = false;
                txbApellidos.Visible = false;
                LabelCedula.Visible = false;
                txbCedula.Visible = false;
                LabelUsuario.Visible = false;
                txbUsuario.Visible = false;
                LabelEmail.Visible = false;
                txbEmail.Visible = false;
                LabelPassword_registro.Visible = false;
                txbPassword_registro.Visible = false;
                btnUpButton.Visible = false;
                LabelRol.Visible = false;
                DDL_Rol.Visible = false;
            }
        }

        protected void lnklnkSIGN_IN_Click(object sender, EventArgs e)
        {
            LabelUser.Visible = true;
            LabelPassword.Visible = true;
            txbUser.Visible = true;
            txbPassword.Visible = true;
            btnsignIn.Visible = true;

            LabelNombre.Visible = false;
            txbNombre.Visible = false;
            LabelApellidos.Visible = false;
            txbApellidos.Visible = false;
            LabelCedula.Visible = false;
            txbCedula.Visible = false;
            LabelUsuario.Visible = false;
            txbUsuario.Visible = false;
            LabelEmail.Visible = false;
            txbEmail.Visible = false;
            LabelPassword_registro.Visible = false;
            txbPassword_registro.Visible = false;
            btnUpButton.Visible = false;
            LabelRol.Visible = false;
            DDL_Rol.Visible = false;
        }

        protected void lnklnkSIGN_UP_Click(object sender, EventArgs e)
        {
            LabelUser.Visible = false;
            LabelPassword.Visible = false;
            txbUser.Visible = false;
            txbPassword.Visible = false;
            btnsignIn.Visible = false;

            LabelNombre.Visible = true;
            txbNombre.Visible = true;
            LabelApellidos.Visible = true;
            txbApellidos.Visible = true;
            LabelCedula.Visible = true;
            txbCedula.Visible = true;
            LabelUsuario.Visible = true;
            txbUsuario.Visible = true;
            LabelEmail.Visible = true;
            txbEmail.Visible = true;
            LabelPassword_registro.Visible = true;
            txbPassword_registro.Visible = true;
            btnUpButton.Visible = true;
            LabelRol.Visible = true;
            DDL_Rol.Visible = true;
        }

        protected void signInButton_Click(object sender, EventArgs e)
        {
            LoginUsersLogic loginLogic = new LoginUsersLogic();

            string user = txbUser.Text;
            string password = txbPassword.Text;

            string resultado = loginLogic.ValidarUsuario(user, password);
            if(resultado == "Desembarque") 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Credenciales válidas');", true);
                Response.Redirect("Desembarque.aspx");
            }
            if (resultado == "Despegue") 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Credenciales válidas');", true);
                string rol = "Despegue";
                Session["rol"] = rol;
                Response.Redirect("FiltradoVuelos1.aspx");

            }
            if (resultado == "Embarque")
            {
                string rol = "Embarque";
                Session["rol"] = rol;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Credenciales válidas');", true);
                Response.Redirect("FiltradoVuelos1.aspx");
            }
            if (resultado == "Hangares")
            {
                string rol = "Hangares";
                Session["rol"] = rol;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Credenciales válidas');", true);
                Response.Redirect("Hangares.aspx");
            }
            else if (resultado == "Usuario no existe" || resultado == "Credenciales incorrectas")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", "Swal.fire('Login', 'Credenciales invalidas', 'error');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", "Swal.fire('Login', 'Usuario no autorizado', 'error');", true);
            }
        }

        protected void signUpButton_Click(object sender, EventArgs e)
        {
            LoginUsersLogic loginLogic = new LoginUsersLogic();

            string nombre = txbNombre.Text.Trim();
            string apellidos = txbApellidos.Text.Trim();
            string cedulaStr = txbCedula.Text.Trim();
            string nombreUsuario = txbUsuario.Text.Trim();
            string correo = txbEmail.Text.Trim();
            string contraseña = txbPassword_registro.Text.Trim();
            string rol = DDL_Rol.SelectedValue;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) || string.IsNullOrEmpty(cedulaStr)
                || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña)
                || string.IsNullOrEmpty(rol))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Todos los campos deben estar llenos.');", true);
                return;
            }

            int cedula;
            if (!int.TryParse(cedulaStr, out cedula))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('La cédula debe ser un número válido.');", true);
                return;
            }

            string resultado = loginLogic.RegistarUsuario(nombre, apellidos, cedula, nombreUsuario, correo, contraseña, rol);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + resultado + "');", true);
        }
    }
}