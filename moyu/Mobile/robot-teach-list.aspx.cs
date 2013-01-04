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
    public partial class robot_teach_list : System.Web.UI.Page
    {
        private string title="";
        private Robot.Main myRobot = new Robot.Main();
        Hashtable[] items;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=" + HttpUtility.UrlEncode("robot-teach-list.aspx?type=hasAnswer"));
            }
            if (Request.Params["type"].ToString() == "hasAnswer")
            {
                items = myRobot.getTeachList(0, 15);
                title = "最新调教";
            }
            else if (Request.Params["type"].ToString() == "noAnswer")
            {
                items = myRobot.questionsGet(0, 15);
                title = "等我调教";
            }
            else if (Request.Params["type"].ToString() == "answerByMe")
            {
                items = myRobot.getTeachListByUser(Convert .ToInt32(Session["id"]),0, 15);
                title = "我的调教";
            }
            else
            {
                title = "调教";
            }
        }
        public void getTitle()
        {
            Response.Write(title);
        }
        public void getType()
        {
            Response.Write(Request.Params["type"]);
        }
        public void getTabClass(string btn)
        {
            if (Request.Params["type"].ToString() == "hasAnswer" && btn == "hasAnswer")
            {
                Response.Write("active");
            }
            else if (Request.Params["type"].ToString() == "noAnswer" && btn == "noAnswer")
            {
                Response.Write("active");
            }
            else if (Request.Params["type"].ToString() == "answerByMe" && btn == "answerByMe")
            {
                Response.Write("active");
            }
            else
            {
                Response.Write("normal");
            }
        }
        public void getContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable item in items)
            {
                if (title == "等我调教")
                {
                    sb.Append("<li class=\"teachItems padding10 bg-color-blueLight\" data-id=\"" + item["id"] + "\">");
                    sb.Append("<h4><i class=\"icon-help\"></i> " + item["question"] + "</h4>");
                    sb.Append("<a href=\"robot-teach.aspx?q=" + HttpUtility.UrlEncode( item["question"].ToString() )+ "\">点这里去调教</a>");
                    sb.Append("</li>");
                }
                else
                {
                    sb.Append("<li class=\"teachItems padding10 bg-color-blueLight\" data-id=\"" + item["id"] + "\">");
                    sb.Append("<h4><i class=\"icon-help\"></i> " + item["keyWord"] + "</h4>");
                    sb.Append("<div class=\"padding10 bg-color-light-yellow fg-color-darken\">");
                    sb.Append(item["body"] + "  ——By:<span class=\"fg-color-red\">" + (item["niceName"].ToString() == "" ? "匿名用户" : item["niceName"].ToString()));
                    sb.Append("</span></div>");
                    sb.Append("</li>");
                }
            }
            Response.Write(sb);
        }
    }
}