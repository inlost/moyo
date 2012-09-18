using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.sale
{
    public partial class index : System.Web.UI.Page
    {
        private Sale.Goods myGoods = new Sale.Goods();
        private Hashtable[] cats;
        private Data.Db myDb = new Data.Db();
        protected void Page_Load(object sender, EventArgs e)
        {
            cats = myGoods.mallCatsGet();
        }
        public void listCats()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] goods;
            string strSql;
            foreach (Hashtable cat in cats)
            {
                sb.Append("<div class=\"saleH-cat clearfix\">");
                sb.Append("<h3>" + cat["name"] + "</h3>");
                strSql = "select top 3 * from sale_goods where saleType=1 and cid=" + cat["id"] +" order by id desc";
                goods = Data.Type.dtToHash( myDb.GetQuerySql(strSql, "rt"));
                foreach (Hashtable good in goods)
                {
                    sb.Append("<div class=\"saleH-c-bigGood saleH-c-good left side\" href=\"Services\\Sale_Goods.ashx?action=get&gid=" + good["id"] + "\">");
                    sb.Append("<img src=\"" + good["pic"] + "\" alt=\"" + good["name"] + "\"/>");
                    sb.Append("<div class=\"saleH-c-price\">￥：<span>"+good["price"]+"</span> RMB</div>");
                    sb.Append("<h4>"+good["name"]+"</h4>");
                    sb.Append("</div>");
                }
                strSql = "select top 6 * from sale_goods where saleType=2 and cid=" + cat["id"] + " order by id desc";
                goods = Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
                foreach (Hashtable good in goods)
                {
                    sb.Append("<div class=\"saleH-c-smallGood saleH-c-good left side\" href=\"Services\\Sale_Goods.ashx?action=get&gid="+good["id"]+"\">");
                    sb.Append("<img src=\"" + good["pic"] + "\" alt=\"" + good["name"] + "\"/>");
                    sb.Append("<div class=\"saleH-c-price\"><span>" + good["price"] + "</span> RMB</div>");
                    sb.Append("<h4>" + good["name"] + "</h4>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
            }
            Response.Write(sb);
        }
    }
}