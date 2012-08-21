using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace moyu.Services
{
    [DataContract]
    public class clothesItem
    {
        [DataMember(Order = 0, IsRequired = true)]
        public int id { get; set; }
        [DataMember(Order = 1)]
        public string img { get; set; }
        [DataMember(Order = 2)]
        public string title { get; set; }
        [DataMember(Order = 3)]
        public string inventory { get; set; }
        [DataMember(Order = 4)]
        public string like { get; set; }
        [DataMember(Order = 5)]
        public string price { get; set; }
    }
    /// <summary>
    /// Sale_Clothes 的摘要说明
    /// </summary>
    public class Sale_Clothes : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Sale.Clothes myClothes = new Sale.Clothes();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/plain";
            if (theContext.Request.Form["action"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            switch (context.Request.Form["action"].ToString())
            {
                case "clothesGet":
                    clothesGet();
                    break;
                case "getDetail":
                    detailGet();
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

        private void detailGet()
        {
            int cid = Convert.ToInt32(theContext.Request.Params["id"]);
            Hashtable clothes;
            clothes = myClothes.get(cid);
            theContext.Response.Write(clothes["introduce"]);
        }

        private void clothesGet()
        { 
            int cat=Convert.ToInt32( theContext.Request.Form["cat"]);
            int last = Convert.ToInt32(theContext.Request.Form["last"]);
            Hashtable[] clothes;
            string imgSrc = "";
            clothes = myClothes.get(cat, last, 8);
            theContext.Response.Write("[");
            foreach( Hashtable clothe in clothes )
            {

                imgSrc = clothe["image"].ToString();
                imgSrc = imgSrc.Substring(0, imgSrc.LastIndexOf("/")+1) + "a" + imgSrc.Substring(imgSrc.LastIndexOf("/") + 1, imgSrc.Length - imgSrc.LastIndexOf("/") - 1);
                clothesItem theClothe = new clothesItem();
                theClothe.id = Convert.ToInt32(clothe["id"]);
                theClothe.img = imgSrc;
                theClothe.title=clothe["title"].ToString();
                theClothe.price = clothe["salePrice"].ToString();
                theClothe.inventory = Convert.ToInt32(clothe["inventory"]) > 0 ?  clothe["inventory"].ToString(): "卖完啦！";
                theClothe.like = clothe["showTime"].ToString();
                theContext.Response.Write(Data.Type.objToJson(theClothe));
                theContext.Response.Write (",");
            }
            theContext.Response.Write("]");
        }
    }
}