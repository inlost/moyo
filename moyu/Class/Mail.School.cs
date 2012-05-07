using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace moyu.Mail
{
    public class School
    {
        moyu.Data.Db myDb = new Data.Db();
        /// <summary>
        /// 新邮件添加
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="body">内容</param>
        /// <param name="uid">用户编号</param>
        /// <param name="receivers">接受机构编号[ 1;2;3 ]</param>
        /// <returns>邮件编号</returns>
        public int add(string title, string body, int uid, string receivers)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            inQuery["@uid"] = uid;
            try
            {
                int rst = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_mail_add", inQuery, "rt"))[0]["Column1"]);
                inQuery = new Hashtable();
                foreach (string receiver in receivers.Split(';'))
                {
                    inQuery["@mid"] = rst;
                    inQuery["@ruid"] = receiver;
                    myDb.ExecNoneQuery("ecard_school_mail_receiver_add", inQuery);
                }
                return rst;
            }
            catch
            {
                return 0;
            }

        }
    }
}