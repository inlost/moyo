using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class placeSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getName()
        {
            Response .Write (Request .Params["name"]);
        }
        public void getAddress()
        {
            Response.Write(Request.Params["address"]);
        }
        public void getTel()
        {
            if (Request.Params["tel"] != null)
            {
                Response.Write("<a href=\"tel:" + Request.Params["tel"] + "\">" + Request.Params["tel"] + "</a>");
            }
        }
        public void getMapUrl()
        {
            Response.Write("http://api.map.baidu.com/staticimage?width=340&center=" + Request.Params["lng"] + "," + Request.Params["lat"] + "&zoom=17&markers=" + Request.Params["lng"] + "," + Request.Params["lat"]);
        }
    }
}