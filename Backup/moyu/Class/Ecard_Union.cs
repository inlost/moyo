using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
namespace moyu.Ecard
{
    public class Union
    {
        Data.Db myDb = new Data.Db();
        public bool shopLogin(int sid, string password)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@sid"] = sid;
            inQuery["@password"] = password;
            if (moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_union_shop_login", inQuery, "rt")).Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool newUser(int sid, Int64 cid, string password, string realName, bool sex, DateTime birthday, string phone, string address)
        { 
            Hashtable inQuery=new Hashtable();
            inQuery["@sid"] = sid;
            inQuery["@cid"] = cid;
            inQuery["@password"] = password;
            inQuery["@realname"] = realName;
            inQuery["@sex"] = Convert .ToBoolean(sex )?1:0;
            inQuery["@birtyday"] = birthday;
            inQuery["@phone"] = phone;
            inQuery["@address"] = address;
            try
            {
                myDb.ExecNoneQuery("ecard_union_user_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool consumption_log(int uid, int sid, double price)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@sid"] = sid;
            inQuery["@price"] = price;
            try
            {
                myDb.ExecNoneQuery("ecard_union_consumption_log_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Hashtable user_get(Int64 cid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            Hashtable theUser = new Hashtable();
            theUser = Data.Type.dtToHash(myDb.GetQueryStro("ecard_user_get", inQuery, "rt"))[0];
            return theUser;
        }
        public Hashtable user_get(Int64 cid, int sid)
        {
            Hashtable theUser = user_get(cid);
            theUser["tootle"] = user_xf_tootle_get( Convert .ToInt32( theUser["id"]), sid);
            return theUser;
        }
        public string user_xf_tootle_get(int uid,int sid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@sid"] = sid;
            Hashtable tootle = new Hashtable();
            try
            {
                tootle =Data.Type.dtToHash( myDb.GetQueryStro("ecard_user_union_xf_tootle_get", inQuery, "rt"))[0];
                return tootle["tootle"].ToString();
            }
            catch
            {
                return "该用户在本店还没有消费过";
            }
        }
    }
}