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
    public partial class FiltradoVuelos1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string rol = Session["rol"].ToString();
                    if(rol == "Hangares")
                    {
                        Response.Redirect("Inicio.aspx");
                    }
                    else 
                    {
                        CargarAeropuertosEnDropDown();

                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los aereopuertos: " + ex.Message + "');", true);
                }
            }
        }
        protected void btmAereopuerto_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalAereopuerto", "mostrarModal('modalAereopuerto');", true);
        }

        protected void btmDestino_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalDestino", "mostrarModal('modalDestino');", true);

        }

        protected void btmHorario_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalHorario", "mostrarModal('modalHorario');", true);

        }

        protected void btmFiltrarAereopuerto_Click(object sender, EventArgs e)
        {
            string rol = Session["rol"].ToString();
            Session["rol"] = rol;
            int op = 1;
            string nombreAeropuerto = ddlAereopuerto.SelectedItem.Text;
            Session["valor"] = nombreAeropuerto;
            Session["op"] = op;
            Response.Redirect("ListaVuelos.aspx");
        }

        protected void btmFiltrarDestino_Click(object sender, EventArgs e)
        {
            string rol = Session["rol"].ToString();
            Session["rol"] = rol;
            int op = 2;
            string destino = txtDestino.Text;
            Session["valor"] = destino;
            Session["op"] = op;
            Response.Redirect("ListaVuelos.aspx");
        }

        protected void btmFiltrarHorario_Click(object sender, EventArgs e)
        {
            string rol = Session["rol"].ToString();
            Session["rol"] = rol;
            int op = 3;
            string horario = cldHorario.SelectedDate.ToString("yyyy-MM-dd");
            Session["valor"] = horario;
            Session["op"] = op;
            Response.Redirect("ListaVuelos.aspx");
        }

        protected void cldHorario_SelectionChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalHorario", "mostrarModal('modalHorario');", true);
        }

        protected void cldHorario_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalHorario", "mostrarModal('modalHorario');", true);
        }
        protected void CargarAeropuertosEnDropDown()
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                DataTable aeropuertos = hangaresLogic.ObtenerAeropuertos();

                ddlAereopuerto.Items.Clear();

                foreach (DataRow fila in aeropuertos.Rows)
                {
                    string nombreAeropuerto = fila["Nombre"].ToString();
                    ddlAereopuerto.Items.Add(nombreAeropuerto);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Surgio un problema al cargar los aereopuertos.');", true);
            }
        }

        protected void btmPersonalizado_Click(object sender, EventArgs e)
        {
            string rol = Session["rol"].ToString();
            Session["rol"] = rol;
            Response.Redirect("AplicarFiltros.aspx");
        }
    }
}