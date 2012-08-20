using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Ecard
{
    public class Living
    {
        Data.Db myDb = new Data.Db();
        /// <summary>
        /// 积分兑换商品添加
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="number">数量</param>
        /// <param name="cost">需要积分</param>
        /// <param name="img">图片</param>
        /// <returns>成功/失败</returns>
        public bool exchangeAdd(string name, int number, int cost, string img)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@title"] = name;
            inQuery["@pic"] = img;
            inQuery["@need"] = number;
            inQuery["@number"] = cost;
            try
            {
                myDb.ExecNoneQuery("ecard_union_exchange_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 积分兑换商品获取
        /// </summary>
        /// <returns>商品们</returns>
        public Hashtable[] exchangeGet()
        {
            string strSql = "select * from eCard_union_exchange where number>0 order by id desc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 积分兑换商品获取
        /// </summary>
        /// <param name="gid">商品编号</param>
        /// <returns>商品</returns>
        public Hashtable exchangeGet(int gid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@gid"] = gid;
            return Data.Type.dtToHash(myDb.GetQueryStro("ecard_union_exchange_get", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 用户积分获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public int userJfGet(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            Hashtable rst = new Hashtable();
            try
            {
                rst = moyu.Data.Type.dtToHash(myDb.GetQueryStro("ecard_union_integral_get", inQuery, "rt"))[0];
                return Convert.ToInt32(rst["integral"]);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 用户积分兑换添加
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="gid">兑换商品编号</param>
        /// <param name="ip">用户ip</param>
        /// <returns>成功/失败</returns>
        public bool userExchangeAdd(int uid,int gid,string ip)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@gid"] = gid;
            inQuery["@ip"] = ip;
            try
            {
                Hashtable rst = new Hashtable();
                rst = moyu.Data.Type.dtToHash( myDb.GetQueryStro("ecard_union_userExchange_add", inQuery, "rt"))[0];
                Convert.ToInt32(rst["integral"]);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 用户兑换日志获取
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>日志们</returns>
        public Hashtable[] userExchangeGet(int uid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            return moyu.Data.Type.dtToHash( myDb.GetQueryStro("ecard_union_exchange_log_get", inQuery, "rt"));
        }
    }
}