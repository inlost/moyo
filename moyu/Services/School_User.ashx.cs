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
    /// 学校用户服务
    /// </summary>
    [DataContract]
    public class boolRst
    {
        [DataMember(Order = 0, IsRequired = true)]
        public bool rst { get; set; }
        [DataMember(Order = 1)]
        public string message { get; set; }
    }
    public class School_User : IHttpHandler, IRequiresSessionState 
    {
        private moyu.User.School myUser = new moyu.User.School();
        private HttpContext theContext;
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
                case "login":
                    login();
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

        private void login()
        {
            string id=theContext.Request.Form["id"];
            string key=theContext.Request.Form["key"];
            short permission=Convert .ToInt16(theContext.Request.Form ["permission"]) ;
            boolRst myRst = new boolRst();
            Hashtable[] theUser = myUser.login(id, key, permission);
            myRst.rst= theUser.Count()!=0 ? true : false;
            if (myRst.rst)
            {
                theContext.Session["isLogin"] = "true";
                foreach (DictionaryEntry infoPar in theUser[0])
                {
                    theContext.Session[infoPar.Key.ToString()] = infoPar.Value.ToString();
                }
                theContext.Session["password"] = null;
            }
            theContext.Response.Write(Data.Type.objToJson(myRst));
        }
    }
}