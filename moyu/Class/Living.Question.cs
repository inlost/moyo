using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Living
{
    public class Question
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 问题添加
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="title">问题标题</param>
        /// <param name="body">问题内容</param>
        /// <returns>问题编号</returns>
        public int add(int uid, string title, string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            return Convert .ToInt32( Data.Type.dtToHash(myDb.GetQueryStro("living_knows_add",inQuery,"rt"))[0]["id"]);
        }
        /// <summary>
        /// 获取知道
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="last">最后一条，第一次获取传0</param>
        /// <returns></returns>
        public Hashtable[] getNew(int count, int last)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            inQuery["@last"] = last;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_knows_new", inQuery, "rt"));
        }
        /// <summary>
        /// 获取单条知道
        /// </summary>
        /// <param name="kid">知道编号</param>
        /// <returns>知道内容</returns>
        public Hashtable get(int kid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@id"] = kid;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_knows_get", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 问题答案添加
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="body">回复内容</param>
        /// <param name="nid">问题编号</param>
        public void answerAdd(int uid, string body, int nid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@body"] = body;
            inQuery["@nid"] = nid;
            myDb.ExecNoneQuery("living_knows_answer_add", inQuery);
        }
        /// <summary>
        /// 获取回答
        /// </summary>
        /// <param name="nid">问题编号</param>
        /// <returns>回答们</returns>
        public Hashtable[] answersGet(int nid)
        {
            string strSql = "select * from living_knows_answer where nid=" + nid + " order by id desc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        }
        /// <summary>
        /// 最新回答的问题获取
        /// </summary>
        /// <param name="count">获取条数</param>
        /// <returns>问题们</returns>
        public Hashtable[] answeredQuestionGet(int count)
        {
            string strSql = "select top("+count+") * from living_knows where updateDate > postDate order by updateDate desc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 等待回答的问题获取
        /// </summary>
        /// <param name="count">获取条数</param>
        /// <returns>问题们</returns>
        public Hashtable[] noAnswerQuestionGet(int count)
        {
            string strSql = "select top("+count+") * from living_knows where updateDate = postDate order by updateDate asc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
    }
}