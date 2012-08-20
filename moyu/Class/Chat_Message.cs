using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Threading;
using System.Runtime.Serialization;
namespace moyu.ChatMessage
{
    /// <summary>
    /// 聊天消息全局存储类
    /// </summary>
    public class globalCache
    {
        public static ArrayList userCache =new ArrayList();
        public static ArrayList messagePool = new ArrayList();
        public static DateTime dbMessageLastUpdate = new DateTime();
    }
    /// <summary>
    /// 消息结构
    /// </summary>
    [DataContract]
    public class messageType
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string message { get; set; }
        [DataMember(Order = 1, IsRequired = true)]
        public string source { get; set; }
        [DataMember(Order = 2, IsRequired = true)]
        public string sourceName { get; set; }
        [DataMember(Order = 3, IsRequired = true)]
        public int type { get; set; }
        [DataMember(Order = 4, IsRequired = true)]
        public string time { get; set; }
        [DataMember(Order = 5, IsRequired = true)]
        public int onlineCount { get; set; }
        [DataMember(Order = 6, IsRequired = true)]
        public string id { get; set; }
    }
    /// <summary>
    /// 异步请求处理类
    /// </summary>
    public class MessageTask
    {
        public ManualResetEvent waitEvent;//WaitHandle，用于阻塞当前请求，等待新消息通知
        public ChatNowDelegate _chatNow;//通知的回调方法
        public string _guid;//用户编号
        public string _lastSource;//最后收到的消息源
        public string _lastSourceName;//消息源名称
        public string _lastMessage;//最后收到的消息
        public int _messageType;//消息类型

        private WaitCallback _proc = null;//异步执行的实际方法指针/委托
        private HttpContext _context;//当前请求上下文

        public delegate void ChatNowDelegate(string source, string sourceName,string msg,int messageType);//回调消息通知的委托。
        public MessageTask(HttpContext context)
        {
            _context = context;
            waitEvent = new ManualResetEvent(false);//默认为阻塞，等待wait方法
        }

        //异步页面的任务启动方法
        public IAsyncResult OnBegin(object sender, EventArgs e,
            AsyncCallback cb, object extraData)
        {
            _proc = new WaitCallback(ChatInvokeProc);
            Hashtable theUser = new Hashtable();
            theUser = (Hashtable)extraData;
            string user = theUser["guid"].ToString();
            _guid = user;
            Thread.CurrentThread.Name = "上下文线程" + user;
            //用户处理，不存在则增加，即为登录
            theUser["asyn"] = this;
            theUser["lastUpdateTime"] = DateTime.Now.ToString();
            Hashtable feachUser = new Hashtable();
            bool isInCach=false;
            for (var i = 0; i < globalCache.userCache.Count; i++)
            {
                feachUser = (Hashtable)globalCache.userCache[i];
                if (theUser["guid"].ToString() == feachUser["guid"].ToString())
                {
                    globalCache.userCache[i] = theUser;
                    isInCach = true;
                }
            }
            if (!isInCach)
            {
                globalCache.userCache.Add(theUser);
            }
            //开始异步执行，这里会开启一个新的辅助线程
            return _proc.BeginInvoke(extraData, cb, extraData);
        }

        //异步页面的任务结束方法
        public void OnEnd(IAsyncResult ar)
        {
            //推送系统消息
            ChatMessage.message myMessage = new ChatMessage.message();
            myMessage.updateMessagePool();
            Hashtable nowCheck = new Hashtable();
            for (var i = 0; i < ChatMessage.globalCache.messagePool.Count; i++)
            {
                nowCheck = (Hashtable)ChatMessage.globalCache.messagePool[i];
                if (nowCheck["messageTo"].ToString() ==this._guid)
                {
                    this._lastMessage = nowCheck["message"].ToString();
                    this._lastSource = nowCheck["messageFrom"].ToString();
                    this._lastSourceName = "系统";
                    this._messageType = Convert.ToInt32(nowCheck["type"]);
                    this._chatNow(_lastSource, _lastSourceName, _lastMessage, _messageType);
                    myMessage.setMessageReaded(Convert.ToInt32(nowCheck["id"]));
                    ChatMessage.globalCache.messagePool.RemoveAt(i);
                    return;
                }
            }
            //放弃异步执行
            _proc.EndInvoke(ar);
        }

        //如果异步任务执行超时的对应处理，即页面中的asynctimeout设置的时间到达且任务尚未返回。
        public void OnTimeout(IAsyncResult ar)
        {
            //应该发条应答，偷懒了。呵呵
        }
        private void ChatInvokeProc(object param)
        {
            Thread.CurrentThread.Name = "异步线程" + param.ToString();
            if (waitEvent.WaitOne(6 * 1000))//阻塞线程20秒，或等到wait的释放
            {
                if (_lastSource != string.Empty && _lastMessage != string.Empty)
                {
                    if (_chatNow != null)
                        _chatNow(_lastSource,_lastSourceName, _lastMessage,_messageType);//回调页面方法，页面继续执行
                }
            }
            else
            {
                _lastSource = string.Empty;
                _lastMessage = string.Empty;
                _lastSourceName = string.Empty;
                _messageType = -1;
            }
        }
    }
}