using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.Collections;
namespace moyu.Services
{
    /// <summary>
    /// living_knows 的摘要说明
    /// </summary>
    public class living_knows : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        moyu.Living.Question myQuestion = new Living.Question();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/plain";

            if (theContext.Request.Params["action"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            switch (context.Request.Form["action"].ToString())
            {
                case "new":
                    newQuestion();
                    break;
                case "answerAdd":
                    answerAdd();
                    break;
                case "ansertGet":
                    anwerGet();
                    break;
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void newQuestion()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            string title = theContext.Request.Form["title"];
            string body=theContext.Request.Form["body"].ToString().Replace("\n", "<br/>");
            theContext.Response.Write( myQuestion.add(uid, title, body));
        }
        public void answerAdd()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int nid = Convert.ToInt32(theContext.Request.Form["nid"]);
            string body = theContext.Request.Form["body"].ToString().Replace("\n", "<br/>");
            myQuestion.answerAdd(uid, body, nid);
        }
        public void anwerGet()
        {
            int nid = Convert.ToInt32(theContext.Request.Form["nid"]);
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
            theContext.Response.Write(sb);
        }
    }
}