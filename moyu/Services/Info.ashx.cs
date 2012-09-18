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
    /// Info 的摘要说明
    /// </summary>
    public class Info : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private moyu.Infos.Post myPost = new Infos.Post();
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
                case "new":
                    newPost();
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
        private void newPost()
        {
            int cid = Convert.ToInt32(theContext.Request.Form["cid"]);
            string title = theContext.Request.Form["title"];
            string name = theContext.Request.Form["name"];
            double price = Convert.ToDouble(theContext.Request.Form["price"]);
            string body = theContext.Request.Form["body"];
            Int64 phone = Convert.ToInt64(theContext.Request.Form["phone"]);
            string pass = theContext.Request.Form["pass"];
            if (name.Length == 0) { name = "匿名"; }
            myPost.add(cid, title, name, price, body, phone, pass, 0);
        }
    }
}