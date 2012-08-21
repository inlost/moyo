using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.living
{
    public partial class info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(DropDownList1.SelectedValue);
            int sid = Convert.ToInt32(DropDownList2.SelectedValue);
            string title = TextTitle.Text.Trim();
            string body = TextBody.Text.Trim();
            Living.shops myShop = new Living.shops();
            myShop.info_add(cid, sid, title, body);
            Response.Redirect("info.aspx");
        }
    }
}