using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using moyu.Data;
using System.Collections;
namespace moyu.ChatMessage
{
    public class message
    {
        private Db myDb = new Db();
        /// <summary>
        /// 将消息设置成已读
        /// </summary>
        /// <param name="mid">消息编号</param>
        public void setMessageReaded(int mid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@mid"] = mid;
            myDb.ExecNoneQuery("message_read_set", inQuery);
        }
        public void updateMessagePool()
        {
            if (ChatMessage.globalCache.dbMessageLastUpdate.AddMinutes(1) < DateTime.Now)
            {
                string strSql = "select * from user_message where isRead=0 order by id desc";
                Hashtable[] messages;
                messages = Data.Type.dtToHash( myDb.GetQuerySql(strSql, "rt"));
                bool isInPool = false;
                foreach (Hashtable message in messages)
                {
                    foreach (Hashtable msg in ChatMessage.globalCache.messagePool)
                    {
                        if (msg["id"].ToString() == message["id"].ToString())
                        {
                            isInPool = true;
                            break;
                        }
                    }
                    if (!isInPool)
                    {
                        ChatMessage.globalCache.messagePool.Add(message);
                    }
                }
                ChatMessage.globalCache.dbMessageLastUpdate = DateTime.Now;
            }
        }
    }
}