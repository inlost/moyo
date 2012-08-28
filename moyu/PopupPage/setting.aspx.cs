using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.PopupPage
{
    public partial class setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getInfos(string w)
        {
            switch (w)
            { 
                case "title":
                    Response.Write(Session["niceName"] + "的设置");
                    break;
                case "name":
                    Response.Write(Session["niceName"]);
                    break;
                case "avatar":
                    Response.Write(Session["avatar"].ToString().Replace("320_320","32_32"));
                    break;
                case "avatar-big":
                    Response.Write(Session["avatar"].ToString());
                    break;
            }
        }
    }
}