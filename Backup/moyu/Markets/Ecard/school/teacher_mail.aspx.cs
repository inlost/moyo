using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Markets.Ecard.school
{
    public partial class teacher_mail : System.Web.UI.Page
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
                Response.Write("<li class=\"ui-widget-content ui-selectee\" data-uid=\""+contact["id"]+"\">"+contact["realname"]+"</li>");
            }
        }
    }
}