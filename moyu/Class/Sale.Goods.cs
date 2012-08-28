using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace moyu.Sale
{
	public class Goods
	{
        Data.Db myDb = new Data.Db();
        private int MallCatId = 7; //商城分类编号
        public int mallCatId
        {
            get { return MallCatId; }
        }
		/// <summary>
		/// 商品添加
		/// </summary>
		/// <param name="cid">分类编号</param>
		/// <param name="name">名称</param>
        /// <param name="price">价格</param>
		/// <param name="pic">图片</param>
		/// <param name="introduce">介绍</param>
		/// <param name="seller">卖家</param>
		/// <param name="saleType">销售类型</param>
        public void goodAdd(int cid,string name,double price,string pic,string introduce,string seller,int saleType)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            inQuery["@name"] = name;
            inQuery["@price"] = price;
            inQuery["@pic"] = pic;
            inQuery["@introduce"] = introduce;
            inQuery["@seller"] = seller;
            inQuery["@saleType"] = saleType;
            myDb.ExecNoneQuery("sale_good_add", inQuery);
        }
        /// <summary>
        /// 商城分类获取
        /// </summary>
        /// <returns>分类们</returns>
        public Hashtable[] mallCatsGet()
        {
            Hashtable[] cats;
            string strSql = "select * from sale_cat where father=" + MallCatId;
            cats = Data.Type.dtToHash( myDb.GetQuerySql(strSql, "rt"));
            return cats;
        }
	}
}