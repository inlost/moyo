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
        private new bool IsRarPic;
        private new int Rarwidth;
        private new int Suowidth;
        private new bool IsSuowidth;
        private new bool IsSuoImg;
        private new bool IsRate;
        private new string UploadFileExt;
        private new bool IsAddWaterMark;

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

        public void newUpload(int uid, HttpPostedFile postAvatar)
        { 
            
        }
        public new string RenameFile(string upOriFileName, string upOriFileExt)
        {
            string tempName = null;
            if (this.isUseRandFileName)
            {
                Random rd = new Random();
                int tempR = rd.Next(1, 1000);
                tempName = Guid.NewGuid().ToString() + tempR + reName;
                tempName = tempName + "-600_600." +  upOriFileExt; ;
            }
            else
            {
                tempName = upOriFileName;
            }
            return tempName;
        }
        private new bool Upload(string fpath, HttpPostedFile myFileUpload)
        {
            string fileExtends = "";
            string fileName = myFileUpload.FileName.Trim();
            //判断是否选取上传图片或图片名为空
            if (!string.IsNullOrEmpty(fileName))
            {
                FileExtension[] fe = { FileExtension.GIF, FileExtension.JPG, FileExtension.PNG };
                if (FileValidation.IsAllowedExtension(myFileUpload, fe))
                {
                }
                else
                {
                    msg = "请上传" + this.uploadFileExt + "格式图片!";
                    return false;
                }
                //取得图片后缀
                fileExtends = this.GetFileExtends(fileName);
                //判断图片类型是否合法
                if (!this.IsFileExtendsOk(fileExtends))
                {
                    msg = "上传图片类型不合法!";
                    return false;
                }
                else
                {
                    //判断待传图片大小是否超出范围
                    if (!this.ISFileSizeOK(myFileUpload))
                    {
                        msg = "图片最大为 " + this.uploadFileSize + "M!";
                        return false;
                    }
                    else
                    {
                        if (!this.ISimgOK(myFileUpload))
                        {
                            msg = "图片宽度应在（" + this.minwidth + " - " + this.maxwidth + "）之间，高度在（" + this.minheight + " - " + this.maxheight + "）之间!";
                            return false;
                        }
                        else
                        {
                            //重命名图片
                            OFullName = RenameFile(fileName, fileExtends);
                            TFullName = OFullName.Replace("T", "S");
                            if (fpath.LastIndexOf(@"/") < 0 || fpath.LastIndexOf(@"") < 0)
                            {
                                fpath = fpath + "\\";
                            }
                            fpath = System.Web.HttpContext.Current.Server.MapPath("~") + fpath;
                            //如上传目录为不存在，则创建目录
                            if (!Directory.Exists(fpath))
                            {
                                Directory.CreateDirectory(fpath);
                            }
                            //最终经处理的图片处理路径及图片名
                            //string file = fpath + s;
                            //确保图片唯一性，以免错误覆盖
                            string file = BeSureOneFile(fpath, OFullName);
                            string file2 = BeSureOneFile(fpath, TFullName);
                            try
                            {
                                if (this.isRarPic)
                                {
                                    int aheight;
                                    int awidth = rarwidth;
                                    //按比例计算出大图的宽度和高度  awidth  suoheight twidth theight
                                    if (twidth >= awidth)
                                    {
                                        aheight = (int)Math.Floor(Convert.ToDouble(theight) * (Convert.ToDouble(awidth) / Convert.ToDouble(twidth)));//等比设定高度
                                    }
                                    else
                                    {
                                        awidth = twidth;
                                        aheight = theight;
                                    }
                                    //生成缩略原图
                                    Bitmap tImage = new Bitmap(awidth, aheight);
                                    Graphics g = Graphics.FromImage(tImage);
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法
                                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
                                    g.Clear(Color.Transparent); //清空画布并以透明背景色填充
                                    Stream oStream = myFileUpload.InputStream;
                                    System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
                                    g.DrawImage(oImage, new Rectangle(0, 0, awidth, aheight), new Rectangle(0, 0, twidth, theight), GraphicsUnit.Pixel);
                                    tImage.Save(file);
                                }
                                else
                                {
                                    myFileUpload.SaveAs(file);
                                }

                                //添加水印
                                if (this.isAddWaterMark)
                                {
                                    this.addWaterMark(file, fpath, fileName, this.waterMarkMode);
                                }
                                //生成缩略图
                                if (this.isSuoImg)
                                {
                                    if (this.isSuowidth)
                                    {
                                        suoheight = (int)Math.Floor(Convert.ToDouble(theight) * (Convert.ToDouble(suowidth) / Convert.ToDouble(twidth)));//等比设定高度
                                    }
                                    else
                                    {
                                        if (this.isRate)
                                        {
                                            //按比例计算出缩略图的宽度和高度  suowidth suoheight twidth  theight
                                            if (twidth >= theight)
                                            {
                                                suoheight = (int)Math.Floor(Convert.ToDouble(theight) * (Convert.ToDouble(suowidth) / Convert.ToDouble(twidth)));//等比设定高度
                                            }
                                            else
                                            {
                                                suowidth = (int)Math.Floor(Convert.ToDouble(twidth) * (Convert.ToDouble(suoheight) / Convert.ToDouble(theight)));//等比设定宽度
                                            }
                                        }
                                    }
                                    //生成缩略原图
                                    Bitmap tImage = new Bitmap(suowidth, suoheight);
                                    Graphics g = Graphics.FromImage(tImage);
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法
                                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
                                    g.Clear(Color.Transparent); //清空画布并以透明背景色填充
                                    Stream oStream = myFileUpload.InputStream;
                                    System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
                                    g.DrawImage(oImage, new Rectangle(0, 0, suowidth, suoheight), new Rectangle(0, 0, twidth, theight), GraphicsUnit.Pixel);
                                    tImage.Save(file2);
                                }
                                msg = string.Format("图片{0}上传成功!", fileName);
                                issuccess = true;
                                return false;
                            }
                            catch (Exception ee)
                            {
                                throw new Exception(ee.ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                msg = "您没有选择待上传图片!";
                return false;
            }
        }
    }
}