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
    /// Mall 的摘要说明
    /// </summary>
    public class Mall : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private moyu.Mall.apply myApply =new moyu.Mall.apply();
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
                case "addApply":
                    addApply();
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
        private void addApply()
        {
            string what = theContext.Request.Form["what"];
            string shopName = theContext.Request.Form["shopName"];
            string bossName = theContext.Request.Form["bossName"];
            Int64 tel=0;
            try
            {
                tel= Convert.ToInt64(theContext.Request.Form["tel"].ToString());
            }
            catch
            {
                theContext.Response.Redirect("~/mobile/mall-newShop.aspx?msg=-3");
            }
            if (string.IsNullOrEmpty(what) || string.IsNullOrEmpty(shopName) || string.IsNullOrEmpty(bossName))
            {
                theContext.Response.Redirect("~/mobile/mall-newShop.aspx?msg=-1");
            }
            myApply.add(what, shopName, bossName, tel);
            theContext.Response.Redirect("~/mobile/mall-newShop.aspx?msg=1");
        }
    }
}