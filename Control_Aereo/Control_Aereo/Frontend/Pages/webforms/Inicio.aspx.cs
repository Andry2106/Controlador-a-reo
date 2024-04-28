using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Frontend.Pages.webforms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btmEmbarcamiento_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btmDespegue_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btmDesembarque_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}