using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class group
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 创建新圈子
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="uid">创建者id</param>
        /// <param name="introduce">介绍</param>
        /// <param name="joinType">加入类型</param>
        /// <param name="img">图标</param>
        /// <returns>新建圈子编号</returns>
        public int group_apply(string name,int uid,string introduce,int joinType,string img)
        {
            Hashtable inQuery=new Hashtable();
            inQuery["@name"]=name;
            inQuery["@adminUid"]=uid;
            inQuery["@introduce"]=introduce;
            inQuery["@joinType"]=joinType;
            inQuery["@img"]=img;
            inQuery["@createUid"] = uid;
            inQuery["@adminUid"] = uid;
            return Convert .ToInt32( Data.Type.dtToHash( myDb.GetQueryStro("information_group_new",inQuery,"rt"))[0]["id"]);
        }
        /// <summary>
        /// 小组获取
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>小组们</returns>
        public Hashtable[] group_get(int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_get", inQuery, "rt"));
        }
        /// <summary>
        /// 小组详情获取
        /// </summary>
        /// <param name="gid">小组编号</param>
        /// <returns>小组详情</returns>
        public Hashtable group_get_byId(int gid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@gid"] = gid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_get_byId", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 判断用户是否在小组中
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="gid">小组编号</param>
        /// <returns>结果</returns>
        public bool isInGroup(int uid, int gid)
        {
            Hashtable inQuery = new Hashtable();
            Hashtable theGroup = new Hashtable();
            theGroup = group_get_byId(gid);
            if (theGroup["adminUid"].ToString() == uid.ToString()) { return true; }
            inQuery["@uid"] = uid;
            inQuery["@gid"] = gid;
            return (Convert .ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_group_isInGroup",inQuery,"rt"))[0]["rst"])>0?true:false);
        }
        /// <summary>
        /// 加入无需申请的小组
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="gid">小组编号</param>
        public void joinGroupNoNeed(int uid, int gid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@gid"] = gid;
            myDb.ExecNoneQuery("information_group_join_noNeed", inQuery);
        }
        /// <summary>
        /// 发表新话题
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="title">标题</param>
        /// <param name="gid">小组编号</param>
        /// <param name="uid">用户编号</param>
        /// <param name="body">正文</param>
        /// <returns>话题编号</returns>
        public int topicNew(string tag,string title,int gid,int uid,string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tag"] = tag;
            inQuery["@title"] = title;
            inQuery["@gid"] = gid;
            inQuery["@uid"] = uid;
            inQuery["@body"] = body;
            if (!isInGroup(uid, gid))
            {
                return 0;
            }
            return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_new", inQuery, "rt"))[0]["tid"]);
        }
        public int topicNewByWeixin(string tag, string title, int gid, int uid, string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tag"] = tag;
            inQuery["@title"] = title;
            inQuery["@gid"] = gid;
            inQuery["@uid"] = uid;
            inQuery["@body"] = body;
            return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_new", inQuery, "rt"))[0]["tid"]);
        }
        /// <summary>
        /// 小组话题获取
        /// </summary>
        /// <param name="gid">小组编号</param>
        /// <param name="last">上页最后一条</param>
        /// <param name="count">要获取的条数</param>
        /// <returns>话题们</returns>
        public Hashtable[] topicGet(int gid, int last, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@gid"] = gid;
            inQuery["@last"] = last;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_get", inQuery, "rt"));
        }
        /// <summary>
        /// 小组指定话题获取
        /// </summary>
        /// <param name="tid">话题编号</param>
        /// <returns>话题</returns>
        public Hashtable topicGetById(int tid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_get_byId", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 新评论
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="tid">帖子编号</param>
        /// <param name="comment">评论</param>
        public void commentNew(int uid, int tid, string comment)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@tid"] = tid;
            inQuery["@comment"] = comment;
            myDb.ExecNoneQuery("information_group_comment_new", inQuery);   
        }
        /// <summary>
        /// 话题评论获取
        /// </summary>
        /// <param name="tid">小组编号</param>
        /// <returns>评论们</returns>
        public Hashtable[] commentGet(int tid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tid"] = tid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_comment_get", inQuery, "rt"));
        }
        /// <summary>
        /// 通过标签获取文章
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="last">最后一条</param>
        /// <param name="count">总条数</param>
        /// <returns>文章们</returns>
        public Hashtable[] postGetByTat(string tag,int last,int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tag"] = tag;
            inQuery["@last"] = last;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_get_byTag", inQuery, "rt"));
        }
        /// <summary>
        /// 获取用户发表的文章
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="last">最后条</param>
        /// <param name="count">要获取的条数</param>
        /// <returns>文章们</returns>
        public Hashtable[] postGetByUser(int uid, int last, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@last"] = last;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_group_topic_get_byUser", inQuery, "rt"));
        }
    }
}