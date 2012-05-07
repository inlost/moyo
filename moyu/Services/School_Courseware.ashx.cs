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
    /// 教案服务
    /// </summary>
    public class School_Courseware : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Courseware.School myCourseware = new Courseware.School();
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
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int sid = Convert.ToInt32(theContext.Session["sid"]);
            short grade=Convert .ToInt16( theContext.Request.Form["grade"]);
            short subject = Convert.ToInt16(theContext.Request.Form["subject"]);
            boolRst myRst = new boolRst();
            myRst.rst = myCourseware.add(uid, sid, title, body, grade, subject);
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