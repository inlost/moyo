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
    public partial class robot_group_topUser : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getTitle()
        {
            Response.Write(DateTime.Now.ToShortDateString()+"左邻排行");
        }
        public void getContent()
        {
            Hashtable[] list;
            list = myGroup.pointListGet(10, -15);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < list.Length; i++)
            {
                sb.Append("<li class=\"postItem\">");
                sb.Append("<h2 class=\"group-post-info clear\" style=\"text-indent:0;\">");
                sb.Append("<span class=\"left group-post-info-tag group_tag_"+i+"\">"+(i+1)+(i<5?"  管理":"")+"</span>");
                sb.Append("<span class=\"left group-post-info-user\">"+list[i]["niceName"]+"</span>");
                sb.Append("</h2></li>");
            }
            Response.Write(sb);
        }
        public void getNotRead()
        {
            int count = 0;
            try
            {
                count = myGroup.getNotReadAtCount(Convert.ToInt32(Session["id"]));
            }
            catch
            {
                count = 0;
            }
            if (count != 0)
            {
                Response.Write("<span class=\"notReadCount\">" + count + "</span>");
            }
        }
    }
}