using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
namespace moyu.Markets.Informations
{
    public partial class TopicList : System.Web.UI.Page
    {
        Information.topic myTopic = new Information.topic();
        int last,cid;
        private Hashtable thePlate = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            last = Request.Params["last"] == null ? 0 : Convert.ToInt32(Request.Params["last"]);
            cid = Convert.ToInt32(Request.Params["cid"]);
            thePlate = myTopic.getPlate(cid);
            if (Session["isLogin"] == null)
            {
                Session["isLogin"] = "false";
            }

        }
        public void getCid()
        {
            Response.Write(Request .Params ["cid"]);
        }
        public void getName()
        {
            Response.Write(Request.Params["name"] + (Convert .ToInt32( thePlate["needLogin"])==0?" 【可匿名板块】":" 【不可匿名板块】"));
        }
        public void getTopicCount(int cid)
        {
            Response.Write(myTopic.getChannelTopicCount(cid));
        }
        public void getPar()
        {
            Response.Write("cid="+Request .Params["cid"]+"&name="+Request .Params ["name"]);
        }
        public void getTopicList()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] topics;
            topics = myTopic.get(cid, last, 20);
            int comments;
            string topicTitle = "";
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            string uName="";
            foreach (Hashtable topic in topics)
            {
                topicTitle = topic["topic_title"].ToString().Length > 12 ? getStr(topic["topic_title"].ToString(), 26,"…")  : topic["topic_title"].ToString();
                sb.Append("<tr  data-tid=\"" + topic["id"] + "\">");
                sb.Append("<th><a href=\"javascript:void(0);\" data-dst=\"Markets/forum/TopicShow.aspx?id=" + topic["id"] + "&last=" + last + "\" class=\"jump\" title=\"" + topic["topic_title"].ToString() + "\">" + topicTitle + "</a></th>");
                if(Convert .ToInt32( topic["uid"])==0)
                {
                    uName="匿名网友";
                }
                else
                {
                    theUser=myUser.get(Convert .ToInt32(topic["uid"]));
                    uName=theUser["niceName"].ToString();
                }
                sb.Append("<th>"+uName+"</th>");
                sb.Append("<th>" + topic["showTime"] + "</th>");
                comments = myTopic.getCommentsCount(Convert.ToInt32(topic["id"]));
                sb.Append("<th>"+(comments==0?"":comments.ToString())+"</th>");
                sb.Append("<th class=\"t_t_l_table_lastUpdate\">" + Convert.ToDateTime(topic["lastUpdate"]).ToShortDateString().Replace("2012/","") +" "+ Convert.ToDateTime(topic["lastUpdate"]).ToShortTimeString() + "</th>");
                sb.Append("</tr>");
            }
            Response.Write(sb);
        }
        public void getMoreLink()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a id=\"loadMoreTopic\" href=\"#\">点击查看更多帖子</a>");
            Response.Write(sb);
        }
        private int findIndex(Hashtable[] ids, int page)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                if (page.ToString() == ids[i]["id"].ToString()) { return i; }
            }
            return 0;
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
        public void isNeedLogin()
        {
            if (Convert.ToInt32(thePlate["needLogin"]) == 1 && Session["isLogin"].ToString() != "true")
            {
                Response.Write("needLogin");
            }
            else
            {
                Response.Write("jump");
            }
        }
    }
}