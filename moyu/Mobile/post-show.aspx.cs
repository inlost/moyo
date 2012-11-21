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
    public partial class post_show : System.Web.UI.Page
    {
        string type = "";
        Hashtable thePost = new Hashtable();
        moyu.Information.topic myTopic = new Information.topic();
        moyu.Information.group myGroup = new Information.group();
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.Params["type"];
            if (type == "t")
            {
               Information.topic myTopic = new Information.topic();
               thePost = myTopic.get(Convert.ToInt32(Request.Params["id"]));
            }
            else if (type == "g")
            {
                Information.group myGroup = new Information.group();
                thePost = myGroup.topicGetById(Convert.ToInt32(Request.Params["id"]));
            }
        }
        public void getTid()
        {
            Response.Write(thePost["id"]);
        }
        public void getTitle()
        {
            if (type == "t")
            {
                Response.Write(thePost["topic_title"]);
            }
            else if (type == "g")
            {
                Response.Write(thePost["title"]);
            }
        }
        public void getTime()
        {
            if (type == "t")
            {
                Response.Write(thePost["topic_date"]);
            }
            else if (type == "g")
            {
                Response.Write(thePost["postDate"]);
            }
        }
        public void getContent()
        {
            if (type == "t")
            {
                Response.Write(thePost["topic_body"].ToString().Replace("src=\"upload/images", "src=\"http://www.ai0932.com/upload/images"));
            }
            else if (type == "g")
            {
                Response.Write(thePost["body"]);
            }
        }
        public void getComment()
        {
            Hashtable[] comments;
            comments=(type=="t"?myTopic.commentsGet( Convert.ToInt32(thePost["id"])):myGroup.commentGet(Convert.ToInt32(thePost["id"])));
            StringBuilder sb = new StringBuilder();
            int index = 1;
            string uName = "";
            moyu.User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            foreach (Hashtable comment in comments)
            {
                if (Convert.ToInt32(comment["uid"]) == 0)
                {
                    uName = "匿名网友";
                }
                else
                {
                    theUser = myUser.get(Convert.ToInt32(thePost["uid"]));
                    uName = theUser["niceName"].ToString();
                }
                sb.Append("<li class=\"commentItem\">");
                sb.Append("<div class=\"commentItemBody\">");
                sb.Append(type == "t" ? comment["body"].ToString() : comment["comment"].ToString());
                sb.Append("</div>");
                sb.Append("<div class=\"commentItemInfo\">");
                sb.Append("<span>"+index+"楼 </span>");
                sb.Append("<span> "+uName+"</span>");
                sb.Append("<span> " + (type == "t" ? comment["date"] : comment["postDate"]) + "</span>");
                sb.Append("</div>");
                sb.Append("</li>");
                index++;
            }
            Response.Write(sb);
        }
    }
}