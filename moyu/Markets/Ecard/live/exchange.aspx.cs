using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.Ecard.live
{
    public partial class exchange : System.Web.UI.Page
    {
        private moyu.Ecard.Living myLive = new moyu.Ecard.Living();
        private Hashtable[] goods;
        private Hashtable[] logs;
        protected void Page_Load(object sender, EventArgs e)
        {
            goods = myLive.exchangeGet();
            logs = myLive.userExchangeGet(Convert.ToInt32(Session["id"]));
        }
        /// <summary>
        /// 积分商品获取
        /// </summary>
        public void getGoods()
        {
            StringBuilder sb = new StringBuilder();
            string imgSrc="";
            foreach (Hashtable good in goods)
            {
                imgSrc = good["pic"].ToString();
                imgSrc = imgSrc.Substring(0, imgSrc.LastIndexOf("/")+1) + "a" + imgSrc.Substring(imgSrc.LastIndexOf("/") + 1, imgSrc.Length - imgSrc.LastIndexOf("/") - 1);
                sb.Append("<div class=\"mC-e-g-item left\">");
                sb.Append("<h3>" + good["title"] + "</h3>");
                sb.Append("<div class=\"img\">");
                sb.Append("<img alt=\"" + good["title"] + "\" src=\"" + imgSrc + "\"/>");
                sb.Append("</div>");
                sb.Append("<p>需积分：<span class=\"cost\">" + good["need"] + "</span> 剩余：<span class=\"number\">" + good["number"] + "</span></p>");
                sb.Append("<button data-gid=\""+good["id"]+"\">我要兑换</button>");
                sb.Append("</div>");
            }
            Response.Write(sb);
        }
        /// <summary>
        /// 用户积分获取
        /// </summary>
        public void myJfGet()
        {
            int Jf = myLive.userJfGet(Convert.ToInt32(Session["id"]));
            Response.Write(Jf);
        }
        /// <summary>
        /// 兑换记录获取
        /// </summary>
        public void exchangeLogGet()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable good = new Hashtable();
            int count = 0;
            foreach (Hashtable log in logs)
            {
                if (Convert.ToInt32(log["type"]) == 0)
                {
                    good = myLive.exchangeGet(Convert.ToInt32(log["gid"]));
                    sb.Append("<li>");
                    sb.Append(good["title"] + " [" + Convert .ToDateTime( log["time"] ).ToShortDateString()+ "] ");
                    sb.Append( Convert .ToBoolean( good["isFinished"])?"已领取":"未领取");
                    sb.Append("</li>");
                    count++;
                }
            }
            if (count== 0)
            {
                Response.Write("<li>没有记录</li>");
                return;
            }
            Response.Write(sb);
        }
        public void jfLogGet()
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (Hashtable log in logs)
            {
                if (Convert.ToInt32(log["type"]) == 1)
                {
                    sb.Append("<li>");
                    sb.Append(log["cost"] + "分 [" + log["time"] + "] " + log["place"]);
                    sb.Append("</li>");
                    count++;
                }
            }
            if (count == 0)
            {
                Response.Write("<li>没有记录</li>");
                return;
            }
            Response.Write(sb);
        }
    }
}