using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Frontend.Logic;


namespace Frontend.Pages.webforms
{
    public partial class Desembarque : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerHangares();
                ObtenerHangaresConAviones();
                ObtenerHangaresSinAviones();
                CargarPuertasActivas();
                CargarEmbarque();
                CargarTodosAvionesExistentes();
            }
        }
        protected void gvEmbarque_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Verifica si es una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el control LinkButton en la fila
                LinkButton lnkEmbarque = (LinkButton)e.Row.FindControl("lnkEmbarque");

                // Verifica si se encontró el control
                if (lnkEmbarque != null)
                {
                    // Asigna el evento OnClientClick al LinkButton
                    lnkEmbarque.OnClientClick = "javascript:seleccionarEmbarque('" + lnkEmbarque.Text + "'); return false;";
                }
            }
        }





        protected void gvAviones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtiene el LinkButton dentro de la celda de número de registro
                LinkButton lnkNumeroRegistro = e.Row.FindControl("lnkNumeroRegistro") as LinkButton;

                // Adjunta un script JavaScript para manejar la selección del número de registro
                lnkNumeroRegistro.Attributes["onclick"] = "seleccionarNumeroRegistro('" + lnkNumeroRegistro.Text + "'); return false;";
            }
        }

        protected void CargarTodosAvionesExistentes()
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                DataTable aviones = desembarqueLogic.CargarTodosLosAviones();

                gvAviones.DataSource = aviones;
                gvAviones.DataBind();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error al cargar todos los aviones: " + ex.Message);
            }
        }


        protected void SeleccionarHangar(object sender, EventArgs e)
        {

        }
        protected void Desembarcar_Click(object sender, EventArgs e)
        {
            // Código para procesar el desembarque, si es necesario
        }

        protected void ObtenerHangares()
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                DataTable hangaresDisponibles = desembarqueLogic.ObtenerHangares();

                foreach (DataRow row in hangaresDisponibles.Rows)
                {
                    HtmlGenericControl card = new HtmlGenericControl("div");
                    card.Attributes["class"] = "mini-card";
                    card.Attributes["id"] = "hangarCard_" + row["IdHangar"].ToString();

                    // Añadir título (nombre del hangar)
                    HtmlGenericControl title = new HtmlGenericControl("h2");
                    title.InnerText = row["Nombre"].ToString();
                    card.Controls.Add(title);

                    // Ocultar toda la información excepto el nombre del hangar
                    HtmlGenericControl infoContainer = new HtmlGenericControl("div");
                    infoContainer.Style["display"] = "none";
                    card.Controls.Add(infoContainer);

                    // Añadir información oculta
                    string[] infoLabels = { "IdTipoHangar", "IdHangar", "Ubicacion", "Capacidad", "Estado", "Tamaño", "Altura" };
                    foreach (string label in infoLabels)
                    {
                        HtmlGenericControl infoItem = new HtmlGenericControl("p");
                        infoItem.InnerText = $"{label}: {row[label].ToString()}";
                        infoContainer.Controls.Add(infoItem);
                    }

                    // Evento para mostrar información al pasar el mouse sobre la mini-card
                    card.Attributes["onmouseover"] = $"javascript:this.children[1].style.display='block';";
                    card.Attributes["onmouseleave"] = $"javascript:this.children[1].style.display='none';";

                    miniCardsContainer.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar los hangares: " + ex.Message);
            }
        }





        private string SerializeHangarData(DataRow row)
        {
            // Serializa todos los datos del hangar en una cadena JSON
            string idTipoHangar = row["IdTipoHangar"].ToString();
            string idHangar = row["IdHangar"].ToString();
            string nombre = row["Nombre"].ToString();
            string ubicacion = row["Ubicacion"].ToString();
            string capacidad = row["Capacidad"].ToString();
            string estado = row["Estado"].ToString();
            string tamaño = row["Tamaño"].ToString();
            string altura = row["altura"].ToString();

            // Construye una cadena JSON
            string serializedData = $"{{ \"IdTipoHangar\": \"{idTipoHangar}\", \"IdHangar\": \"{idHangar}\", \"Nombre\": \"{nombre}\", \"Ubicacion\": \"{ubicacion}\", \"Capacidad\": \"{capacidad}\", \"Estado\": \"{estado}\", \"Tamaño\": \"{tamaño}\", \"Altura\": \"{altura}\" }}";

            return serializedData;
        }
        protected void ObtenerHangaresConAviones()
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                DataTable hangaresConAviones = desembarqueLogic.CargarHangaresConAviones();

                foreach (DataRow row in hangaresConAviones.Rows)
                {
                    // Agrega un botón para cada hangar ocupado
                    Button btnHangar = new Button();
                    btnHangar.ID = "btnHangar_" + row["IdHangar"].ToString(); // Asigna un ID único al botón
                    btnHangar.Text = row["Nombre"].ToString();
                    btnHangar.CssClass = "hangar-button";
                    btnHangar.CssClass = "info-btn"; // Aplica la clase de estilo CSS
                    btnHangar.Attributes["data-idhangar"] = row["IdHangar"].ToString(); // Atributo personalizado con el ID del hangar
                    btnHangar.UseSubmitBehavior = false; // Evita que el botón envíe el formulario
                    btnHangar.Click += new EventHandler(SeleccionarHangar); // Asigna el manejador de eventos al botón
                    btnHangar.OnClientClick = $"javascript:MostrarIdHangar('{row["Nombre"].ToString()}'); return false;"; // Llama a la función en JavaScript al hacer clic

                    // Agregar el botón al contenedor de hangares ocupados
                    hangarListContainer.Controls.Add(btnHangar);

                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción aquí
                Console.WriteLine("Error al cargar los hangares con aviones: " + ex.Message);
            }
        } 

        protected void ObtenerHangaresSinAviones()
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                DataTable hangaresSinAviones = desembarqueLogic.CargarHangaresSinAviones();

                foreach (DataRow row in hangaresSinAviones.Rows)
                {
                    // Agrega un botón para cada hangar sin aviones
                    Button btnHangar = new Button();
                    btnHangar.ID = "btnHangar_" + row["IdHangar"].ToString(); // Asigna un ID único al botón
                    btnHangar.Text = row["Nombre"].ToString();
                    btnHangar.CssClass = "hangar-button";
                    btnHangar.CssClass = "info-btn";
                    btnHangar.Attributes["data-idhangar"] = row["IdHangar"].ToString(); // Atributo personalizado con el ID del hangar
                    btnHangar.UseSubmitBehavior = false; // Evita que el botón envíe el formulario
                    btnHangar.Click += new EventHandler(SeleccionarHangar); // Asigna el manejador de eventos al botón
                    btnHangar.OnClientClick = $"javascript:MostrarIdHangar('{row["Nombre"].ToString()}'); return false;"; // Llama a la función en JavaScript al hacer clic

                    // Agregar el botón al contenedor de hangares vacíos
                    hangarListVacioContainer.Controls.Add(btnHangar);

                    // Llama al método para cargar la información del avión asociado a este hangar

                } 
            }
            catch (Exception ex)
            {
                // Maneja la excepción aquí
                Console.WriteLine("Error al cargar los hangares sin aviones: " + ex.Message);
            }
        } 

        protected void btnAgregarAvion_Click(object sender, EventArgs e)
        {
            string nombreHangar = txtNombreHangar.Text;
            string numeroDeRegistroAvion = txtNumeroRegistroAvion.Text;

            AgregarAvionEnHangar(nombreHangar, numeroDeRegistroAvion);
        }

        protected void btnEliminarAvion_Click(object sender, EventArgs e)
        {
            string nombreHangar = txtNombreHangar.Text;
            string numeroDeRegistroAvion = txtNumeroRegistroAvion.Text;

            EliminarAvionDeHangar(nombreHangar, numeroDeRegistroAvion);
        }

        protected void AgregarAvionEnHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                desembarqueLogic.AgregarAvionEnHangar(nombreHangar, numeroDeRegistroAvion);

                // Registro del script SweetAlert
                string script = @"<script>
                            mostrarSweetAlert('success', '¡Éxito!', 'Avión enviado a Hangar sin problemas.');
                         </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar avión en el hangar: " + ex.Message);
                // Registro del script SweetAlert para mostrar mensaje de error
                string errorScript = @"<script>
                                  mostrarSweetAlert('error', '¡Error!', 'Error al agregar avión en el hangar.');
                              </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlertError", errorScript);
            }
        }

        protected void EliminarAvionDeHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                desembarqueLogic.EliminarAvionDeHangar(nombreHangar, numeroDeRegistroAvion);

                // Registro del script SweetAlert
                string script = @"<script>
                            mostrarSweetAlert('success', '¡Éxito!', 'Avión sacado de Hangar sin problemas.');
                         </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar avión del hangar: " + ex.Message);
                // Registro del script SweetAlert para mostrar mensaje de error
                string errorScript = @"<script>
                                  mostrarSweetAlert('error', '¡Error!', 'Error al eliminar avión del hangar.');
                              </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlertError", errorScript);
            }
        }

        private DataTable modelosAviones;



        protected void btnBuscarAviones_Click(object sender, EventArgs e)
        {
            string nombreHangar = TextBox1.Text;


            BuscarModelosAviones(nombreHangar);


            GridView1.DataSource = modelosAviones;
            GridView1.DataBind();
        }

        protected void BuscarModelosAviones(string nombreHangar)
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();

                modelosAviones = desembarqueLogic.BuscarModelosAvionesPorHangar(nombreHangar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar avión del hangar: " + ex.Message);
            }
        }
        protected void DesembarcarPasajero(int idEmbarque, int idPuerta)
        {

            DesembarqueLogic desembarqueLogic = new DesembarqueLogic();


            string resultado = desembarqueLogic.DesembarcarPasajero(idEmbarque, idPuerta);
         
            CargarEmbarque();

        }


        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox txtDatosEmbarque
            int idEmbarque;
            if (!int.TryParse(txtDatosEmbarque.Text, out idEmbarque))
            {
                // Manejar caso de error de conversión

                return;
            }

            // Obtener el valor seleccionado del DropDownList ddlPuertas
            int idPuerta;
            if (!int.TryParse(ddlPuertas.SelectedValue, out idPuerta))
            {
                // Manejar caso de error de conversión
               
                return;
            }

            // Llamar al método DesembarcarPasajero con los valores obtenidos
            DesembarcarPasajero(idEmbarque, idPuerta);
            CargarEmbarque();
        }








        //nuevo
        protected void CargarEmbarque()
        {
            try
            {
                DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
                DataTable embarque = desembarqueLogic.CargarEmbarque();

                gvEmbarque.DataSource = embarque; // Asigna el DataTable al GridView
                gvEmbarque.DataBind(); // Enlaza los datos al GridView
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el embarque: " + ex.Message);
            }
        }


        private void CargarPuertasActivas()
        {
            DesembarqueLogic desembarqueLogic = new DesembarqueLogic();
            DataTable puertasActivas = desembarqueLogic.ObtenerPuertasActivas();

            ddlPuertas.DataSource = puertasActivas;
            ddlPuertas.DataTextField = "Nombre"; // Nombre de la columna que muestra el nombre de la puerta
            ddlPuertas.DataValueField = "Id"; // Nombre de la columna que contiene el ID de la puerta
            ddlPuertas.DataBind();
        }
    }
}


