using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.Informations
{
    public partial class index : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getGroupList()
        {
            Hashtable[] groups;
            StringBuilder sb = new StringBuilder();
            groups = myGroup.group_get(12);
            foreach (Hashtable group in groups)
            {
                sb.Append("<li class=\"ifMk-g-groups left clearfix\">");
                sb.Append("<img class=\"left\" alt=\"" + group["name"] + "\" src=\"" + group["img"] + "\"/>");
                sb.Append("<div class=\"ifMk-g-n-detal left\">");
                sb.Append("<h3><a class=\"jump\" data-dst=\"Markets/forum/GroupTopicList.aspx?id=" + group["id"] + "\" href=\"/" + group["name"] + "_定西吧_沁辰左邻/Markets---forum---GroupTopicList@aspx/id=" + group["id"] + "\">" + group["name"] + "</a></h3>");
                sb.Append("<div>"+group["introduce"]+"</div>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}