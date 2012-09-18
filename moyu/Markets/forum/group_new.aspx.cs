using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Markets.forum
{
    public partial class group_new : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        private Hashtable theGroup = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            theGroup = myGroup.group_get_byId(Convert.ToInt32(Request.Params["id"]));
        }
        public void getName()
        {
            Response.Write(theGroup["name"]);
        }
        public void getGid()
        {
            Response.Write(Request.Params["id"]);
        }
        public void getGroupIcon()
        {
            Response.Write(theGroup["img"]);
        }
    }
}