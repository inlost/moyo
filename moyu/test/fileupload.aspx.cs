using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using moyu.Data;
namespace moyu.test
{
    public partial class fileupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Storage myStorage = new Storage();
            Response.Write(myStorage.uploadFile(Storage.Bucket.image, "test/aa.jpg", "G:\\WebProject\\moyu\\moyu\\Images\\had-1.jpg", FileUpload222.PostedFile.ContentType));
        }
    }
}