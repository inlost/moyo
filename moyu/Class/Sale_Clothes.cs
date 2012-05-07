using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Sale
{
    public class Clothes
    {
        Data.Db myDb = new Data.Db();
        public bool add(int cat, double oldPrice, double salePrice, int inventory, int volume,string image,string title,string introduce)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@oldPrice"] = oldPrice;
            inQuery["@salePrice"] = salePrice;
            inQuery["@inventory"] = inventory;
            inQuery["@volume"] = volume;
            inQuery["@image"] = image;
            inQuery["@title"] = title;
            inQuery["@introduce"] = introduce;
            try
            {
                myDb.ExecNoneQuery("sale_clothes_add", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}