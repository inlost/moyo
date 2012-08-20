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
    /// other 的摘要说明
    /// </summary>
    public class other : IHttpHandler, IRequiresSessionState
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
                case "weather":
                    weather();
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
        public void weather()
        {
            Http myHttp = new Http();
            string weather=myHttp.HttpGet("http://m.weather.com.cn/data/101160201.html", "");
            theContext.Response.Write(weather);
        }
    }
}