using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Services
{
    public partial class Message_Comet : System.Web.UI.Page
    {
        private Data.Db myDb = new Data.Db();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["action"] != null)
            {
                switch (Request.Form["action"])
                { 
                    case "loop":
                        startComet();
                        break;
                }
            }
        }
        protected void startComet()
        {
            ChatMessage.MessageTask myMessageTask = new ChatMessage.MessageTask(this.Context);
            myMessageTask._chatNow = ChatNow;
            string uid = Request.Form["uid"];
            Hashtable theUser = new Hashtable();
            theUser["guid"] = Session["guid"];
            theUser["niceName"] = Session["niceName"].ToString();
            PageAsyncTask async = new PageAsyncTask(new BeginEventHandler(myMessageTask.OnBegin), new EndEventHandler(myMessageTask.OnEnd), new EndEventHandler(myMessageTask.OnTimeout), theUser);
            Page.RegisterAsyncTask(async);
            Page.ExecuteRegisteredAsyncTasks();//异步执行
        }
        private void ChatNow(string source,string sourceName,string msg,int messageType)
        {
            ChatMessage.messageType theMessage = new ChatMessage.messageType();
            theMessage.message = msg;
            theMessage.source = source;
            theMessage.sourceName = sourceName;
            theMessage.time = DateTime.Now.ToString();
            theMessage.type = messageType;
            theMessage.onlineCount = ChatMessage.globalCache.userCache.Count;
            theMessage.id = Guid.NewGuid().ToString("N");
            Random ran = new Random();
            theMessage.onlineCount += ran.Next(0, 30);
            Response .Write (Data.Type.objToJson(theMessage));
        }
    }
}