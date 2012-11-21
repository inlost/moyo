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
    /// Information_group 的摘要说明
    /// </summary>
    public class Information_group : IHttpHandler, IRequiresSessionState
    {
        private HttpContext theContext;
        private Information.group myGroup = new Information.group();
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
            switch (theContext.Request.Form["action"].ToString())
            {
                case "group_new":
                    groupNew();
                    break;
                case "join_group_noNeed":
                    joinGroupNoNeed();
                    break;
                case "topic_new":
                    topicNew();
                    break;
                case "commentNew":
                    commentNew();
                    break;
                case "commentGet":
                    commentGet();
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
        public void groupNew()
        {
            int rst = 0;
            if (theContext.Session["isLogin"] != null && theContext.Session["isLogin"].ToString() == "true")
            {
                string name = theContext.Request.Form["newGroup_name"];
                int uid = Convert.ToInt32(theContext.Session["id"]);
                string introduce = theContext.Request.Form["newGroup_introduce"];
                int joinType =Convert .ToInt32( theContext.Request.Form["newGroup_joinType"]);
                if (theContext.Request.Files.Count != 0)
                {
                    moyu.Upload.image myImage = new Upload.image();
                    myImage.UploadFileSize = 1;
                    myImage.IsAddWaterMark = false;
                    myImage.Minheight = 60;
                    myImage.Minwidth = 60;
                    myImage.Suowidth = 80;
                    myImage.IsRate = true;
                    DateTime dt = DateTime.Now;
                    string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
                    myImage.Upload(uploadPath, theContext.Request.Files[0]);
                    if (name.Trim().Length == 0 || introduce.Trim().Length == 0 || myImage.IsSuccess == false)
                    {
                        rst = 0;
                    }
                    else
                    {
                        rst = myGroup.group_apply(name, uid, introduce, joinType, uploadPath + myImage.TFullName);
                    }
                }
                else
                {
                    rst = 0;
                }
            }
            else
            {
                rst = 0;
            }
            theContext.Response.Write("<script>window.parent.moyo.Group.applyBack("+rst+");</script>");
        }
        private void joinGroupNoNeed()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int gid = Convert.ToInt32(theContext.Request.Form["gid"]);
            myGroup.joinGroupNoNeed(uid, gid);
        }
        private void topicNew()
        {
            string tag = theContext.Request.Form["tag"].ToString().Trim();
            string title = theContext.Request.Form["title"];
            string body = theContext.Request.Form["body"].ToString().Replace("\n", "</p><p>");
            body = "<p>" + body + "</p>";
            int gid = Convert.ToInt32(theContext.Request.Form["gid"]);
            int uid = Convert.ToInt32(theContext.Session["id"]);
            theContext.Response.Write(myGroup.topicNew(tag, title, gid, uid, body));
        }
        private void commentNew()
        {
            int uid = Convert.ToInt32(theContext.Session["id"]);
            int tid = Convert.ToInt32(theContext.Request.Form["tid"]);
            string comment = theContext.Request.Form["comment"];
            myGroup.commentNew(uid, tid, comment);
        }
        private void commentGet()
        {
            Hashtable[] comments;
            comments = myGroup.commentGet( Convert .ToInt32( theContext.Request.Form["tid"]));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string icon = "";
            int iFloor = 0;
            foreach (Hashtable comment in comments)
            {
                iFloor++;
                icon = comment["uid"].ToString() == "0" ? "/Images/avatar/avatar-64_64.jpg" : comment["avatar"].ToString().Replace("320_320", "64_64");
                sb.Append("<li class=\"clearfix\">");
                sb.Append("<img src=\"" + icon + "\" class=\"left\"/>");
                sb.Append("<div class=\"left commentMain\">");
                sb.Append("<div class=\"commentM_info clearfix\">");
                sb.Append("<span class=\"commentM_i_name left\">" + (comment["uid"].ToString() == "0" ? "匿名网友" : comment["niceName"].ToString()) + "</span>");
                sb.Append("<span class=\"commentM_i_time left\">" + Convert.ToDateTime(comment["postDate"]).GetDateTimeFormats('f')[0] + "</span>");
                sb.Append("<span class=\"commentM_i_floor right\">#" + iFloor + "</span>");
                sb.Append("</div>");
                sb.Append("<div class=\"comment_body_holder\">" + comment["comment"] + "</div>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            theContext.Response.Write(sb);
        }
    }
}