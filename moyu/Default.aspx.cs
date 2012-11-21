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
        private Information.topic myTopic = new Information.topic();
        private Functions myFunctions = new Functions();
        protected void Page_Load(object sender, EventArgs e)
        {
            string browser = Request.Browser.Browser;
            if (browser == "IE")
            {
                if (Convert.ToDouble(Request.Browser.Version) < 7)
                {
                    Response.Redirect("Browser.html");
                }
            }
            if (Session["isLogin"] == null)
            {
                Session["isLogin"] = "false";
            }
        }
        public void getForumInfo()
        {
            Information.Forum myForum = new Information.Forum();
            StringBuilder sb = new StringBuilder();
            if (Cache["homeForumCount"] == null)
            {
                Hashtable info = myForum.globalInfoGet();
                sb.Append("<p><span>" + info["gCount"] + "</span>个朋友圈子</p>");
                sb.Append("<p>有<span>" + (Convert.ToInt32(info["ptCount"]) + Convert.ToInt32(info["gtCount"])) + "</span>个话题</p>");
                sb.Append("<p><span>" + info["cCount"] + "</span>次评论</p>");
                sb.Append("<p><span>" + info["uCount"] + "</span>位居民</p>");
                sb.Append("<h2>和<span>你</span>在一起<span>！</span></h2>");
                Cache.Insert("homeForumCount", sb, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            Response.Write(Cache["homeForumCount"]);
        }
        public void getForumTopicGet()
        {
            Hashtable[] topics;
            StringBuilder sb = new StringBuilder();
            if (Cache["homeForumTopic"] == null)
            {
                topics = myForum.forumTopicGet(4);
                foreach (Hashtable topic in topics)
                {
                    sb.Append("<li>");
                    sb.Append("<a class=\"jump\" href=\"/" + topic["topic_title"] + "_定西吧_沁辰左邻/Markets---forum---TopicShow@aspx/id=" + topic["id"] + "&last=0\" data-dst=\"Markets/forum/TopicShow.aspx?id=" + topic["id"] + "&last=0\">" + topic["topic_title"] + "</a>");
                    sb.Append("</li>");
                }
                Cache.Insert("homeForumTopic", sb, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            Response.Write(Cache["homeForumTopic"]);
        }
        public void getBigGoods()
        {
            Hashtable[] goods;
            Sale.Goods myGoods = new Sale.Goods();
            StringBuilder sb = new StringBuilder();
            if (Cache["homeSaleGoods"] == null)
            {
                goods = myGoods.goodsGet(6, 2);
                Random ran = new Random();
                int iCount = 0;
                foreach (Hashtable good in goods)
                {
                    sb.Append("<li" + (iCount != 0 ? " style=\"display:none\"" : "") + " class=\"homeGoodsItem clearfix\"><a class=\"jump clearfix\" href=\"/定西网上商城_沁辰左邻/Markets---sale---index@aspx\" data-dst=\"Markets/sale/index.aspx\">");
                    sb.Append("<div class=\"hgiImg left\">");
                    sb.Append("<img alt=\"" + good["name"] + "\" src=\"" + good["pic"] + "\">");
                    sb.Append("</div>");
                    sb.Append("<div class=\"hgiDetal left\">");
                    sb.Append("<h3>" + good["name"] + "</h3>");
                    sb.Append("<div id=\"goodShowContent_price\">￥:<span class=\"oldPrice\">   " + (Convert.ToDouble(good["price"]) + (good["price"].ToString().Length > 6 ? Convert.ToInt32(ran.Next(143, 379)) : Convert.ToDouble(ran.Next(20, 40)))) + "   </span>       <span>" + good["price"] + "</span></div>");
                    sb.Append("</div>");
                    sb.Append("</a></li>");
                    iCount++;
                }
                Cache.Insert("homeSaleGoods", sb, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            Response.Write(Cache["homeSaleGoods"]);
        }
        public void shopCountGet()
        {
            Living.shops myShop = new Living.shops();
            int count = (Cache["shopCount"] == null ? myShop.shopCountGet() : Convert.ToInt32(Cache["shopCount"]));
            if (Cache["shopCount"] == null) { Cache.Insert("shopCount",count,null,DateTime.Now.AddHours(12),TimeSpan.Zero);}
            Response.Write(count);
        }
        public void infoCountGet()
        {
            Infos.Post myPost = new Infos.Post();
            int count = (Cache["infiCount"] == null ? myPost.postCountGet() : Convert.ToInt32(Cache["infiCount"]));
            if (Cache["infoCount"] == null) { Cache.Insert("infoCount", count, null, DateTime.Now.AddHours(12), TimeSpan.Zero); }
            Response.Write(count);
        }
        public void getForumHotTopic()
        {
            Hashtable[] topics;
            StringBuilder sb = new StringBuilder();
            if (Cache["forumHotTopic"] == null)
            {
                topics = myForum.forumTopicHotGet(10);
                foreach (Hashtable topic in topics)
                {
                    sb.Append("<li>");
                    sb.Append("<a class=\"jump\" href=\"/" + topic["topic_title"] + "_定西吧_沁辰左邻/Markets---forum---TopicShow@aspx/id=" + topic["id"] + "&last=0\" data-dst=\"Markets/forum/TopicShow.aspx?id=" + topic["id"] + "&last=0\">" + topic["topic_title"] + "<span> " + topic["showTime"] + "℃</span></a>");
                    sb.Append("</li>");
                }
                Cache.Insert("forumHotTopic", sb, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }
            else
            {
                sb.Append(Cache["forumHotTopic"]);
            }
            Response.Write(sb);
        }
        public void getForumNewsTopic()
        {
            Hashtable[] topics;
            StringBuilder sb = new StringBuilder();
            if (Cache["forumNewsTopic"] == null)
            {
                topics =  myTopic.get(12, 0, 10);
                foreach (Hashtable topic in topics)
                {
                    sb.Append("<li>");
                    sb.Append("<a class=\"jump\" href=\"/" + topic["topic_title"] + "_定西吧_沁辰左邻/Markets---forum---TopicShow@aspx/id=" + topic["id"] + "&last=0\" data-dst=\"Markets/forum/TopicShow.aspx?id=" + topic["id"] + "&last=0\">" + topic["topic_title"] + "<span> " + myFunctions.kindTime(Convert.ToDateTime(topic["topic_date"])) + "</span></a>");
                    sb.Append("</li>");
                }
                Cache.Insert("forumNewsTopic", sb, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }
            else
            {
                sb.Append(Cache["forumNewsTopic"]);
            }
            Response.Write(sb);
        }
        public void getNewInfos()
        {
            moyu.Infos.Post myPost = new moyu.Infos.Post();
            Hashtable[] posts;
            StringBuilder sb = new StringBuilder();
            if (Cache["homeNewInfos"] == null)
            {
                posts = myPost.postsGet(10, 0, 0, 0);
                foreach (Hashtable topic in posts)
                {
                    sb.Append("<li>");
                    sb.Append("<a class=\"jump\" href=\"/[" + topic["catName"] + "]" + topic["title"] + "_定西信息港_沁辰左邻/Markets---Infos---index@aspx/id=" + topic["id"] + "&last=0\" data-dst=\"Markets/Infos/index.aspx?id=" + topic["id"] + "&last=0\">[" + topic["catName"] + "]  " + topic["title"] + "<span> " + myFunctions.kindTime(Convert.ToDateTime(topic["postDate"])) + "</span></a>");
                    sb.Append("</li>");
                }
                Cache.Insert("homeNewInfos", sb, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }
            else
            {
                sb.Append(Cache["homeNewInfos"]);
            }
            Response.Write(sb);
        }
        public void getNewKnows()
        {
            moyu.Living.Question myQuestion = new moyu.Living.Question();
            Hashtable[] questions;  
            StringBuilder sb = new StringBuilder();
            if (Cache["homeNewKnows"] == null)
            {
                questions = myQuestion.getNew(10, 0);
                foreach (Hashtable question in questions)
                {
                    sb.Append("<li>");
                    sb.Append("<a class=\"jump\" data-dst=\"Markets/Living/knowsShow.aspx?id=" + question["id"] + "\" href=\"/" + question["title"] + "_定西生活_沁辰左邻/Markets---Living---knowsShow@aspx/id=" + question["id"] + "\" >" + question["title"] + "<span>   " + myFunctions.kindTime(Convert.ToDateTime(question["postDate"])) + "</span></a>");
                    sb.Append("</li>");
                }
                Cache.Insert("homeNewKnows", sb, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }
            else
            {
                sb.Append(Cache["homeNewKnows"]);
            }
            Response.Write(sb);
        }
    }
}