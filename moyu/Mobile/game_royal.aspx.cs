using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class game_royal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=game_royal.aspx");
            }
        }
        public void getInfo(string w)
        {
            switch (w) 
            { 
                case "name":
                    Response.Write(Session["niceName"]);
                    break;
                case "avatar":
                    Response .Write (Session["avatar"].ToString().Replace('\\','/'));
                    break;
            }
        }
    }
}