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
    public partial class GroupTopicList : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        private Hashtable theGroup = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] == null)
            {
                return;
            }
            theGroup = myGroup.group_get_byId(Convert .ToInt32( Request .Params["id"]));
        }
        public void getName()
        {
            Response.Write(theGroup["name"]);
        }
        public void getGid()
        {
            Response.Write(theGroup["id"]);
        }
        public void getSideBar_firstFun()
        {
            StringBuilder sb = new StringBuilder();
            if (Session["isLogin"] == null || Session["isLogin"].ToString() != "true")
            {
                sb.Append("<span class=\"left\">喜欢这个小组么？</span> <a class=\"right groupJoin needLogin\" id=\"t_f_b_joinGroup\" href=\"javascript:void(0);\" data-isAllow=\"" + theGroup["joinType"] + "\" data-gid=\""+theGroup["id"]+"\">加入小组</a>");
            }
            else
            {
                if (myGroup.isInGroup(Convert.ToInt32(Session["id"]), Convert.ToInt32(theGroup["id"])))
                {
                    sb.Append("<span class=\"left\">有啥想说的？</span> <a class=\"right jump\" id=\"t_f_b_newTopic\" data-dst=\"Markets/forum/NewTopic.aspx?action=group&id=" + theGroup["id"] + "\" href=\"javascript:void(0);\">发表新帖子</a>");
                }
                else
                {
                    sb.Append("<span class=\"left\">喜欢这个小组么？</span> <a class=\"right groupJoin\" id=\"t_f_b_joinGroup\" href=\"javascript:void(0);\" data-isAllow=\""+theGroup["joinType"]+"\" data-gid=\""+theGroup["id"]+"\">加入小组</a>");
                }
            }
            Response.Write(sb);
        }
    }
}