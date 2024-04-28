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
    public partial class EmbarcamientoAviones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdAvionSeleccionado"] != null)
                {
                    int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);
                    CargarDatosAvion();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaAvionNoSeleccionado", "alert('No hay un avión seleccionado.');", true);
                    Response.Redirect("ListaVuelos.aspx");
                }
                btmSeleccionar.Enabled = false;
                btmSeleccionar.Visible = false;
                btmEmbarcarMasivo.Visible = false;
                btmSelecionarEmbarcamiento.Visible = false;
                btmEmbarcamientoAvion_Eliminar_Individual.Visible = false;
                btmEmbarcamientoAvion_Eliminar_Automatico.Visible = false;
                CargarDatosEmbarque();
                LlenarDDLPuertas();
            }
        }
        private void LlenarDDLPuertas()
        {
            DDLPuertas.Items.Clear();

            try
            {
                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                DataTable puertasActivas = embarcamientoLogic.ObtenerPuertasActivas();

                if (puertasActivas != null && puertasActivas.Rows.Count > 0)
                {
                    foreach (DataRow fila in puertasActivas.Rows)
                    {
                        string idPuerta = fila["Id"].ToString();
                        string nombrePuerta = fila["Nombre"].ToString();

                        DDLPuertas.Items.Add(new ListItem(nombrePuerta, idPuerta));
                    }
                }
                else
                {
                    DDLPuertas.Items.Add(new ListItem("No hay puertas activas", "0"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al llenar DDLPuertas: " + ex.Message);
            }
        }

        protected void lnkEmbarcamiento_Click(object sender, EventArgs e)
        {
            btmInsertarEmbarcamiento.Visible = true;
            btmInsertarEmbarcamiento.Enabled = true;
            btmEmbarcamientoMasivo.Visible = true;
            btmSeleccionar.Visible = false;
            btmSelecionarEmbarcamiento.Visible = false;
            btmEmbarcarMasivo.Visible = false;
            GvDatosEmbarque.Visible = true;
            GvEquipamentos.Visible = false;
            DDLPuertas.Visible = true;


            //Embarcamientos
            btmEliminar_Embarque_Individual.Visible = true;
            btmElimiar_Embarque_Masivo.Visible = true;

            //Embarcamiento Avion
            btmEmbarcamientoAvion_Eliminar_Individual.Visible = false;
            btmEmbarcamientoAvion_Eliminar_Automatico.Visible = false;

            CargarDatosEmbarque();
        }

        protected void LinkEquipaje_Click(object sender, EventArgs e)
        {
            btmEmbarcamientoMasivo.Visible = false;
            btmSelecionarEmbarcamiento.Visible = false;
            btmInsertarEmbarcamiento.Visible = false;
            btmSeleccionar.Visible = true;
            btmEmbarcarMasivo.Visible = true;
            btmSelecionarEmbarcamiento.Visible = false;
            GvDatosEmbarque.Visible = false;
            GvEquipamentos.Visible = true;
            DDLPuertas.Visible = false;

            //Embarcamientos
            btmEliminar_Embarque_Individual.Visible = false;
            btmElimiar_Embarque_Masivo.Visible = false;

            //Embarcamiento Avion
            btmEmbarcamientoAvion_Eliminar_Individual.Visible = false;
            btmEmbarcamientoAvion_Eliminar_Automatico.Visible = false;

            CargarEquipamento();
            CargarPasajeros();

        }

        protected void lnkEmbarcamiento_Avion_Click(object sender, EventArgs e)
        {
            btmEmbarcamientoMasivo.Visible = false;
            btmSelecionarEmbarcamiento.Visible = false;
            btmInsertarEmbarcamiento.Visible = false;
            btmSeleccionar.Visible = false;
            btmEmbarcarMasivo.Visible = false;
            btmSelecionarEmbarcamiento.Visible = false;
            GvEquipamentos.Visible = false;
            DDLPuertas.Visible = false;

            //Embarcamientos
            btmEliminar_Embarque_Individual.Visible = false;
            btmElimiar_Embarque_Masivo.Visible = false;

            //Embarcamiento Avion
            btmEmbarcamientoAvion_Eliminar_Automatico.Visible = true;
            btmEmbarcamientoAvion_Eliminar_Individual.Visible = true;
            GvDatosEmbarque.Visible = true;
            ObtenerEmbarcamientosPorAvion();
        }

        protected void btmSelecionar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HiddenFieldIDEquipamiento.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalPasajeros", "mostrarModal('modalPasajeros');", true);
                btmAgregar.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Debe seleccionar un equipamiento.');", true);
            }
        }

        private void ObtenerEmbarcamientosPorAvion()
        {
            int idAvionSeleccionado = 0;
            if (Session["IdAvionSeleccionado"] != null)
            {
                idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);
                try
                {
                    EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                    DataTable datosAvion = embarcamientoLogic.ObtenerEmbarcamientosPorAvion(idAvionSeleccionado);

                    GvDatosEmbarque.DataSource = datosAvion;
                    GvDatosEmbarque.DataBind();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos del avión: " + ex.Message + "');", true);
                }
            }
            else
            {
                // Aquí se manejaría el caso de no tener un ID de avión seleccionado
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
            }
        }

        private void CargarPasajeros()
        {
            try
            {
                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();

                DataTable pasajeros = embarcamientoLogic.ListadoPasajeros();

                GvPasajeros.DataSource = pasajeros;
                GvPasajeros.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los pasajeros: " + ex.Message + "');", true);
            }
        }

        private void CargarEquipamento()
        {
            try
            {
                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();

                DataTable pasajeros = embarcamientoLogic.ListadoEquipamiento();

                GvEquipamentos.DataSource = pasajeros;
                GvEquipamentos.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los pasajeros: " + ex.Message + "');", true);
            }
        }

        private void CargarDatosEmbarque()
        {
            try
            {
                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();

                DataTable pasajeros = embarcamientoLogic.ObtenerDatosEmbarque();

                GvDatosEmbarque.DataSource = pasajeros;
                GvDatosEmbarque.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los pasajeros: " + ex.Message + "');", true);
            }
        }

        protected void GvEquipamentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idEquipamiento = GvEquipamentos.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["data-idequipamiento"] = idEquipamiento;
                e.Row.Attributes["onclick"] = $"javascript:handleRowClickEquipamiento({e.Row.RowIndex});";

            }
        }

        protected void GvPasajeros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idPasajero = GvPasajeros.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["data-idpasajero"] = idPasajero;
                e.Row.Attributes["onclick"] = $"javascript:handleRowClickPasajero({e.Row.RowIndex});";
            }
        }

        protected void GvDatosEmbarque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idPasajero = GvDatosEmbarque.DataKeys[e.Row.RowIndex].Value.ToString();
                e.Row.Attributes["data-IDEmbarque"] = idPasajero;
                e.Row.Attributes["onclick"] = $"javascript:handleRowClickEmbarque({e.Row.RowIndex});";
            }
        }

        protected void btmAgregar_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
            {
                int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

                if (string.IsNullOrWhiteSpace(HiddenFieldIDEquipamiento.Value) || string.IsNullOrWhiteSpace(HiddenFieldIDPasajero.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Los datos de Equipaje y Pasajero no pueden estar vacíos.');", true);
                    return;
                }

                int IDEquipaje = Convert.ToInt32(HiddenFieldIDEquipamiento.Value);
                int IDPasajero = Convert.ToInt32(HiddenFieldIDPasajero.Value);

                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                embarcamientoLogic.InsertarEmbarque(IDEquipaje, IDPasajero);

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Embarque insertado correctamente.');", true);
                CargarPasajeros();
                CargarEquipamento();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione un embarque.');", true);
                Response.Redirect("ListaVuelos.aspx");
            }
        }

        private void CargarDatosAvion()
        {
            int idAvionSeleccionado = 0;
            if (Session["IdAvionSeleccionado"] != null)
            {
                idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);
                try
                {
                    EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                    DataTable datosAvion = embarcamientoLogic.ObtenerDatosAvionPorID(idAvionSeleccionado);

                    if (datosAvion != null && datosAvion.Rows.Count > 0)
                    {
                        DataRow fila = datosAvion.Rows[0]; // Asumimos que solo hay una fila, ya que el ID debe ser único
                        LabelIDAvion.Text = $"Identificación del avión: {fila["IDAvion"]}";
                        LabelModelo.Text = $"Modelo: {fila["Modelo"]}";
                        LabelCompaniaAeria.Text = $"Compañía Aérea: {fila["CompaniaAerea"]}";
                        LabelCapacidadCarga.Text = $"Capacidad de Carga: {fila["CapacidadCarga"]}";
                        LabelCapacidadPasajeros.Text = $"Capacidad de Pasajeros: {fila["CapacidadPasajeros"]}";
                        LabelCapacidadCombustible.Text = $"Capacidad de Combustible: {fila["CapacidadCombustible"]}";
                        LabelEstado.Text = $"Estado: {fila["Estado"]}";
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos del avión: " + ex.Message + "');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
            }
        }

        protected void btmInsertarEmbarcamiento_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

                if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
                {
                    int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

                    if (DDLPuertas.SelectedValue != "0" && !string.IsNullOrEmpty(DDLPuertas.SelectedValue))
                    {
                        int idPuertaSeleccionada = Convert.ToInt32(DDLPuertas.SelectedValue);

                        EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                        string resultado = embarcamientoLogic.InsertarEmbarcamiento(idAvionSeleccionado, idEmbarqueSeleccionado, idPuertaSeleccionada);

                        string script = resultado == "Operación realizada con éxito."
                            ? "Swal.fire('¡Exelecte!', '" + resultado + "', 'success');"
                            : "Swal.fire('Atención', '" + resultado + "', 'warning');";

                        ScriptManager.RegisterStartupScript(this, GetType(), "swalAlert", script, true);

                        CargarDatosEmbarque();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "swal", "Swal.fire('Error', 'Por favor, seleccione una puerta válida.', 'error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "swal", "Swal.fire('Error', 'Por favor, seleccione un embarque.', 'error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "swal", "Swal.fire('Error', 'No hay un avión seleccionado.', 'error').then((result) => { if (result.isConfirmed) { window.location = 'ListaVuelos.aspx'; } });", true);
                Response.Redirect("ListaVuelos.aspx");

            }
        }


        protected void btmEmbarcarMasivo_Click(object sender, EventArgs e)
        {
            try
            {
                EmbarcamientoLogic embarcamiento = new EmbarcamientoLogic();
                embarcamiento.InsertarEmbarqueMasivo();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", "Swal.fire('¡Completado!', 'Embarque masivo correcto.', 'success');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('¡Error!', 'Embarque masivo inválido. " + ex.Message + "', 'error');", true);
            }
        }


        //protected void btmEmbarcarMasivo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EmbarcamientoLogic embarcamiento = new EmbarcamientoLogic();
        //        embarcamiento.InsertarEmbarqueMasivo();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarque masivo corecto.');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarque masivo invalido.');", true);
        //    }
        //}


        protected void btmEmbarcamientoMasivo_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

                if (DDLPuertas.SelectedValue != "0" && !string.IsNullOrEmpty(DDLPuertas.SelectedValue))
                {
                    int idPuertaSeleccionada = Convert.ToInt32(DDLPuertas.SelectedValue);

                    EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                    string resultado = embarcamientoLogic.InsertarEmbarcamientoMasivo(idAvionSeleccionado, idPuertaSeleccionada);

                    // Modificado para usar SweetAlert2
                    string script = resultado == "Operación completada con éxito."
                        ? "Swal.fire('¡Completado!', '" + resultado + "', 'success');"
                        : "Swal.fire('¡Atención!', '" + resultado + "', 'warning');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalAlert", script, true);

                    CargarDatosEmbarque();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire('Error', 'Por favor, seleccione una puerta válida.', 'error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", "Swal.fire('Error', 'No hay un avión seleccionado.', 'error');", true);
                Response.Redirect("ListaVuelos.aspx");
            }
        }

        //protected void btmEmbarcamientoMasivo_Click(object sender, EventArgs e)
        //{
        //    if (Session["IdAvionSeleccionado"] != null)
        //    {
        //        int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);


        //        if (DDLPuertas.SelectedValue != "0" && !string.IsNullOrEmpty(DDLPuertas.SelectedValue))
        //        {
        //            int idPuertaSeleccionada = Convert.ToInt32(DDLPuertas.SelectedValue);

        //            EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
        //            string resultado = embarcamientoLogic.InsertarEmbarcamientoMasivo(idAvionSeleccionado, idPuertaSeleccionada);

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{resultado}');", true);

        //            CargarDatosEmbarque();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione una puerta válida.');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
        //        Response.Redirect("ListaVuelos.aspx");
        //    }

        //}


        protected void btmEmbarcamientoAvion_Eliminar_Individual_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

                if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
                {
                    int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

                    EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                    embarcamientoLogic.EliminarEmbarcamientoPorID(idEmbarqueSeleccionado);
                    // Usando SweetAlert2 para confirmar la eliminación
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", "Swal.fire('¡Eliminado!', 'Embarcamiento eliminado correctamente.', 'success');", true);
                    ObtenerEmbarcamientosPorAvion();
                }
                else
                {
                    // Usando SweetAlert2 para advertir sobre la selección
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalWarning", "Swal.fire('Atención', 'Por favor, seleccione un embarcamiento.', 'warning');", true);
                }
            }
            else
            {
                // Usando SweetAlert2 para error y redirección
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('Error', 'No hay un avión seleccionado.', 'error').then((result) => { if (result.isConfirmed) { window.location = 'ListaVuelos.aspx'; } });", true);
            }
        }

        //protected void btmEmbarcamientoAvion_Eliminar_Individual_Click(object sender, EventArgs e)
        //{
        //    if (Session["IdAvionSeleccionado"] != null)
        //    {
        //        int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

        //        if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
        //        {
        //            int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

        //            EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
        //            embarcamientoLogic.EliminarEmbarcamientoPorID(idEmbarqueSeleccionado);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarcamiento eliminado.');", true);
        //            ObtenerEmbarcamientosPorAvion();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione un embarcamiento.');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
        //        Response.Redirect("ListaVuelos.aspx");
        //    }
        //}

        protected void btmEmbarcamientoAvion_Eliminar_Automatico_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

                EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                embarcamientoLogic.EliminarEmbarcamientosPorIDAvion(idAvionSeleccionado);
                // Usando SweetAlert2 para confirmar la eliminación
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", "Swal.fire('¡Eliminados!', 'Embarcamientos eliminados correctamente.', 'success');", true);
                ObtenerEmbarcamientosPorAvion();
            }
            else
            {
                // Usando SweetAlert2 para error y redirección
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('Error', 'No hay un avión seleccionado.', 'error').then((result) => { if (result.isConfirmed) { window.location = 'ListaVuelos.aspx'; } });", true);
            }
        }


        //protected void btmEmbarcamientoAvion_Eliminar_Automatico_Click(object sender, EventArgs e)
        //{
        //    if (Session["IdAvionSeleccionado"] != null)
        //    {
        //        int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

        //        EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
        //        embarcamientoLogic.EliminarEmbarcamientosPorIDAvion(idAvionSeleccionado);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embaracamientos eliminados.');", true);
        //        ObtenerEmbarcamientosPorAvion();

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
        //        Response.Redirect("ListaVuelos.aspx");
        //    }
        //}

        protected void btmEliminar_Embarque_Individual_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

                if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
                {
                    int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

                    EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
                    embarcamientoLogic.EliminarEmbarquePorIDEmbarque(idEmbarqueSeleccionado);
                    // Usando SweetAlert2 para confirmar la eliminación
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", "Swal.fire('¡Eliminado!', 'Embarque eliminado correctamente.', 'success');", true);
                    CargarDatosEmbarque();
                }
                else
                {
                    // Usando SweetAlert2 para advertir sobre la selección
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalWarning", "Swal.fire('Atención', 'Por favor, seleccione un embarcamiento.', 'warning');", true);
                }
            }
            else
            {
                // Usando SweetAlert2 para error y redirección
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('Error', 'No hay un avión seleccionado.', 'error').then((result) => { if (result.isConfirmed) { window.location = 'ListaVuelos.aspx'; } });", true);
            }
        }


        //protected void btmEliminar_Embarque_Individual_Click(object sender, EventArgs e)
        //{
        //    if (Session["IdAvionSeleccionado"] != null)
        //    {
        //        int idAvionSeleccionado = Convert.ToInt32(Session["IdAvionSeleccionado"]);

        //        if (!string.IsNullOrEmpty(HiddenFieldIDEmbarque.Value))
        //        {
        //            int idEmbarqueSeleccionado = Convert.ToInt32(HiddenFieldIDEmbarque.Value);

        //            EmbarcamientoLogic embarcamientoLogic = new EmbarcamientoLogic();
        //            embarcamientoLogic.EliminarEmbarquePorIDEmbarque(idEmbarqueSeleccionado);
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarque eliminado.');", true);
        //            CargarDatosEmbarque();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, seleccione un embarcamiento.');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
        //        Response.Redirect("ListaVuelos.aspx");
        //    }
        //}




        protected void btmElimiar_Embarque_Masivo_Click(object sender, EventArgs e)
        {
            if (Session["IdAvionSeleccionado"] != null)
            {
                try
                {
                    EmbarcamientoLogic embarcamiento = new EmbarcamientoLogic();
                    embarcamiento.EliminarEmbarquesNoVinculados();
                    // Usando SweetAlert2 para confirmar la operación exitosa
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalSuccess", "Swal.fire('¡Completado!', 'Embarque masivo correcto.', 'success');", true);
                    CargarDatosEmbarque();
                }
                catch (Exception ex)
                {
                    // Usando SweetAlert2 para mostrar un error durante la operación
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('¡Error!', 'Embarque masivo inválido: " + ex.Message + "', 'error');", true);
                }
            }
            else
            {
                // Usando SweetAlert2 para error y redirección si no hay avión seleccionado
                ScriptManager.RegisterStartupScript(this, this.GetType(), "swalError", "Swal.fire('¡Error!', 'No hay un avión seleccionado.', 'error').then((result) => { if (result.isConfirmed) { window.location = 'ListaVuelos.aspx'; } });", true);
            }
        }


        //protected void btmElimiar_Embarque_Masivo_Click(object sender, EventArgs e)
        //{
        //    if (Session["IdAvionSeleccionado"] != null)
        //    {
        //        try
        //        {
        //            EmbarcamientoLogic embarcamiento = new EmbarcamientoLogic();
        //            embarcamiento.EliminarEmbarquesNoVinculados();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarque masivo corecto.');", true);
        //            CargarDatosEmbarque();
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Embarque masivo invalido.');", true);
        //        }

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No hay un avión seleccionado.');", true);
        //        Response.Redirect("ListaVuelos.aspx");
        //    }
        //}
    }
}