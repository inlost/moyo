using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
namespace moyu.User
{
    public class Functions
    {
        private moyu.Data.Db myDb = new Data.Db();
        /// <summary>
        /// 用户签到
        /// </summary>
        /// <param name="uid">用户编号</param>
        public void signIn(int uid)
        {
            Hashtable inQuer = new Hashtable();
            inQuer["@uid"] = uid;
            inQuer["@lastDate"] = DateTime.Now.AddDays(-1).ToShortDateString();
            inQuer["@nowDate"] = DateTime.Now.ToShortDateString();
            myDb.ExecNoneQuery("user_signIn", inQuer);
        }
        /// <summary>
        /// 用户签到历史获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>历史们</returns>
        public Hashtable[] getSigInLog(int uid)
        {
            string strSql = "select top(30) * from users_signIn where uid=" + uid +" order by id desc";
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        }
        /// <summary>
        /// 判断用户是否已经签到
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>布尔</returns>
        public bool isSigIn(int uid)
        {
            string strSql = "select count(id) as count from users_signIn where uid=" + uid + " and date='" + DateTime.Now.ToShortDateString() + "'";
            int count = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"))[0]["count"]);
            return count == 0 ? false : true;
        }
        /// <summary>
        /// 用户积分获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>积分们</returns>
        public Hashtable getPoint(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_point_get", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 判断用户过去一周是否获奖
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>布尔</returns>
        public bool isHasGiftLastWeek(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@date2"] = DateTime.Now.AddDays(-7).ToString();
            return (Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_gift_isHasGift", inQuery, "rt"))[0]["number"]) < 1) ? false : true;
        }
        /// <summary>
        /// 用户抽奖
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>0-未中奖，1中奖，-1积分不足</returns>
        public int luckyMe(int uid)
        {
            int totleGift = 3;
            int maxNumber = 9;
            Random rd = new Random();
            int thisTime = rd.Next(1, maxNumber);
            bool isLeftGift = false;
            int userPoint = 0;
            string strSql = "select point from users_points where uid=" + uid;
            userPoint = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0]["point"]);
            if (userPoint < 10) { return -1; }//积分判断
            userPointChange(uid, -10, "抽奖消耗积分", 1);
            if (thisTime <= totleGift && (!isHasGiftLastWeek(uid)))
            {
                strSql = "select count(id) as count from users_gifts where date2='" + DateTime.Now.ToShortDateString() + "'";
                int count = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0]["count"]);
                count = totleGift - count;
                isLeftGift = count > 0 ? true : false;
                if (!isLeftGift) { return 0; }
                Hashtable inQuery = new Hashtable();
                inQuery["@uid"] = uid;
                myDb.ExecNoneQuery("user_gift_firstAdd", inQuery);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void giftAdd(int uid,string gift,string message)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@gift"] = gift;
            inQuery["@message"] = message;
            myDb.ExecNoneQuery("user_gift_update", inQuery);
        }
        /// <summary>
        /// 用户获奖信息获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="count">条数</param>
        /// <returns>信息们</returns>
        public Hashtable[] giftGet(int uid, int count)
        {
            string strSql = "select top("+count+") * from users_gifts where uid=" + uid +" order by id desc";
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        }
        public Hashtable[] giftGet(int count)
        {
            string strSql = "select top(" + count + ") * from users_gifts where gift<>'0' order by id desc";
            return moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 用户积分修改
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="point">积分数量</param>
        /// <param name="body">说明文字</param>
        /// <param name="type">1，积分，2，贡献</param>
        public void userPointChange(int uid, int point, string body, int type)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@point"] = point;
            inQuery["@body"] = body;
            inQuery["@type"] = type;
            myDb.ExecNoneQuery("user_point_change", inQuery);
        }
        /// <summary>
        /// 推荐用户获取积分
        /// </summary>
        /// <param name="fromUid">感谢者</param>
        /// <param name="toName">被感谢者</param>
        public void giveThanksPoint(int fromUid, string toName)
        {
            Hashtable userPoint = new Hashtable();
            userPoint = getPoint(fromUid);
            if (Convert.ToBoolean(userPoint["hasNewPoint"]) == true)
            {
                Hashtable inQuery = new Hashtable();
                inQuery["@from"] = fromUid;
                inQuery["@to"] = toName;
                myDb.ExecNoneQuery("user_point_giveThanksPoint", inQuery);
            }
        }
        /// <summary>
        /// 贡献兑换积分
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="point">贡献数</param>
        /// <returns>成功失败</returns>
        public bool exchange(int uid, int point)
        {
            Hashtable userPoint = new Hashtable();
            userPoint = getPoint(uid);
            if (Convert.ToInt32(userPoint["contribute"]) < point)
            {
                return false;
            }
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@point"] = point;
            myDb.ExecNoneQuery("user_point_exchange", inQuery);
            return true;
        }
    }
}