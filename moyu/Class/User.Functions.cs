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
        public int signIn(int uid)
        {
            Hashtable inQuer = new Hashtable();
            inQuer["@uid"] = uid;
            inQuer["@lastDate"] = DateTime.Now.AddDays(-1).ToShortDateString();
            inQuer["@nowDate"] = DateTime.Now.ToShortDateString();
            Information.group myGroup = new Information.group();
            User.Web myUser = new User.Web();
            Hashtable theUser = new Hashtable();
            theUser = myUser.get(uid);

            int tid;
            if (!isSigIn(uid))
            {
                myDb.ExecNoneQuery("user_signIn", inQuer);
                tid = myGroup.topicNewByWeixin("签到", theUser["niceName"].ToString() + "在" + System.DateTime.Now.ToString() + "在左邻签到", -2, uid, theUser["niceName"] + "在" + System.DateTime.Now.ToShortTimeString() + "默默地签了一个到，什么都没有说，然后就默默默默地走掉了……");
            }
            else
            {
                tid = getLastSiginTopic(uid);
            }
            return tid;
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
        /// 获取用户签到日志编号
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>签到日志编号</returns>
        public int getLastSiginTopic(int uid)
        {
            string strSql = "select top(1) id from information_group_topic where gid in(-1,-2) and uid=" + uid + " and tag='签到' order by id desc";
            return Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0]["id"]);
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
            inQuery["@date2"] = DateTime.Now.AddDays(-12).ToString();
            return (Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_gift_isHasGift", inQuery, "rt"))[0]["number"]) < 1) ? false : true;
        }
        /// <summary>
        /// 用户抽奖
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>0-未中奖，1中奖，-1积分不足</returns>
        public int luckyMe(int uid)
        {
            int totleGift = 2;
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
        /// <param name="type">2，贡献,其它，积分</param>
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
        /// <summary>
        /// 用户信息发送
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="to">投向</param>
        /// <param name="message">信息内容</param>
        /// <param name="type">信息类型</param>
        /// <param name="relId">关联id</param>
        /// <returns>成功失败</returns>
        public bool sendMessage(int from, int to, string message, int type, int relId)
        {
            try
            {
                Hashtable inQuery = new Hashtable();
                inQuery["@messageFrom"] = from;
                inQuery["@messageTo"] = to;
                inQuery["@message"] = message;
                inQuery["@type"] = type;
                inQuery["@relId"] = relId;
                myDb.ExecNoneQuery("user_message_add", inQuery);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 上传图片入库
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="img">图片文件名</param>
        /// <param name="introduce">介绍说明</param>
        /// <param name="url">图片链接</param>
        /// <returns>编号</returns>
        public int upLoadImg(int uid, string img, string introduce, string url)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@img"] = img;
            inQuery["@introduce"] = introduce;
            inQuery["@url"] = url;
            return Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_photo_add", inQuery, "rt"))[0]["id"]);
        }
        /// <summary>
        /// 用户发帖积分获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="body">内容</param>
        /// <param name="point">分数</param>
        public void givePostPoint(int uid, string body, int point)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@maxPoint"] = 10;
            inQuery["@uid"] = uid;
            inQuery["@point"] = point;
            inQuery["@date"] = DateTime.Now.ToShortDateString();
            int pointAllowAdd = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_point_postAdd", inQuery, "rt"))[0]["point"]);
            if (pointAllowAdd != 0)
            {
                userPointChange(uid, pointAllowAdd, body, 3);
            }
        }
        /// <summary>
        /// 用户名修改
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="newName">新用户名</param>
        /// <returns>成功失败</returns>
        public bool changeUserName(int uid, string newName)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@niceName"] = newName;
            inQuery["@uid"] = uid;
            try
            {
                myDb.ExecNoneQuery("user_name_update", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}