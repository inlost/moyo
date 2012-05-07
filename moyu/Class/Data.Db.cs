using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace moyu.Data
{
    public partial class Db
    {
        private SqlConnection myConn;
        private DataSet ds;
        private SqlDataAdapter adapt;
    }
    public partial class Db
    {
        /// <summary>
        /// 向sqlCommand对象添加参数
        /// </summary>
        /// <param name="cmd">sqlCommand对象</param>
        /// <param name="inQuery">参数集合</param>
        /// <returns>添加过参数的sqlCommand对象</returns>
        private SqlCommand setQueryPar(SqlCommand cmd, Hashtable inQuery)
        {
            foreach (DictionaryEntry par in inQuery)
            {
                SqlParameter sqlPar = new SqlParameter();
                sqlPar.ParameterName = par.Key.ToString();
                sqlPar.Value = par.Value.ToString();
                cmd.Parameters.Add(sqlPar);
            }
            return cmd;
        }
    }
    public partial class Db
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns>数据库连接对象</returns>
        private SqlConnection GetConnection()
        {
            
            string strCon = ConfigurationManager.ConnectionStrings["conStr"].ToString();
            myConn = new SqlConnection(strCon);
            return myConn;
        }
        /// <summary>
        /// 执行一次无返回值的Sql查询
        /// </summary>
        /// <param name="strSql">sql语句</param>
        public void ExecNonQuery(string strSql)
        {
            SqlCommand myCmd = new SqlCommand();
            try
            {
                myConn = GetConnection();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = strSql;
                if (myCmd.Connection.State == ConnectionState.Closed)
                {
                    myConn.Open();
                }
                myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally 
            {
                if(myCmd .Connection .State ==ConnectionState .Open )
                {
                    myConn.Close();
                    myCmd.Dispose();
                    myConn.Dispose();
                }
            }
        }
        /// <summary>
        /// 执行一次无返回值的存储过程查询
        /// </summary>
        /// <param name="stroName">存储过程的名字</param>
        /// <param name="inQuery">存储过程参数列表</param>
        public void ExecNoneQuery(string stroName, Hashtable inQuery)
        {
            SqlCommand myCmd = new SqlCommand();
            try
            {
                myConn = GetConnection();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.CommandText = stroName;
                myCmd = setQueryPar(myCmd, inQuery);
                myConn.Open();
                myCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (myCmd.Connection.State == ConnectionState.Open)
                {
                    myConn.Close();
                    myCmd.Dispose();
                    myConn.Dispose();
                }
            }
        }
        /// <summary>
        /// 返回结果集的sql查询
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="tableName">datatable名称</param>
        /// <returns>返回的结果集</returns>
        public DataTable GetQuerySql(string strSql, string tableName)
        {
            ds = new DataSet();
            try
            {
                myConn = GetConnection();
                adapt = new SqlDataAdapter(strSql ,myConn );
                adapt.Fill(ds, tableName);
                return ds.Tables[tableName];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                myConn.Close();
                adapt.Dispose();
                ds.Dispose();
                myConn.Dispose();
            }
        }
        /// <summary>
        /// 返回存储过程查询的结果集
        /// </summary>
        /// <param name="stroName">存储过程的名字</param>
        /// <param name="inQuery">存储过程参数集合</param>
        /// <param name="tableName">返回结果集的名字</param>
        /// <returns>查询结果集</returns>
        public DataTable GetQueryStro(string stroName, Hashtable inQuery, string tableName)
        {
            SqlCommand myCmd = new SqlCommand();
            ds = new DataSet();
            try
            {
                myConn = GetConnection();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.CommandText = stroName;
                myCmd = setQueryPar(myCmd , inQuery);
                adapt = new SqlDataAdapter();
                myConn.Open();
                adapt.SelectCommand = myCmd;
                adapt.Fill(ds, tableName);
                myCmd.Parameters.Clear();
                return ds.Tables[tableName];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                myConn.Close();
                adapt.Dispose();
                ds.Dispose();
                myCmd.Dispose();
                myConn.Dispose();
            }
        }
    }
}