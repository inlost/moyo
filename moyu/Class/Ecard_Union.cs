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
        /// <summary>
        /// 正在发行的电子优惠券获取
        /// </summary>
        /// <returns>优惠券们</returns>
        public Hashtable[] couponsGet()
        {
            string strSql = "select * from eCard_union_coupons where endTime > '" + DateTime.Now +"'";
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        }
        public Hashtable couponsGet(int id)
        {
            string strSql = "select * from eCard_union_coupons where id = " + id;
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0];
        }
        /// <summary>
        /// 判断用户是否拥有优惠券
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="cid">优惠券编号</param>
        /// <returns>真假</returns>
        public bool isHaveCoupon(int uid, int cid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@cid"] = cid;
            int number = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_union_coupons_isHave", inQuery, "rt"))[0]["number"]);
            return number == 0 ? false : true;
        }
        public void changePassword(string passWord,int sid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@sid"] = sid;
            inQuery["@password"] = passWord;
            myDb.ExecNoneQuery("ecard_union_shop_changePass", inQuery);
        }
        /// <summary>
        /// 用户获取优惠券
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="cid">优惠券编号</param>
        /// <returns>状态码</returns>
        public int getCoupons(int uid, int cid)
        {
            User.Functions myFunctions = new User.Functions();
            Hashtable theCoupons = new Hashtable();
            Hashtable thePoint = new Hashtable();
            theCoupons = couponsGet(cid);
            thePoint = myFunctions.getPoint(uid);
            int needPoint=Convert .ToInt32(theCoupons["needPoint"]);
            if (isHaveCoupon(uid, cid))
            {
                return 0; //已经拥有优惠券
            }
            if (Convert.ToDateTime(theCoupons["endTime"]) < DateTime.Now)
            {
                return -1; //已经过期
            }
            if (needPoint > Convert.ToInt32(thePoint["point"]))
            {
                return -2; //积分不足
            }
            else
            {
                myFunctions.userPointChange(uid, (needPoint * -1), "兑换优惠券消耗积分", 1);
                Hashtable inQuery = new Hashtable();
                Random rd = new Random();
                string no = rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                no += rd.Next(0, 9).ToString();
                inQuery["@uid"] = uid;
                inQuery["@cid"] = cid;
                inQuery["@no"] = no;
                myDb.ExecNoneQuery("ecard_union_coupons_setHave", inQuery);
            }
            return 1; //成功
        }
        /// <summary>
        /// 获取用户特定优惠券
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="cid">优惠券编号</param>
        /// <returns>优惠券</returns>
        public Hashtable getUserCoupons(int uid, int cid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@cid"] = cid;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_union_coupons_getUsers", inQuery, "rt"))[0];
        }
        public Hashtable[] getUserCoupons(int uid)
        {
            string strSql = "select * from eCard_union_coupons_have where uid=" + uid;
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        }
        public Hashtable getCouponsByNo(int no)
        {
            string strSql = "select * from eCard_union_coupons_have where no=" + no;
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0];
        }
        /// <summary>
        /// 获取店铺优惠券
        /// </summary>
        /// <param name="sid">店铺编号</param>
        /// <returns>优惠券们</returns>
        public Hashtable[] getShopCoupons(int sid)
        {
            string strSql = "select * from v_eCard_union_coupons_have where useShop=" + sid;
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 使用优惠券
        /// </summary>
        /// <param name="no">优惠券密码</param>
        /// <param name="sid">店铺编号</param>
        public void useCoupons(int no,int sid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@no"] = no;
            inQuery["@useShop"] = sid;
            myDb.ExecNoneQuery("ecard_union_coupons_useIt", inQuery);
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