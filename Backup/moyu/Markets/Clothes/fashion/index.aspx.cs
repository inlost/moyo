using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Markets.Clothes.fashion
{
    public partial class index : System.Web.UI.Page
    {
        private Sale.Clothes myClothes = new Sale.Clothes();
        Hashtable[] clothes;
        protected void Page_Load(object sender, EventArgs e)
        {
            clothes = myClothes.get(6, 0, 12);
        }
        public void getRow(int row)
        {
            StringBuilder sb = new StringBuilder();
            string imgSrc = "";
            for (int i = (row - 1) * 3; i <= row * 3-1; i++)
            {
                if (i <= clothes.Count() - 1)
                {
                    imgSrc = clothes[i]["image"].ToString();
                    imgSrc = imgSrc.Substring(0, imgSrc.LastIndexOf("/")+1) + "a" + imgSrc.Substring(imgSrc.LastIndexOf("/") + 1, imgSrc.Length - imgSrc.LastIndexOf("/") - 1);
                    sb.Append("<a class=\"fall-items popDetail\" data-service=\"Sale_Clothes.ashx?id=" + clothes[i]["id"] + "\" data-canBuy=\"true\" data-action=\"getDetail\" data-pid=\"" + clothes[i]["id"] + "\" data-left=\"" + clothes[i]["inventory"] + "\">");
                    sb.Append("<div class=\"fallImages\">");
                    sb.Append("<img src=\"" + imgSrc + "\" title=\"点击查看详情\">");
                    sb.Append("<h3 class=\"fallTitle\">" + clothes[i]["title"] + "</h3>");
                    sb.Append("</div>");
                    sb.Append("<ul class=\"clearfix fall-item-info\">");
                    sb.Append("<li class=\"left  fall-item-inventory\">");
                    sb.Append(Convert.ToInt32(clothes[i]["inventory"]) > 0 ? "还有 " + clothes[i]["inventory"] + " 件" : "卖完啦！");
                    sb.Append("</li>");
                    sb.Append("<li class=\"left fall-itenm-hot\"> " + clothes[i]["showTime"] + " 人喜欢</li>");
                    sb.Append("<li class=\"fall-item-price\">￥:" + clothes[i]["salePrice"] + "</li>");
                    sb.Append("</ul>");
                    sb.Append("</a>");
                }
            }
            Response.Write(sb);
        }
    }
}