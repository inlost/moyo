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
    /// Information_Game 的摘要说明
    /// </summary>
    public class Information_Game : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Information.Game myGame = new Information.Game();
        private int gameCat, gameLast;
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/html";
            if (theContext.Request.Form["action"] == null)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            switch (context.Request.Form["action"].ToString())
            {
                case "loadGame":
                    gameLoad();
                    break;
            }

            context.Response.End();
        }
        private void gameLoad()
        {
            StringBuilder sb = new StringBuilder();
            gameCat = (theContext.Request.Params["gameCat"] == null ? 0 : Convert.ToInt32(theContext.Request.Params["gameCat"]));
            gameLast = (theContext.Request.Params["gameLast"] == null ? 0 : Convert.ToInt32(theContext.Request.Params["gameLast"]));
            Hashtable[] games = myGame.gamesGet(gameCat, 28, gameLast);
            foreach (Hashtable game in games)
            {
                sb.Append("<li class=\"left\"><a class=\"jump\" href=\"/" + game["m_name"] + "_游戏盒子_定西吧_沁辰左邻/Markets---forum---GameShow@aspx/gid=" + game["m_id"] + "\" title=\"" + game["m_name"] + "\" data-dst=\"Markets/forum/GameShow.aspx?gid=" + game["m_id"] + "\" data-gid=\"" + game["m_id"] + "\">");
                sb.Append("<img alt=\"" + game["m_name"] + "\" src=\"" + game["m_pic"] + "\" />");
                sb.Append("<h4>" + game["m_name"] + "</h4>");
                sb.Append("</a></li>");
            }
            theContext.Response.Write(sb);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}