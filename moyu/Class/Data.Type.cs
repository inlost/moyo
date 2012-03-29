using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
namespace moyu.Data
{
    public class Type
    {
        public static Hashtable[] dtToHash(DataTable dt)
        {
            Hashtable[] dataHash=new Hashtable [dt.Rows .Count];
            int index=0;
            foreach ( DataRow r in dt.Rows)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    dataHash[index][c.ColumnName] = r[c];
                }
                index++;
            }
            return dataHash;
        }
    }
}