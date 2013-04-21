using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class topic
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 论坛信息获取
        /// </summary>
        /// <param name="pid">论坛编号</param>
        /// <returns>论坛信息</returns>
        public Hashtable getPlate(int pid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@pid"] = pid;
            try
            {
                return moyu.Data.Type.dtToHash(myDb.GetQueryStro("information_plate_get", inQuery, "rt"))[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取指定板块的帖子总数
        /// </summary>
        /// <param name="cid">板块编号</param>
        /// <returns></returns>
        public int getChannelTopicCount(int cid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            try
            {
                return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_count_get", inQuery, "rt"))[0]["Column1"]);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 发表新帖子
        /// </summary>
        /// <param name="cid">频道编号</param>
        /// <param name="uid">用户编号</param>
        /// <param name="title">标题</param>
        /// <param name="body">正文</param>
        /// <returns>帖子编号</returns>
        public int addNew(int cid, int uid, string title, string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            inQuery["@uid"] = uid;
            inQuery["@topic_title"] = title;
            inQuery["@topic_body"] = body;
            try
            {
                return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_topic_add", inQuery, "rt"))[0]["Column1"]);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 帖子获取
        /// </summary>
        /// <param name="pid">帖子编号</param>
        /// <returns>帖子</returns>
        public Hashtable get(int pid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = pid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_topic_get", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 帖子获取
        /// </summary>
        /// <param name="cid">频道编号</param>
        /// <param name="last">最后一条编号</param>
        /// <param name="count">条数</param>
        /// <returns>帖子</returns>
        public Hashtable[] get(int cid, int last,int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            inQuery["@last"] = last;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_topic_list_get", inQuery, "rt"));
        }
        /// <summary>
        /// 获取评论条数
        /// </summary>
        /// <param name="tid">帖子编号</param>
        /// <returns>评论条数</returns>
        public int getCommentsCount(int tid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            try
            {
                return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_topic_comment_count_get", inQuery, "rt"))[0]["Column1"]);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 增加浏览次数
        /// </summary>
        /// <param name="tid">帖子编号</param>
        public void addShowTime(int tid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            myDb.ExecNoneQuery("information_topic_showTime_add", inQuery);
        }
        /// <summary>
        /// 添加平路
        /// </summary>
        /// <param name="tid">帖子编号</param>
        /// <param name="uid">用户编号</param>
        /// <param name="body">内容</param>
        /// <returns>成功失败</returns>
        public bool commentsAdd(int tid, int uid, string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            inQuery["@uid"] = uid;
            inQuery["@body"] = body;
            try
            {
                myDb.ExecNoneQuery("information_topic_comments_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 帖子评论获取
        /// </summary>
        /// <param name="tid">帖子编号</param>
        /// <returns>评论</returns>
        public Hashtable[] commentsGet(int tid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_topic_comment_get", inQuery, "rt"));
        }
        /// <summary>
        /// 分页id获取
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public Hashtable[] topicPage(int cid, short count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_topic_list_pageId_get", inQuery, "rt"));
        }
        /// <summary>
        /// 获取发布的图片
        /// </summary>
        /// <param name="pid">图片编号</param>
        /// <returns>图片信息</returns>
        public Hashtable getPostImgByPid(int pid)
        {
            string strSql = "select * from users_photos where id=" + pid;
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"))[0];
        }
    }
}