using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace moyu.Infos
{
    public class cat
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 信息分类添加
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="father">父类</param>
        /// <param name="deep">层级</param>
        /// <param name="order">排序</param>
        public void add(string name,int father,int deep,int order)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@name"] = name;
            inQuery["@father"] = father;
            inQuery["@deep"] = deep;
            inQuery["@orders"] = order;
            myDb.ExecNoneQuery("infos_cat_add", inQuery);
        }
        /// <summary>
        /// 信息分类删除
        /// </summary>
        /// <param name="id">分类编号</param>
        public void del(int id)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cid"] = id;
            myDb.ExecNoneQuery("infos_cat_del", inQuery);
        }
        /// <summary>
        /// 信息分类获取
        /// </summary>
        /// <param name="deep">层级</param>
        /// <param name="father">父类</param>
        /// <returns></returns>
        public Hashtable[] get(int deep, int father)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@deep"] = deep;
            inQuery["@father"] = father;
            return Data.Type.dtToHash(myDb.GetQueryStro("info_cat_get", inQuery, "rt"));
        }
    }
}