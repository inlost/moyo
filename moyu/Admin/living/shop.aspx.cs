using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.living
{
    public partial class shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cat = Convert.ToInt32(DropDownList1.SelectedValue);
            string name = TextName.Text.Trim();
            string introduce = TextIntroduce.Text.Trim();
            string address = TextAddress.Text.Trim();
            string phone = TextPhone.Text.Trim();
            double pointX = Convert.ToDouble(TextX.Text.Trim());
            double pointY = Convert.ToDouble(TextY.Text.Trim());
            string time = TextTime.Text.Trim();
            bool isDisplay = RadioButtonList1.SelectedValue == "0" ? false : true;
            bool isActive = true;
            int source = 0;
            moyu.Upload.image myImage = new Upload.image();
            myImage.UploadFileSize = 3;
            myImage.WaterMarkMode = 1;
            myImage.Minwidth = 50;
            myImage.Minheight = 50;
            myImage.Suowidth = 60;
            myImage.IsRate = true;
            DateTime dt = DateTime.Now;
            string uploadPath = "\\upload\\images\\" + dt.Year.ToString() + "\\" + dt.Month + "\\" + dt.Day + "\\";
            if (FileUpload1.HasFile)
            { 
                myImage.Upload(uploadPath, FileUpload1.PostedFile);
            }
            string pic =FileUpload1.HasFile? (uploadPath+ myImage.TFullName):"";
            Living.shops myShop = new Living.shops();
            myShop.shop_add(cat, pic, name, introduce, address, phone, pointX, pointY, time, isDisplay, isActive, source);
            TextName.Text = "";
            TextIntroduce.Text = "";
            TextAddress.Text = "";
            TextPhone.Text = "";
            TextX.Text = "";
            TextY.Text = "";
            TextTime.Text = "";
        }
    }
}