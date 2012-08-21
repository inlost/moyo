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
    /// User 的摘要说明
    /// </summary>
    public class User : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        moyu.User.Web myUser = new moyu.User.Web();
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
                case "login":
                    login();
                    break;
                case "reg":
                    reg();
                    break;
                case "isLogined":
                    isLogined();
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
            string uid = theContext.Request.Form["uid"];
            string password = theContext.Request.Form["password"];
            Hashtable theUser = new Hashtable();
            theUser = myUser.login(uid, password);
            if (theUser == null)
            {
                theContext.Response.Write(false);
            }
            else
            {
                theContext.Session["isLogin"] = "true";
                theContext.Session["guid"] = theUser["id"].ToString();
                foreach (DictionaryEntry infoPar in theUser)
                {
                    theContext.Session[infoPar.Key.ToString()] = infoPar.Value.ToString();
                }
                theContext.Session["password"] = null;
                theContext.Response.Write(true);
            }
        }
        private void reg()
        {
            string niceName = theContext.Request.Form["niceName"];
            string realName = theContext.Request.Form["realName"];
            bool sex =( theContext.Request.Form["sex"].ToString() == "boy" ? true : false);
            DateTime birthday = Convert.ToDateTime(theContext.Request.Form["Birth"]);
            string email = theContext.Request.Form["email"];
            Int64 phone = Convert.ToInt64(theContext.Request.Form["phone"]);
            string password = theContext.Request.Form["password"];
            Hashtable theUser = new Hashtable();
            theUser = myUser.reg(niceName, realName, sex, birthday, email, phone, password);
            if (theUser == null)
            {
                theContext.Response.Write(false);
            }
            else
            {
                theContext.Session["isLogin"] = "true";
                foreach (DictionaryEntry infoPar in theUser)
                {
                    theContext.Session[infoPar.Key.ToString()] = infoPar.Value.ToString();
                }
                theContext.Session["password"] = null;
                theContext.Response.Write(true);
            }
        }
        private void isLogined()
        {
            if (theContext.Session["isLogin"].ToString() == "true")
            {
                theContext.Response.Write(theContext.Session["niceName"]);
            }
            else
            {
                theContext.Response.Write("false");
            }
        }
    }
}