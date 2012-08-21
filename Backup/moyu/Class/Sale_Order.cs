using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Sale
{
    public class Orders
    {
        Data.Db myDb = new Data.Db();
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="title">订单标题</param>
        /// <param name="gid">关联商品编号</param>
        /// <param name="name">收货人姓名</param>
        /// <param name="phone">收货人电话</param>
        /// <param name="address">收货人地址</param>
        /// <param name="eid">会员卡号码，无则传0</param>
        /// <returns></returns>
        public bool add(string title, int gid, string name, string phone, string address, Int64 eid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@title"] = title;
            inQuery["@gid"] = gid;
            inQuery["@name"] = name;
            inQuery["@phone"] = phone;
            inQuery["@address"] = address;
            inQuery["@eid"] = eid;
            try
            {
                myDb.ExecNoneQuery("orders_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}