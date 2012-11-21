using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Sa
{
    public partial class changePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sLogin"] == null || Session["sLogin"].ToString() != "true")
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim().Length == 0)
            {
                Response.Write("<script>alert('密码不能为空')</script>");
                return;
            }
            if (TextBox1.Text == TextBox2.Text)
            {
                moyu.Ecard.Union myUnion = new Ecard.Union();
                myUnion.changePassword(TextBox1.Text, Convert .ToInt32( Session["sid"]));
                Session["password"] = TextBox1.Text;
                Response.Redirect("default.aspx");
            }
            else
            {
                Response.Write("<script>alert('两次输入的密码不一致')</script>");
            }
        }
    }
}