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
    /// Message 的摘要说明
    /// </summary>
    public class Message : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/plain";
            if (theContext.Request.Form["action"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }

            switch (context.Request.Form["action"].ToString())
            {
                case "newUserOnLine":
                    newUserOnLine();
                    break;
                case "newMessage":
                    newMessage();
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
        private void newMessage()
        {
            theContext.Response.Write("ok");
            for (var i = 0; i < ChatMessage.globalCache.userCache.Count; i++)
            {
                if (Convert.ToDateTime(((Hashtable)ChatMessage.globalCache.userCache[i])["lastUpdateTime"]).AddSeconds(15) < DateTime.Now)
                {
                    ChatMessage.globalCache.userCache.RemoveAt(i);
                }
            }
            string uid=theContext.Request.Form["uid"];
            string message=theContext.Request.Form["message"];
            SendToAll(uid,message,1);
        }
        private void newUserOnLine()
        {
            if (theContext.Session["id"] != null)
            {
                theContext.Session["guid"] = theContext.Session["id"];
                theContext.Response.Write(theContext.Session["id"]);
                return;
            }
            string uid = Guid.NewGuid().ToString();
            if (theContext.Session["niceName"] == null)
            {
                theContext.Session["niceName"] = "游客";
            }
            if (theContext.Session["isLogin"] == null)
            {
                theContext.Session["guid"] = uid;
                theContext.Session["isLogin"] = "false";
            }
            theContext.Response.Write(uid);
        }
        private void SendToAll(string source, string message,int messageType)
        {
            string sourceName = theContext.Session["niceName"].ToString();
            ArrayList _copy =(ArrayList) ChatMessage.globalCache.userCache.Clone();
            foreach (Hashtable user in _copy)
            {
                lock (user["asyn"])
                {
                    ((ChatMessage.MessageTask)user["asyn"])._lastMessage = message;
                    ((ChatMessage.MessageTask)user["asyn"])._lastSource = source;
                    ((ChatMessage.MessageTask)user["asyn"])._lastSourceName = sourceName;
                    ((ChatMessage.MessageTask)user["asyn"])._messageType= messageType;
                    ((ChatMessage.MessageTask)user["asyn"]).waitEvent.Set();//让对应的挂起的请求继续执行，即释放掉阻塞
                }
            }
        }
    }
}