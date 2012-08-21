using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Markets.Informations
{
    public partial class NewTopic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getName()
        {
            Response.Write(Request.Params["name"]);
        }
        public void getCid()
        {
            Response.Write(Request.Params["cid"]);
        }
    }
}