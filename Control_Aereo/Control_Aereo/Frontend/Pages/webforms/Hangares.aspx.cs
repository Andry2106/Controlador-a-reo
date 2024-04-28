using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Frontend.Logic;

namespace Frontend.Pages.webforms
{
    public partial class Hangares : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string rol = Session["rol"].ToString();
                    CargarDatosHangares();
                    CargarAeropuertosEnDropDown();
                    CargarTiposHangaresEnDropDown();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los hangares: " + ex.Message + "');", true);
                }
            }
        }

        private void CargarDatosHangares()
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                DataTable datosHangares = hangaresLogic.MantenimientoHangares(5,0, 0, "", "", 0, "",0,0, 0);

                if (datosHangares != null && datosHangares.Rows.Count > 0)
                {
                    GvHangares.DataSource = datosHangares;
                    GvHangares.DataBind();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron datos de hangares.');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al cargar los datos de los hangares: " + ex.Message + "');", true);
            }
        }
        protected void GvHangares_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Text += " metros"; 
                e.Row.Cells[5].Text += " metros"; 
                e.Row.Attributes["onclick"] = "handleRowClick(" + e.Row.RowIndex + ");";
            }

        }

        protected void BtmAgregar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalAgregar", "mostrarModal('modalAgregar');", true);
        }

        protected void btmAgregarModal_Click(object sender, EventArgs e)
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                string nombreAeropuerto = ddlAereopuertoAgregar.SelectedItem.Text;
                string nombreTipoHangar = ddlTiposHangaresAgregar.SelectedItem.Text;
                int idAeropuerto = hangaresLogic.ObtenerIdAeropuertoPorNombre(nombreAeropuerto);
                int idTipoHangar = hangaresLogic.ObtenerIdTipoHangarPorNombre(nombreTipoHangar);

                if (string.IsNullOrWhiteSpace(txtNombreAgregar.Text) ||
                    string.IsNullOrWhiteSpace(txtUbicacionAgregar.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacidadAgregar.Text) ||
                    string.IsNullOrWhiteSpace(txtEstadoAgregar.Text) ||
                    string.IsNullOrWhiteSpace(txtTamañoAgregar.Text) ||
                    string.IsNullOrWhiteSpace(txtAlturaAgregar.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor complete todos los campos.');", true);
                    return;
                }

                if (!esNumero(txtCapacidadAgregar.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un número válido en el campo de capacidad.');", true);
                    return;
                }

                hangaresLogic.MantenimientoHangares(1, 0, idTipoHangar, txtNombreAgregar.Text, txtUbicacionAgregar.Text, Int32.Parse(txtCapacidadAgregar.Text), txtEstadoAgregar.Text, decimal.Parse(txtTamañoAgregar.Text), decimal.Parse(txtAlturaAgregar.Text),idAeropuerto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('¡Éxito!', 'Hangar agregado correctamente', 'success');", true);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ocurrió un error al agregar el hangar: " + ex.Message + "');", true);
            }
        }

        protected void BtmModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HiddenFieldHangarName.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor seleccione una fila para modificar.');", true);
                return;
            }

            string hangarName = HiddenFieldHangarName.Value;
            HangaresLogic hangaresLogic = new HangaresLogic();
            int idHangar = hangaresLogic.ObtenerIdHangarPorNombre(hangarName);
            DataTable dtHangar = hangaresLogic.MantenimientoHangares(4,idHangar,0, "", "", 0, "",0,0,0);
            foreach (DataColumn col in dtHangar.Columns)
            {
                Console.WriteLine(col.ColumnName);
            }
            if (dtHangar.Rows.Count > 0)
            {
                DataRow drHangar = dtHangar.Rows[0];
                txtNombreModificar.Text = drHangar["Nombre"].ToString();
                txtUbicacionModificar.Text = drHangar["Ubicacion"].ToString();
                txtCapacidadModificar.Text = drHangar["Capacidad"].ToString();
                txtEstadoModificar.Text = drHangar["Estado"].ToString();
                txtTamañoModificar.Text = drHangar["Tamaño"].ToString();
                txtAlturaModificar.Text = drHangar["Altura"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModalModificar", "mostrarModal('modalModificar');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontraron detalles del hangar.');", true);
            }
        }

        protected void BtmModificarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string hangarName = HiddenFieldHangarName.Value;
                HangaresLogic hangaresLogic = new HangaresLogic();
                string nombreAeropuerto = ddlAereopuertoModificar.SelectedItem.Text;
                string nombreTipoHangar = ddlTiposHangaresModificar.SelectedItem.Text;
                int idAeropuerto = hangaresLogic.ObtenerIdAeropuertoPorNombre(nombreAeropuerto);
                int idHangar = hangaresLogic.ObtenerIdHangarPorNombre(hangarName);
                int idTipoHangar = hangaresLogic.ObtenerIdTipoHangarPorNombre(nombreTipoHangar);

                if (string.IsNullOrWhiteSpace(txtNombreModificar.Text) ||
                    string.IsNullOrWhiteSpace(txtUbicacionModificar.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacidadModificar.Text) ||
                    string.IsNullOrWhiteSpace(txtEstadoModificar.Text) ||
                    string.IsNullOrWhiteSpace(txtTamañoModificar.Text) ||
                    string.IsNullOrWhiteSpace(txtAlturaModificar.Text))

                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor complete todos los campos.');", true);
                    return;
                }

                if (!esNumero(txtCapacidadModificar.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor ingrese un número válido en el campo de capacidad.');", true);
                    return;
                }

                hangaresLogic.MantenimientoHangares(2,idHangar,idTipoHangar,txtNombreModificar.Text, txtUbicacionModificar.Text, Int32.Parse(txtCapacidadModificar.Text), txtEstadoModificar.Text,decimal.Parse(txtTamañoModificar.Text), decimal.Parse(txtAlturaModificar.Text),idAeropuerto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('¡Éxito!', 'Hangar modificado correctamente', 'success');", true);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Surgió un problema al modificar el hangar.');", true);
            }
        }

        protected void BtmEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HiddenFieldHangarName.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Por favor seleccione una fila para eliminar.');", true);
                return;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "if(!confirm('¿Está seguro de que desea eliminar este hangar?')) return;", true);
            try
            {
                string hangarName = HiddenFieldHangarName.Value;
                HangaresLogic hangaresLogic = new HangaresLogic();
                int idHangar = hangaresLogic.ObtenerIdHangarPorNombre(hangarName);
                hangaresLogic.MantenimientoHangares(3, idHangar,0, "", "", 0, "",0,0, 0);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "Swal.fire('¡Éxito!', 'Hangar eliminado correctamente', 'success');", true);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Surgió un problema al eliminar el hangar.');", true);
            }
        }

        protected void CargarAeropuertosEnDropDown()
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                DataTable aeropuertos = hangaresLogic.ObtenerAeropuertos();

                ddlAereopuertoAgregar.Items.Clear();

                foreach (DataRow fila in aeropuertos.Rows)
                {
                    string nombreAeropuerto = fila["Nombre"].ToString();
                    ddlAereopuertoAgregar.Items.Add(nombreAeropuerto);
                    ddlAereopuertoModificar.Items.Add(nombreAeropuerto);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Surgio un problema al cargar los aereopuertos.');", true);
            }
        }

        protected void CargarTiposHangaresEnDropDown()
        {
            try
            {
                HangaresLogic hangaresLogic = new HangaresLogic();
                DataTable tiposHangares = hangaresLogic.ObtenerTiposHangares();

                ddlTiposHangaresAgregar.Items.Clear();

                foreach (DataRow fila in tiposHangares.Rows)
                {
                    string nombreTipoHangar = fila["Nombre"].ToString();
                    ddlTiposHangaresAgregar.Items.Add(nombreTipoHangar);
                    ddlTiposHangaresModificar.Items.Add(nombreTipoHangar);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Se produjo un problema al cargar los tipos de hangares.');", true);
            }
        }

        private bool esNumero(string valor)
        {
            return int.TryParse(valor, out _);
        }
    }
}
