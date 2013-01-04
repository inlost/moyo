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
            theContext.Response.ContentType = "text/html";
            if (theContext.Request.Params["action"] != null)
            {
                if (theContext.Request.Params["action"] == "quit")
                {
                    theContext.Response.Cookies.Clear();
                    theContext.Session.Clear();
                    theContext.Response.Redirect(theContext.Request.Params["type"].ToString() == "mobile" ? "~/Mobile/index.aspx" : "~/default.aspx");
                }
            }
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
                case "mobileLogin":
                    mobileLogin();
                    break;
                case "reg":
                    reg();
                    break;
                case "mobileReg":
                    mobileReg();
                    break;
                case "isLogined":
                    isLogined();
                    break;
                case "avatarUp":
                    avatarUp();
                break;
                case "avatarUpBack":
                    avatarUpBack();
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
        private int login()
        {
            string uid = theContext.Request.Form["uid"];
            string password = theContext.Request.Form["password"];
            Hashtable theUser = new Hashtable();
            theUser = myUser.login(uid, password);
            if (theUser == null)
            {
                theContext.Session["isLogin"] = "false";
                theContext.Response.Write(false);
                return 0;
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
                return Convert.ToInt32(theUser["id"]);
            }
        }
        private void mobileLogin()
        {
            string url = theContext.Request.Form["rdUrl"].ToString();
            int uid=login();
            if (uid != 0 && theContext.Request.Form["wu"] != null && theContext.Request.Form["wu"].ToString().Length>0)
            {
                myUser.bindWeixin(uid, theContext.Request.Form["wu"]);
            }
            theContext.Response.Redirect("~/Mobile/login.aspx" + (url.Length == 0 ? "" : ("?rdUrl=" + HttpUtility.UrlEncode(url))));
        }
        private int reg()
        {
            string niceName = theContext.Request.Form["niceName"].Trim();
            string realName = theContext.Request.Form["realName"].Trim();
            bool sex =( theContext.Request.Form["sex"].ToString() == "boy" ? true : false);
            DateTime birthday = Convert.ToDateTime(theContext.Request.Form["Birth"]);
            string email = theContext.Request.Form["email"].Trim();
            if (email.IndexOf("@") < 0) { email += "@qq.com"; }
            Int64 phone = 0;
            try
            {
                phone = Convert.ToInt64(theContext.Request.Form["phone"].Trim());
            }
            catch
            {
                return -5;//电话号码不正确
            }
            string password = theContext.Request.Form["password"].Trim();
            Hashtable theUser = new Hashtable();
            if (myUser.isNameUsed(niceName))
            {
                return -1;//用户名已使用
            }
            if (niceName.Length == 0)
            {
                return -2;//用户名为空
            }
            if (realName.Length == 0)
            {
                return -3;//真实姓名为空
            }
            if (email == "@qq.com")
            {
                return -4;//QQ为空
            }
            if (password.Length == 0)
            {
                return -6;//密码为空
            }
            theUser = myUser.reg(niceName, realName, sex, birthday, email, phone, password);
            if (theUser == null)
            {
                theContext.Response.Write(false);
                return 0;
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
                return Convert .ToInt32(  theUser["id"]);
            }
        }
        private void mobileReg()
        {
            string url = theContext.Request.Form["rdUrl"].ToString();
            int uid= reg();
            if (uid > 0)
            {
                myUser.bindWeixin(uid, theContext.Request.Form["wu"]);
            }
            else
            {
                theContext.Response.Redirect("~/Mobile/login.aspx?regMsg=" + uid);
            }
            theContext.Response.Redirect("~/Mobile/login.aspx" + (url.Length ==0 ? "" : ("?rdUrl=" + HttpUtility.UrlEncode( url))));
        }
        private void isLogined()
        {
            if (theContext.Session["isLogin"]!=null && theContext.Session["isLogin"].ToString() == "true")
            {
                theContext.Response.Write(theContext.Session["niceName"]);
            }
            else
            {
                theContext.Response.Write("false");
            }
        }
        public void avatarUp()
        {
            Upload.image myImage = new Upload.image();
            myImage.IsSuoImg = false;
            myImage.UploadFileSize = 2;
            myImage.IsAddWaterMark = false;
            myImage.Minwidth = 50;
            myImage.Minheight = 50;
            myImage.IsUseRandFileName = true;
            myImage.IsRarPic = true;
            myImage.Rarwidth = 320;
            DateTime dt = DateTime.Now;
            string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
            myImage.Upload(uploadPath, theContext.Request.Files[0]);
            if (myImage.IsSuccess)
            {
                theContext.Response.Write("<script>window.parent.moyo.setting.avatarUploadBack('" +uploadPath.Replace("\\","/")+ myImage.OFullName + "');</script>");
            }
            else
            {
                theContext.Response.Write("<script>window.parent.moyo.setting.avatarUploadBack(0);</script>");
            }
        }
        private void avatarUpBack()
        {
            string img = theContext.Request.Form["img"];
            int x1 =Convert .ToInt32(  theContext.Request.Form["x1"]);
            int y1 = Convert.ToInt32( theContext.Request.Form["y1"]);
            int x2 = Convert.ToInt32( theContext.Request.Form["x2"]);
            int y2 = Convert.ToInt32(theContext.Request.Form["y2"]);
            int height = Convert.ToInt32(theContext.Request.Form["height"]);
            int width = Convert.ToInt32(theContext.Request.Form["width"]);
            Upload.Avatar myAvatar = new Upload.Avatar();
            theContext.Response.Write ( myAvatar.newUpload(Convert.ToInt32(theContext.Session["id"]), x1, y1, height, width,img));
        }
    }
}