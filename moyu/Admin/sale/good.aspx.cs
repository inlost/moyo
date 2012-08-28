using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.sale
{
    public partial class good : System.Web.UI.Page
    {
        private Sale.Goods myGoods = new Sale.Goods();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cid = Convert .ToInt32( DropDownList2.SelectedValue);
            string name = TextName.Text.Trim();
            string seller = TextSeller.Text.Trim();
            int saleType = Convert.ToInt32(DropDownList3.SelectedValue);
            string introduce = TextIntroduce.Text.Trim();
            double price = Convert.ToDouble(TextPrice.Text);
            moyu.Upload.image myImage = new Upload.image();
            myImage.UploadFileSize = 3;
            myImage.WaterMarkMode = 1;
            myImage.Minwidth = 90;
            myImage.Minheight = 90;
            myImage.Suowidth = 150;
            myImage.IsRate = true;
            DateTime dt = DateTime.Now;
            string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
            myImage.Upload(uploadPath, FileUpload1.PostedFile);
            if (name.Length > 4 && myImage.IsSuccess)
            {
                myGoods.goodAdd(cid, name, price , uploadPath + myImage.TFullName, introduce, seller, saleType);
            }
            Response.Redirect("good.aspx");
        }
    }
}