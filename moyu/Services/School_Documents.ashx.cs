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
    /// 学校文档服务
    /// </summary>
    public class School_Documents : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Documents.School myDocuments = new Documents.School();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/javascript";
            if (theContext.Request.Form["action"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            switch (context.Request.Form["action"].ToString())
            {
                case "addNew":
                    addNew();
                    break;
            }
            context.Response.End();
        }

        private void addNew()
        {
            string title = theContext.Request.Form["title"];
            string body = theContext.Request.Form["body"];
            int uid=Convert .ToInt32( theContext.Session["id"]);
            bool status = Convert.ToBoolean(theContext.Request.Form["status"]);
            string reveivers = theContext.Request.Form["receivers"];
            boolRst myRst = new boolRst();
            myRst.message = myDocuments.Add(title, body, uid, status, reveivers).ToString();
            myRst.rst = myRst.message != "0" ? true : false;
            theContext.Response.Write(Data.Type.objToJson(myRst));
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