using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Collections;
namespace moyu
{
    public class Images
    {
        public string UpLoadForClothes(HttpPostedFile uploadFile, string uploadpath)
        {
            Hashtable rst = new Hashtable();
            rst = UpLoad(uploadFile, uploadpath);
            MakeThumbnail(rst["uploadpath"].ToString() + rst["filename"], rst["uploadpath"] + "a" + rst["filename"], 225, 463);
            return rst["url"].ToString();
        }
        public string upLoadForExchange(HttpPostedFile uploadFile, string uploadpath)
        {
            Hashtable rst = new Hashtable();
            rst = UpLoad(uploadFile, uploadpath);
            MakeThumbnail(rst["uploadpath"].ToString() + rst["filename"], rst["uploadpath"] + "a" + rst["filename"], 162, 162);
            return rst["url"].ToString();
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="uploadFile">HttpPostedFile</param>
        /// <param name="uploadpath">uploadpath（Server.MapPath("~")）</param>
        /// <returns></returns>
        private Hashtable UpLoad(HttpPostedFile uploadFile, string uploadpath)
        {
            Hashtable rst = new Hashtable();
            //上传配置
            uploadpath += "upload\\images\\"+DateTime.Now.Year+"\\"+DateTime.Now.Month+"\\";
            String pathbase = "/upload/images/"+DateTime.Now.Year+"/"+DateTime.Now.Month+"/";                                      //保存路径
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };          //文件允许格式
            int size = 1024;                                                          //文件大小限制，单位KB

            //文件上传状态,初始默认成功，可选参数{"SUCCESS","ERROR","SIZE","TYPE"}
            String state = "SUCCESS";

            String title = String.Empty;
            String filename = String.Empty;
            String url = String.Empty;
            String currentType = String.Empty;

            try
            {
                title = uploadFile.FileName;

                //目录验证
                if (!Directory.Exists(uploadpath))
                {
                    Directory.CreateDirectory(uploadpath);
                }

                //格式验证
                string[] temp = uploadFile.FileName.Split('.');
                currentType = "." + temp[temp.Length - 1];
                if (Array.IndexOf(filetype, currentType) == -1)
                {
                    state = "TYPE";
                }

                //大小验证
                if (uploadFile.ContentLength / 1024 > size)
                {
                    state = "SIZE";
                }

                //保存图片
                if (state == "SUCCESS")
                {
                    filename = System.Guid.NewGuid() + currentType;
                    uploadFile.SaveAs(uploadpath + filename);
                    url = pathbase + filename;
                    rst["state"] = state;
                    rst["uploadpath"] = uploadpath;
                    rst["filename"] = filename;
                    rst["url"] = url;
                }
                return rst;
            }
            catch (Exception)
            {
                state = "ERROR";
                rst["state"] = state;
                return rst;
            }
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            //switch (mode)
            //{
            //    case "HW"://指定高宽缩放（可能变形）                
            //        break;
            //    case "W"://指定宽，高按比例                    
            toheight = originalImage.Height * width / originalImage.Width;
            //        break;
            //    case "H"://指定高，宽按比例
            //        towidth = originalImage.Width * height / originalImage.Height;
            //        break;
            //    case "Cut"://指定高宽裁减（不变形）                
            //        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
            //        {
            //            oh = originalImage.Height;
            //            ow = originalImage.Height * towidth / toheight;
            //            y = 0;
            //            x = (originalImage.Width - ow) / 2;
            //        }
            //        else
            //        {
            //            ow = originalImage.Width;
            //            oh = originalImage.Width * height / towidth;
            //            x = 0;
            //            y = (originalImage.Height - oh) / 2;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //toheight = originalImage.Height * width / originalImage.Width;
            //towidth = originalImage.Width * height / originalImage.Height;
            //if (toheight > height)
            //{
            //    toheight = height;
            //}
            //if (towidth > width)
            //{
            //    towidth = width;
            //}
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            //catch (System.Exception e)
            //{
            //    throw e;
            //}
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}