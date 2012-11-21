using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Mobile
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null)
            {
                moyu.User.Web myUser = new moyu.User.Web();
                HttpCookie theCookie = Request.Cookies["login"];
                int uid = Convert.ToInt32(theCookie.Values["uid"]);
                Hashtable theUser = new Hashtable();
                theUser = myUser.get(uid);
                foreach (DictionaryEntry infoPar in theUser)
                {
                    Session[infoPar.Key.ToString()] = infoPar.Value.ToString();
                }
                Session["password"] = null;
            }
        }
        public void getLoginMsg()
        {
            if (Session["isLogin"] != null)
            {
                if (Session["isLogin"].ToString() != "true")
                {
                    Response.Write("<li class=\"functionList-half\" style=\"color:red;\">用户名或密码错误</li>");
                }
                else
                {
                    HttpCookie theCookie=new HttpCookie("login");
                    DateTime theDt=DateTime.Now;
                    TimeSpan theTs=new TimeSpan(240,0,0);
                    theCookie.Expires=theDt.Add(theTs);
                    theCookie.Values.Add("isLogin","true");
                    theCookie.Values.Add("uid",Session["id"].ToString());
                    Response.AppendCookie(theCookie);
                    Response.Redirect(Request.Params["rdUrl"] == null ? "index.aspx" : HttpUtility.UrlDecode( Request.Params["rdUrl"]));
                }
            }
            else
            {
                Response.Write("<li class=\"functionList-half left\">&nbsp;</li>");
            }
        }
        public void getUrl()
        {
            if (Request.Params["url"] != null)
            {
                Response.Write(Request.Params["rdUrl"]);
            }
        }
        public void getWu()
        {
            if (Request.Params["wu"] != null)
            {
                Response.Write(Request.Params["wu"]);
            }
        }
    }
}