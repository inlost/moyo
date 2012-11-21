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
    /// Union 的摘要说明
    /// </summary>
    public class Union : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        moyu.Ecard.Union myUnion = new moyu.Ecard.Union();
        moyu.Ecard.Living myLiving = new moyu.Ecard.Living();
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
                case "webLogin":
                    webLogin();
                    break;
                case "new":
                    newUser();
                    break;
                case "consumption":
                    consumption();
                    break;
                case "userGet":
                    user_get();
                    break;
                case "userExchangeAdd":
                    user_exchange_add();
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
            int sid = Convert.ToInt32(theContext.Request.Form["sid"]);
            string password = theContext.Request.Form["password"];
            theContext.Response.Write(myUnion.shopLogin(sid,password));
        }
        private void webLogin()
        {
            int sid = Convert.ToInt32(theContext.Request.Form["sid"]);
            string password = theContext.Request.Form["password"];
            if (myUnion.shopLogin(sid, password))
            {
                theContext.Session["sid"] = sid;
                theContext.Session["sLogin"] = "true";
                theContext.Session["password"] = password;
            }
            theContext.Response.Redirect("~/Sa/default.aspx");
        }
        private void newUser()
        {
            int sid = Convert.ToInt32(theContext.Request.Form["sid"]);
            Int64 cid = Convert.ToInt64(theContext.Request.Form["cid"]);
            string password = cid.ToString().Substring(1, 6);
            string realName = theContext.Request.Form["realName"];
            bool sex = Convert.ToBoolean(theContext.Request.Form["sex"]);
            DateTime birthday = Convert.ToDateTime(theContext.Request.Form["birthday"]);
            string phone = theContext.Request.Form["phone"];
            string address = theContext.Request.Form["address"];
            theContext.Response.Write ( myUnion.newUser(sid,cid,password,realName,sex,birthday,phone,address));
        }
        private void consumption()
        {
            int uid = Convert.ToInt32(theContext.Request.Form["uid"]);
            int sid = Convert.ToInt32(theContext.Request.Form["sid"]);
            double price = Convert.ToDouble(theContext.Request.Form["price"]);
            theContext.Response.Write(myUnion.consumption_log(uid,sid,price));
        }
        private void user_get()
        { 
            Int64 cid=Convert .ToInt64(theContext.Request.Form["cid"]);
            int sid=Convert .ToInt32(theContext.Request.Form["sid"]);
            Hashtable theUser = myUnion.user_get(cid, sid);
            StringBuilder sb = new StringBuilder();
            sb.Append(theUser["address"]+"|");
            sb.Append(theUser["birtyday"] + "|");
            sb.Append(theUser["time"] + "|");
            sb.Append(theUser["tootle"]+"|" );
            sb.Append(theUser["id"] );
            theContext.Response.Write(sb);
        }
        private void user_exchange_add()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int gid = Convert.ToInt32(theContext.Request.Form["gid"]);
            string ip = GetRealIP();
            theContext.Response.Write(myLiving.userExchangeAdd(uid,gid,ip));
        }
        private static string GetRealIP()
        {
            string ip;
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return ip;
        }
    }
}