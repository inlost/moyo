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
    public partial class coupons_show : System.Web.UI.Page
    {
        private Hashtable coupons=new Hashtable();
        private Hashtable theCoupons = new Hashtable();
        private Ecard.Union myUnion = new Ecard.Union();
        protected void Page_Load(object sender, EventArgs e)
        {
            coupons = myUnion.couponsGet(Convert.ToInt32(Request.Params["id"]));
            if (Request.Params["msg"] != null)
            {
                int msg = Convert.ToInt32(Request.Params["msg"]);
                if (msg == 0)
                {
                    Response.Write("<script>alert(\"您已拥有\")</script>");
                }
                else if (msg == -1)
                {
                    Response.Write("<script>alert(\"该券已经停止发放\")</script>");
                }
                else if (msg == -2)
                {
                    Response.Write("<script>alert(\"您的积分不足，无法领取该券\")</script>");
                }
                else if (msg == -3)
                {
                    Response.Write("<script>alert(\"您的贡献不足，无法领取该券\")</script>");
                }
                else if (msg == -10)
                {
                    Response.Write("<script>alert(\"这个优惠券已经被大家抢完啦\")</script>");
                }
            }
        }
        public void getTitle()
        {
            Response.Write(coupons["title"]);
        }
        public void getBody()
        {
            Response.Write(coupons["body"]);
        }
        public void getNo()
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                string strUrl = "login.aspx?rdUrl=" + HttpUtility.UrlEncode("../Services/Ecard.ashx?action=getCoupons&id=" + coupons["id"].ToString());
                Response.Write("<a href=\"" + strUrl + "\"><button class=\"command-button bg-color-blue fg-color-white\" style=\"width:100%;\">点击领取优惠券<small>需要" + (Convert.ToInt32(coupons["pointType"]) == 1 ? "积分【" : "贡献【") + "<span style=\"font-weight:bold;color:red;\">" + coupons["needPoint"] + "</span>】</small></button></a>");
            }
            else
            {
                if (myUnion.isHaveCoupon(Convert.ToInt32(Session["id"]), Convert.ToInt32(coupons["id"])))
                {
                    theCoupons = myUnion.getUserCoupons(Convert.ToInt32(Session["id"]), Convert.ToInt32(coupons["id"]));
                    string pass = Convert.ToBoolean(theCoupons["isUsed"]) ? "该优惠券已使用" : theCoupons["no"].ToString();
                    Response.Write("<button class=\"command-button bg-color-blue fg-color-white\" style=\"width:100%;\">优惠密码:" + pass + "</button>");
                }
                else
                {
                    string strUrl = "login.aspx?rdUrl=" + HttpUtility.UrlEncode("../Services/Ecard.ashx?action=getCoupons&id=" + coupons["id"].ToString());
                    Response.Write("<a href=\"" + strUrl + "\"><button class=\"command-button bg-color-blue fg-color-white\" style=\"width:100%;\">点击领取优惠券<small>需要" + (Convert.ToInt32(coupons["pointType"]) == 1 ? "积分【" : "贡献【") + "<span style=\"font-weight:bold;color:red;\">" + coupons["needPoint"] + "</span>】</small></button></a>");
                }
            }
        }
    }
}