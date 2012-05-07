using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

namespace moyu.Markets.Ecard.school
{
    public partial class teacher_contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void contactGet()
        {
            moyu.User.School myUser = new User.School();
            System.Collections.Hashtable[] contacts;
            contacts = myUser.contacGet(Convert.ToInt32(Session["id"]));
            foreach (System.Collections.Hashtable contact in contacts)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<li id=\"contact-" + contact["id"] + "\" class=\"ui-widget-content ui-selectee left schoolEcard-c-f-c-l-item\" data-uid=\"" + contact["id"] + "\">");
                sb.Append("<div class=\"schoolEcard-c-f-c-u-name\">" + contact["realname"] +"("+ ( Convert .ToBoolean( contact ["sex"])?"男":"女")+ ")</div>");
                sb.Append("<ul class=\"schoolEcard-c-f-c-u-other clearfix\"><li>" + contact["phone"] + "</li><li>" + contact["architecture"] + "</li></ul>");
                sb.Append("<div class=\"schoolEcard-c-f-c-u-address\">" + contact["address"] + "</div>");
                sb.Append("</li>");
                Response.Write(sb);
            }
        }
    }
}