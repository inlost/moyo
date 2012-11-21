using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.Living
{
    public partial class index : System.Web.UI.Page
    {
        private Hashtable[] cats;
        private moyu.Living.shops myShop = new moyu.Living.shops();
        private moyu.Living.Question myQuestion = new moyu.Living.Question();
        protected void Page_Load(object sender, EventArgs e)
        {
            cats = myShop.cat_get();
        }
        public void listCats()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable ad = new Hashtable();
            Hashtable[] shops,shopsNew,shopAll,infos;
            int shopIndex = 1;
            foreach (Hashtable cat in cats)
            {
                shopIndex = 1;
                shops = myShop.shop_get_byPoint(Convert.ToInt32(cat["id"]), 3);
                shopsNew = myShop.shop_get_byNew(Convert.ToInt32(cat["id"]), 5);
                shopAll = myShop.shop_get_all(Convert.ToInt32(cat["id"]), 80);
                infos = myShop.infos_get(Convert.ToInt32(cat["id"]), 9);
                try
                {
                    ad = myShop.cat_ad_get(Convert.ToInt32(cat["id"]), 0);
                }
                catch
                {
                    ad = null;
                }
                sb.Append("<div class=\"l-catHolder\">");
                sb.Append("<h2 class=\"l-catTitle clearfix\">定西");
                sb.Append(cat["name"]);
                sb.Append("</h2>");
                sb.Append("<div class=\"l-catContent clearfix\">");
                sb.Append("<div class=\"left t-c-ad\">");
                if (ad != null)
                { 
                    sb.Append("<a class=\"side\" href=\"" + ad["url"] + "\"><img alt=\"" + ad["title"] + "\" src=\"" + ad["pic"] + "\"/></a>");
                }
                sb.Append("</div>");
                sb.Append("<div class=\"left t-c-ranking clearfix\">");
                foreach (Hashtable shop in shops)
                {
                    sb.Append("<div class=\"left t-c-r-shops clearfix t-c-r-shops-index"+shopIndex+"\">");
                    sb.Append("<a class=\"side\" href=\"Services/living_shop.ashx?id="+shop["id"]+"\">");
                    sb.Append("<div class=\"left t-c-r-s-index\">"+shopIndex+"</div>");
                    sb.Append("<div class=\"t-c-r-s-points\">" + Convert .ToDouble( shop["point"]).ToString("f0")+ "</div>");
                    sb.Append("<div class=\"left t-c-r-s-img\">");
                    if (shop["pic"].ToString().Length != 0)
                    {
                        sb.Append("<img alt=\"" + shop["pic"] + "\" src=\"" + shop["pic"] + "\">");
                    }
                    sb.Append("</div>");
                    sb.Append("<div class=\"left t-c-r-s-inContent\">");
                    sb.Append("<h3>" + shop["name"] + "</h3>");
                    if (shopIndex == 1)
                    { 
                        sb.Append("<div class=\"t-c-r-s-i-introduce\">" + shop["introduce"] + "</div>");
                    }
                    sb.Append("<div class=\"t-c-r-s-i-address\">地址：<span>" + shop["address"] + "</span>  <span>" + shop["phone"] + "</span></div>");
                    sb.Append("</div>");
                    sb.Append("</a></div>");
                    shopIndex++;
                }
                sb.Append("</div>");
                sb.Append("<div class=\"left t-c-infos\">");
                sb.Append("<h3 class=\"t-c-i-title\">找优惠</h3>");
                sb.Append("<ul>");
                foreach (Hashtable info in infos)
                {
                    sb.Append("<li class=\"t-c-n-items\">");
                    sb.Append("<a class=\"side\" href=\"Services/living_shop_info.ashx?id=" + info["id"] + "\">");
                    sb.Append("<span class=\"t-c-n-i-shop\">["+info["name"]+"]</span>");
                    sb.Append("<span class=\"t-c-n-i-title\">"+info["title"]+"</span>");
                    sb.Append("</a>");
                    sb.Append("</li>");
                }
                sb.Append("</ul></div>");
                sb.Append("<div class=\"left t-c-newShop\">");
                sb.Append("<h3 class=\"t-c-i-title\">最新入驻</h3>");
                sb.Append("<ul>");
                shopIndex = 1;
                foreach (Hashtable shop in shopsNew)
                {
                    sb.Append("<li>");
                    sb.Append("<h4 class=\"clearfix" + (shopIndex != 1 ? "" : " hide") + "\"><span class=\"left\">" + shop["name"] + "</span><span class=\"right\">" + Convert.ToDouble(shop["point"]).ToString("f0") + "</span></h4>");
                    sb.Append("<div class=\"t-c-i-detal clearfix"+(shopIndex==1?" show":" hide")+"\">");
                    sb.Append("<a class=\"side\" href=\"Services/living_shop.ashx?id="+shop["id"]+"\">");
                    sb.Append("<img class=\"left\" alt=\"" + shop["name"] + "\" src=\""+shop["pic"]+"\"/>");
                    sb.Append("<div class=\"left t-c-i-d-infos\">");
                    sb.Append("<h5>"+shop["name"]+"</h5>");
                    sb.Append("<div>"+shop["address"]+"</div>");
                    sb.Append("</div>");
                    sb.Append("</a></div>");
                    sb.Append("</li>");
                    shopIndex++;
                }
                sb.Append("</ul></div>");
                sb.Append("<div class=\"left t-c-shops\">");
                sb.Append("<h3 class=\"t-c-i-title\">商家列表</h3>");
                sb.Append("<ul class=\"clearfix\">");
                foreach (Hashtable shop in shopAll)
                {
                    sb.Append("<li class=\"left t-c-s-items t-c-s-i-" + (shop["isDisplay"].ToString()=="True"?"hilight":"normal") + "\">");
                    sb.Append("<a class=\"side\" href=\"Services/living_shop.ashx?id="+shop["id"]+"\">");
                    sb.Append(shop["name"]);
                    sb.Append("</a></li>");
                }
                sb.Append("</ul></div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
            Response.Write(sb);
        }
        public void getLoginClass()
        {
            if (Session["isLogin"] != null && Session["isLogin"].ToString() != "false")
            {
                Response.Write("jump");
            }
            else
            {
                Response.Write("needLogin");
            }
        }
        public void getKnows()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] questions;
            questions = myQuestion.getNew(24,0);
            foreach (Hashtable question in questions)
            {
                sb.Append("<li class=\"clearfix\">");
                sb.Append("<a class=\"jump left\" data-dst=\"Markets/Living/knowsShow.aspx?id=" + question["id"] + "\" href=\"/" + question["title"] + "_定西生活_沁辰左邻/Markets---Living---knowsShow@aspx/id="+question["id"]+"\" >" + question["title"] + "</a>");
                sb.Append("<span class=\"right\">更新于:" + Convert.ToDateTime(question["updateDate"]).ToShortDateString().Replace("2012/", "") + " " + Convert.ToDateTime(question["updateDate"]).ToShortTimeString() + "</span>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}