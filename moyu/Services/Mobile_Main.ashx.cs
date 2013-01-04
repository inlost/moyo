using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;

namespace moyu.Services
{
    /// <summary>
    /// Mobile_Main 的摘要说明
    /// </summary>
    public class Mobile_Main : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private moyu.User.Functions myFunctions = new moyu.User.Functions();
        private Api.WeiXin myWeiXin = new Api.WeiXin();
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
            switch (context.Request.Params["action"].ToString())
            {
                case "signin":
                    userSignIn();
                    break;
                case "luckyMe":
                    luckyMe();
                    break;
                case "giftAdd":
                    giftAdd();
                    break;
                case "giveThanks":
                    giveThanks();
                    break;
                case "exchange":
                    exchange();
                    break;
                case "teachRobot":
                    teachRobot();
                    break;
                case "loadTeach":
                    loadTeach();
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
        public void userSignIn()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            myFunctions.signIn(uid);
            theContext.Response.Redirect("~/Mobile/signIn.aspx");
        }
        public void luckyMe()
        {
            int rst = myFunctions.luckyMe(Convert .ToInt32(theContext.Session["id"]));
            if (rst == 1) { theContext.Session["isLucky"] = "1"; }
            theContext.Response.Redirect("~/Mobile/lucky.aspx?rst="+rst);
        }
        public void giftAdd()
        {
            if (theContext.Session["isLucky"] == null || theContext.Session["isLucky"].ToString() != "1")
            {
                theContext.Response.Redirect("~/Mobile/lucky.aspx?rst=0");
            }
            int uid = Convert.ToInt32(theContext.Session["id"]);
            string gift = theContext.Request.Form["gift"];
            string message = theContext.Request.Form["message"] + "|||" + theContext.Request.Form["message2"];
            if (theContext.Request.Form["message"].ToString().Trim().Length == 0)
            {
                theContext.Response.Redirect("~/Mobile/lucky-gift.aspx");
            }
            myFunctions.giftAdd(uid, gift, message);
            theContext.Session.Remove("isLucky");
            theContext.Response.Redirect("~/Mobile/lucky.aspx");
        }
        public void giveThanks()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            string to = theContext.Request.Form["to"];
            myFunctions.giveThanksPoint(uid, to);
            theContext.Response.Redirect("~/Mobile/index.aspx");
        }
        private void exchange()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int point = Convert.ToInt32(theContext.Request.Form["jf"]);
            bool rst = myFunctions.exchange(uid, point);
            theContext.Response.Redirect("~/Mobile/index.aspx");
        }
        private void teachRobot()
        {
            ///TODO
            ///finish the ruleBody
            string q = theContext.Request.Form["q"];
            string a = theContext.Request.Form["a"];
            string url="http://www.ai0932.com/mobile/robot-teach.aspx?q=" + HttpUtility.UrlEncode( q) + "&show=true";
            int uid=Convert .ToInt32( theContext.Session["id"]);
            int rid = myWeiXin.addRule(uid, 1, a, a, myWeiXin.getPicUrl(false), myWeiXin.getPicUrl(true),
                url, 0);
            if (a.Length > 1)
            {
                theContext.Cache.Remove("weixinRobotKeywords");
                string[] keys = new string[1] { q.Replace("?", "").Replace("？", "") };
                myWeiXin.addKeyWord(keys, rid);
                theContext.Response.Redirect(url);
            }
            else
            {
                theContext.Response.Redirect("http://www.ai0932.com/mobile/robot-teach-list.aspx?type=hasAnswer");
            }
        }
        private void loadTeach()
        {
            Hashtable[] items;
            Robot.Main myRobot = new Robot.Main();
            int last = Convert.ToInt32(theContext.Request.Form["last"]);
            if (theContext.Request.Form["type"].ToString() == "noAnswer")
            {
                items = myRobot.questionsGet(last, 15);
            }
            else if (theContext.Request.Form["type"].ToString() == "answerByMe")
            {
                items = myRobot.getTeachListByUser(Convert.ToInt32(theContext.Session["id"]), last, 15);
            }
            else 
            {
                items = myRobot.getTeachList(last, 15);
            }
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable item in items)
            {
                if (theContext.Request.Form["type"].ToString() == "noAnswer")
                {
                    sb.Append("<li class=\"teachItems padding10 bg-color-blueLight\" data-id=\"" + item["id"] + "\">");
                    sb.Append("<h4><i class=\"icon-help\"></i> " + item["question"] + "</h4>");
                    sb.Append("<a href=\"robot-teach.aspx?q=" + HttpUtility.UrlEncode(item["question"].ToString()) + "\">点这里去调教</a>");
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
            theContext.Response.Write(sb);
        }
    }
}