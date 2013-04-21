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
    public partial class robot_group_topUser : System.Web.UI.Page
    {
        private Information.group myGroup = new Information.group();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void getTitle()
        {
            Response.Write(DateTime.Now.ToShortDateString()+"左邻排行");
        }
        public void getUserPoint()
        {
            if (Session["isLogin"] != null && Session["isLogin"].ToString() == "true")
            {
                moyu.Data.Db myDb = new Data.Db();
                Hashtable inQuery = new Hashtable();
                inQuery["@maxPoint"] = 10;
                inQuery["@uid"] = Session["id"].ToString();
                inQuery["@point"] = 10;
                inQuery["@date"] = DateTime.Now.ToShortDateString();
                int pointAllowAdd = Convert.ToInt32(moyu.Data.Type.dtToHash(myDb.GetQueryStro("user_point_postAdd", inQuery, "rt"))[0]["point"]);
                StringBuilder sb = new StringBuilder();
                sb.Append("<li class=\"postItem\">");
                sb.Append("<h2 class=\"group-post-info clear\" style=\"text-indent:0;\">");
                sb.Append(Session["niceName"].ToString() + "，今天的10分已经拿到<span" + getWidthStyle(10 - pointAllowAdd) + " class=\"group-post-info-tag group_tag_5\">" + (10 - pointAllowAdd) + "</span>分了，还有<span" + getWidthStyle(pointAllowAdd) + " class=\"group-post-info-tag group_tag_4\">" + pointAllowAdd + "</span>分可拿。");
                sb.Append("</h2></li>");
                Response.Write(sb);
            }
        }
        private string getWidthStyle(int point)
        {
            if (point == 0)
            {
                return " style=\"width:10%;\" ";
            }
            else
            {
                return " style=\"width:"+point+"0%;\" ";
            }
        }
        public void getContent()
        {
            Hashtable[] list;
            list = myGroup.pointListGet(10, -15);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < list.Length; i++)
            {
                sb.Append("<li class=\"postItem\">");
                sb.Append("<h2 class=\"group-post-info clear\" style=\"text-indent:0;\">");
                sb.Append("<span class=\"left group-post-info-tag group_tag_"+i+"\">"+(i+1)+(i<5?"  管理":"")+"</span>");
                sb.Append("<span class=\"left group-post-info-user\">"+list[i]["niceName"]+"</span>");
                sb.Append("<span class=\"right group-post-info-point\">" + list[i]["point"] + "</span>");
                sb.Append("</h2></li>");
            }
            Response.Write(sb);
        }
        public void getNotRead()
        {
            int count = 0;
            try
            {
                count = myGroup.getNotReadAtCount(Convert.ToInt32(Session["id"]));
            }
            catch
            {
                count = 0;
            }
            if (count != 0)
            {
                Response.Write("<span class=\"notReadCount\">" + count + "</span>");
            }
        }
    }
}