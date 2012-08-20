using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using System.Collections;
namespace moyu.Services
{
    /// <summary>
    /// living_shop_info 的摘要说明
    /// </summary>
    public class living_shop_info : IHttpHandler
    {
        private HttpContext theContext;
        private Living.shops myShop = new Living.shops();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/plain";

            if (theContext.Request.Params["id"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            getInfo();
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private void getInfo()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable info = new Hashtable();
            Hashtable theShop = new Hashtable();
            info = myShop.info_get( Convert .ToInt32(theContext.Request.Params["id"]));
            theShop = myShop.shop_get_detal(Convert.ToInt32(info["sid"]));
            sb.Append("<div id=\"detal-shop\" data-sid=\"" + theShop["id"] + "\">");
            sb.Append("<div id=\"d-s-points\">" + Convert.ToDouble(theShop["point"]).ToString("f0") + "</div>");
            sb.Append("<h2>" + theShop["name"] + "</h2>");
            sb.Append("<div id=\"d-s-useful\" class=\"clearfix\">");
            sb.Append("<img id=\"d-s-u-icon\" class=\"left\" alt=\"" + theShop["name"] + "\" src=\"" + theShop["pic"] + "\"/>");
            sb.Append("<div id=\"d-s-u-infos\" class=\"left\">");
            sb.Append("<div id=\"d-s-u-i-introduce\">" + theShop["introduce"] + "</div>");
            sb.Append("</div>");//infos
            sb.Append("<div id=\"d-s-u-infos2\" class=\"left\">");
            sb.Append("<div id=\"d-s-u-i-openTime\">营业时间：<span>" + theShop["openTime"] + "</span></div>");
            sb.Append("<div id=\"d-s-u-i-phone\">电话：<span>" + theShop["phone"] + "</span></div>");
            sb.Append("<div id=\"d-s-u-i-address\">地址：<span>" + theShop["address"] + "</span></div>");
            sb.Append("</div>");//infow2
            sb.Append("</div>");//usful
            sb.Append("<div id=\"d-s-mpaRate\" class=\"clearfix\">");
            sb.Append("<div id=\"d-s-m-rate\" class=\"left\"><ul>");
            sb.Append("<li id=\"d-s-m-r-point\" class=\"clearfix\"><div class=\"left\">总评分：</div><div class=\"left rates\" data-rated=\"" + theShop["point"] + "\"></div><input type=\"hidden\" id=\"d-s-m-r-s-point\"/></li>");
            sb.Append("<li class=\"clearfix\"><div class=\"left\">价格：</div><div class=\"left rates\" data-rated=\"" + theShop["quality"] + "\"></div><input type=\"hidden\" id=\"d-s-m-r-s-price\"/></li>");
            sb.Append("<li class=\"clearfix\"><div class=\"left\">服务：</div><div class=\"left rates\" data-rated=\"" + theShop["service"] + "\"></div><input type=\"hidden\" id=\"d-s-m-r-s-service\"/></li>");
            sb.Append("<li class=\"clearfix\"><div class=\"left\">环境：</div><div class=\"left rates\" data-rated=\"" + theShop["circumstance"] + "\"></div><input type=\"hidden\" id=\"d-s-m-r-s-circumstance\"/></li>");
            sb.Append("</ul>");
            sb.Append("<textarea id=\"d-s-m-r-comment\" cols=\"36\" rows=\"3\"></textarea>");
            sb.Append("<button id=\"d-s-m-r-submit\">发表评价</button>");
            sb.Append("</div>");//rate
            sb.Append("<div id=\"d-s-m-map\" class=\"left\" data-lng=\"" + theShop["pointY"] + "\" data-lat=\"" + theShop["pointX"] + "\"></div>");//map
            sb.Append("</div>");//map
            sb.Append("<div id=\"d-s-comment\">");
            sb.Append("<h3 id=\"d-s-c-title\">" + info["title"] + "</h3>");
            sb.Append("<div class=\"d-s-c-comments\">");
            sb.Append("<div class=\"d-s-c-c-body\">" + info["body"] + "</div>");
            sb.Append("<div class=\"d-s-c-c-time\">" + info["time"] + "</div></div>");
            sb.Append("</div>");//comment
            sb.Append("</div>");//detal-shop
            sb.Append("<script type=\"text/javascript\" src=\"Script/raty/js/jquery.raty.min.js\"></script>");
            sb.Append("<script>");
            sb.Append("moyo.Living.mapInit();");
            sb.Append("function mapCallBack(){moyo.Living.mapCallBack();}");
            sb.Append("moyo.Living.rateInit();");
            sb.Append("</script>");
            theContext.Response.Write(sb);
        }
    }
}