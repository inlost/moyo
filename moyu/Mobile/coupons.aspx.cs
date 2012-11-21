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
    public partial class coupons : System.Web.UI.Page
    {
        private Ecard.Union myUnion = new Ecard.Union();
        int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=coupons.aspx");
            }
            uid = Convert.ToInt32(Session["id"]);
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
        public void getUserCoupons()
        {
            Hashtable[] coupons;
            coupons = myUnion.getUserCoupons(uid);
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable coupon in coupons)
            {
                sb.Append("<li>");
                sb.Append("<a href=\"coupons-show.aspx?id=" + coupon["cid"] + "\">" + myUnion.couponsGet(Convert.ToInt32(coupon["cid"]))["title"] + "</a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        public void getCoupons()
        {
            Hashtable[] coupons;
            coupons = myUnion.couponsGet();
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable coupon in coupons)
            {
                sb.Append("<li>");
                sb.Append("<a href=\"coupons-show.aspx?id=" + coupon["id"] + "\">[" + coupon["needPoint"] + "]积分 " + coupon["title"] + "</a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}