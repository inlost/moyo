using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Mobile
{
    public partial class signIn : System.Web.UI.Page
    {
        private moyu.User.Functions myFunctions = new User.Functions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=signin.aspx");
            }
        }
        public void getUserName()
        {
            Response.Write (Session["niceName"]);
        }
        public void getUserPoint()
        {
            Hashtable points = new Hashtable();
            points = myFunctions.getPoint(Convert.ToInt32(Session["id"]));
            Response.Write("已连续签到" + points["signInDays"] + "天，积分:" + points["point"] + "，贡献:" + points["contribute"] + "。");
        }
        public void signInTexe()
        {
            Response.Write(myFunctions.isSigIn(Convert .ToInt32(Session["id"]))?"今日已签到":"点我签到");
        }
        public void getSignLog()
        {
            Hashtable[] logs;
            logs = myFunctions.getSigInLog(Convert .ToInt32(Session["id"]));
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable log in logs)
            {
                sb.Append("<li> 在 " + log["time"] + " 签到</li>");
            }
            Response.Write(sb);
        }
    }
}