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
    }
}