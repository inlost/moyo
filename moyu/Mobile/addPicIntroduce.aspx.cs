using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Mobile
{
    public partial class addPicIntroduce : System.Web.UI.Page
    {
        int tid, pid;
        Hashtable thePost = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            tid=Convert .ToInt32( Request.Params["tid"]);
            pid = Convert.ToInt32(Request.Params["pid"]);
            Information.group myGroup = new Information.group();
            thePost = myGroup.topicGetById(tid);
        }
        public void getBody()
        {
            if (pid != 0)
            {
                Response.Write(thePost["body"]);
            }
            else
            {
                Response.Write("今天心情怎么样？");
            }
        }
        public void getBtnText()
        {
            if (pid != 0)
            {
                Response.Write("添加说明");
            }
            else
            {
                Response.Write("记录心情");
            }
        }
        public void getTid()
        {
            Response.Write(tid);
        }
        public void getPid()
        {
            Response.Write(pid);
        }
    }
}