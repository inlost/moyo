using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace moyu.Services
{
    /// <summary>
    /// 订单服务页
    /// </summary>
    public class Sale_Orders : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Sale.Orders myOrder = new Sale.Orders();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            context.Response.ContentType = "text/plain";
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
            string title = theContext.Request.Form["title"];
            int gid=Convert .ToInt32( theContext.Request.Form["gid"]);
            string name = theContext.Request.Form["name"];
            string phone = theContext.Request.Form["phone"];
            string address = theContext.Request.Form["address"];
            Int64 eid =Convert.ToInt64( theContext.Request.Form["eid"]);
            if (myOrder.add(title, gid, name, phone, address, eid))
            {
                theContext.Response.Write("ok");
            }
            else
            {
                theContext.Response.Write("error");
            }
        }
    }
}