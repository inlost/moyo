using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
namespace moyu.Markets.forum
{
    public partial class GamePlay : System.Web.UI.Page
    {
        private Information.Game myGame = new Information.Game();
        private Information.topic myTopic = new Information.topic();
        int last, cid, gameCat,gameLast,nowLast;
        private Hashtable thePlate = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            last = Request.Params["last"] == null ? 0 : Convert.ToInt32(Request.Params["last"]);
            cid = Convert.ToInt32(Request.Params["cid"]);
            gameCat = (Request.Params["gameCat"] == null ? 0 : Convert.ToInt32(Request.Params["gameCat"]));
            thePlate = myTopic.getPlate(cid);
            gameLast = (Request.Params["gameLast"] == null ? 0 : Convert.ToInt32(Request.Params["gameLast"]));
        }
        public void getHotGames()
        {
            StringBuilder sb = new StringBuilder();
            if (Cache["hotGame"] == null)
            {
                Hashtable[] games = myGame.getHotGame(18);
                foreach (Hashtable game in games)
                {
                    sb.Append("<li class=\"left\"><a class=\"jump\" href=\"/" + game["m_name"] + "_游戏盒子_定西吧_沁辰左邻/Markets---forum---GameShow@aspx/gid=" + game["m_id"] + "\" title=\"" + game["m_name"] + "\" data-dst=\"Markets/forum/GameShow.aspx?gid=" + game["m_id"] + "\">");
                    sb.Append("<img alt=\"" + game["m_name"] + "\" src=\"" + game["m_pic"] + "\" />");
                    sb.Append("<h4>" + game["m_name"] + "</h4>");
                    sb.Append("</a></li>");
                }
                Cache.Insert("hotGame", sb, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            Response.Write(Cache["hotGame"]);
        }
        public void getTopicList()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] topics;
            topics = myTopic.get(cid, last, 20);
            string topicTitle = "";
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            foreach (Hashtable topic in topics)
            {
                topicTitle = topic["topic_title"].ToString().Length > 20 ? getStr(topic["topic_title"].ToString(), 45, "…") : topic["topic_title"].ToString();
                sb.Append("<li><a href=\"/" + topic["topic_title"] + "—定西吧_沁辰左邻/Markets---forum---TopicShow@aspx/name=" + thePlate["name"] + "&id=" + topic["id"] + "&last=" + last + "\" data-dst=\"Markets/forum/TopicShow.aspx?id=" + topic["id"] + "&last=" + last + "\" class=\"jump\" title=\"" + topic["topic_title"].ToString() + "\">" + topicTitle + "</a></li>");
            }
            Response.Write(sb);
        }
        public void listCat()
        {
            Hashtable[] cats = myGame.catGet();
            StringBuilder sb = new StringBuilder();
            sb.Append("<li" + (gameCat == 0 ? " class=\"gameActCat\"" : "") + "><a class=\"gameJump\" href=\"全部游戏_游戏吧_定西吧_沁辰左邻/Markets---forum---GamePlay@aspx/cid=8&name=游戏盒子\" data-cat=\"0\" data-last=\"0\">全部</a></li>");
            foreach (Hashtable cat in cats)
            {
                sb.Append("<li" + (cat["m_id"].ToString()==gameCat.ToString() ? " class=\"gameActCat\"" : "") + ">");
                sb.Append("<a class=\"gameJump\" href=\"/" + cat["m_name"] + "_游戏吧_定西吧_沁辰左邻/Markets---forum---GamePlay@aspx/cid=8&name=游戏盒子&gameCat=" + cat["m_id"] + "\" title=\"" + cat["m_name"] + "下的所有游戏\" data-cat=\"" + cat["m_id"] + "\" data-last=\"0\">");
                sb.Append(cat["m_name"]);
                sb.Append("</a></li>");
            }
            Response.Write(sb);
        }
        public void listGames()
        {
            if (Cache["listGames-"+gameCat+"-"+gameLast] == null)
            {
                Hashtable[] games = myGame.gamesGet(gameCat, 28, gameLast);
                StringBuilder sb = new StringBuilder();
                foreach (Hashtable game in games)
                {
                    sb.Append("<li class=\"left\"><a class=\"jump\" href=\"/" + game["m_name"] + "_游戏盒子_定西吧_沁辰左邻/Markets---forum---GameShow@aspx/gid=" + game["m_id"] + "\" title=\"" + game["m_name"] + "\" data-dst=\"Markets/forum/GameShow.aspx?gid=" + game["m_id"] + "\" data-gid=\"" + game["m_id"] + "\">");
                    sb.Append("<img alt=\"" + game["m_name"] + "\" src=\"" + game["m_pic"] + "\" />");
                    sb.Append("<h4>" + game["m_name"] + "</h4>");
                    sb.Append("</a></li>");
                }
                Cache.Insert("listGames-"+gameCat+"-"+gameLast, sb, null, DateTime.Now.AddDays(7), TimeSpan.Zero);
                Cache.Insert("nowLast-" + gameCat + "-" + gameLast, games[games.Count() - 1]["m_id"], null, DateTime.Now.AddDays(7), TimeSpan.Zero);
            }
            nowLast = Convert.ToInt32(Cache["nowLast-" + gameCat + "-" + gameLast]);
            Response.Write(Cache["listGames-"+gameCat+"-"+gameLast]);
        }
        public void getLast()
        {
            Response.Write(nowLast);
        }
        public void getCat()
        {
            Response.Write(gameCat);
        }
        public static string getStr(string s, int l, string endStr)
        {
            string temp = s.Substring(0, (s.Length < l) ? s.Length : l);

            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= l - endStr.Length)
                {
                    return temp + endStr;
                }
            }
            return endStr;
        }
    }
}