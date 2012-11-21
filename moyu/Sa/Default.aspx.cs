using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Sa
{
    public partial class Default : System.Web.UI.Page
    {
        private moyu.Ecard.Union myUnion = new Ecard.Union();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sLogin"] == null || Session["sLogin"].ToString() != "true")
            {
                Response.Redirect("login.aspx");
            }
            if (Session["password"].ToString() == "123456")
            {
                Response.Redirect("changePass.aspx");
            }
        }
        public void getShopCoupons()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] theCoupons;
            theCoupons = myUnion.getShopCoupons(Convert .ToInt32(Session["sid"]));
            foreach (Hashtable theCoupon in theCoupons)
            {
                sb.Append("<li>");
                sb.Append(theCoupon["realname"] + " 在 " + theCoupon["useTime"] +" 在本店使用了编号为："+theCoupon["no"]+"的【"+theCoupon["title"]+"】优惠券");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string no=TextBox1.Text.Trim();
            Hashtable theCoupons = new Hashtable();
            try
            {
                theCoupons = myUnion.getCouponsByNo(Convert .ToInt32( no));
                if (Convert.ToBoolean(theCoupons["isUsed"]))
                {
                    Response.Write("<script>alert('优惠券已经使用过，不能重复使用')</script>");
                }
                else
                {
                    myUnion.useCoupons(Convert.ToInt32(no), Convert.ToInt32(Session["sid"]));
                    Response.Write("<script>alert('使用成功')</script>");
                    TextBox1.Text = "";
                }
            }
            catch
            {
                Response.Write("<script>alert('优惠券无效')</script>");
            }
        }
    }
}