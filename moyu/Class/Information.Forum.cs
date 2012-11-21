using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class Forum
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 获取贴吧全局统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public Hashtable globalInfoGet()
        {
            Hashtable inQuery=new Hashtable();
            return Data.Type.dtToHash(myDb.GetQueryStro("forum_info_get",inQuery,"rt"))[0];
        }
        /// <summary>
        /// 贴吧最新帖子获取
        /// </summary>
        /// <param name="count">要获取的条数</param>
        /// <returns>帖子们</returns>
        public Hashtable[] forumTopicGet(int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("forum_topic_new", inQuery, "rt"));
        }
        /// <summary>
        /// 热门帖子获取
        /// </summary>
        /// <param name="count">条数</param>
        /// <returns>帖子们</returns>
        public Hashtable[] forumTopicHotGet(int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("forum_topic_hot", inQuery, "rt"));
        }
    }
}