using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace moyu.Admin.clothes
{
    public partial class clothes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            moyu.Images myImg=new moyu.Images();
            int cat =Convert.ToInt32( DropDownCat.SelectedValue);
            double oldPrice = Convert.ToDouble(TextOldPrice.Text);
            double salePrice = Convert.ToDouble(TextSalePrice.Text);
            int inventory = Convert.ToInt32(TextInventory.Text);
            int volume = 0;
            string title = TextName.Text;
            string introduce = TextIntroduce.Text;
            string image = myImg.UpLoadForClothes(FileUpload1.PostedFile, Server.MapPath("~"));
            Sale.Clothes myClothes = new Sale.Clothes();
            if (myClothes.add(cat, oldPrice, salePrice, inventory, volume, image, title, introduce))
            {
                Response.Redirect("clothes.aspx");
            }
            else
            {
                Response.Write("<script>alert(\"添加失败\");</script>");
            }
        }
    }
}