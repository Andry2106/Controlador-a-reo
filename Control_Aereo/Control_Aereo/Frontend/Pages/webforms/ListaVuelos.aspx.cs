using System;
using System.Data;
using System.Web.UI.WebControls;
using Frontend.Logic;

namespace Frontend.Pages
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string rol = Session["rol"].ToString();
                CargarVuelosProgramados();
                if(rol == "Despegue")
                {
                    btmEmbarcar.Visible = false;    
                }
                else 
                {
                    btmSeleccionar.Visible = false;
                }
            }
        }
        protected void lnkVuelosProgramados_Click(object sender, EventArgs e)
        {
            HiddenFieldVuelo.Value =null;
            GvListaVuelos.Visible = true;
            CargarVuelosProgramados();
            GvVuelosCancelados.Visible = false;

            string rol = Session["rol"].ToString();
            CargarVuelosProgramados();
            if (rol == "Despegue")
            {
                btmSeleccionar.Visible = true;
                HiddenFieldVuelo.Value = null;
                GvListaVuelos.Visible = true;
                CargarVuelosProgramados();
                GvVuelosCancelados.Visible = false;
            }
            else
            {
                btmEmbarcar.Visible = true;
                HiddenFieldVuelo.Value = null;
                GvListaVuelos.Visible = true;
                CargarVuelosProgramados();
                GvVuelosCancelados.Visible = false;
            }
        }
        protected void lnkVuelosCancelados_Click(object sender, EventArgs e)
        {
            string rol = Session["rol"].ToString();
            CargarVuelosProgramados();
            if (rol == "Despegue")
            {
                btmSeleccionar.Visible = false;
                GvVuelosCancelados.Visible = true;
                CargarVuelosCancelados();
                GvListaVuelos.Visible = false;
            }
            else
            {
                btmEmbarcar.Visible = false;
                GvVuelosCancelados.Visible = true;
                CargarVuelosCancelados();
                GvListaVuelos.Visible = false;
            }
        }
        private void CargarVuelosProgramados()
        {
            string aeropuerto = Session["aereopuerto"] != null ? Session["aereopuerto"].ToString() : null;
            string destino = Session["destino"] != null ? Session["destino"].ToString() : null;
            string horario = Session["horario"] != null ? Session["horario"].ToString() : null;
            string valor = Session["valor"].ToString();
            string op = Session["op"].ToString();
            int operacion = Int32.Parse(op);
            if (Session["valor"] != null && Session["op"] != null)
            {
                try
                {
                    ListaVuelosLogic listaVuelosLogic = new ListaVuelosLogic();
                    DataTable vuelosProgramados = listaVuelosLogic.FiltrarVuelos(operacion, valor);
                    GvListaVuelos.DataSource = vuelosProgramados;
                    GvListaVuelos.DataBind();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los vuelos: " + ex.Message + "');", true);
                }
            }
            else
            {
                try
                {
                    ListaVuelosLogic listaVuelosLogic = new ListaVuelosLogic();
                    DataTable vuelosProgramados = listaVuelosLogic.ListadoVuelos(1);
                    GvListaVuelos.DataSource = vuelosProgramados;
                    GvListaVuelos.DataBind();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los vuelos: " + ex.Message + "');", true);
                }
            }
            if (operacion == 4)
            {
                    ListaVuelosLogic listaVuelosLogic = new ListaVuelosLogic();
                    DataTable vuelosProgramados = listaVuelosLogic.FiltrarVuelosVarios(aeropuerto, destino , horario);
                    GvListaVuelos.DataSource = vuelosProgramados;
                    GvListaVuelos.DataBind();
            }
        }
        private void CargarVuelosCancelados()
        {
            try
            {
                ListaVuelosLogic listaVuelosLogic = new ListaVuelosLogic();
                DataTable vuelosCancelados = listaVuelosLogic.ListadoVuelos(2);
                GvVuelosCancelados.DataSource = vuelosCancelados;
                GvVuelosCancelados.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los vuelos: " + ex.Message + "');", true);
            }
        }
        protected void GvListaVuelos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = "handleRowClickListado(" + e.Row.RowIndex + ");";
            }
        }
        protected void GvVuelosCancelados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = "handleRowClickCancelado(" + e.Row.RowIndex + ");";
            }
        }
        protected void btmSeleccionar_Click(object sender, EventArgs e)
        {
            string avion = HiddenFieldIdAvion.Value;

            if (string.IsNullOrEmpty(HiddenFieldVuelo.Value) || string.IsNullOrEmpty(HiddenFieldIdAvion.Value) || string.IsNullOrEmpty(HiddenFieldOrigen.Value) || string.IsNullOrEmpty(HiddenFieldDestino.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor seleccione un vuelo antes de continuar.');", true);
                return;
            }
            string vuelo = HiddenFieldVuelo.Value;
            string origen = HiddenFieldOrigen.Value;
            string destino = HiddenFieldDestino.Value;
            Session["VueloSeleccionado"] = vuelo;
            Session["IdAvionSeleccionado"] = avion;
            Session["Origen"] = origen;
            Session["Destino"] = destino;
            Response.Redirect("Despegue.aspx");
        }
        protected void btmEmbarcar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HiddenFieldVuelo.Value) || string.IsNullOrEmpty(HiddenFieldIdAvion.Value) || string.IsNullOrEmpty(HiddenFieldOrigen.Value) || string.IsNullOrEmpty(HiddenFieldDestino.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor seleccione un vuelo antes de continuar.');", true);
                return;
            }
            string vuelo = HiddenFieldVuelo.Value;
            string avion = HiddenFieldIdAvion.Value;
            string origen = HiddenFieldOrigen.Value;
            string destino = HiddenFieldDestino.Value;
            Session["VueloSeleccionado"] = vuelo;
            Session["IdAvionSeleccionado"] = avion;
            Session["Origen"] = origen;
            Session["Destino"] = destino;
            Response.Redirect("EmbarcamientoAviones.aspx");
        }
    }
}
