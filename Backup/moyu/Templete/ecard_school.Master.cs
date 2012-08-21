using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Templete
{
    public partial class ecard_school : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getTimeBar()
        {
            Response.Write("你好 "+ Session["realname"] +" 欢迎登录系统，今天是"+DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToString("ddd", new System.Globalization.CultureInfo("zh-cn")));
        }
    }
}