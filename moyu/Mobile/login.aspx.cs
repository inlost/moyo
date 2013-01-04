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

        }
        public void getLoginMsg()
        {
            if (Request.Params["regMsg"] != null)
            {
                string strMsg = "";
                switch (Request.Params["regMsg"].ToString())
                {
                    case "0":
                        strMsg = "无法注册用户，请与左邻管理员联系";
                        break;
                    case "-1":
                        strMsg = "您输入的用户名已经被别人使用，请重新选择一个用户名";
                        break;
                    case "-2":
                        strMsg = "用户名不能为空";
                        break;
                    case "-3":
                        strMsg = "真实姓名不能为空";
                        break;
                    case "-4":
                        strMsg = "QQ不能为空";
                        break;
                    case "-5":
                        strMsg = "手机号码不能为空，且只能为纯数字";
                        break;
                    case "-6":
                        strMsg = "密码不能为空";
                        break;
                }
                Response.Write("<script>alert(\"" + strMsg + "\");</script>");
            }
            if (Session["isLogin"] != null)
            {
                if (Session["isLogin"].ToString() != "true")
                {
                    Response.Write("<li class=\"functionList-half\" style=\"color:red;\">用户名或密码错误</li>");
                }
                else
                {
                    Response.Redirect(Request.Params["rdUrl"] == null ? "index.aspx" : HttpUtility.UrlDecode( Request.Params["rdUrl"]));
                }
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