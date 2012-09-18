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
    public partial class TopicShow : System.Web.UI.Page
    {
        Hashtable thePost = new Hashtable();
        Information.topic myTopic = new Information.topic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null)
            {
                int tid=Convert.ToInt32(Request .Params ["id"]);
                thePost = myTopic.get(tid);
                myTopic.addShowTime(tid);
            }
        }
        public void getTopicCount(int cid)
        {
            Information.topic myTopic = new Information.topic();
            Response.Write(myTopic.getChannelTopicCount(cid));
        }
        public void getTid()
        {
            Response.Write(thePost["id"]);
        }
        public void getName()
        {
            Response.Write(thePost["topic_title"]);
        }
        public void getContent()
        {
            Response.Write(thePost["topic_body"]);
        }
        public void getInfo()
        {
            string uName="";
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            if (Convert.ToInt32(thePost["uid"]) == 0)
            {
                uName="匿名网友";
            }
            else
            {
                theUser = myUser.get(Convert.ToInt32(thePost["uid"]));
                uName=theUser["niceName"].ToString();
            }
            Response.Write("<li>" + uName + "</li>");
            Response.Write("<li>发表于：<span  class=\"topic_date\">" + thePost["topic_date"] + "</span></li>");
            Response.Write("<li>帖子热度：" + thePost["showTime"] + "</li>");
            Response.Write("<li>最后回应时间：<span class=\"topic_date\">" + thePost["lastUpdate"] + "</span></li>");
        }
        public void commentsGet()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] comments;
            comments = myTopic.commentsGet( Convert.ToInt32(thePost["id"]));
            int i = 1;
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            string uName = "";
            foreach (Hashtable comment in comments)
            {
                sb.Append("<div class=\"comment\">");
                sb.Append("<ul class=\"comment_info clearfix\">");
                sb.Append("<li><span class=\"comment_floor\">"+i+"</span> F</li>");
                if (Convert.ToInt32(comment["uid"]) == 0)
                {
                    uName = "匿名网友";
                }
                else
                {
                    theUser = myUser.get(Convert.ToInt32(comment["uid"]));
                    uName = theUser["niceName"].ToString();
                }
                sb.Append("<li>"+uName+"</li>");
                sb.Append("<li>发表于：<span class=\"topic_date\">" + comment["date"] + "</span></li>");
                sb.Append("</ul>");
                sb.Append("<div class=\"comment_body\">" + comment["body"] + "</div>");
                sb.Append("<img class=\"usersAvatar\" src=\"" + (uName == "匿名网友" ? "/Images/avatar.png" : theUser["avatar"].ToString().Replace("320_320", "64_64")) + "\"/>");
                sb.Append("</div>");
                i++;
            }
            Response.Write(sb);
        }
        public void isNeedLogin()
        {
            if (Session["isLogin"].ToString() == "false")
            {
                Response.Write("   <a href=\"javascript:void(0);\" class=\"needLogin\">[我不是匿名网友]</a>");
            }
            else
            {
                Response.Write("   <a href=\"javascript:void(0);\">[" + Session["niceName"] + "]</a>");
            }
        }
    }
}