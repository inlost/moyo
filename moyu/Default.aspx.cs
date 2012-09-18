using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu
{
    public partial class Default : System.Web.UI.Page
    {
        private Information.Forum myForum = new Information.Forum();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null)
            {
                Session["isLogin"] = "false";
            }
        }
        public void getForumInfo()
        {
            Information.Forum myForum = new Information.Forum();
            Hashtable info = myForum.globalInfoGet();
            StringBuilder sb = new StringBuilder();
            sb.Append("<p><span>"+info["gCount"]+"</span>个朋友圈子</p>");
            sb.Append("<p>有<span>"+(Convert.ToInt32( info["ptCount"])+Convert.ToInt32(info["gtCount"]))+"</span>个话题</p>");
            sb.Append("<p><span>"+info["cCount"]+"</span>次评论</p>");
            sb.Append("<p>住着<span>" + info["uCount"] + "</span>位居民</p>");
            sb.Append("<h2>我们和<span>你</span>在一起<span>！</span></h2>");
            Response.Write(sb);
        }
        public void getForumTopicGet()
        {
            Hashtable[] topics;
            topics = myForum.forumTopicGet(4);
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable topic in topics)
            {
                sb.Append("<li>");
                sb.Append("<a class=\"jump\" href=\"#\" data-dst=\"Markets/forum/TopicShow.aspx?id="+topic["id"]+"&last=0\">" + topic["topic_title"] + "</a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
        public void getBigGoods()
        {
            Hashtable[] goods;
            Sale.Goods myGoods = new Sale.Goods();
            goods = myGoods.goodsGet(6, 2);
            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            int iCount = 0;
            foreach (Hashtable good in goods)
            {
                sb.Append("<li"+(iCount!=0?" style=\"display:none\"":"")+" class=\"homeGoodsItem clearfix\"><a class=\"jump clearfix\" href=\"#\" data-dst=\"Markets/sale/index.aspx\">");
                sb.Append("<div class=\"hgiImg left\">");
                sb.Append("<img alt=\""+good["name"]+"\" src=\""+good["pic"]+"\">");
                sb.Append("</div>");
                sb.Append("<div class=\"hgiDetal left\">");
                sb.Append("<h3>"+good["name"]+"</h3>");
                sb.Append("<div id=\"goodShowContent_price\">￥:<span class=\"oldPrice\">   " + (Convert.ToDouble(good["price"]) + (good["price"].ToString().Length > 6 ? Convert.ToInt32(ran.Next(143, 379)) : Convert.ToDouble(ran.Next(20, 40)))) + "   </span>       <span>" + good["price"] + "</span></div>");
                sb.Append("</div>");
                sb.Append("</a></li>");
                iCount++;
            }
            Response.Write(sb);
        }
        public void shopCountGet()
        {
            Living.shops myShop = new Living.shops();
            int count = (Cache["shopCount"] == null ? myShop.shopCountGet() : Convert.ToInt32(Cache["shopCount"]));
            if (Cache["shopCount"] == null) { Cache.Insert("shopCount",count,null,DateTime.Now.AddHours(1),TimeSpan.Zero);}
            Response.Write(count);
        }
        public void infoCountGet()
        {
            Infos.Post myPost = new Infos.Post();
            int count = (Cache["infiCount"] == null ? myPost.postCountGet() : Convert.ToInt32(Cache["infiCount"]));
            if (Cache["infoCount"] == null) { Cache.Insert("infoCount", count, null, DateTime.Now.AddHours(1), TimeSpan.Zero); }
            Response.Write(count);
        }
    }
}