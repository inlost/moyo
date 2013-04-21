using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=setting.aspx");
            }
        }
        public void getInfos(string what)
        {
            switch (what)
            { 
                case "userName":
                    Response.Write(Session["niceName"]);
                    break;
            }
        }
    }
}