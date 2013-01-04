using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Robot
{
    public class Main
    {
        private moyu.Data.Db myDb = new Data.Db();
        /// <summary>
        /// 获取教学列表
        /// </summary>
        /// <param name="last">最后条</param>
        /// <param name="number">条数</param>
        /// <returns>教学条目们</returns>
        public Hashtable[] getTeachList(int last, int number)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@last"] = last;
            inQuery["@count"] = number;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("robot_rules_get", inQuery, "rt"));
        }
        public Hashtable[] getTeachListByUser(int uid, int last, int number)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@last"] = last;
            inQuery["@count"] = number;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("robot_rules_getByUser", inQuery, "rt"));
        }
        /// <summary>
        /// 添加机器人问题
        /// </summary>
        /// <param name="question">问题</param>
        /// <param name="uid">用户编号</param>
        public void questionAdd(string question, int uid)
        { 
            Hashtable inQuery=new Hashtable();
            inQuery["@question"] = question;
            inQuery["@uid"] = uid;
            myDb.ExecNoneQuery("robot_question_add", inQuery);
        }
        /// <summary>
        /// 待回答问题获取
        /// </summary>
        /// <param name="last">最后条</param>
        /// <param name="number">条数</param>
        /// <returns>问题们</returns>
        public Hashtable[] questionsGet(int last, int number)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@last"] = last;
            inQuery["@count"] = number;
            return Data.Type.dtToHash(myDb.GetQueryStro("robot_question_get", inQuery, "rt"));
        }
    }
}