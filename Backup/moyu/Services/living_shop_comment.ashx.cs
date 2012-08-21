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
    /// living_shop_comment 的摘要说明
    /// </summary>
    public class living_shop_comment : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Living.shops myShop = new Living.shops();
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
                case "add":
                    add();
                    break;
                case "get":
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
        private void add()
        { 
            int uid;
            try
            {
                uid = Convert.ToInt32(theContext.Session["id"]);
            }
            catch
            {
                uid = 0;
            }
            int sid = Convert.ToInt32(theContext.Request.Form["sid"]);
            int point = Convert .ToInt32( Convert.ToDouble(theContext.Request.Form["point"]).ToString("f0"));
            int qulity = Convert.ToInt32(Convert.ToDouble(theContext.Request.Form["price"]).ToString("f0"));
            int service = Convert.ToInt32(Convert.ToDouble(theContext.Request.Form["service"]).ToString("f0"));
            int circumstance = Convert.ToInt32(Convert.ToDouble(theContext.Request.Form["circumstance"]).ToString("f0"));
            string comment = theContext.Request.Form["comment"];
            myShop.comment_add(uid, sid, point, qulity, service, circumstance, comment);
        }
    }
}