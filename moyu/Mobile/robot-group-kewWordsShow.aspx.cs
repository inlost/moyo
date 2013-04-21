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
        int power = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["power"] != null && Convert.ToInt32(Session["power"]) > 0)
            {
                power = Convert.ToInt32(Session["power"]);
            }
            switch (Request.Params["type"].ToString())
            { 
                case "tag":
                    tag = HttpUtility.UrlDecode (Request.Params["tag"].ToString());
                    posts = myGroup.postGetByTat(tag,0,15);
                    break;
                case "group":
                    tag = Request.Params["tag"];
                    posts = myGroup.topicGet(Convert.ToInt32(tag), 0, 15);
                    break;
                case "user":
                    tag = Session["niceName"].ToString() + "发表的内容";
                    posts = myGroup.postGetByUser(Convert.ToInt32(Session["id"]), 0, 15);
                    break;
                case "atMe":
                    tag = "@"+Session["niceName"].ToString();
                    posts = myGroup.postGetByMessage(Convert.ToInt32(Session["id"]), 0, 15);
                    break;
                case "elite":
                    tag = "精华帖子";
                    posts = myGroup.postGetByElite(0, 15);
                    break;
            }
        }
        public void getTabClass(string btn)
        {
            if (Request.Params["type"].ToString() == "group" && btn=="all")
            {
                Response.Write("active");
            }
            else if (Request.Params["type"].ToString() == "atMe" && btn == "atMe")
            {
                Response.Write("active");
            }
            else if (Request.Params["type"].ToString() == "user" && btn == "user")
            {
                Response.Write("active");
            }
            else if (Request.Params["type"].ToString() == "elite" && btn == "elite")
            {
                Response.Write("active");
            }
            else
            {
                Response.Write("normal");
            }
        }
        public void setMessageRead()
        {
            if (Request.Params["type"].ToString() == "atMe")
            {
                myGroup.setAtMessageRead(Convert.ToInt32(Session["id"]));
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
            string userName = "";
            Hashtable userPoint=new Hashtable();
            moyu.User.Functions myFunction=new User.Functions();
            string bodyClass = "";
            moyu.Functions myOtherFunction = new Functions();
            foreach (Hashtable p in posts)
            {
                userPoint=myFunction.getPoint( Convert .ToInt32(p["uid"]));
                if (p["tag"].ToString() == "秘密")
                {
                    userName = "就不告诉你";
                }
                else
                {
                    userName = (p["niceName"].ToString() == "" ? "匿名用户" : p["niceName"].ToString());
                    userName = "<a href=\"weixin://profile/" + p["weixinId"] + "\" class=\"" + (Convert.ToInt32(userPoint["signInDays"]) > 15 ? "red" : "") + "\">" + userName + "</a>";
                }
                bodyClass = Convert.ToBoolean(p["isElite"]) ? "f" : "c";
                bodyClass = p["tag"].ToString() == "秘密" ? "e" : bodyClass;
                sb.Append("<li data-id=\"" + p["id"] + "\" class=\"postItem ui-body ui-body-" + bodyClass + "\">");
                sb.Append("<h2 class=\"group-post-info clear\" style=\"text-indent:0;\">");
                if (Convert.ToBoolean(p["isElite"]))
                {
                    sb.Append("<span class=\"left group-post-info-tag group_tag_4\">精华</span>");
                }
                if (Convert.ToBoolean(p["isTop"]))
                {
                    sb.Append("<span class=\"left group-post-info-tag group_tag_9\">置顶</span>");
                }
                sb.Append("<span class=\"left group-post-info-tag group_tag_"+p["id"].ToString().Substring(p["id"].ToString().Length-1)+"\">" + p["tag"] + "</span>");
                sb.Append("<span class=\"left group-post-info-user\">");
                sb.Append(userName);
                sb.Append("</span><span class=\"right group-post-info-date\">");
                sb.Append( myOtherFunction.kindTime(Convert.ToDateTime( p["postDate"])));
                sb.Append("</span></h2>");
                sb.Append(p["body"].ToString().Replace("src=\"upload/images", "src=\"http://www.ai0932.com/upload/images"));
                sb.Append("<div class=\"group-post-functions\">");
                sb.Append("<a class=\"viewComments\" href=\"javascript:void(0)\">评论(" + p["commentsCount"] + ")</a>");
                sb.Append("<a class=\"shareThisItem\" href=\"javascript:void(0)\" onclick=\"weixinShare('" + p["title"] + "','真是太有意思啦','http://www.ai0932.com/Mobile/post-show.aspx?type=g&id=" + p["id"] + "');\">分享</a>");
                if (power > 0)
                {
                    if (Convert.ToInt32(p["gid"]) == -10)
                    {
                        sb.Append("<a target=\"_self\"  href=\"../Services/Information_group.ashx?action=back&tid=" + p["id"] + "\">恢复</a>");
                    }
                    else
                    {
                        sb.Append("<a target=\"_self\"  href=\"../Services/Information_group.ashx?action=del&tid=" + p["id"] + "\">删</a>");
                    }
                    if (Convert.ToBoolean(p["isElite"]))
                    {
                        sb.Append("<a target=\"_self\"  href=\"../Services/Information_group.ashx?action=delElite&tid=" + p["id"] + "\">消精</a>");
                    }
                    else
                    {
                        sb.Append("<a target=\"_self\"  href=\"../Services/Information_group.ashx?action=addElite&tid=" + p["id"] + "\">加精</a>");
                    }
                }
                if (power > 5)
                {
                    if (Convert.ToBoolean(p["isTop"]))
                    {
                        sb.Append("<a class=\"goTop\" target=\"_self\"  href=\"../Services/Information_group.ashx?action=delTop&tid=" + p["id"] + "\">消顶</a>");
                    }
                    else
                    {
                        sb.Append("<a class=\"goTop\" target=\"_self\"  href=\"../Services/Information_group.ashx?action=addTop&tid=" + p["id"] + "\">置顶</a>");
                    }
                }
                else
                {
                    sb.Append("<a class=\"goTop\" target=\"_self\"  href=\"#\">顶部</a>");
                }
                sb.Append("</div>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}