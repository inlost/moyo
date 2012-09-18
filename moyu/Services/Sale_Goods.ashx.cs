using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;

namespace moyu.Services
{
    /// <summary>
    /// Sale_Goods 的摘要说明
    /// </summary>
    public class Sale_Goods : IHttpHandler
    {
        private HttpContext theContext;
        private Sale.Goods myGoods = new Sale.Goods();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            context.Response.ContentType = "text/plain";
            switch (context.Request.Params["action"].ToString())
            {
                case "get":
                    get();
                    break;
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private void get()
        {
            int gid = Convert .ToInt32( theContext.Request.Params["gid"]);
            Hashtable good = myGoods.goodGet(gid);
            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            sb.Append("<div class=\"PopPage_Content\">");
            sb.Append("<h1>" + good["name"] + "</h1>");
            sb.Append("<div id=\"goodShowContent\" class=\"clearfix\">");
            sb.Append("<div class=\"left\" id=\"goodShowContent_price\">￥：<span class=\"oldPrice\">   "+(Convert .ToDouble(good["price"])+( good["price"].ToString().Length>6? Convert .ToInt32(ran.Next(143,379)) : Convert .ToDouble( ran.Next(20,40))))+"   </span>       <span>"+good["price"]+"</span></div>");
            sb.Append("<div id=\"goodShowContent_functions\" class=\"left\">");
            sb.Append("<a class=\"gSC_f_submit submitForm\" href=\"javascript:void(0);\" data-for=\"["+good["id"]+"]@商城商品\">购买</a>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div id=\"goodShowIntroduce\">");
            sb.Append(good["introduce"]);
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<script>moyo.computer.bindSubmit();</script>");
            theContext.Response.Write(sb);
        }
    }
}