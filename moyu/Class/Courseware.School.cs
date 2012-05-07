using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using moyu.Data;
namespace moyu.Courseware
{
    public class School
    {
        private Db myDb = new Db();
        /// <summary>
        /// 教案添加
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="sid">学校编号</param>
        /// <param name="title">标题</param>
        /// <param name="body">主体</param>
        /// <param name="grade">年级</param>
        /// <param name="subject">科目</param>
        /// <returns>成功失败</returns>
        public bool add(int uid,int sid,string title,string body,short grade,short subject)
        { 
            Hashtable inQuery=new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@sid"] = sid;
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            inQuery["@grade"] = grade;
            inQuery["@subject"] = subject;
            try
            {
                myDb.ExecNoneQuery("ecard_school_courseware_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}