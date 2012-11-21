using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace moyu.Mobile
{
    public partial class lucky : System.Web.UI.Page
    {
        private moyu.User.Functions myFunctions = new User.Functions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogin"] == null || Session["isLogin"].ToString() == "false")
            {
                Response.Redirect("login.aspx?rdUrl=lucky.aspx");
            }
        }
        public void getMessage()
        {
            if (Request.Params["rst"] == null)
            {
                Response.Write("每次抽奖消耗10分");
                return;
            }
            switch (Request.Params["rst"].ToString())
            { 
                case "0":
                    Response.Write("这次运气不太好");
                    break;
                case "1":
                    Response.Redirect("lucky-gift.aspx");
                    break;
                case "-1":
                    Response.Write("积分不够了");
                    break;
            }
        }
        public void getGift()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] gifts;
            gifts = myFunctions.giftGet(Convert.ToInt32(Session["id"]), 7);
            foreach (Hashtable gift in gifts)
            {
                sb.Append("<li>在<span>" + Convert .ToDateTime( gift["date2"]).ToShortDateString() + "</span>获得了");
                sb.Append(gift["gift"]);
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}