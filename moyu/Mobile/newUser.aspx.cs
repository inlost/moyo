using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Mobile
{
    public partial class newUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=newUser.aspx");
            }
            moyu.User.Functions myFunctions = new User.Functions();
            if (myFunctions.getPoint(Convert.ToInt32(Session["id"]))["hasNewPoint"].ToString() == "False")
            {
                Response.Write("<script>alert(\"你已经感谢过别人了\\n快去介绍别人来左邻为自己赚贡献吧~\\n\\nPS:贡献可是很珍贵的。\");</script>");
            }
        }
    }
}