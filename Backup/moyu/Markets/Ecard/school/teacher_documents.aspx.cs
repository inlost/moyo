using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;

namespace moyu.Markets.Ecard.school
{
    public partial class teacher_documents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void architecture_get()
        { 
            moyu.User.School mySchool=new moyu.User.School();
            Hashtable[] architectures = mySchool.architectureGet(Convert.ToInt32(Session["id"]));
            if (architectures.Count() == 0)
            {
                Response.Write("<option value=\"-1\">没有可以发送的构架</option>");
            }
            else
            {
                foreach (Hashtable architecture in architectures)
                {
                    Response.Write("<option value=\"" + architecture["id"] + "\">" + architecture["name"] + "</option>");
                }
            }
        }
    }
}