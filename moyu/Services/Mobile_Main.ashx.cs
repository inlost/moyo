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
            myFunctions.giftAdd(uid, gift, message);
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
            int rid = myWeiXin.addRule(0, 1, a, a, myWeiXin.getPicUrl(false), myWeiXin.getPicUrl(true),
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
                theContext.Response.Redirect("http://www.ai0932.com/mobile/robot-teach.aspx?q=" + HttpUtility.UrlEncode( q));
            }
        }
    }
}