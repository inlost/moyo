using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
namespace moyu.Mobile.forum
{
    public partial class topic_list : System.Web.UI.Page
    {
        Information.topic myTopic = new Information.topic();
        int last, cid, thisPageLast;
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
            Response.Write(cid);
        }
        public void getTopicList()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] topics;
            topics = myTopic.get(cid, last, 30);
            int comments;
            string topicTitle = "";
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            string uName = "";

            thisPageLast = topics.Count() > 0 ? Convert.ToInt32(topics[topics.Count() - 1]["id"]) : 0;
            foreach (Hashtable topic in topics)
            {
                topicTitle = topic["topic_title"].ToString().Length > 12 ? getStr(topic["topic_title"].ToString(), 32, "…") : topic["topic_title"].ToString();
                comments = myTopic.getCommentsCount(Convert.ToInt32(topic["id"]));
                if (Convert.ToInt32(topic["uid"]) == 0)
                {
                    uName = "匿名网友";
                }
                else
                {
                    theUser = myUser.get(Convert.ToInt32(topic["uid"]));
                    uName = theUser["niceName"].ToString();
                }
                sb.Append("<li class=\"topic_item clear\">");
                sb.Append("<a href=\"/Mobile/post-show.aspx?type=t&id="+topic["id"]+"\">");
                sb.Append("<div class=\"commentsInfo left\">");
                sb.Append("<div class=\"commentsInfo_count\">" + comments.ToString() + "</div>");
                sb.Append("<div class=\"commentsInfo_time\">" + Convert .ToDateTime( topic["lastUpdate"] ).ToShortTimeString()+ "</div>");
                sb.Append("</div>");
                sb.Append("<div class=\"topic_item_body left\">");
                sb.Append("<div class=\"topic_item_body_title\">");
                sb.Append(topic["topic_title"]);
                sb.Append("</div>");
                sb.Append("<div class=\"topic_item_body_author\">");
                sb.Append(uName);
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</a>");
                sb.Append("</li>");
            }
            Response.Write(sb);
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