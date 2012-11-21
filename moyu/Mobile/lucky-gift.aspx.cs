using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class lucky_gift : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLucky"] == null || Session["isLucky"].ToString() != "1")
            {
                Response.Redirect("~/Mobile/lucky.aspx?rst=0");
            }
        }
    }
}