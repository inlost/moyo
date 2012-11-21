using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu
{
    public partial class routes : System.Web.UI.Page
    {
        private string url = "", channel="";
        protected void Page_Load(object sender, EventArgs e)
        {
            channel = Page.RouteData.Values["Channel"].ToString();
            url = (Page.RouteData.Values.Count() == 2 ? Page.RouteData.Values["url"].ToString() : (Page.RouteData.Values["url"].ToString() + "~" + Page.RouteData.Values["parameter"].ToString()));
        }
        public void getContent()
        {
            string urlInclude="~/"+url.Replace("---","/").Replace("@",".").Replace("~","?");
            Server.Execute(urlInclude);
            //Response.Write(urlInclude);
        }
        public void deepFix()
        {
            if (Page.RouteData.Values.Count == 3)
            {
                Response.Write("../../");
            }
            else
            {
                Response.Write("../");
            }
        }
        public void getTitle()
        {
            Response.Write(channel);
        }
        public void getKeyWords()
        {
            Response.Write(channel.Replace("—", ",").Replace("_", ","));
        }
    }
}