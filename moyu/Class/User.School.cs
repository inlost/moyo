using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using moyu.Data;
namespace moyu.User
{
    public class School
    {
        private Db myDb = new Db();
        /// <summary>
        /// 学校用户登录
        /// </summary>
        /// <param name="loginId">登录id</param>
        /// <param name="key">登录密钥</param>
        /// <param name="permission">用户身份1-老师 2-学生 3-工作人员 4-家长</param>
        /// <returns></returns>
        public Hashtable[] login(string loginId, string key, short permission)
        {
            short loginType = 0; 
            try
            {
                Convert.ToInt64(loginId);
                loginType = Convert.ToInt16(loginId.Length == 11 ? 1 : 2);
            }
            catch
            {
                loginType = 3;
            }
            Hashtable inQuery = new Hashtable();
            inQuery["@loginType"] = loginType;
            inQuery["@phone"] = loginType!=3?loginId:"0";
            inQuery["@cid"] = loginType != 3 ? loginId : "0";
            inQuery["@niceName"] = loginId;
            inQuery["@password"] = key;
            inQuery["@permission"] = permission;
            Hashtable[] rst;
            try
            {
                rst=moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_login", inQuery, "rt"));
                return rst;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 用户相关构架获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>构架</returns>
        public Hashtable[] architectureGet(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"]=uid;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_personArchitecture_get",inQuery,"rt"));
        }
        /// <summary>
        /// 用户联系人获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>联系人</returns>
        public Hashtable[] contacGet(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_contac_get", inQuery, "rt"));
        }
        /// <summary>
        /// 班级学生获取
        /// </summary>
        /// <param name="classId">班级编号</param>
        /// <returns>学生</returns>
        public Hashtable[] studentGet(int classId)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@classId"] = classId;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_school_student_get_byClass", inQuery, "rt"));
        }
    }
}