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
    /// Ecard 的摘要说明
    /// </summary>
    public class Ecard : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private moyu.Ecard.Union myUnion = new moyu.Ecard.Union();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            context.Response.ContentType = "text/plain";
            switch (context.Request.Params["action"])
            { 
                case "getCoupons":
                    getCoupons();
                    break;
            }
            context.Response.End();
        }

        public void getCoupons()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int cid = Convert.ToInt32(theContext.Request.Params["id"]);
            int rst=myUnion.getCoupons(uid, cid);
            theContext.Response.Redirect("~/Mobile/coupons-show.aspx?id="+cid+"&msg="+rst);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}