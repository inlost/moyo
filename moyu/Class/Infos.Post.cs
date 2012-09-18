using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
namespace moyu.Infos
{
    public class Post
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 添加信息       
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="name">姓名</param>
        /// <param name="price">价格</param>
        /// <param name="body">主体</param>
        /// <param name="phone">电话</param>
        /// <param name="pass">密码</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>信息编号</returns>
        public int add(int cid,string title, string name, double price, string body, Int64 phone, string pass, Int16 isTop)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@catId"] = cid;
            inQuery["@title"] = title;
            inQuery["@name"] = name;
            inQuery["@price"] = price;
            inQuery["@body"] = body;
            inQuery["@phone"] = phone;
            inQuery["@passowrd"] = pass;
            inQuery["@isTop"] = isTop;
            return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQueryStro("info_post_add", inQuery, "rt"))[0]["id"]);
        }
        /// <summary>
        /// 信息获取
        /// </summary>
        /// <param name="count">条数</param>
        /// <param name="last">最后一条编号</param>
        /// <param name="cat">所在分类</param>
        /// <param name="father">所在大类</param>
        /// <returns>信息们</returns>
        public Hashtable[] postsGet(int count, int last, int cat, int father)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@count"] = count;
            inQuery["@last"] = last;
            inQuery["@cat"] = cat;
            inQuery["@father"] = father;
            return Data.Type.dtToHash(myDb.GetQueryStro("info_posts_get", inQuery, "rt"));
        }
        /// <summary>
        /// 信息条数获取
        /// </summary>
        /// <returns>条数</returns>
        public int postCountGet()
        {
            string strSql = "select count(id) as count from info_post";
            return Convert.ToInt32(Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"))[0]["count"]);
        }
    }
}