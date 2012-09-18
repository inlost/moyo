using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.infos
{
    public partial class cat : System.Web.UI.Page
    {
        private Infos.cat myCat = new Infos.cat();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            myCat.add(TextBox1.Text.Trim(), 0, 1, 0);
            Response.Redirect("cat.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            myCat.del( Convert .ToInt32 (ListBox1.SelectedValue));
            Response.Redirect("cat.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            myCat.add(TextBox2.Text.Trim(), Convert.ToInt32(ListBox1.SelectedValue), 2, 0);
            Response.Redirect("cat.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            myCat.del(Convert.ToInt32(ListBox2.SelectedValue));
            Response.Redirect("cat.aspx");
        }
    }
}