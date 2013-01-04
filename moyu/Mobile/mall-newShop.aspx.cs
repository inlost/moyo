using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace moyu.Mobile
{
    public partial class mall_newShop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["msg"] != null)
            {
                int msg = Convert.ToInt32(Request.Params["msg"]);
                if (msg == 1)
                {
                    Response.Write("<script>alert('提交成功，工作人员会马上与您取得联系');</script>");
                }
                else if (msg == -3)
                {
                    Response.Write("<script>alert('电话号码不能为空且只能为纯数字，请重新填写后提交');</script>");
                }
                else if (msg == -1)
                {
                    Response.Write("<script>alert('请完整填写表单后重新提交');</script>");
                }
            }
        }
        public void getApplys()
        {
            Mall.apply myApply=new Mall.apply();
            Hashtable[] applys;
            int last = Request.Params["last"] != null ? Convert.ToInt32(Request.Params["last"]) : 0;
            applys = myApply.get(15,last);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Hashtable apply in applys)
            {
                sb.Append("<li>");
                sb.Append("<p>" + apply["shopName"] + "---" + apply["shopBoss"] + "<br/>");
                sb.Append("<p>" + apply["what"] + "---" + apply["date"] + "<br/>");
                sb.Append("<p><a href=\"tel:" + apply["tel"] + "\">" + apply["tel"] + "</a></p>");
                sb.Append("</li>");
            }
            Response.Write(sb);
        }
    }
}