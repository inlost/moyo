using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class Robot
    {
        private moyu.Data.Db myDb = new Data.Db();
        private string getType(string type)
        {
            Hashtable types = new Hashtable();
            types["xh"] = "笑话";
            types["yy"] = "音乐";
            types["wz"] = "文字";
            types["wxs"] = "微说";
            types["ggs"] = "恐怖";
            foreach (DictionaryEntry dic in types)
            {
                if (type == dic.Key.ToString())
                {
                    return dic.Value.ToString();
                }
            }
            return "*";
        }
        public Hashtable getItem(string type)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@tag"] = getType(type);
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("information_robot_get", inQuery, "rt"))[0];
        }
    }
}