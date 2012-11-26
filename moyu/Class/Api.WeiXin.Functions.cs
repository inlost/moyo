using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
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
            rt[0]["url"] = "http://www.ai0932.com/Mobile/post-show.aspx?type=t&id=602";
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
            myFunctions.signIn(uid);
            StringBuilder strBody = new StringBuilder();
            Hashtable points = new Hashtable();
            points = myFunctions.getPoint(uid);
            strBody.Append (myFunctions.isSigIn(uid) ? "签到成功，" : "签到失败，");
            strBody .Append("已经连续签到" + points["signInDays"] + "天，");
            strBody.Append("积分:" + points["point"] + ",");
            strBody.Append("贡献:" + points["contribute"] + "。\n\n");
            strBody .Append( "<a href=\""+getUserUrl(userData,"http://www.ai0932.com/mobile/index.aspx")+"\">点这里去首页</a>\n\n");
            strBody .Append( "<a href=\""+getUserUrl(userData,"http://www.ai0932.com/mobile/lucky.aspx")+"\">点这里去抽奖处</a>\n\n");
            strBody.Append("<a href=\"" + getUserUrl(userData, "http://www.ai0932.com/mobile/robot-group-kewWordsShow.aspx?type=group&tag=-1") + "\">点这里去贴吧</a>");
            Hashtable[] rt = new Hashtable[1];
            rt[0] = new Hashtable();
            rt[0]["id"] = 0;
            rt[0]["messageType"] = 1;
            rt[0]["title"] = "用户签到";
            rt[0]["body"] = strBody;
            rt[0]["picSmall"] = getPicUrl(false);
            rt[0]["picBig"] = getPicUrl(true);
            rt[0]["url"] = getUserUrl(userData, "http://www.ai0932.com/mobile/signIn.aspx");
            rt[0]["orders"] = 90;
            return rt;
        }
    }
}