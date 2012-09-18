using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Markets.forum
{
    public partial class GroupTopicShow : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        private Hashtable theGroup = new Hashtable();
        private Hashtable theTopic = new Hashtable();
        private Hashtable[] comments;
        protected void Page_Load(object sender, EventArgs e)
        {
            theTopic=myGroup.topicGetById( Convert .ToInt32 (Request .Params ["id"]));
            theGroup = myGroup.group_get_byId(Convert.ToInt32(theTopic["gid"]));
            comments = myGroup.commentGet(Convert.ToInt32(theTopic["id"]));
        }
        public void getName()
        {
            Response.Write(theGroup["name"]);
        }
        public void getGid()
        {
            Response.Write(theGroup["id"]);
        }
        public void getTid()
        {
            Response.Write(theTopic["id"]);
        }
        public void getGroupIcon()
        {
            Response.Write(theGroup["img"]);
        }
        public void getTopicTitle()
        {
            Random ran = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<div class=\"t_topicList_topic clearfix\">");
            sb.Append("<img src=\"" + theTopic["avatar"].ToString().Replace("320_320", "32_32") + "\" title=\"" + theTopic["niceName"] + "\" alt=\"" + theTopic["niceName"] + "\" class=\"left\">");
            if (theTopic["tag"].ToString().Length == 2)
            {
                sb.Append("<span class=\"left group_tag group_tag_" + ran.Next(0, 9) + "\">" + theTopic["tag"] + "</span>");
            }
            sb.Append("<h1 class=\"left\">"+ theTopic["title"] + "</h1>");
            sb.Append("<span class=\"right\">" + theTopic["showTime"] + "</span>");
            sb.Append("</div>");
            Response.Write(sb);
        }
        public void getBody()
        {
            Response.Write(theTopic["body"]);
        }
        public void commentGet()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string icon = "";
            int iFloor = 0;
            foreach (Hashtable comment in comments)
            {
                iFloor++;
                icon = comment["uid"].ToString() == "0" ? "/Images/avatar/avatar-64_64.jpg" : comment["avatar"].ToString().Replace("320_320","64_64");
                sb.Append("<li class=\"clearfix\">");
                sb.Append("<img src=\""+icon+"\" class=\"left\"/>");
                sb.Append("<div class=\"left commentMain\">");
                sb.Append("<div class=\"commentM_info clearfix\">");
                sb.Append("<span class=\"commentM_i_name left\">"+(comment["uid"].ToString()=="0"?"匿名网友":comment["niceName"].ToString())+"</span>");
                sb.Append("<span class=\"commentM_i_time left\">" + Convert .ToDateTime( comment["postDate"]).GetDateTimeFormats('f')[0] + "</span>");
                sb.Append("<span class=\"commentM_i_floor right\">#" + iFloor + "</span>");
                sb.Append("</div>");
                sb.Append("<div class=\"comment_body_holder\">" + comment["comment"] + "</div>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}