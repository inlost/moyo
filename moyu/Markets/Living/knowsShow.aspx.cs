using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets
{
    public partial class knowsShow : System.Web.UI.Page
    {
        private moyu.Living.Question myQuestion = new moyu.Living.Question();
        private Hashtable theQuestion = new Hashtable();
        Functions myFunction = new Functions();
        protected void Page_Load(object sender, EventArgs e)
        {
            theQuestion = myQuestion.get(Convert .ToInt32(Request.Params["id"]));
        }
        public void getBody()
        {
            Response.Write("<p>" + theQuestion["body"].ToString().Replace("<br/>", "</p><p>") + "</p>");
        }
        public void getTitle()
        {
            Response.Write(theQuestion["title"]);
        }
        public void getTid()
        {
            Response.Write(theQuestion["id"]);
        }
        public void getWaitAnswer()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] knows;
            knows = myQuestion.noAnswerQuestionGet(16);
            foreach (Hashtable know in knows)
            {
                sb.Append("<li>");
                sb.Append("<a class=\"jump\" data-dst=\"Markets/Living/knowsShow.aspx?id=" + know["id"] + "\" href=\"/" + know["title"] + "_定西生活_沁辰左邻/Markets---Living---knowsShow@aspx/id=" + know["id"] + "\" >" + know["title"] + "<span>   " + myFunction.kindTime(Convert.ToDateTime(know["postDate"])) + "</span></a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        public void getLastAnswer()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] knows;
            knows = myQuestion.answeredQuestionGet(16);
            foreach (Hashtable know in knows)
            {
                sb.Append("<li>");
                sb.Append("<a class=\"jump\" data-dst=\"Markets/Living/knowsShow.aspx?id=" + know["id"] + "\" href=\"/" + know["title"] + "_定西生活_沁辰左邻/Markets---Living---knowsShow@aspx/id=" + know["id"] + "\" >" + know["title"] + "<span>   " + myFunction.kindTime(Convert.ToDateTime(know["postDate"])) + "</span></a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        public void anwerGet()
        {
            int nid = Convert.ToInt32(theQuestion["id"]);
            Hashtable[] answers;
            answers = myQuestion.answersGet(nid);
            StringBuilder sb = new StringBuilder();
            int i = 1;
            moyu.User.Web myUser = new moyu.User.Web();
            Hashtable theUser = new Hashtable();
            string uName = "";
            foreach (Hashtable answer in answers)
            {
                sb.Append("<div class=\"comment\">");
                sb.Append("<ul class=\"comment_info clearfix\">");
                sb.Append("<li><span class=\"comment_floor\">" + i + "</span> F</li>");
                if (Convert.ToInt32(answer["uid"]) == 0)
                {
                    uName = "匿名网友";
                }
                else
                {
                    theUser = myUser.get(Convert.ToInt32(answer["uid"]));
                    uName = theUser["niceName"].ToString();
                }
                sb.Append("<li>" + uName + "</li>");
                sb.Append("<li>回答于：<span class=\"topic_date\">" + answer["postDate"] + "</span></li>");
                sb.Append("</ul>");
                sb.Append("<div class=\"comment_body\">" + answer["body"] + "</div>");
                sb.Append("<img class=\"usersAvatar\" src=\"" + (uName == "匿名网友" ? "/Images/avatar.png" : theUser["avatar"].ToString().Replace("320_320", "64_64")) + "\"/>");
                sb.Append("</div>");
                i++;
            }
            Response.Write(sb);
        }
    }
}