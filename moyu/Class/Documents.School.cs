using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Documents
{
    public class School
    {
        moyu.Data.Db myDb = new Data.Db();
        /// <summary>
        /// 新公文添加
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="body">正文</param>
        /// <param name="uid">用户编号</param>
        /// <param name="status">公文状态</param>
        /// <param name="receivers">接受机构编号[ 1|1;2|2 ]</param>
        /// <returns>公文编号</returns>
        public int Add(string title,string body,int uid,bool status,string receivers)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            inQuery["@uid"] = uid;
            inQuery["@status"] = status;
            try
            {
                int rst = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_documents_add", inQuery, "rt"))[0]["Column1"]);
                inQuery = new Hashtable();
                foreach (string receiver in receivers.Split(';'))
                {
                    inQuery["@did"] = rst;
                    inQuery["@aid"] = receiver.Split('|')[0];
                    inQuery["@aType"] = receiver.Split('|')[1];
                    myDb.ExecNoneQuery("ecard_school_documents_receiver_add", inQuery);
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