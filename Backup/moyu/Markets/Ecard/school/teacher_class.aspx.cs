using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Markets.Ecard.school
{
    public partial class teacher_class : System.Web.UI.Page
    {
        private int classId = 0;
        private moyu.User.School myUser = new User.School();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["class"] != null)
            {
                classId = Convert .ToInt32( Request.Params["class"]);
            }
        }
        public void architecture_get()
        {
            moyu.User.School mySchool = new moyu.User.School();
            Hashtable[] architectures = mySchool.architectureGet(Convert.ToInt32(Session["id"]));
            int iIndex = 1;
            if (architectures.Count() == 0)
            {
                Response.Write("<li data-value=\"-1\">无</li>");
            }
            else
            {
                foreach (Hashtable architecture in architectures)
                {
                    if (architecture["type"].ToString() == "2")
                    {
                        if (classId == 0)
                        {
                            if (iIndex == 1)
                            {
                                Response.Write("<li class=\"selected schoolEcard-c-f-s-architecture\" data-value=\"" + architecture["id"] + "\">" + architecture["name"] + "</li>");
                                classId = Convert.ToInt32(architecture["id"]);
                            }
                            else
                            {
                                Response.Write("<li class=\"schoolEcard-c-f-s-architecture\" data-value=\"" + architecture["id"] + "\">" + architecture["name"] + "</li>");
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(architecture["id"]) == classId)
                            {
                                Response.Write("<li class=\"selected schoolEcard-c-f-s-architecture\" data-value=\"" + architecture["id"] + "\">" + architecture["name"] + "</li>");
                            }
                            else
                            {
                                Response.Write("<li class=\"schoolEcard-c-f-s-architecture\" data-value=\"" + architecture["id"] + "\">" + architecture["name"] + "</li>");
                            }
                        }
                        iIndex++;
                    }
                }
            }
        }
        public void studentGet()
        {
            if (classId == 0) { return; }
            moyu.User.School myUser = new User.School();
            Hashtable[] students;
            students = myUser.studentGet(classId);
            foreach (Hashtable student in students)
            {
                Response.Write("<li class=\"student-List-student\" data-value=\""+student["id"]+"\" >"+student["realname"]+"</li>");
            }
        }
    }
}