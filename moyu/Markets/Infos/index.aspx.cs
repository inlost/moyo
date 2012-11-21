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
    public partial class index : System.Web.UI.Page
    {
        private Hashtable[] cats;
        private int cat1;
        private int cat2;
        private int last;
        private moyu.Infos.cat myCat = new moyu.Infos.cat();
        private moyu.Infos.Post myPost = new moyu.Infos.Post();
        protected void Page_Load(object sender, EventArgs e)
        {
            cat1 = Request.Params["cat1"] == null ? 0 : Convert.ToInt32(Request.Params["cat1"]);
            cat2 = Request.Params["cat2"] == null ? 0 : Convert.ToInt32(Request.Params["cat2"]);
            last = Request.Params["last"] == null ? 0 : Convert.ToInt32(Request.Params["last"]);
            cats = myCat.get(1, 0);
        }
        public void listPost()
        {
            Hashtable[] posts;
            posts= myPost.postsGet(50, 0, cat2, cat1);
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable post in posts)
            {
                sb.Append("<li class=\"infoItems left\"><div class=\"infoItemHeader\">");
                sb.Append("[" + post["catName"] + "]<br/>" + post["title"] + "</div>");
                sb.Append("<div class=\"infoItemBody\">");
                sb.Append("<p class=\"infoItemBody_body\">"+post["body"]+"</p>");
                sb.Append("<p>联系人：" + post["name"] + "</p>");
                sb.Append("<p>联系电话：" + post["phone"] + "</p>");
                sb.Append("</div>");
                sb.Append("<div class=\"infoItemFooter clearfix\">");
                sb.Append("<span class=\"infoItemDate left\">" + post["postDate"] + "</span>");
                sb.Append("<span class=\"infoItemPrice right\">" + (post["price"].ToString()=="0.00"?"面议":post["price"]) + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        public void listCat(int deep)
        {
            StringBuilder sb = new StringBuilder();
            if (deep == 1)
            {
                sb.Append("<li><a href=\"/全部—定西信息港_沁辰左邻/Markets---Infos---index@aspx/cat1=0&cat2=0\" data-dst=\"Markets/Infos/index.aspx?cat1=0&cat2=0\" class=\"" + (cat1 == 0 ? "actCat " : "") + "jump\">全部</a></li>");
                foreach (Hashtable cat in cats)
                {
                    sb.Append("<li>");
                    sb.Append("<a href=\"/" + cat["name"] + "—定西信息港_沁辰左邻/Markets---Infos---index@aspx/cat1=" + cat["id"] + "&cat2=0\" data-dst=\"Markets/Infos/index.aspx?cat1=" + cat["id"] + "&cat2=0\" class=\"" + (cat1 == Convert.ToInt32(cat["id"]) ? "actCat " : "") + "jump\">" + cat["name"] + "</a>");
                    sb.Append("</li>");
                }
            }
            else
            {
                Hashtable[] subCats;
                subCats = myCat.get(2, cat1);
                sb.Append("<li><a href=\"/全部—定西信息港_沁辰左邻/Markets---Infos---index@aspx/cat1=" + cat1 + "&cat2=0\" data-dst=\"Markets/Infos/index.aspx?cat1=" + cat1 + "&cat2=0\" class=\"" + (cat2 == 0 ? "actCat " : "") + "jump\">全部</a></li>");
                foreach (Hashtable cat in subCats)
                {
                    sb.Append("<li>");
                    sb.Append("<a href=\"/" + cat["name"] + "—定西信息港_沁辰左邻/Markets---Infos---index@aspx/cat1=" + cat1 + "&cat2=" + cat["id"] + "\" data-dst=\"Markets/Infos/index.aspx?cat1=" + cat1 + "&cat2=" + cat["id"] + "\" class=\"" + (cat2 == Convert.ToInt32(cat["id"]) ? "actCat " : "") + "jump\">" + cat["name"] + "</a>");
                    sb.Append("</li>");
                }
            }
            Response.Write(sb);
        }
    }
}