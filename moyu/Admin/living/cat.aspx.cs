using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.living
{
    public partial class cat : System.Web.UI.Page
    {
        private Living.shops myShop = new Living.shops();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextName.Text.Trim() != "" && TextOrder.Text.Trim() != "")
            {
                myShop.cat_add(TextName.Text.Trim(), Convert.ToInt32(TextOrder.Text.Trim()));
                TextName.Text = string.Empty;
                TextOrder.Text = string.Empty;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int cat = Convert.ToInt32(DropDownList1.SelectedValue);
                string title = TextAdTitle.Text.Trim();
                string url = TextAdUrl.Text.Trim();
                int type = 0;
                moyu.Upload.image myImage = new Upload.image();
                myImage.UploadFileSize = 3;
                myImage.WaterMarkMode = 1;
                myImage.Minwidth = 50;
                myImage.Minheight = 50;
                myImage.IsSuoImg = true;
                myImage.Suoheight = 150;
                myImage.Suowidth = 150;
                myImage.IsRate = true;
                DateTime dt=DateTime.Now;
                string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
                myImage.Upload(uploadPath, FileUpload1.PostedFile);
                myShop.cat_ad_add(cat, title, uploadPath+ myImage.TFullName, url, type);
            }
            catch
            { 
                
            }
        }
    }
}