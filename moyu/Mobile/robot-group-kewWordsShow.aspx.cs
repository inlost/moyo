using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Mobile
{
    public partial class robot_group_kewWordsShow : System.Web.UI.Page
    {
        private string tag;
        private Information.group myGroup = new Information.group();
        Hashtable[] posts;
        protected void Page_Load(object sender, EventArgs e)
        {
            tag = HttpUtility.UrlDecode (Request.Params["tag"].ToString());
            posts = myGroup.postGetByTat(tag,0,10);
        }
        public void getTitle()
        {
            Response.Write(tag);
        }
        public void getTime()
        {
            Response.Write(DateTime.Now);
        }
        public void getContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable p in posts)
            {
                sb.Append("<li data-role=\"collapsible\" data-collapsed=\"false\" data-content-theme=\"c\" data-iconpos=\"right\"  data-mini=\"true\">");
                sb.Append("<h3 style=\"text-indent:0;\">");
                sb.Append((p["niceName"].ToString()==""?"匿名用户":p["niceName"].ToString()));
                sb.Append("</h3>");
                sb.Append(p["body"].ToString().Replace("src=\"upload/images", "src=\"http://www.ai0932.com/upload/images"));
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}