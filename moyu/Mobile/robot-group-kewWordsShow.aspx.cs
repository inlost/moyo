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
    public partial class robot_group_kewWordsShow : System.Web.UI.Page
    {
        private string tag;
        private Information.group myGroup = new Information.group();
        Hashtable[] posts;
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.Params["type"].ToString())
            { 
                case "tag":
                    tag = HttpUtility.UrlDecode (Request.Params["tag"].ToString());
                    posts = myGroup.postGetByTat(tag,0,10);
                    break;
                case "group":
                    tag = Request.Params["tag"];
                    posts = myGroup.topicGet(Convert.ToInt32(tag), 0,10);
                    break;
                case "user":
                    tag = Session["niceName"].ToString() + "发表的内容";
                    posts = myGroup.postGetByUser(Convert.ToInt32(Session["id"]), 0, 10);
                    break;
            }
        }
        public void getType()
        {
            Response.Write(Server.HtmlEncode(Request.Params["type"]));
        }
        public void getTag()
        {
            Response.Write(tag);
        }
        public void getTitle()
        {
            Response.Write(tag=="-1"?"大家正在说":tag);
        }
        public void getTime()
        {
            Response.Write(DateTime.Now);
        }
        public void getContent()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Hashtable p in posts)
            {
                sb.Append("<li data-id=\""+p["id"]+"\" class=\"postItem ui-body ui-body-c\">");
                sb.Append("<h2 class=\"group-post-info clear\" style=\"text-indent:0;\">");
                sb.Append("<span class=\"left group-post-info-tag group_tag_"+p["id"].ToString().Substring(p["id"].ToString().Length-1)+"\">" + p["tag"] + "</span>");
                sb.Append("<span class=\"left group-post-info-user\">");
                sb.Append((p["niceName"].ToString()==""?"匿名用户":p["niceName"].ToString()));
                sb.Append("</span>");
                sb.Append("</h2>");
                sb.Append(p["body"].ToString().Replace("src=\"upload/images", "src=\"http://www.ai0932.com/upload/images"));
                sb.Append("<div class=\"group-post-functions\">");
                sb.Append("<a class=\"viewComments\" href=\"javascript:void(0)\">评论(" + p["commentsCount"] + ")</a>");
                sb.Append("<a class=\"shareThisItem\" href=\"javascript:void(0)\" onclick=\"weixinShare('" + p["title"] + "','真是太有意思啦','http://www.ai0932.com/Mobile/post-show.aspx?type=g&id=" + p["id"] + "');\">分享</a>");
                sb.Append("<a class=\"collectItem\" href=\"javascript:void(0)\">收藏</a>");
                sb.Append("<a class=\"goTop\" target=\"_self\"  href=\"#\">回顶部</a>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}