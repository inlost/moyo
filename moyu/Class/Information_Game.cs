using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
namespace moyu.Information
{
    public class Game
    {
        private Data.Db myDb = new Data.Db();
        /// <summary>
        /// 获取热门游戏
        /// </summary>
        /// <param name="count">总数</param>
        /// <returns>游戏们</returns>
        public Hashtable[] getHotGame(int count)
        {
            string strSql = "select top(" + count + ") * from information_game order by m_digg desc";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"));
        } 
        /// <summary>
        /// 游戏分类获取
        /// </summary>
        /// <returns>分类们</returns>
        public Hashtable[] catGet()
        {
            string strSql = "select * from information_game_cat order by m_sort";
            return Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
        }
        /// <summary>
        /// 分类游戏获取
        /// </summary>
        /// <param name="cat">分类</param>
        /// <param name="count">条数</param>
        /// <param name="last">最后一条</param>
        /// <returns></returns>
        public Hashtable[] gamesGet(int cat, int count, int last)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@cat"] = cat;
            inQuery["@count"]=count;
            inQuery["@last"] = last;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_games_get", inQuery, "rt"));
        }
        /// <summary>
        /// 游戏获取
        /// </summary>
        /// <param name="gid">游戏编号</param>
        /// <returns>游戏</returns>
        public Hashtable gameGet(int gid)
        {
            Hashtable inQuery = new Hashtable();
            inQuery["@gid"] = gid;
            return Data.Type.dtToHash(myDb.GetQueryStro("information_game_get", inQuery, "rt"))[0];
        }
    }
}