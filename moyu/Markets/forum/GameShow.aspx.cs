using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace moyu.Markets.forum
{
    public partial class GameShow : System.Web.UI.Page
    {
        private Hashtable theGame = new Hashtable();
        private Information.Game myGame=new Information.Game();
        protected void Page_Load(object sender, EventArgs e)
        {
            theGame = myGame.gameGet(Convert.ToInt32(Request.Params["gid"]));
        }
        public void getName()
        {
            Response.Write(theGame["m_name"]+"_游戏");
        }
        public void getUrl()
        {
            Response.Write(theGame["m_playdata"]);
        }
        public void getIntroduce()
        {
            Response.Write(theGame["m_des"]);
        }
    }
}