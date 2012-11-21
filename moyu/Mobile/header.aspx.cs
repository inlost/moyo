using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
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
    }
}