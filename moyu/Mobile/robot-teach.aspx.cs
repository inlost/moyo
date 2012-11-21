using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Mobile
{
    public partial class robot_teach : System.Web.UI.Page
    {
        string q;
        Hashtable rules = new Hashtable();
        moyu.Data.Db myDb = new Data.Db();
        public bool isShowTeach = true;
        Hashtable user = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            q= HttpUtility.UrlDecode(Request.Params["q"]).Replace("？", "").Replace("?", "");
            string strSql;
            Hashtable[] keyWords;
            if (Cache["weixinRobotKeywords"] == null)
            {
                strSql = "select * from weixin_robot_rules_keyWords";
                keyWords = moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql, "rt"));
                Cache.Insert("weixinRobotKeywords", keyWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            else
            {
                keyWords = (Hashtable[])Cache["weixinRobotKeywords"];
            }
            strSql = "select * from weixin_robot_rules where id =";
            foreach (Hashtable keyWord in keyWords)
            {
                if (q.IndexOf(keyWord["keyWord"].ToString()) > -1)
                {
                    strSql += Convert.ToInt32(keyWord["rulesId"]).ToString();
                    rules = moyu.Data.Type.dtToHash(myDb.GetQuerySql(strSql,"rt"))[0];
                    isShowTeach = false;
                    moyu.User.Web myUser = new User.Web();
                    user= myUser.get(Convert.ToInt32(rules["uid"]));
                    break;
                }
            }
        }
        public void getRuleBody()
        {
            Response .Write (rules["body"]);
        }
        public void getNiceName()
        {
            Response.Write(user==null?"匿名用户":user["niceName"]);
        }
        public void getQuestion()
        {
            Response.Write(HttpUtility.UrlDecode( Request.Params["q"]));
        }
        public void getRobotQuestion()
        {
            Response.Write(q);
        }
    }
}