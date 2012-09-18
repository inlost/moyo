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
    public partial class weiXin : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        private Hashtable theTopic = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            theTopic = myGroup.topicGetById(Convert.ToInt32(Request.Params["id"]));
        }
        public void getTitle()
        {
            Response.Write(theTopic["title"]);
        }
        public void getContent()
        {
            Response.Write(theTopic["body"]);
        }
        public void getTime()
        {
            Response.Write(theTopic["postDate"]);
        }
    }
}