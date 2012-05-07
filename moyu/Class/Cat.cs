using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu
{
    public class Cat
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 销售分类添加
        /// </summary>
        /// <param name="father">父类编号</param>
        /// <param name="deep">深度</param>
        /// <param name="name">类名称</param>
        /// <returns>成功失败</returns>
        public bool add(int father, short deep, string name)
        { 
            Hashtable inQuery=new Hashtable();
            inQuery["@father"] = father;
            inQuery["@deep"] = deep;
            inQuery["@name"] = name;
            try
            {
                myDb.ExecNoneQuery("sale_cat_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id">分类编号</param>
        /// <returns>成功失败</returns>
        public bool del(int id)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@id"] = id;
            try
            {
                myDb.ExecNoneQuery("sale_cat_del", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}