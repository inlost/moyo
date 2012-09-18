using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.Infos
{
    public partial class _new : System.Web.UI.Page
    {
        private moyu.Infos.cat myCat = new moyu.Infos.cat();
        private Hashtable[] cat1, cat2;
        protected void Page_Load(object sender, EventArgs e)
        {
            cat1 = myCat.get(1, 0);
        }
        public void getCid()
        {
            Response.Write(Request.Params["cat"]);
        }
        public void listCats()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable cat in cat1)
            {
                sb.Append("<li class=\"infoM-n-c-mainCat\">");
                sb.Append("<div>"+cat["name"]+"</div><ul class=\"clearfix\">");
                cat2 = myCat.get(2, Convert.ToInt32(cat["id"]));
                foreach (Hashtable subCat in cat2)
                {
                    sb.Append("<li class=\"infoM-n-c-subCat\">");
                    sb.Append("<a href=\"#\" class=\"jump\" data-dst=\"Markets/Infos/new.aspx?action=detal&cat="+subCat["id"]+"\">" + subCat["name"] + "</a>");
                    sb.Append("</li>");
                }
                sb.Append("</ul></li>");
            }
            Response.Write(sb);
        }
    }
}