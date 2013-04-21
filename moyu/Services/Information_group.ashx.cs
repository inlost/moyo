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
            if (theContext.Request.Params["action"] != null)
            {
                string action = theContext.Request.Params["action"].ToString();
                if (action == "del")
                {
                    del();
                }
                if (action == "back")
                {
                    back();
                }
                if (action == "addElite")
                {
                    addElite();
                }
                if (action == "delElite")
                {
                    delElite();
                }
                if (action == "addTop")
                {
                    addTop();
                }
                if (action == "delTop")
                {
                    delTop();
                }
            }
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
                case "getMore":
                    getMore();
                    break;
                case "addPicPostIntroduce":
                    addPicPostIntroduce();
                    break;
                case "setAvatar":
                    setAvatar();
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
        private void addTop()
        {
            int tid = Convert.ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToBoolean(thePost["isTop"]) == false && Convert.ToInt32(theContext.Session["power"]) > 5)
            {
                    myGroup.topicSetTop(tid, true);
                    moyu.User.Functions myFunction = new moyu.User.Functions();
                    string comment = "该帖已被管理置顶";
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(thePost["uid"]), comment, -2, tid);//通知被置顶人
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                    myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid, comment);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void delTop()
        {
            int tid = Convert.ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToBoolean(thePost["isTop"]) == true && Convert.ToInt32(theContext.Session["power"]) > 5)
            {
                myGroup.topicSetTop(tid, false);
                moyu.User.Functions myFunction = new moyu.User.Functions();
                string comment = "该帖已被管理取消置顶";
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(thePost["uid"]), comment, -2, tid);//通知被置顶人
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid, comment);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void addElite()
        {
            int tid = Convert.ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToBoolean(thePost["isElite"])==false && Convert.ToInt32(theContext.Session["power"]) > 0)
            {
                if (Convert.ToInt32(theContext.Session["id"]) != Convert.ToInt32(thePost["uid"]))
                {
                    myGroup.topicSetElite(tid, true);
                    moyu.User.Functions myFunction = new moyu.User.Functions();
                    string comment = "该帖已被管理加为精华，您得到了一个贡献";
                    myFunction.userPointChange(Convert.ToInt32(thePost["uid"]), 1, "帖子加精华获得贡献", 2);
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(thePost["uid"]), comment, -2, tid);//通知被删帖人
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                    myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                    myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid, comment);
                }
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void delElite()
        {
            int tid = Convert.ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToBoolean(thePost["isElite"]) && Convert.ToInt32(theContext.Session["power"]) > 0)
            {
                myGroup.topicSetElite(tid, false);
                moyu.User.Functions myFunction = new moyu.User.Functions();
                string comment = "该帖已被管理取消精华，您扣除了一个贡献";
                myFunction.userPointChange(Convert.ToInt32(thePost["uid"]), -1, "帖子取消精华扣除贡献", 2);
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(thePost["uid"]), comment, -2, tid);//通知被删帖人
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid, comment);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void back()
        {
            int tid = Convert.ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToInt32(thePost["gid"]) == -10 && Convert.ToInt32(theContext.Session["power"]) > 0)
            {
                string comment = "您的帖子已于"+DateTime.Now.ToString()+"恢复。";
                moyu.User.Functions myFunction = new moyu.User.Functions();
                myFunction.userPointChange(Convert.ToInt32(thePost["uid"]), 2, "删帖恢复积分", 3);
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(thePost["uid"]), comment, -2, tid);//通知被删帖人
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid, comment);
                myGroup.updatePost(tid, thePost["body"].ToString(), thePost["title"].ToString(), thePost["tag"].ToString(), -1);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void del()
        {
            int tid = Convert .ToInt32(theContext.Request.Params["tid"]);
            Hashtable thePost = new Hashtable();
            thePost = myGroup.topicGetById(tid);
            if (Convert.ToInt32(thePost["gid"]) == -1 && Convert.ToInt32(theContext.Session["power"]) > 0)
            {
                string comment="您发表的帖子由于违反了贴吧发帖规范而被删除，为了给大家一个良好的社区环境，要自觉遵守贴吧发帖规范哦~如有疑问，请在本贴回复我。";
                moyu.User.Functions myFunction = new moyu.User.Functions();
                myFunction.userPointChange(Convert.ToInt32(thePost["uid"]), -2, "删帖回收积分", 3);
                myFunction.sendMessage(Convert .ToInt32(theContext.Session["id"]), Convert .ToInt32(thePost["uid"]), comment, -2, tid);//通知被删帖人
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), Convert.ToInt32(theContext.Session["id"]), comment, -2, tid);//通知操作管理
                myFunction.sendMessage(Convert.ToInt32(theContext.Session["id"]), 1, comment, -2, tid);//通知大管理
                myGroup.commentNew(Convert.ToInt32(theContext.Session["id"]), tid,comment );
                myGroup.updatePost(tid, thePost["body"].ToString(), thePost["title"].ToString(), thePost["tag"].ToString(), -10);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void addPicPostIntroduce()
        {
            int tid = Convert.ToInt32(theContext.Request.Form["tid"]);
            int pid = Convert.ToInt32(theContext.Request.Form["pid"]);
            Hashtable thePost = myGroup.topicGetById(tid);
            string introduce = theContext.Request.Form["introduce"];
            if (Convert.ToInt32(thePost["uid"]) == Convert.ToInt32(theContext.Session["id"]))
            {
                string body = "";
                if (pid == 0)
                {
                    body = "<span style=\"color:#F78000;font-weight:bold;\">" + DateTime.Now.ToShortDateString() + "</span>：<br/>" + introduce ;
                    if (Convert.ToInt32(thePost["gid"]) == -2)
                    {
                        moyu.User.Functions myFunctions = new moyu.User.Functions();
                        myFunctions.givePostPoint(Convert.ToInt32(thePost["uid"]), "签到心情奖励积分", 2);
                    }
                }
                else
                {
                    body = thePost["body"].ToString() + "<p>" + introduce + "</p>";
                }
                myGroup.updatePost(tid,body, thePost["title"].ToString(), thePost["tag"].ToString(),-1);
            }
            theContext.Response.Redirect("~/Mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1");
        }
        private void setAvatar()
        {
            Upload.Avatar myAvatar = new Upload.Avatar();
            Information.topic myTopic = new Information.topic();
            Hashtable picInfo = new Hashtable();
            int pid = Convert.ToInt32(theContext.Request.Form["pid"]);
            string newFileName = "A_" + System.Guid.NewGuid().ToString("N") + "_A" + pid + ".jpg";
            string folderPath=System.DateTime.Now.Year + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/";
            string savePath = HttpContext.Current.Server.MapPath("~/upload/userImages/" + folderPath);

            picInfo = myTopic.getPostImgByPid(pid);
            Images.MakeThumbnail(picInfo["img"].ToString(), savePath+newFileName, 320, 320, "Cut");

            myAvatar.newUpload(Convert.ToInt32(theContext.Session["id"]), 0, 0, 320, 320, "/upload/userImages/" + folderPath + newFileName);

            addPicPostIntroduce();
        }
        private void getMore()
        {
            string tag;
            Hashtable[] posts;
            int last = Convert.ToInt32(theContext.Request.Form["last"]);
            switch (theContext.Request.Form["type"].ToString())
            {
                case "tag":
                    tag = HttpUtility.UrlDecode(theContext.Request.Params["tag"].ToString());
                    posts = myGroup.postGetByTat(tag, last, 15);
                    break;
                case "group":
                    tag = theContext.Request.Params["tag"];
                    posts = myGroup.topicGet(Convert.ToInt32(tag), last, 15);
                    break;
                case "user":
                    tag = theContext.Session["niceName"].ToString() + "发表的内容";
                    posts = myGroup.postGetByUser(Convert.ToInt32(theContext.Session["id"]), last, 15);
                    break;
                case "atMe":
                    tag = "@" + theContext.Session["niceName"].ToString();
                    posts = myGroup.postGetByMessage(Convert.ToInt32(theContext.Session["id"]), last, 15);
                    break;
                case "elite":
                    tag = "精华帖子";
                    posts = myGroup.postGetByElite(last, 15);
                    break;
                default:
                    posts=null;
                    break;
            }
            StringBuilder sb = new StringBuilder();
            string userName = "";
            Hashtable userPoint = new Hashtable();
            moyu.User.Functions myFunction = new moyu.User.Functions();
            string bodyClass = "";
            int power = Convert.ToInt32(theContext.Session["power"]);
            moyu.Functions myOtherFunction = new Functions();
            foreach (Hashtable p in posts)
            {
                userPoint = myFunction.getPoint(Convert.ToInt32(p["uid"]));
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
                sb.Append("<span class=\"left group-post-info-tag group_tag_" + p["id"].ToString().Substring(p["id"].ToString().Length - 1) + "\">" + p["tag"] + "</span>");
                sb.Append("<span class=\"left group-post-info-user\">");
                sb.Append(userName);
                sb.Append("</span><span class=\"right group-post-info-date\">");
                sb.Append(myOtherFunction.kindTime(Convert.ToDateTime(p["postDate"])));
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
            theContext.Response.Write(sb);
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
            if (gid == -1 && theContext.Cache["mobilePostHome"] != null)
            {
                theContext.Cache.Remove("mobilePostHome");
            }
            theContext.Response.Write(myGroup.topicNew(tag, title, gid, uid, body));
        }
        private void commentNew()
        {
            int uid =theContext.Session["id"]!=null? Convert.ToInt32(theContext.Session["id"]):0;
            int tid = Convert.ToInt32(theContext.Request.Form["tid"]);
            string comment = theContext.Request.Form["comment"];
            Hashtable thePost = new Hashtable();
            moyu.User.Functions myFunction = new moyu.User.Functions();
            thePost = myGroup.topicGetById(tid);
            int atUid = Convert.ToInt32(theContext.Request.Form["atUid"]);
            if (Convert.ToInt32(thePost["uid"]) != atUid && atUid != 0)
            {
                myFunction.sendMessage(uid, atUid, comment, -2, tid);
            }
            if (Convert.ToInt32(thePost["uid"]) != 0 && uid != Convert.ToInt32(thePost["uid"]))
            {
                myFunction.sendMessage(uid, Convert.ToInt32(thePost["uid"]), comment, -2, tid);
            }
            myGroup.commentNew(uid, tid, comment);
            if (Convert.ToInt32(thePost["gid"]) == -1 && uid!=0)
            {
                myFunction.givePostPoint(uid, "回复积分", 1);
            }
            if ( Convert.ToInt32( thePost["gid"]) == -1 && theContext.Cache["mobilePostHome"] != null)
            {
                theContext.Cache.Remove("mobilePostHome");
            }
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
                sb.Append("<span class=\"commentM_i_at right\"  data-uid=\"" + comment["uid"].ToString() + "\" data-name=\"" + (comment["uid"].ToString() == "0" ? "匿名网友" : comment["niceName"].ToString()) + "\"><a href=\"javascript:void(0);\">" + (comment["uid"].ToString() == "9" ? "" : "回复") + "</a></span>");
                sb.Append("</div>");
                sb.Append("<div class=\"comment_body_holder\">" + comment["comment"] + "</div>");
                sb.Append("</div>");
                sb.Append("</li>");
            }
            theContext.Response.Write(sb);
        }
    }
}