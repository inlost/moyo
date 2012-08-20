using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using moyu.Data;
using System.Collections;
namespace moyu.User
{
    public class Web
    {
        private Db myDb = new Db();
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginId">用户id或名字</param>
        /// <param name="password">密码</param>
        /// <returns>该用户的信息</returns>
        public Hashtable login(string loginId,string password)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uName"] = loginId;
            inQuery["@password"] = password;
            try
            {
                try
                {
                    Convert.ToInt32(inQuery["@uName"]);
                    return moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_login_byNo", inQuery, "rt"))[0];
                }
                catch
                {
                    return moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_login_byName", inQuery, "rt"))[0];
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 网站用户注册
        /// </summary>
        /// <param name="niceName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="birth">生日</param>
        /// <param name="email">电邮</param>
        /// <param name="phone">电话</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户</returns>
        public Hashtable reg(string niceName, string realName, bool sex, DateTime birth, string email, Int64 phone, string password)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@niceName"] = niceName;
            inQuery["@realname"] = realName;
            inQuery["@sex"] = sex;
            inQuery["@birthday"] = birth;
            inQuery["@email"] = email;
            inQuery["@phone"] = phone;
            inQuery["@password"] = password;
            try
            {
                return moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_reg_web", inQuery, "rt"))[0];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 用户获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>用户信息</returns>
        public Hashtable get(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            try
            {
                return moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_get_web", inQuery, "rt"))[0];
            }
            catch
            {
                return null;
            }
        }
    }
}