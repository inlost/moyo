using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Mall
{
	public class apply
	{
        private moyu.Data.Db myDb = new Data.Db();
		/// <summary>
		/// 店铺申请添加
		/// </summary>
		/// <param name="what">申请事项</param>
		/// <param name="shopName">店铺名称</param>
		/// <param name="shopBoss">店铺老板</param>
		/// <param name="tel">联系电话</param>
		public void add(string what,string shopName,string shopBoss,Int64 tel)
        {
			Hashtable inQuery=new Hashtable();
			inQuery["@what"]=what;
			inQuery["@shopName"]=shopName;
			inQuery["@shopBoss"]=shopBoss;
			inQuery["@tel"]=tel;
			myDb.ExecNoneQuery("mall_apply_add",inQuery);
        }
        /// <summary>
        /// 申请获取
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="last">最后一条</param>
        /// <returns>申请们</returns>
        public Hashtable[] get(int count, int last)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            inQuery["@last"] = last;
            return moyu.Data.Type.dtToHash(myDb.GetQueryStro("mall_apply_get", inQuery, "rt"));
        }
	}
}