using Frontend.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frontend.Pages.webforms
{
    public partial class AplicarFiltros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string rol = Session["rol"].ToString();
                    CargarAeropuertosEnDropDown();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los aereopuertos: " + ex.Message + "');", true);
                }
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
        }
        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalCalendario", "mostrarModal('modalCalendario');", true);
        }
        protected void btmAbrirCalendario_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalCalendario", "mostrarModal('modalCalendario');", true);
        }
        protected void CargarAeropuertosEnDropDown()
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                DataTable aeropuertos = hangaresLogic.ObtenerAeropuertos();
                ddlAereopuertos.Items.Clear();
                ddlAereopuertos.Items.Add(new ListItem("", ""));
                foreach (DataRow fila in aeropuertos.Rows)
                {
                    string nombreAeropuerto = fila["Nombre"].ToString();
                    ddlAereopuertos.Items.Add(nombreAeropuerto);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Surgió un problema al cargar los aeropuertos.');", true);
            }
        }
        protected void CargarDestinosAeropuerto(string nombreAeropuerto)
        {
            try
            {
                ListaVuelosLogic listaVuelosLogic = new ListaVuelosLogic();
                DataTable destinos = listaVuelosLogic.CargarDestinosAeropuerto(nombreAeropuerto);

                ddlDestinos.Items.Clear();
                ddlDestinos.Items.Add(new ListItem("", ""));
                foreach (DataRow fila in destinos.Rows)
                {
                    string nombreDestino = fila["Destino"].ToString();
                    ddlDestinos.Items.Add(nombreDestino);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Surgió un problema al cargar los destinos del aeropuerto. Error: {ex.Message}');", true);
            }
        }
        protected void btmAplicar_Click(object sender, EventArgs e)
        {
            string aeropuerto = ddlAereopuertos.SelectedItem?.Text;
            string destino = txtDestino.Text;
            if (string.IsNullOrEmpty(aeropuerto))
            {
                destino = string.IsNullOrEmpty(txtDestino.Text) ? null : txtDestino.Text;
            }
            string horario = cldHorario.SelectedDate != DateTime.MinValue ? cldHorario.SelectedDate.ToString("yyyy-MM-dd") : null;           
            string valor = "test";
            int op = 4;
            Session["rol"] = Session["rol"].ToString();
            Session["op"] = op;
            Session["aereopuerto"] = aeropuerto;
            Session["destino"] = destino;
            Session["horario"] = horario;
            Session["valor"] = valor;
            Response.Redirect("ListaVuelos.aspx");
        }

        protected void btmContinuar_Click(object sender, EventArgs e)
        {
            string aeropuerto = null;
            string destino = null;
            string horario = null;
            string valor = "test";
            int op = 4;

            string rol = Session["rol"].ToString();
            Session["rol"] = rol;

            Session["op"] = op;
            Session["aereopuerto"] = aeropuerto;
            Session["destino"] = destino;
            Session["horario"] = horario;
            Session["valor"] = valor;

            Response.Redirect("ListaVuelos.aspx");
        }
        protected void btmLimpiar_Click(object sender, EventArgs e)
        {
            cldHorario.SelectedDate = DateTime.MinValue;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalCalendario", "mostrarModal('modalCalendario');", true);
        }
        protected void btmVerDestinos_Click(object sender, EventArgs e)
        {
            CargarDestinosAeropuerto(ddlAereopuertos.SelectedItem.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalDestinos", "mostrarModal('modalDestinos');", true);
        }
        protected void txtConfirmarDestino_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDestinos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDestino.Text = ddlDestinos.SelectedValue;
        }

    }
}