using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Mobile
{
    public partial class robot_show : System.Web.UI.Page
    {
        private string keyWords = "";
        private moyu.Information.Robot myRobot = new Information.Robot();
        private Hashtable thePost = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            keyWords = Request.Params["keyWords"];
            thePost = myRobot.getItem(keyWords);
        }
        public void getContent()
        {
            Response.Write(thePost["body"].ToString().Replace("src=\"upload/images", "src=\"http://www.ai0932.com/upload/images"));
        }
        public void getTitle()
        {
            Response.Write(thePost["title"]);
        }
        public void getTime()
        {
            Response.Write(thePost["postDate"]);
        }
        public void getUrl()
        {
            Response.Write(HttpContext.Current.Request.Url);
        }
    }
}