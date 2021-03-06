﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
namespace moyu.Mobile
{
    public partial class index : System.Web.UI.Page
    {
        private moyu.User.Functions myFunctions = new User.Functions();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getLogout()
        {
            if (Session["isLogin"] != null && Session["isLogin"].ToString() == "true")
            {
                Response.Write("<a href=\"/Services/User.ashx?action=quit&type=mobile\"> [退出]</a>");
            }
        }
        public void getNiceName()
        {
            if (Session["isLogin"] != null && Session["isLogin"].ToString() == "true")
            {
                Response.Write(Session["niceName"]);
            }
            else
            {
                Response.Write("未登录");
            }
        }
        public void getUserPoint()
        {
            if (Session["isLogin"] != null && Session["isLogin"].ToString() == "true")
            {
                StringBuilder sb = new StringBuilder();
                Hashtable points = new Hashtable();
                points = myFunctions.getPoint(Convert.ToInt32(Session["id"]));
                sb.Append("<li class=\"functionList-half left\">连续签到" + points["signInDays"] + "天，</li>");
                sb.Append("<li class=\"functionList-half left\">积分:" + points["point"] + "，</li>");
                sb.Append("<li class=\"functionList-half left\">贡献:" + points["contribute"] + "，</li>");
                sb.Append("<li class=\"functionList-half left\">" +( myFunctions.isSigIn(Convert.ToInt32(Session["id"])) ? "今日已签到" : "今日未签到") + "</li>");
                Response.Write(sb);
            }
        }
        public void getGift()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] gifts;
            gifts = myFunctions.giftGet(18);
            moyu.Functions myFunction = new Functions();
            foreach (Hashtable gift in gifts)
            {
                try
                {
                    string number = gift["message"].ToString().Split(new char[3] { '|', '|', '|' })[0];
                    sb.Append("<li>" + gift["message"].ToString().Substring(0, 3) + "**" + number.Substring(number.Length - 2, 2) + "在<span>" + myFunction.kindTime(Convert.ToDateTime(gift["date"])) + "</span>获得了");
                    sb.Append(gift["gift"]);
                    sb.Append("</li>");
                }
                catch
                { }
            }
            Response.Write(sb);
        }
    }
}