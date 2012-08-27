using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.clothes
{
    public partial class cat : System.Web.UI.Page
    {
        private Cat myCat = new Cat();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strName=string.Empty;
            strName=TextBox1.Text.Trim();
            if (strName.Length != 0)
            {
                if (myCat.add(0, 1, strName))
                {
                    Response.Redirect("cat.aspx");
                }
                else
                {
                    Response.Write("<script>alert(\"添加失败\");</script>");
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string strName = string.Empty;
            strName = TextBox2.Text.Trim();
            if (strName.Length != 0 && ListBox1.SelectedIndex >= 0)
            {
                if (myCat.add(Convert.ToInt32(ListBox1.SelectedValue), 2, strName))
                {
                    Response.Redirect("cat.aspx");
                }
                else
                {
                    Response.Write("<script>alert(\"添加失败\");</script>");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex >= 0)
            {
                if (myCat.del(Convert.ToInt32(ListBox1.SelectedValue)))
                {
                    Response.Redirect("cat.aspx");
                }
                else
                {
                    Response.Write("<script>alert(\"删除\");</script>");
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedIndex >= 0)
            {
                if (myCat.del(Convert.ToInt32(ListBox2.SelectedValue)))
                {
                    Response.Redirect("cat.aspx");
                }
                else
                {
                    Response.Write("<script>alert(\"删除\");</script>");
                }
            }
        }
    }
}