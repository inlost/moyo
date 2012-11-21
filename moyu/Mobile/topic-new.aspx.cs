using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class topic_new : System.Web.UI.Page
    {
        int cid;
        protected void Page_Load(object sender, EventArgs e)
        {
            cid = Convert.ToInt32(Request.Params["cid"]);
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=" + HttpUtility.UrlEncode("topic-new.aspx?cid="+cid));
            }
        }
        public void getCid()
        {
            Response.Write (cid);
        }
    }
}