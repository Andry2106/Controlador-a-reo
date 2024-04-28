using Frontend.DataAccess;
using Frontend.Logic;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

namespace Frontend.Pages.webforms
{
    public partial class Despegue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["VueloSeleccionado"] != null && Session["IdAvionSeleccionado"] != null)
                {

                    btmAutorizar.BackColor = System.Drawing.Color.Orange;
                    btmAutorizar.Visible = true;

                    string vuelo = Session["VueloSeleccionado"].ToString();
                    string avion = Session["IdAvionSeleccionado"].ToString();
                    string origen = Session["Origen"].ToString();
                    string destino = Session["Destino"].ToString();
                    lblVuelo.Text = "N.Vuelo: " + vuelo;
                    lblIdentificacionAvion.Text = "Identificacion del avion: " + avion;
                    int vueloID = int.Parse(vuelo);
                    int avionID = int.Parse(avion);
                    DespegueLogic despegueLogic = new DespegueLogic();
                    DataTable datosVuelo = despegueLogic.ObtenerVueloPorID(vueloID);
                    DataTable datosAvion = despegueLogic.ObtenerAvionPorID(avionID);

                    if (datosVuelo.Rows.Count > 0 && datosAvion.Rows.Count > 0)
                    {
                        lblOrigen.Text = "Origen: " + datosVuelo.Rows[0]["Origen"].ToString();
                        lblDestino.Text = "Destino: " + datosVuelo.Rows[0]["Destino"].ToString();
                        lblHoraSalida.Text = "Hora de salida: " + ((DateTime)datosVuelo.Rows[0]["HoraSalida"]).ToString("dd/MM/yyyy HH:mm");
                        lblHoraLlegada.Text = "Hora de llegada: " + ((DateTime)datosVuelo.Rows[0]["HoraLlegada"]).ToString("dd/MM/yyyy HH:mm");
                        lblModelo.Text = "Modelo: " + datosAvion.Rows[0]["Modelo"].ToString();
                        lblPesoMaximo.Text = "Peso máximo: " + datosAvion.Rows[0]["CapacidadCarga"].ToString() + " kg";
                        lblCapacidadPasajeros.Text = "Capacidad de pasajeros: " + datosAvion.Rows[0]["CapacidadPasajeros"].ToString();
                        lblCapacidadCombustible.Text = "Capacidad de combustible: " + datosAvion.Rows[0]["CapacidadCombustible"].ToString() + " litros";
                        lblUltimaRevision.Text = "Última revisión: " + ((DateTime)datosAvion.Rows[0]["UltimaRevision"]).ToString("dd/MM/yyyy");
                        lblProximaRevision.Text = "Próxima revisión: " + ((DateTime)datosAvion.Rows[0]["ProximaRevision"]).ToString("dd/MM/yyyy");
                        lblCompaniaAerea.Text = "Compañía aérea: " + datosAvion.Rows[0]["CompaniaAerea"].ToString();
                        lblObservaciones.Text = "Observaciones: " + datosAvion.Rows[0]["Observaciones"].ToString();
                        lblEstado.Text = "Estado: " + datosAvion.Rows[0]["Estado"].ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron datos del vuelo o del avión.');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se pudieron cargar los datos.');", true);
                }
            }
        }
        protected void btmDenegar_Click(object sender, EventArgs e)
        {
            try
            {
                string aeropuerto = null;
                string destino2 = null;
                string horario = null;
                string valor = "test";
                int op = 4;
                string rol = Session["rol"].ToString();
                string vuelo = Session["VueloSeleccionado"].ToString();
                int vueloID = int.Parse(vuelo);
                DespegueLogic despegueLogic = new DespegueLogic();
                Label1.Text = vueloID.ToString();
                despegueLogic.CambiarEstadoVuelo(vueloID, 0);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaAdvertencia", "Swal.fire('Mensaje de advertencia', 'Se denego el vuelo de manera correcta', 'warning');", true);
                Session["rol"] = rol;
                Session["op"] = op;
                Session["aereopuerto"] = aeropuerto;
                Session["destino"] = destino2;
                Session["horario"] = horario;
                Session["valor"] = valor;
                Response.Redirect("ListaVuelos.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", "Swal.fire('Mensaje de error', 'Hubo un error al intentar denegar el vuelo', 'error');", true);
            }
        }
        protected void btmAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                if(btmAutorizar.BackColor == Color.Orange)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaAdvertencia", "Swal.fire('Mensaje de advertencia', 'Tiene que validar el vuelo primero , este tiene que cumplir todas las validaciones para poder ser despegado', 'warning');", true);
                }
                else
                {
                    string aeropuerto = null;
                    string destino2 = null;
                    string horario = null;
                    string valor = "test";
                    int op = 4;
                    string rol = Session["rol"].ToString();

                    string origen = Session["Origen"].ToString();
                    string destino = Session["Destino"].ToString();
                    string vuelo = Session["VueloSeleccionado"].ToString();
                    int vueloID = int.Parse(vuelo);
                    DateTime horaDespegue = DateTime.Now;
                    string horaDespegueTexto = horaDespegue.ToString("yyyy-MM-dd HH:mm:ss");
                    DespegueLogic despegueLogic = new DespegueLogic();
                    despegueLogic.InsertarDespegue(horaDespegueTexto, origen, destino, vueloID, 1, 1);
                    despegueLogic.PasarDespegue(vueloID);
                    despegueLogic.BorrarVueloPorNumero(vueloID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExito", "Swal.fire('Mensaje del vuelo', 'Se autorizo correctamente', 'success');", true);
                    Session["rol"] = rol;
                    Session["op"] = op;
                    Session["aereopuerto"] = aeropuerto;
                    Session["destino"] = destino2;
                    Session["horario"] = horario;
                    Session["valor"] = valor;
                    Response.Redirect("ListaVuelos.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", "Swal.fire('Mensaje de error', 'Ocurrio un error al intentar dar la orden de despegue', 'error');", true);
            }
        }
        protected void btmValidar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalDespegue", "mostrarModal('modalDespegue');", true);
        }
        protected void btmValidarModal_Click(object sender, EventArgs e)
        {
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);

            DespegueLogic despegueLogic = new DespegueLogic();

            despegueLogic.InsertarValidacionesDespegue02(vueloID);

            int resultadoValidacion = despegueLogic.ValidarValidacionesDespegue(vueloID);

            string validacionFallida = ValidarValidacionesDespegue2(vueloID); 

            if (resultadoValidacion == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalDespegue", "mostrarModal('modalDespegue');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaExito", "Swal.fire('Validaciones', 'Se han cumplido las validaciones', 'success');", true);

                btmRutaEstablecida.BackColor = System.Drawing.Color.LimeGreen;
                btmInfoMeteorologica.BackColor = System.Drawing.Color.LimeGreen;
                btmCoordinacion.BackColor = System.Drawing.Color.LimeGreen;
                btmSeparacionInicial.BackColor = System.Drawing.Color.LimeGreen;
                btmEmbarcamiento.BackColor = System.Drawing.Color.LimeGreen;
                btmEquipajePasajeros.BackColor = System.Drawing.Color.LimeGreen;
                btmEspacio.BackColor = System.Drawing.Color.LimeGreen;


                btmRutaEstablecida.ForeColor = System.Drawing.Color.White;
                btmInfoMeteorologica.ForeColor = System.Drawing.Color.White;
                btmCoordinacion.ForeColor = System.Drawing.Color.White;
                btmSeparacionInicial.ForeColor = System.Drawing.Color.White;
                btmEmbarcamiento.ForeColor = System.Drawing.Color.White;
                btmEquipajePasajeros.ForeColor = System.Drawing.Color.White;
                btmEspacio.ForeColor = System.Drawing.Color.White;


                btmRutaEstablecida.Enabled = false;
                btmInfoMeteorologica.Enabled = false;
                btmCoordinacion.Enabled = false;
                btmSeparacionInicial.Enabled = false;
                btmEmbarcamiento.Enabled = false;
                btmEquipajePasajeros.Enabled = false;
                btmEspacio.Enabled = false;

                btmAutorizar.Enabled = true;
                btmAutorizar.BackColor = System.Drawing.Color.LimeGreen;

            }
            else if (resultadoValidacion == -1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", "Swal.fire('Validaciones', 'fallaron las validaciones', 'error');", true);
                if (btmRutaEstablecida.Text == validacionFallida)
                {
                    btmRutaEstablecida.ForeColor = System.Drawing.Color.White;
                    btmRutaEstablecida.BackColor = System.Drawing.Color.Red;
                    btmRutaEstablecida.Enabled = true;
                }
                else
                {
                    btmRutaEstablecida.ForeColor = System.Drawing.Color.White;
                    btmRutaEstablecida.BackColor = System.Drawing.Color.LimeGreen;
                    btmRutaEstablecida.Enabled = false;

                }
                if (btmInfoMeteorologica.Text == validacionFallida)
                {
                    btmRutaEstablecida.Enabled = true;
                    btmInfoMeteorologica.ForeColor = System.Drawing.Color.White;
                    btmInfoMeteorologica.BackColor = System.Drawing.Color.Red;

                }
                else
                {
                    btmInfoMeteorologica.ForeColor = System.Drawing.Color.White;
                    btmInfoMeteorologica.BackColor = System.Drawing.Color.LimeGreen;
                    btmInfoMeteorologica.Enabled = false;
                }
                if (btmCoordinacion.Text == validacionFallida)
                {
                    btmCoordinacion.Enabled = true;
                    btmCoordinacion.ForeColor = System.Drawing.Color.White;
                    btmCoordinacion.BackColor = System.Drawing.Color.Red;

                }
                else
                {
                    btmCoordinacion.ForeColor = System.Drawing.Color.White;
                    btmCoordinacion.BackColor = System.Drawing.Color.LimeGreen;
                    btmCoordinacion.Enabled = false;

                }
                if (btmSeparacionInicial.Text == validacionFallida)
                {
                    btmSeparacionInicial.Enabled = true;
                    btmSeparacionInicial.ForeColor = System.Drawing.Color.White;
                    btmSeparacionInicial.BackColor = System.Drawing.Color.Red;

                }
                else
                {
                    btmSeparacionInicial.ForeColor = System.Drawing.Color.White;
                    btmSeparacionInicial.BackColor = System.Drawing.Color.LimeGreen;
                    btmSeparacionInicial.Enabled = false;

                }
                if (btmEspacio.Text == validacionFallida)
                {
                    btmEspacio.Enabled = true;
                    btmEspacio.ForeColor = System.Drawing.Color.White;
                    btmEspacio.BackColor = System.Drawing.Color.Red;

                }
                else
                {
                    btmEspacio.ForeColor = System.Drawing.Color.White;
                    btmEspacio.BackColor = System.Drawing.Color.LimeGreen;
                    btmEspacio.Enabled= false;

                }
                if (btmEmbarcamiento.Text == validacionFallida)
                {
                    btmEmbarcamiento.Enabled = true;
                    btmEquipajePasajeros.Enabled = true;
                    btmEmbarcamiento.ForeColor = System.Drawing.Color.White;
                    btmEquipajePasajeros.ForeColor = System.Drawing.Color.White;
                    btmEmbarcamiento.BackColor = System.Drawing.Color.Red;
                    btmEquipajePasajeros.BackColor = System.Drawing.Color.Red;

                }
                else
                {
                    btmEmbarcamiento.ForeColor = System.Drawing.Color.White;
                    btmEquipajePasajeros.ForeColor = System.Drawing.Color.White;
                    btmEmbarcamiento.BackColor = System.Drawing.Color.LimeGreen;
                    btmEquipajePasajeros.BackColor = System.Drawing.Color.LimeGreen;
                    btmEmbarcamiento.Enabled = false;
                    btmEquipajePasajeros.Enabled = false;

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalDespegue", "mostrarModal('modalDespegue');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El estado de las validaciones es indeterminado.');", true);
            }
        }
        public string ValidarValidacionesDespegue2(int numeroVuelo)
        {
            string validacionFallida = string.Empty;

            DespegueLogic despegueLogic = new DespegueLogic();

            DataTable validacionesFallidas = despegueLogic.BuscarValidacionesFallidas(numeroVuelo);
            if (validacionesFallidas.Rows.Count > 0)
            {
                foreach (DataRow row in validacionesFallidas.Rows)
                {
                    validacionFallida += row["ValidacionFallida"].ToString() + ", ";
                }
                validacionFallida = validacionFallida.TrimEnd(',', ' ');
            }

            return validacionFallida;
        }
        protected void btmInfoMeteorologica_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";

        }
        protected void btmRutaEstablecida_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
        protected void btmCoordinacion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
        protected void btmSeparacionInicial_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
        protected void btmEspacio_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
        protected void btmEmbarcamiento_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
        protected void btmEquipajePasajeros_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalValidacionTexto", "mostrarModal('modalTextoValidacion');", true);
            string vuelo = Session["VueloSeleccionado"].ToString();
            int vueloID = int.Parse(vuelo);
            DespegueLogic despegueLogic = new DespegueLogic();
            despegueLogic.InsertarValidacionesDespegue02(vueloID);
            string validacionFallida = ValidarValidacionesDespegue2(vueloID);
            lblEstadoValidacion.Text = "Fallo la validacion de: " + validacionFallida + ". No se puede autorizar el despegue.";
        }
    }
}
//ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaAdvertencia", "Swal.fire('Mensaje de advertencia', 'Descripción del mensaje', 'warning');", true);
