using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
namespace moyu.Services
{
    /// <summary>
    /// 信息港帖子服务
    /// </summary>
    public class Information_Topic : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        Information.topic myTopic = new Information.topic();
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
                case "addNew":
                    addNew();
                    break;
                case "topicNew_mobile":
                    topicNew_mobile();
                    break;
                case "commentsNew":
                    commentsNew();
                    break;
                case "commentsNew_mobile":
                    commentsNew_mobile();
                    break;
                case "moreTopic":
                    getMoreTopic();
                    break;
                case "commentsGet":         
                    commentsGet();
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
        private void addNew()
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = theContext.Request.Form["cid"].ToString();
            inQuery["@uid"] = (theContext.Session["isLogin"] == null || theContext.Session["isLogin"].ToString() != "true") ? "0" : theContext.Session["id"].ToString();
            inQuery["@topic_title"] = theContext.Request.Form["title"].ToString();
            inQuery["@topic_body"] = theContext.Request.Form["body"].ToString();
            boolRst myRst = new boolRst();
            myRst.message=myTopic.addNew(Convert.ToInt32(inQuery["@cid"]), Convert.ToInt32(inQuery["@uid"]), inQuery["@topic_title"].ToString(), inQuery["@topic_body"].ToString()).ToString();
            myRst.rst = myRst.message == "0" ? false : true;
            theContext.Response.Write(Data.Type.objToJson(myRst));
        }
        private void topicNew_mobile()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int cid = Convert.ToInt32(theContext.Request.Form["cid"]);
            string title = theContext.Request.Form["title"];
            string body=theContext.Request.Form["body"].ToString().Replace("\n", "</p><p>");;
            body = "<p>" + body + "</p>";
            if (theContext.Request.Files.Count > 0)
            { 
                string img=avatarUp();
                body = (img.Length > 0 ? ("<img src=\""+img+"\"/>") : "") + body;
            }
            myTopic.addNew(cid, uid, title, body);
            theContext.Response.Redirect("~/Mobile/topic-list.aspx?cid="+cid);
        }
        private string avatarUp()
        {
            Upload.image myImage = new Upload.image();
            myImage.IsSuoImg = false;
            myImage.UploadFileSize = 2;
            myImage.IsAddWaterMark = false;
            myImage.Minwidth = 50;
            myImage.Minheight = 50;
            myImage.Maxwidth = 3000;
            myImage.Maxheight = 3000;
            myImage.IsUseRandFileName = true;
            myImage.IsRarPic = true;
            myImage.Rarwidth = 320;
            DateTime dt = DateTime.Now;
            string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
            myImage.Upload(uploadPath, theContext.Request.Files[0]);
            if (myImage.IsSuccess)
            {
                return uploadPath.Replace("\\", "/") + myImage.OFullName;
            }
            else
            {
                return "";
            }
        }
        private void commentsNew()
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = theContext.Request.Form["tid"].ToString();
            inQuery["@uid"] = (theContext.Session["isLogin"] == null || theContext.Session["isLogin"].ToString() != "true") ? "0" : theContext.Session["id"].ToString();
            inQuery["@body"] = theContext.Request.Form["body"].ToString().Replace("\n", "<br/>");
            boolRst myRst = new boolRst();
            myRst.rst = myTopic.commentsAdd(Convert.ToInt32(inQuery["@tid"]), Convert.ToInt32(inQuery["@uid"]), inQuery["@body"].ToString());
            theContext.Response.Write(Data.Type.objToJson(myRst));
        }
        private void commentsNew_mobile()
        {
            int tid = Convert .ToInt32( theContext.Request.Form["tid"]);
            int uid = (theContext.Session["isLogin"] == null || theContext.Session["isLogin"].ToString() != "true") ? 0 : Convert .ToInt32( theContext.Session["id"].ToString());
            string body = theContext.Request.Form["body"];
            myTopic.commentsAdd(tid, uid, body);
            theContext.Response.Redirect("~/Mobile/post-show.aspx?type=t&id=" + tid + "#comment");
        }
        private void getMoreTopic()
        {
            int cid = Convert.ToInt32(theContext.Request.Form["cid"]);
            int last = Convert.ToInt32(theContext.Request.Form["last"]);
            StringBuilder sb = new StringBuilder();
            Hashtable[] topics;
            topics = myTopic.get(cid, last, 10);
            int comments;
            string topicTitle = "";
            foreach (Hashtable topic in topics)
            {
                topicTitle = topic["topic_title"].ToString().Length > 12 ? getStr(topic["topic_title"].ToString(), 26, "…") : topic["topic_title"].ToString();
                sb.Append("<tr class=\"newLoad\"  data-tid=\"" + topic["id"] + "\">");
                sb.Append("<th><a href=\"javascript:void(0);\" data-dst=\"Markets/Informations/TopicShow.aspx?id=" + topic["id"] + "&last=" + last + "\" class=\"jump\" title=\"" + topic["topic_title"].ToString() + "\">" + topicTitle + "</a></th>");
                sb.Append("<th>匿名网友</th>");
                sb.Append("<th>" + topic["showTime"] + "</th>");
                comments = myTopic.getCommentsCount(Convert.ToInt32(topic["id"]));
                sb.Append("<th>" + (comments == 0 ? "" : comments.ToString()) + "</th>");
                sb.Append("<th class=\"t_t_l_table_lastUpdate\">" + Convert.ToDateTime(topic["lastUpdate"]).ToShortDateString().Replace("2012/", "") + " " + Convert.ToDateTime(topic["lastUpdate"]).ToShortTimeString() + "</th>");
                sb.Append("</tr>");
            }
            theContext. Response.Write(sb);
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
        private void commentsGet()
        {
            StringBuilder sb = new StringBuilder();
            Hashtable[] comments;
            comments = myTopic.commentsGet(Convert.ToInt32(theContext.Request.Form["pid"]));
            int i = 1;
            moyu.User.Web myUser = new moyu.User.Web();
            Hashtable theUser = new Hashtable();
            string uName = "";
            foreach (Hashtable comment in comments)
            {
                sb.Append("<div class=\"comment\">");
                sb.Append("<ul class=\"comment_info clearfix\">");
                sb.Append("<li><span class=\"comment_floor\">" + i + "</span> F</li>");
                if (Convert.ToInt32(comment["uid"]) == 0)
                {
                    uName = "匿名网友";
                }
                else
                {
                    theUser = myUser.get(Convert.ToInt32(comment["uid"]));
                    uName = theUser["niceName"].ToString();
                }
                sb.Append("<li>" + uName + "</li>");
                sb.Append("<li>发表于：<span class=\"topic_date\">" + comment["date"] + "</span></li>");
                sb.Append("</ul>");
                sb.Append("<div class=\"comment_body\">" + comment["body"] + "</div>");
                sb.Append("<img class=\"usersAvatar\" src=\"" + (uName == "匿名网友" ? "/Images/avatar.png" : theUser["avatar"].ToString().Replace("320_320", "64_64")) + "\"/>");
                sb.Append("</div>");
                i++;
            }
            theContext.Response.Write(sb);
        }
    }
}