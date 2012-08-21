using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.ecard
{
    public partial class exchange : System.Web.UI.Page
    {
        private moyu.Data.Db myDb = new Data.Db();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = textName.Text.Trim();
            string number = textNumber.Text.Trim();
            string jf = textJf.Text.Trim();
            if (name == string.Empty)
            {
                Response.Write("<script>alert(\"名称不能为空\");</script>");
                textName.Focus();
                return;
            }
            try
            {
                Convert.ToInt32(number);
            }
            catch
            {
                Response.Write("<script>alert(\"数量输入错误\");</script>");
                textNumber.Text = "";
                textNumber.Focus();
                return;
            }
            try
            {
                Convert .ToInt32(jf);
            }
            catch
            {
                Response.Write("<script>alert(\"积分输入错误\");</script>");
                textJf.Text="";
                textJf .Focus();
                return;
            }
            moyu.Images myImg = new Images();
            string image = myImg.upLoadForExchange(FileUpload1.PostedFile, Server.MapPath("~"));
            moyu.Ecard.Living myLiving=new Ecard.Living();
            if(myLiving.exchangeAdd(name,Convert .ToInt32(number),Convert .ToInt32(jf),image))
            {
                Response.Write("<script>alert(\"添加陈功\");</script>");
                textJf.Text = "";
                textName.Text="";
                textNumber.Text="";
            }
            else
            {
                Response.Write("<script>alert(\"添加失败\");</script>");
            }
        }
    }
}