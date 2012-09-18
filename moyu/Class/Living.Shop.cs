using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Living
{
    public class shops
    {
        Data.Db myDb = new Data.Db();
        /// <summary>
        /// 商户分类添加
        /// </summary>
        public void cat_add(string name,int order)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@name"] = name;
            inQuery["@showOrder"] = order;
            myDb.ExecNoneQuery("living_cat_add", inQuery);
        }
        /// <summary>
        /// 商户分类获取
        /// </summary>
        /// <returns>分类们</returns>
        public Hashtable[] cat_get()
        {
            string strSql = "select * from living_cat order by showOrder asc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 店铺分类广告添加
        /// </summary>
        /// <param name="cat">广告所在分类</param>
        /// <param name="title">标题</param>
        /// <param name="pic">图片</param>
        /// <param name="url">链接地址</param>
        /// <param name="type">广告类型</param>
        public void cat_ad_add(int cat, string title, string pic, string url, int type)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@title"] = title;
            inQuery["@pic"] = pic;
            inQuery["@url"] = url;
            inQuery["@type"] = type;
            myDb.ExecNoneQuery("living_cat_ad_add", inQuery);
        }
        /// <summary>
        /// 分类广告获取
        /// </summary>
        /// <param name="catId">分类编号</param>
        /// <param name="type">类型编号</param>
        /// <returns></returns>
        public Hashtable cat_ad_get(int catId,int type)
        {
            string strSql = "select top 1 * from living_cat_ad where cat=" + catId + " and type="+type +" order by id desc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0];
        }
        /// <summary>
        /// 新店铺添加
        /// </summary>
        /// <param name="cat">店铺分类</param>
        /// <param name="pic">店铺图片</param>
        /// <param name="name">店铺名称</param>
        /// <param name="introduce">店铺介绍</param>
        /// <param name="address">店铺地址</param>
        /// <param name="phone">店铺电话</param>
        /// <param name="pointX">店铺经度</param>
        /// <param name="pointY">店铺纬度</param>
        /// <param name="time">营业时间</param>
        /// <param name="isDisplay">是否新店展示</param>
        /// <param name="isActive">是否激活</param>
        /// <param name="source">店铺提交来源</param>
        public void shop_add(int cat, string pic, string name, string introduce, string address, string phone, double pointX, double pointY, string time, bool isDisplay, bool isActive, int source)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@pic"] = pic;
            inQuery["@name"] = name;
            inQuery["@introduce"] = introduce;
            inQuery["@address"] = address;
            inQuery["@phone"] = phone;
            inQuery["@pointX"] = pointX;
            inQuery["@pointY"] = pointY;
            inQuery["@openTime"] = time;
            inQuery["@isDisplay"] = isDisplay;
            inQuery["@isActive"] = isActive;
            inQuery["@source"] = source;
            myDb.ExecNoneQuery("living_shop_add", inQuery);
        }
        /// <summary>
        /// 店铺获取（按评分排序）
        /// </summary>
        /// <param name="cat">分类</param>
        /// <param name="count">数量</param>
        /// <returns>店铺们</returns>
        public Hashtable[] shop_get_byPoint(int cat, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_shop_get_orderRank", inQuery, "rt"));
        }
        /// <summary>
        /// 店铺详细获取
        /// </summary>
        /// <param name="sid">编号</param>
        /// <returns>店铺</returns>
        public Hashtable shop_get_detal(int sid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@sid"] = sid;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_shop_get_detal", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 全部商家获取
        /// </summary>
        /// <param name="cat">分类</param>
        /// <param name="count">条数</param>
        /// <returns>商家们</returns>
        public Hashtable[] shop_get_all(int cat, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_shop_get_cat", inQuery, "rt"));
        }
        /// <summary>
        /// 新店获取
        /// </summary>
        /// <param name="cat">分类</param>
        /// <param name="count">数量</param>
        /// <returns>新店们</returns>
        public Hashtable[] shop_get_byNew(int cat, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_shop_get_new", inQuery, "rt"));
        }
        /// <summary>
        /// 信息添加
        /// </summary>
        /// <param name="cid">分类</param>
        /// <param name="sid">店铺</param>
        /// <param name="title">标题</param>
        /// <param name="body">内容</param>
        public void info_add(int cid, int sid, string title, string body)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = cid;
            inQuery["@sid"] = sid;
            inQuery["@title"] = title;
            inQuery["@body"] = body;
            myDb.ExecNoneQuery("living_info_add", inQuery);
        }
        /// <summary>
        /// 活动信息获取
        /// </summary>
        /// <param name="cid">分类</param>
        /// <param name="count">条数</param>
        /// <returns>信息们</returns>
        public Hashtable[] infos_get(int cid, int count)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cid;
            inQuery["@count"] = count;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_infos_get", inQuery, "rt"));
        }
        /// <summary>
        /// 活动信息获取
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>信息</returns>
        public Hashtable info_get(int id)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@id"] = id;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_info_get", inQuery, "rt"))[0];
        }
        /// <summary>
        /// 评论添加
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="sid">店铺编号</param>
        /// <param name="point">总分</param>
        /// <param name="quality">性价比</param>
        /// <param name="service">服务</param>
        /// <param name="circumstance">环境</param>
        /// <param name="comment">评论</param>
        public void comment_add(int uid, int sid, int point, int quality, int service, int circumstance, string comment)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@uid"] = uid;
            inQuery["@sid"] = sid;
            inQuery["@point"] = point;
            inQuery["@quality"] = quality;
            inQuery["@service"] = service;
            inQuery["@circumstance"] = circumstance;
            inQuery["@comment"] = comment;
            myDb.ExecNoneQuery("living_shop_comment_add", inQuery);
        }
        /// <summary>
        /// 评论获取
        /// </summary>
        /// <param name="sid">店铺编号</param>
        /// <returns>评论们</returns>
        public Hashtable[] comment_get(int sid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@sid"] = sid;
            return Data.Type.dtToHash(myDb.GetQueryStro("living_shop_comment_get",inQuery,"rt"));
        }
        /// <summary>
        /// 商城店铺数量获取
        /// </summary>
        /// <returns>数量</returns>
        public int shopCountGet()
        {
            string strSql = "select count(id) as count from living_shops";
            return Convert.ToInt32( Data.Type.dtToHash( myDb.GetQuerySql(strSql,"rt"))[0]["count"]);
        }
    }
}