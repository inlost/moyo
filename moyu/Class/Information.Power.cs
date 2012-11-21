using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class Power
    {
        readonly Hashtable powers=new Hashtable();
        private Data.Db myDb = new Data.Db();
        public Power()
        {
            powers["normal"] = 0;
            powers["superAdmin"] = 1;
            powers["admin"] = 2;
            powers["editor"] = 3;
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="pid">论坛编号</param>
        /// <returns>权限</returns>
        public string getUserPower(int uid,int pid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@pid"] = pid;
            int power = 0;
            try
            {
                power = Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("information_power_get", inQuery, "rt"))[0]["power"]);
            }
            catch
            {
                return "normal";
            }
            foreach (DictionaryEntry key in powers)
            {
                if (key.Value.ToString() == power.ToString())
                {
                    return key.Key.ToString();
                }
            }
            return "normal";
        }
    }
}