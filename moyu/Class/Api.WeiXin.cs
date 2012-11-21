using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Xml;
using System.IO;
namespace moyu.Api
{
    public class WeiXin
    {
        private const string token = "t3pwPIPwr2Fo";
        private moyu.Data.Db myDb = new Data.Db();
        /// <summary>
        /// 签名检查
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <returns>成功失败</returns>
        public bool checkSignature(string signature, string timestamp, string nonce)
        {
            string[] tmpArr = new string[3]{token,timestamp,nonce.ToString()};
            Array.Sort(tmpArr);
            string tmpStr = string.Join("", tmpArr);
            tmpStr = BitConverter.ToString( SHA1.Create().ComputeHash(Encoding.Default.GetBytes(tmpStr))).Replace("-", "");
            return tmpStr == signature.ToUpper() ? true : false;
        }
        /// <summary>
        /// 添加回复规则
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="messageType">消息类型1文字2图文</param>
        /// <param name="title">标题</param>
        /// <param name="body">介绍</param>
        /// <param name="picSmall">小图</param>
        /// <param name="picBig">大图</param>
        /// <param name="url">链接地址</param>
        /// <param name="order">排序</param>
        /// <returns>规则编号</returns>
        public int addRule(int uid, int messageType, string title, string body, string picSmall, string picBig, string url, int order)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@messageType"] = messageType;
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            inQuery["@picSmall"] = picSmall;
            inQuery["@picBig"] = picBig;
            inQuery["@url"] = url;
            inQuery["@orders"] = order;
            return Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("weixin_robot_rules_add", inQuery, "rt"))[0]["id"]);
        }
        /// <summary>
        /// 关键字绑定回复规则
        /// </summary>
        /// <param name="keyWords">关键字数组</param>
        /// <param name="ruleId">规则编号</param>
        public void addKeyWord(string[] keyWords, int ruleId)
        {
            Hashtable inQuery=new Hashtable();
            inQuery["@rulesId"]=ruleId;
            foreach (string keyWord in keyWords)
            {
                inQuery["@keyWord"] = keyWord;
                myDb.ExecNoneQuery("weixin_robot_rules_keyWords_add", inQuery);
            }
        }
        /// <summary>
        /// 用户信息获取
        /// </summary>
        /// <param name="data">信息XML</param>
        /// <returns>信息Hashtable</returns>
        public Hashtable getRequestData(StreamReader data)
        {
            XmlTextReader xReader = new XmlTextReader(data);
            Hashtable msg = new Hashtable();
            while (xReader.Read())
            {
                if (xReader.Name == "ToUserName")
                {
                    msg["@ToUserName"]=xReader.ReadString();
                }
                else if (xReader.Name == "FromUserName")
                {
                    msg["@FromUserName"] = xReader.ReadString();
                }
                else if (xReader.Name == "CreateTime")
                {
                    msg["@CreateTime"] = xReader.ReadString();
                }
                else if (xReader.Name == "MsgType")
                {
                    msg["@MsgType"] = xReader.ReadString();
                }
                else if (xReader.Name == "Content")
                {
                    msg["@body"] = xReader.ReadString();
                }
            }
            try
            {
                if (msg["@MsgType"].ToString() == "text")
                {
                    myDb.ExecNoneQuery("message_weiXin_add_txtMessage", msg);
                }
            }
            catch (Exception e)
            {
                debug("", "", "", "error", e.Message);
            }
            return msg;
        }
        /// <summary>
        /// 获取消息回复
        /// </summary>
        /// <param name="data">用户消息</param>
        /// <returns>回复消息</returns>
        public string getResponse(Hashtable data)
        {
            string strTpl = "";
            Hashtable[] items;
            try
            {
                strTpl=getMsgType(data);
                debug("", "", "", "strTpl", strTpl);
                if (strTpl == "share")
                {
                    items = getFunctionRequtstItems(data,strTpl);
                }
                else if (strTpl == "newUser")
                {
                    items = getNewUserRequestItems(data);
                }
                else if (strTpl == "notBind")
                {
                    items = getBindRequestItems(data);
                }
                else
                {
                    items = getRequestItem(data);
                }
            }
            catch (Exception e)
            {
                items = null;
                debug("", "", "", "error", e.Message);
            }

            if (items.Count() == 1 && items[0]["messageType"].ToString() == "1")
            {
                strTpl= getMsgTpl(data, items[0]["body"].ToString(), 0);
            }
            else if (items.Count() == 0)
            {
                strTpl= getMsgTpl(data, "你刚说了:\"" + data["@body"] +
                    "\"？但是我现在还不知道该怎么回答这个问题……\n教教我该怎么回答好不好？" +
                "<a href=\""+getUserUrl(data, "http://www.ai0932.com/mobile/robot-teach.aspx?q=" + HttpUtility.UrlEncode(data["@body"].ToString())) + "\">点这里教我</a>。\n" +
                "以后定西只要再有人和我这么说，我就照你教我的回答Ta。", 0);
            }
            else
            {
                strTpl= getMsgTpl(data, items, 0);
            }
            return strTpl;
        }
        /// <summary>
        /// 返回消息内容生成
        /// </summary>
        /// <param name="userMsg">用户消息</param>
        /// <param name="msg">要发送的文字消息</param>
        /// <param name="isMark">是否添加星标</param>
        /// <returns>返回消息内容</returns>
        private string getMsgTpl(Hashtable userMsg,string msg,int isMark)
        {
            return @"<xml>
                        <ToUserName><![CDATA["+userMsg["@FromUserName"]+@"]]></ToUserName>
						<FromUserName><![CDATA["+userMsg["@ToUserName"]+@"]]></FromUserName>
						<CreateTime>"+converttotimestamp()+@"</CreateTime>
						<MsgType><![CDATA[text]]></MsgType>
						<Content><![CDATA["+msg+@"]]></Content>
						<FuncFlag>"+isMark+@"<FuncFlag>
                        </xml>";
        }
        /// <summary>
        /// 返回消息内容生成
        /// </summary>
        /// <param name="userMsg">用户消息</param>
        /// <param name="imgMsgs">图文消息们</param>
        /// <param name="isMark">是否添加星标</param>
        /// <returns>返回消息内容</returns>
        private string getMsgTpl(Hashtable userMsg, Hashtable[] imgMsgs, int isMark)
        {
            string strTp = @"  <xml>
                            <ToUserName><![CDATA[" + userMsg["@FromUserName"] + @"]]></ToUserName>
                            <FromUserName><![CDATA[" + userMsg["@ToUserName"] + @"]]></FromUserName>
                            <CreateTime>" + converttotimestamp ()+ @"</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <Content><![CDATA[]]></Content>
                            <ArticleCount>" + imgMsgs.Count() + @"</ArticleCount>
                            <Articles>";
            int index = 1;
            foreach (Hashtable imgMsg in imgMsgs)
            {
                strTp += @"<item>
                 <Title><![CDATA[" + imgMsg["title"] + @"]]></Title>
                 <Discription><![CDATA[" + imgMsg["body"] + @"]]></Discription>
                 <PicUrl><![CDATA[" + (index == 1 ? imgMsg["picBig"].ToString() : imgMsg["picSmall"].ToString()) + @"]]></PicUrl>
                 <Url><![CDATA[" + getUserUrl(userMsg, imgMsg["url"].ToString()) + @"]]></Url>
                 </item>";
                index++;
            }
            strTp += @" </Articles>
                         <FuncFlag>"+isMark+@"</FuncFlag>
                        </xml>";
            return strTp;
        }
        /// <summary>
        /// 获取用户特定的url
        /// </summary>
        /// <param name="userMsg">用户消息</param>
        /// <param name="url">原url</param>
        /// <returns>特定url</returns>
        public string getUserUrl(Hashtable userMsg, string url)
        {
            if (url.IndexOf("?") > -1)
            {
                return url += "&wu=" + userMsg["@FromUserName"];
            }
            else
            {
                return url += "?wu=" + userMsg["@FromUserName"];
            }
        }
        /// <summary>
        /// 获取需要返回的条目
        /// </summary>
        /// <param name="userMsg">用户信息</param>
        /// <returns>需要返回的条目</returns>
        private Hashtable[] getRequestItem(Hashtable userMsg)
        {
            Hashtable[] keyWords;
            ArrayList items = new ArrayList();
            ///关键字索引检查
            if (HttpContext.Current.Cache["weixinRobotKeywords"] == null)
            {
                string strSql = "select * from weixin_robot_rules_keyWords";
                keyWords = moyu.Data.Type.dtToHash( myDb.GetQuerySql(strSql, "rt"));
                HttpContext.Current.Cache.Insert("weixinRobotKeywords", keyWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            keyWords = (Hashtable[])HttpContext.Current.Cache["weixinRobotKeywords"];
            foreach (Hashtable key in keyWords)
            {
                if (userMsg["@body"].ToString().IndexOf(key["keyWord"].ToString()) > -1)
                {
                    key["type"] = "robotKey";
                    items.Add(key);
                }
            }
            //手机圈子文章检查
            if (HttpContext.Current.Cache["weixinRobotGroupPostKeywords"] == null)
            {
                string strSql = "Select id,tag From information_group_topic Where id In (Select Min(id) From information_group_topic Group By tag) and tag<>''";
                keyWords = moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
                HttpContext.Current.Cache.Insert("weixinRobotGroupPostKeywords", keyWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            keyWords = (Hashtable[])HttpContext.Current.Cache["weixinRobotGroupPostKeywords"];
            foreach (Hashtable key in keyWords)
            {
                if (userMsg["@body"].ToString().IndexOf(key["tag"].ToString()) > -1)
                {
                    key["type"] = "groupKey";
                    items.Add(key);
                }
            }
            return responseIdToItem(userMsg, items);
        }
        private Hashtable[] responseIdToItem(Hashtable userMsg, ArrayList ids)
        {
            ArrayList tpItems = new ArrayList();
            string strSql = "select * from weixin_robot_rules where id in(";
            int rbKeyIndex=1;
            foreach (Hashtable id in ids)
            {
                if (id["type"].ToString() == "robotKey")
                {
                    strSql += (rbKeyIndex == 1 ? id["rulesId"].ToString() : ("," + id["rulesId"]));
                    rbKeyIndex++;
                }
                else if (id["type"].ToString() == "groupKey")
                {
                    Hashtable theGpItem = new Hashtable();
                    theGpItem["id"] = 0;
                    theGpItem["messageType"] = 2;
                    theGpItem["title"] = "点这里查看这两天所有人分享过的关于" + id["tag"] + "的内容";
                    theGpItem["body"] = "这里是这几天大家分享的关于" + id["tag"] + "的内容。\n如果你也想跟大家分享你的" + id["tag"] + ",就给左邻发消息“" + id["tag"] + "：内容”，所有人就会收到你分享的" + id["tag"] + "了。\n中间有个冒号！！~主人切记。\n我退下了。";
                    theGpItem["picSmall"] = getPicUrl(false);
                    theGpItem["picBig"] = getPicUrl(true);
                    theGpItem["url"] = getUserUrl( userMsg,"http://www.ai0932.com/mobile/robot-group-kewWordsShow.aspx?tag="+HttpUtility.UrlEncode( id["tag"].ToString()));
                    theGpItem["orders"] = -80;
                    tpItems.Add(theGpItem);
                }
            }
            if (rbKeyIndex == 1)
           {
                strSql+="-1";
           }
           strSql+=" ) order by orders desc";
           Hashtable[] rbItems;//规则返回的条目
           rbItems = moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
            Hashtable[] rtItems = new Hashtable[rbItems.Count() + tpItems.Count];//需要返回的条目
            rbKeyIndex = 0;
            foreach (Hashtable rbItem in rbItems)
            {
                rtItems[rbKeyIndex] = rbItem;
                rbKeyIndex++;
            }
            foreach (Hashtable rgItem in tpItems)
            {
                rtItems[rbKeyIndex] = rgItem;
                rbKeyIndex++;
            }
            return rtItems;
        }
        /// <summary>
        /// 判断消息是否为功能性消息
        /// </summary>
        /// <param name="userMsg">用户消息</param>
        /// <returns>是否</returns>
        private string getMsgType(Hashtable userMsg)
        {
            string strMsg = userMsg["@body"].ToString();
            if (strMsg == "Hello2BizUser")
            {
                return "newUser";
            }
            else if(getWeiUserId(userMsg["@FromUserName"].ToString())==0)
            {
                return "notBind";
            }
            else if (strMsg.IndexOf(":") > -1 || strMsg.IndexOf("：") > -1)
            {
                if (strMsg.IndexOf(":") < 4 || strMsg.IndexOf("：") < 4)
                {
                    return "share";
                }
            }
            return "normal";
        }
        /// <summary>
        /// 消息随机配图
        /// </summary>
        /// <param name="isBig">是否是大图</param>
        /// <returns>图片地址</returns>
        public string getPicUrl(bool isBig)
        {
            Random rd = new Random();
            if (isBig)
            {
                return "http://www.ai0932.com/images/weixin/big/" + rd.Next(1, 20) + ".jpg";
            }
            else
            {
                return "http://www.ai0932.com/images/weixin/small/" + rd.Next(1, 14) + ".png";
            }
        }
        /// <summary>
        /// 用户信息功能性处理
        /// </summary>
        /// <param name="userData">用户信息</param>
        /// <returns>返回条目</returns>
        private Hashtable[] getFunctionRequtstItems(Hashtable userData,string type)
        {
            switch (type)
            { 
                case "share":
                    return funcShare(userData);
            }
            return null;
        }
        private Hashtable[] funcShare(Hashtable userData)
        {
            string userMsg = userData["@body"].ToString().Replace("：",":");
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
            rt[0]["body"] = "左邻是定西人的微信平台\n关注左邻之后，你可以：\n1.足不出户知晓定西发生的新鲜事\n"+
                "2.随时随地查询定西的各种信息\n"+
                "3.和定西在微信上的朋友聊天交流，分享喜怒哀乐和你的见闻\n"+
                "4.认识更多趣味相投的朋友\n"+
                "5.每天签到抽奖，换取代金券，享受定西商家们为左邻会员提供的各种优惠，参与左邻举办的各种活动\n"+
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
        /// 获取微信用户的左邻编号
        /// </summary>
        /// <param name="userName">微信名</param>
        /// <returns>编号</returns>
        public int getWeiUserId(string userName)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@weixinId"]=userName;
            Hashtable[] rt = moyu.Data.Type.dtToHash(myDb.GetQueryStro("weixin_user_getId", inQuery, "rt"));
            if (rt .Length== 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(rt[0]["id"]);
            }
        }
        private void debug(string signature, string timestamp, string nonce, string tmpStr, string other)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@signature"] = signature;
            inQuery["@timestamp"] = timestamp;
            inQuery["@nonce"] = nonce;
            inQuery["@tmpStr"] = tmpStr;
            inQuery["@other"] = other;
            myDb.ExecNoneQuery("wx_debug_insert", inQuery);
        }

        private DateTime converttimestamp(double timestamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
        private double converttotimestamp()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            return Convert.ToDouble( timeStamp.Substring(0, timeStamp.Length - 7));
        }
    }
}