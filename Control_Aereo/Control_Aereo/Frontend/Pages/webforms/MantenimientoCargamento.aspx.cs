using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frontend.Pages.webforms
{
    public partial class MantenimientoCargamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }

        }
        private void CargarDatos()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("ID Cargamento"),
                new DataColumn("Nombre"),
                new DataColumn("Peso")
            });

            Random random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                dt.Rows.Add($"Id {i}", $"Destino {random.Next(1, 5)}", $"{random.Next(1, 100)} Kg");
            }

            GvCargamento.DataSource = dt;
            GvCargamento.DataBind();

            mvVuelos.ActiveViewIndex = 0;
        }

    }
}