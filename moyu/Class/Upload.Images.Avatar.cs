using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace moyu.Upload
{
    /// <summary>
    /// 头像
    /// </summary>
    public class Avatar:Upload.image
    {
        public Avatar()
        {
            isRarPic = true;
            rarwidth = 600;
            isAddWaterMark = false;
            isUseRandFileName = true;
            uploadFileSize = 1;
            suowidth = 100;
            suoheight = 100;
            IsRate = false;
        }
        /// <summary>
        /// 头像上传
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="x1">选择区域左上点X坐标</param>
        /// <param name="y1">选择区域左上点Y坐标</param>
        /// <param name="height">选择区域的高度</param>
        /// <param name="width">选择区域的宽度</param>
        /// <param name="src">上传文件的路径</param>
        public bool newUpload(int uid, int x1,int y1,int height,int width,string src)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~") + src.Replace("/","\\");
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            //文件夹路径
            string tempPath="\\upload\\Avatar\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
            path = System.Web.HttpContext.Current.Server.MapPath("~") + tempPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileExtends = this.GetFileExtends(src.Replace("/", "\\"));//获取后缀
            
            //保存头像们
            string guid=Guid.NewGuid().ToString("N");
            path += guid;
            try
            {
                saveAvatar(image, x1, y1, width, height, 320, 320, path + "-320_320." + fileExtends);
                saveAvatar(image, x1, y1, width, height, 160, 160, path + "-160_160." + fileExtends);
                saveAvatar(image, x1, y1, width, height, 64, 64, path + "-64_64." + fileExtends);
                saveAvatar(image, x1, y1, width, height, 32, 32, path + "-32_32." + fileExtends);
                Data.Db myDb = new Data.Db();
                System.Collections.Hashtable inQuery=new System.Collections.Hashtable();
                inQuery["@uid"] = uid;
                inQuery["@avatar"] = tempPath + guid + "-320_320." + fileExtends;
                myDb.ExecNoneQuery("user_avatar_update", inQuery);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                image.Dispose();
            }
        }
        /// <summary>
        /// 头像保存
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="x1">选择区域左上点X坐标</param>
        /// <param name="y1">选择区域左上点Y坐标</param>
        /// <param name="areaHeight">选择区域高</param>
        /// <param name="areaWidth">选择区域宽</param>
        /// <param name="height">生成头像的高</param>
        /// <param name="width">生成头像的宽</param>
        /// <param name="path">保存路径</param>
        private void saveAvatar(System.Drawing.Image img,int x1,int y1,int areaHeight,int areaWidth,int height, int width, string path)
        {
            Bitmap b = new Bitmap(width,height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(img, new Rectangle(0, 0, height, width), new Rectangle(x1, y1, areaWidth, areaHeight), GraphicsUnit.Pixel);
            b.Save(path);
            b.Dispose();
        }
    }
}