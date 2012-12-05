using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
namespace moyu.Mobile
{
    public partial class header : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["wu"] != null && Session["isLogin"]==null)
            {
                Api.WeiXin myWeiXin = new Api.WeiXin();
                int uid = myWeiXin.getWeiUserId(Request.Params["wu"]);
                if (uid != 0)
                {
                    Hashtable theUser = new Hashtable();
                    moyu.User.Web myUser = new User.Web();
                    theUser = myUser.get(uid);
                    Session["isLogin"] = "true";
                    Session["guid"] = theUser["id"].ToString();
                    foreach (DictionaryEntry infoPar in theUser)
                    {
                        Session[infoPar.Key.ToString()] = infoPar.Value.ToString();
                    }
                    Session["password"] = null;
                }
            }
        }
        public void getNotRead()
        {
            Information.group myGroup = new Information.group();
            int count = myGroup.getNotReadAtCount(Convert.ToInt32(Session["id"]));
            if (count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div id=\"messageTip\">");
                sb.Append("有<span class=\"notReadCount\">" + count + "</span>个人提到你了，快<a href=\"robot-group-kewWordsShow.aspx?type=atMe\">去看看</a>吧。");
                sb.Append("</div>");
                Response.Write(sb);
            }
        }
    }
}