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
                Response.Write("<li><a href=\"" + strUrl + "\">领取优惠券</a></li>");
            }
            else
            {
                if (myUnion.isHaveCoupon(Convert.ToInt32(Session["id"]), Convert.ToInt32(coupons["id"])))
                {
                    theCoupons = myUnion.getUserCoupons(Convert.ToInt32(Session["id"]), Convert.ToInt32(coupons["id"]));
                    string pass = Convert.ToBoolean(theCoupons["isUsed"]) ? "该优惠券已使用" : theCoupons["no"].ToString();
                    Response.Write("<li style=\"text-align:center;\">优惠密码:" + pass + "</li>");
                }
                else
                {
                    string strUrl = "login.aspx?rdUrl=" + HttpUtility.UrlEncode("../Services/Ecard.ashx?action=getCoupons&id=" + coupons["id"].ToString());
                    Response.Write("<li><a href=\"" + strUrl + "\">领取优惠券</a></li>");
                }
            }
        }
    }
}