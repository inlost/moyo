using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Xml;
using System.IO;
using System.Net;
namespace moyu.Api
{
    public partial class WeiXin
    {
        /// <summary>
        /// 获取新用户返回信息
        /// </summary>
        /// <param name="userData">用户信息</param>
        /// <returns>返回信息</returns>
        private Hashtable[] getNewUserRequestItems(Hashtable userData)
        {
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 2;
            rt[0]["title"] = "欢迎加入左邻大家庭，从此我们和你在一起";
            rt[0]["body"] = "左邻是定西人的微信平台\n关注左邻之后，你可以：\n1.足不出户知晓定西发生的新鲜事\n" +
                "2.随时随地查询定西的各种信息\n" +
                "3.和定西在微信上的朋友聊天交流，分享喜怒哀乐和你的见闻\n" +
                "4.认识更多趣味相投的朋友\n" +
                "5.每天签到抽奖，换取代金券，享受定西商家们为左邻会员提供的各种优惠，参与左邻举办的各种活动\n" +
                "……\n点击\"查看全文\"教你如何玩转左邻";
            rt[0]["picSmall"] = "http://www.ai0932.com/images/weixin/newUser.jpg";
            rt[0]["picBig"] = "http://www.ai0932.com/images/weixin/newUser.jpg";
            rt[0]["url"] = "http://www.ai0932.com/Mobile/help.aspx";
            rt[0]["orders"] = 90;
            return rt;
        }
        /// <summary>
        /// 获取未绑定用户的返回信息
        /// </summary>
        /// <param name="userData">用户信息</param>
        /// <returns>返回信息</returns>
        private Hashtable[] getBindRequestItems(Hashtable userData)
        {
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 2;
            rt[0]["title"] = "你还未完成左邻账号的绑定，请按照提示完成绑定";
            rt[0]["body"] = "绑定账号后，你就不用再每次重复登陆左邻了\n" +
                "绑定帮助：\n" +
                "1.点击本条消息进入注册登录页面\n" +
                "4.如果已经注册过左邻账号，直接登录即可绑定成功\n" +
                "5.如果没有注册过左邻账号，点击注册，完成注册后系统将自动绑定左邻账号\n" +
                "……\n点击\"查看全文\"进入注册登录页面";
            rt[0]["picSmall"] = "http://www.ai0932.com/images/weixin/bind.jpg";
            rt[0]["picBig"] = "http://www.ai0932.com/images/weixin/bind.jpg";
            rt[0]["url"] = "http://www.ai0932.com/mobile/login.aspx";
            rt[0]["orders"] = 90;
            return rt;
        }
        private Hashtable[] getImageRequestItems(Hashtable userData)
        {
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 1;
            rt[0]["title"] = "图片上传失败";
            rt[0]["body"] = "啊噢，图片上传失败了。。。\n" +
                "失败的具体原因是：";
            rt[0]["picSmall"] = getPicUrl(false);
            rt[0]["picBig"] = getPicUrl(true);
            rt[0]["url"] = "http://www.ai0932.com/mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1";
            rt[0]["orders"] = 0;
            WebClient wc = new WebClient();
            HttpWebResponse res;
            string folderPath=System.DateTime.Now.Year + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/";
            string savePath = HttpContext.Current.Server.MapPath("~/upload/userImages/" + folderPath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            res = (HttpWebResponse)WebRequest.Create(userData["@PicUrl"].ToString()).GetResponse();
            if (res.StatusCode != HttpStatusCode.OK)
            { 
                //链接不正常
                rt[0]["body"] = rt[0]["body"] + "服务器连接失败";
                return rt;
            }
            string contentType=res.ContentType.ToLower();
            if (contentType.IndexOf("image") == -1)
            { 
                //不是图片
                rt[0]["body"] = rt[0]["body"] + "你上传的好像不是图片";
                return rt;
            }
            string fileName="";
            if (contentType.IndexOf("png") > -1)
            {
                fileName = ".png";
            }
            else if (contentType.IndexOf(".gif") > -1)
            {
                fileName = ".gif";
            }
            else if (contentType.IndexOf("jpeg") > -1 || contentType.IndexOf("jpg") > -1)
            {
                fileName = ".jpg";
            }
            if (fileName.Length == 0)
            { 
                //未知图片格式
                rt[0]["body"] = rt[0]["body"] + "不支持的图片格式";
                return rt;
            }

            fileName = System.Guid.NewGuid().ToString("N") + fileName;
            wc.DownloadFile(userData["@PicUrl"].ToString(), savePath + fileName);
            int uid=getWeiUserId(userData["@FromUserName"].ToString());
            string imgUrl = "http://www.ai0932.com/upload/userImages/" + folderPath + fileName;
            Information.group myGroup = new Information.group();
            int tid = myGroup.topicNewByWeixin("爆照", ("我在" + System.DateTime.Now.ToShortTimeString() + "在左邻分享了一张照片"), -1, uid, "<img src=\"" + imgUrl + "\"/>");
            moyu.User.Functions myUser=new User.Functions();
            int pid = myUser.upLoadImg(uid, fileName, tid.ToString(), imgUrl);
            Hashtable[] rtSuccess = new Hashtable[3];
            rtSuccess[0] = new Hashtable();
            rtSuccess[0]["id"] = 0;
            rtSuccess[0]["messageType"] = 2;
            rtSuccess[0]["title"] = "成功分享了一张图片@" + System.DateTime.Now.ToShortTimeString() + "\n" +
                " 点这里去贴吧查看分享的图片";
            rtSuccess[0]["body"] = "图片分享成功";
            rtSuccess[0]["picSmall"] = getPicUrl(false);
            rtSuccess[0]["picBig"] = imgUrl;
            rtSuccess[0]["url"] = "http://www.ai0932.com/mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1";
            rtSuccess[0]["orders"] = 10;

            rtSuccess[1] = new Hashtable();
            rtSuccess[1]["id"] = 0;
            rtSuccess[1]["messageType"] = 2;
            rtSuccess[1]["title"] = "想让更多朋友看到？点击这里去分享到朋友圈";
            rtSuccess[1]["body"] = "想让更多朋友看到？点击这里去分享到朋友圈";
            rtSuccess[1]["picSmall"] = getPicUrl(false);
            rtSuccess[1]["picBig"] = getPicUrl(true);
            rtSuccess[1]["url"] = "http://www.ai0932.com/Mobile/post-show.aspx?type=g&id="+tid;
            rtSuccess[1]["orders"] = 8;

            rtSuccess[2] = new Hashtable();
            rtSuccess[2]["id"] = 0;
            rtSuccess[2]["messageType"] = 2;
            rtSuccess[2]["title"] = "为图片添加文字说明？点击这里去给图片添加文字说明";
            rtSuccess[2]["body"] = "为图片添加文字说明？点击这里去给图片添加文字说明";
            rtSuccess[2]["picSmall"] = getPicUrl(false);
            rtSuccess[2]["picBig"] = getPicUrl(true);
            rtSuccess[2]["url"] = "http://www.ai0932.com/mobile/addPicIntroduce.aspx?tid="+tid+"&pid="+pid;
            rtSuccess[2]["orders"] = 6;
            User.Functions myFunction = new User.Functions();
            myFunction.givePostPoint(uid, "发图积分", 2);
            return rtSuccess;
        }
        /// <summary>
        /// 分享信息
        /// </summary>
        /// <param name="userData">用户消息</param>
        /// <returns>返回信息</returns>
        private Hashtable[] funcShare(Hashtable userData)
        {
            string userMsg = userData["@body"].ToString().Replace("：", ":");
            string tag = userMsg.Split(':')[0];
            string body = userMsg.Substring(tag.Length + 1);
            Information.group myGroup = new Information.group();
            myGroup.topicNewByWeixin(tag, body.Substring(0, (body.Length > 25 ? 25 : body.Length)), -1, getWeiUserId(userData["@FromUserName"].ToString()), body);
            User.Functions myFunction = new User.Functions();
            myFunction.givePostPoint(getWeiUserId(userData["@FromUserName"].ToString()), "发帖积分", 2);
            if (tag == "秘密")
            {
                myFunction.userPointChange(getWeiUserId(userData["@FromUserName"].ToString()), -3, "发秘密消耗积分", 1);
            }
            if (HttpContext.Current.Cache["weixinRobotGroupPostKeywords"] != null)
            {
                Hashtable[] keyWords = (Hashtable[])HttpContext.Current.Cache["weixinRobotGroupPostKeywords"];
                bool hasThisTag = false;
                foreach (Hashtable keyWord in keyWords)
                {
                    if (keyWord["tag"].ToString().IndexOf(tag) > -1)
                    {
                        hasThisTag = true;
                    }
                }
                if (!hasThisTag)
                {
                    HttpContext.Current.Cache.Remove("weixinRobotGroupPostKeywords");
                }
            }
            return getRequestItem(userData);
        }
        /// <summary>
        /// 用户文字签到
        /// </summary>
        /// <param name="userData">用户信息</param>
        /// <returns>返回信息</returns>
        private Hashtable[] funcSigin(Hashtable userData)
        {
            moyu.User.Functions myFunctions = new User.Functions();
            int uid = getWeiUserId(userData["@FromUserName"].ToString());

            int siginId = myFunctions.signIn(uid);
            StringBuilder strBody = new StringBuilder();
            Hashtable points = new Hashtable();
            points = myFunctions.getPoint(uid);
            strBody.Append(myFunctions.isSigIn(uid) ? "签到成功，" : "签到失败，");
            strBody.Append("连续签到" + points["signInDays"] + "天，");
            strBody.Append("积分:" + points["point"] + ",");
            strBody.Append("贡献:" + points["contribute"] + " 。点我去首页");

            Hashtable[] rt = new Hashtable[2];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 2;
            rt[0]["title"] = strBody; 
            rt[0]["body"] = strBody;
            rt[0]["picSmall"] = getPicUrl(false);
            rt[0]["picBig"] = getPicUrl(true);
            rt[0]["url"] = "http://www.ai0932.com/mobile/index.aspx";
            rt[0]["orders"] = 90;

            rt[1] = new Hashtable();
            rt[1]["id"] = 0;
            rt[1]["messageType"] = 2;
            rt[1]["title"] = "今天心情怎么样？点这里记录每天的心情";
            rt[1]["body"] = "今天心情怎么样？点这里记录每天的心情";
            rt[1]["picSmall"] = getPicUrl(false);
            rt[1]["picBig"] = getPicUrl(true);
            rt[1]["url"] = "http://www.ai0932.com/mobile/addPicIntroduce.aspx?tid=" + siginId + "&pid=0";
            rt[1]["orders"] = 80;
            return rt;
        }
        /// <summary>
        /// 机器人教育
        /// </summary>
        /// <param name="userDate">用户信息</param>
        /// <returns>返回信息</returns>
        private Hashtable[] funcTeach(Hashtable userDate )
        {
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 2;
            rt[0]["title"] = "教左邻回答\"" + userDate["@body"].ToString() + "\"";
            rt[0]["body"] = "你刚说了\"" + userDate["@body"].ToString() + "\"，但是我现在还不知道该怎么回答这个问题……\n"+
                "教教我应该怎么回答好不好【撒娇】，\n"+
                "以后定西只要再有人和我这么说，我就照你教我的回答Ta。";
            rt[0]["picSmall"] = getPicUrl(false);
            rt[0]["picBig"] = "http://www.ai0932.com/images/weixin/teach.jpg";
            rt[0]["url"] = "http://www.ai0932.com/mobile/robot-teach.aspx?q="+ HttpUtility.UrlEncode(userDate["@body"].ToString()) ;
            rt[0]["orders"] = 90;
            Robot.Main myRobot = new Robot.Main();
            int uid = getWeiUserId(userDate["@FromUserName"].ToString());
            myRobot.questionAdd(userDate["@body"].ToString(), uid);
            Information.group myGroup = new Information.group();
            myGroup.topicNewByWeixin("求调教", "教左邻回答“" + userDate["@body"].ToString() + "”", -1, uid, "我刚对左邻说“<span style=\"color:red;\">" + userDate["@body"].ToString() + "</span>”但是她不知道该怎么回答我，<a href=\"robot-teach.aspx?q=" + HttpUtility.UrlEncode(userDate["@body"].ToString()) + "\">点击这里</a>去调教她！");
            return rt;
        }
        /// <summary>
        /// 获取社区今日文章条数
        /// </summary>
        /// <returns></returns>
        private Hashtable[] getTodayPostCount()
        {
            string strSql = "select count(id) as number from information_group_topic where gid =-1 and postDate > '" + System.DateTime.Now.AddDays(-1).ToString()+"'";
            int count = System.Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0]["number"]);
            if (count == 0)
            {
                return null;
            }
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 2;
            rt[0]["title"] = "今天社区有【" + count + "】件新鲜事儿，点我去围观一下";
            rt[0]["body"] = "今天社区有【" + count + "】件新鲜事儿，点我去围观一下";
            rt[0]["picSmall"] = getPicUrl(false);
            rt[0]["picBig"] = getPicUrl(true);
            rt[0]["url"] = "http://www.ai0932.com/mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1";
            rt[0]["orders"] = 0;
            return rt;
        }
        /// <summary>
        /// 合并条目
        /// </summary>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        /// <returns>合并后的条目</returns>
        private Hashtable[] joinItems(Hashtable[] group1, Hashtable[] group2)
        {
            int count = 0;
            if (group1 != null)
            {
                count += group1.Length;
            }
            if (group2 != null)
            {
                count += group2.Length;
            }
            Hashtable[] rt = new Hashtable[count];
            int index = 0;
            foreach (Hashtable item in group1)
            {
                rt[index] = item;
                index++;
            }
            foreach (Hashtable item in group2)
            {
                rt[index] = item;
                index++;
            }
            return rt;
        }
    }
}