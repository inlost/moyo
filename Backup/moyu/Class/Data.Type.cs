using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
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
                dataHash[index] = new Hashtable();
                foreach (DataColumn c in dt.Columns)
                {
                    dataHash[index][c.ColumnName] = r[c]==null?"null":r[c].ToString ();
                }
                index++;
            }
            return dataHash;
        }

        public static T JsonToTT<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string objToJson(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        } 
    }
}