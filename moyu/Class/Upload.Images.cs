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
    public class image
    {
        //YHWPicUpload upload = new YHWPicUpload();
        //upload.UploadFileExt = "gif|jpg|jpeg|png";  //  允许上传文件格式,"|"分隔(默认gif|jpg|jpeg|png)
        //upload.UploadFileSize = 5;  //  允许上传文件大小,int型,单位字节(默认5M)
        //upload.IsUseRandFileName = true;             //  是否随机获取文件名(默认 true)
        //upload.IsAddWaterMark = true;                //  是否添加水印(默认 true)
        //upload.WaterMarkMode = 0;                     //  水印类型,0为图片水印,1为文字水印(默认 0)
        //upload.ImageWaterMark = @"logo.png";       //  图片水印地址(相对路径) 如：../logo.png
        //upload.WaterMarkPos = 3;                      //  图片/文字水印的水印位置 0为左上,1为右上,2为左下,3为右下(默认 3)
        //upload.ImageWaterMarkAlpha = 0.5f;              //  图片水印透明度,0-1的浮点型数字,不在此范围则取默认值(默认 0.7f)
        //upload.WatermarkText = "www.ycfcw.cn";     //  文字水印的添加文字(默认 www.ycfcw.cn!)
        //upload.Maxwidth = 5000;  //最大宽度 超过该宽度图片将无法上传，下面以此类推 (默认700)
        //upload.Maxheight = 5000; //最大高度(默认1000)
        //upload.Minwidth = 150; //最小宽度 (默认150)
        //upload.Minheight = 100; //最小高度 (默认100)
        //upload.IsRarPic = true; //是否压缩原图，当设置为true时，建议把最大宽度和最大高度设置大一些，不然就没意义了  (默认false)
        //upload.Rarwidth = 600;  //压缩为的宽度，高度不限 将大图压缩为该宽度，高度为等比例计算的高度 (默认700)
        //upload.IsSuoImg = true;  //是否生成缩略图(默认true)
        //upload.Suowidth = 150; //缩略图 宽度(默认150)
        //upload.Suoheight = 100; //缩略图 高度(默认100)
        //upload.IsRate = false;  //缩略图 是否等比例，即高度为等比例计算的高度,缩略图宽度和高度谁先到取谁(默认false)
        //upload.IsSuowidth = false; //缩略图 宽度 锁定，即把缩略图锁定在此宽度，高度为等比例计算的高度(默认false)
        //upload.ReName = 888; //加上登录Cookies,例如：用ID命名 防止重复 (默认1234)
        //upload.Upload("\\YHWupimg\\pic", FileUpload1);
        //Label1.Text = upload.MSG + upload.IsSuccess;
        //bool IsSuccess = upload.IsSuccess;
        //if (IsSuccess == true)
        //{
        //    Image1.Visible = true;
        //    //upload.OFullName;  获得大图名称
        //    //upload.TFullName;  获得缩略图名称
        //    Image1.ImageUrl = "YHWupimg\\pic\\" + upload.TFullName.ToString();
        //}
        public image()
        {
            // 该C#类由叶华伟编写，QQ：517025143
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //允许上传图片类型，"|"隔开
        protected string uploadFileExt = "gif|jpg|jpeg|png";
        //允许上传图片大小，字节 为单位,默认5M
        protected int uploadFileSize = 5;
        //是否使用随机上传图片名
        protected bool isUseRandFileName = true;
        //是否添加水印
        protected bool isAddWaterMark = true;
        //添加水印类型(0为图片水印--默认,1为文字水印)
        protected int waterMarkMode = 1;
        //图片水印地址
        protected string imageWaterMark = System.Web.HttpContext.Current.Server.MapPath("logo.png");
        //图片/文字水印的水印位置(0为左上,1为右上,2为左下,3为右下--默认)
        protected int waterMarkPos = 3;
        //默认图片水印透明度
        protected const float imageAlpha = 0.7f;
        //图片水印的透明度
        protected float imageWaterMarkAlpha = imageAlpha;
        //文字水印的添加文字
        protected string watermarkText = "www.ai0932.com";
        //是否压缩原图
        protected bool isRarPic = false;
        public bool IsRarPic  ////是否压缩原图
        {
            get { return isRarPic; }
            set { isRarPic = value; }
        }
        //压缩为的宽度，高度不限
        protected int rarwidth = 700;
        public int Rarwidth   //压缩为的宽度，高度不限
        {
            get { return rarwidth; }
            set { rarwidth = value; }
        }
        //返回值 是否成功
        protected bool issuccess = false;
        //返回值
        protected string msg = "";
        protected int twidth, theight;
        //重命名添加值
        protected int reName = 1234;
        //图片最小 宽度
        protected int minwidth = 150;
        //图片最大 宽度
        protected int maxwidth = 700;
        //图片最小 高度
        protected int minheight = 100;
        //图片最大 高度
        protected int maxheight = 1000;
        //是否生成缩略图
        protected bool isSuoImg = true;
        //生成缩略图 是否等比例
        protected bool isRate = false;
        //缩略图 宽度
        protected int suowidth = 150;
        //缩略图 宽度锁定
        protected bool isSuowidth = false;
        //缩略图 高度
        protected int suoheight = 100;
        protected string _ofullname = "0";  //原始图
        protected string _tfullname = "0";  //缩略图
        public int Suowidth   //缩略图 宽度
        {
            get { return suowidth; }
            set { suowidth = value; }
        }
        public int Suoheight   //缩略图 高度
        {
            get { return suoheight; }
            set { suoheight = value; }
        }
        public int ReName   //重命名添加值
        {
            get { return reName; }
            set { reName = value; }
        }
        public bool IsSuccess  ////返回值 是否成功
        {
            get { return issuccess; }
            set { issuccess = value; }
        }
        public bool IsSuowidth  //缩略图 宽度锁定
        {
            get { return isSuowidth; }
            set { isSuowidth = value; }
        }
        public bool IsSuoImg  //是否生成缩略图
        {
            get { return isSuoImg; }
            set { isSuoImg = value; }
        }
        public bool IsRate  //生成缩略图 是否等比例
        {
            get { return isRate; }
            set { isRate = value; }
        }
        /// <summary>
        /// 返回值
        /// </summary>
        public string MSG
        {
            get { return msg; }
            set { msg = value; }
        }
        /// <summary>
        /// 原始图
        /// </summary>
        public string OFullName
        {
            get { return _ofullname; }
            set { _ofullname = value; }
        }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string TFullName
        {
            get { return _tfullname; }
            set { _tfullname = value; }
        }
        public int Minwidth  //图片最小 宽度
        {
            get { return minwidth; }
            set { minwidth = value; }
        }
        public int Maxwidth  //图片最大 宽度
        {
            get { return maxwidth; }
            set { maxwidth = value; }
        }
        public int Minheight  //图片最小 高度
        {
            get { return minheight; }
            set { minheight = value; }
        }
        public int Maxheight  //图片最大 高度
        {
            get { return maxheight; }
            set { maxheight = value; }
        }

        public string UploadFileExt  //允许上传图片类型
        {
            get { return uploadFileExt; }
            set { uploadFileExt = value; }
        }
        public int UploadFileSize  //允许上传图片大小
        {
            get { return uploadFileSize; }
            set { uploadFileSize = value; }
        }
        public bool IsUseRandFileName  //是否使用随机上传图片名
        {
            get { return isUseRandFileName; }
            set { isUseRandFileName = value; }
        }
        public bool IsAddWaterMark  //是否添加水印
        {
            get { return isAddWaterMark; }
            set { isAddWaterMark = value; }
        }
        public int WaterMarkMode  //添加水印类型(0为图片水印--默认,1为文字水印)
        {
            get { return waterMarkMode; }
            set { waterMarkMode = value; }
        }
        public string ImageWaterMark  //图片水印地址
        {
            get { return imageWaterMark; }
            set { imageWaterMark = System.Web.HttpContext.Current.Server.MapPath(value); }
        }
        public int WaterMarkPos  //图片/文字水印的水印位置(0为左上,1为右上,2为左下,3为右下--默认)
        {
            get { return waterMarkPos; }
            set { waterMarkPos = value; }
        }
        public string WatermarkText  //文字水印的添加文字
        {
            get { return watermarkText; }
            set { watermarkText = value; }
        }
        public float ImageWaterMarkAlpha  //图片水印的透明度
        {
            get { return imageWaterMarkAlpha; }
            set { imageWaterMarkAlpha = value; }
        }
        ///取得图片后缀#region 取得图片后缀
       
        /// <summary>
        /// 取得图片后缀
        /// </summary>
        /// <param name="filename">图片名称</param>
        /// <returns></returns>
        public string GetFileExtends(string filename)
        {
            string ext = null;
            if (filename.IndexOf('.') > 0)
            {
                string[] fs = filename.Split('.');
                ext = fs[fs.Length - 1];
            }
            return ext;
        }
        ///检测图片是否合法#region 检测图片是否合法
       
        /// <summary>
        /// 检测上传图片是否合法
        /// </summary>
        /// <param name="fileExtends">图片后缀名</param>
        /// <returns></returns>
        public bool IsFileExtendsOk(string fileExtends)
        {
            bool status = false;
            string[] fe = this.uploadFileExt.Split(new char[] { '|' });
            fileExtends = fileExtends.ToLower();
            for (int i = 0; i < fe.Length; i++)
            {
                if (fe[i].ToLower() == fileExtends)
                {
                    status = true;
                    break;
                }
            }
            return status;
        }
        ///检测图片大小是否合法#region 检测图片大小是否合法
       
        /// <summary>
        /// 检测上传图片大小是否合法
        /// </summary>
        /// <param name="uploadControl">控件id</param>
        /// <returns>true/false</returns>
        protected bool ISFileSizeOK(HttpPostedFile uploadControl)
        {
            long strLen = uploadControl.ContentLength;
            //判断上传图片大小
            if (strLen >= this.uploadFileSize * 1024 * 1024)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 检测图片 宽度和高度是否合法
        /// </summary>
        /// <param name="uploadControl">控件id</param>
        /// <returns>true/false</returns>
        protected bool ISimgOK(HttpPostedFile uploadControl)
        {
            Stream oStream = uploadControl.InputStream;
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
            twidth = oImage.Width; //原图宽度
            theight = oImage.Height; //原图高度
            //判断上传图片大小
            if (twidth >= this.minwidth & twidth <= this.maxwidth & theight >= this.minheight & theight <= this.maxheight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        ///确保图片名的唯一性，以免覆盖#region 确保图片名的唯一性，以免覆盖
       
        /// <summary>
        /// 确保图片名的唯一性，以免覆盖
        /// </summary>
        /// <param name="filePath">上传路径</param>
        /// <param name="fileName">图片名称</param>
        /// <returns>处理后的图片名</returns>
        public string BeSureOneFile(string filePath, string fileName)
        {
            //int i = 0;
            string PathAndName = filePath + fileName;
            //图片扩展名(含".")
            string tempExt = Path.GetExtension(fileName);
            while (File.Exists(PathAndName))
            {
                PathAndName = PathAndName.Replace("S-", "P-").Replace("T-", "P-");
            }
            return PathAndName;
        }

        ///重命名图片#region 重命名图片
       
        /// <summary>
        /// 重命名图片
        /// </summary>
        /// <param name="upOriFileName">原图片名</param>
        /// <param name="upOriFileExt">原图片扩展名</param>
        /// <returns>处理后的图片名</returns>
        public string RenameFile(string upOriFileName, string upOriFileExt)
        {
            string tempName = null;
            if (this.isUseRandFileName)
            {
                Random rd = new Random();
                int tempR = rd.Next(1, 1000);
                tempName = Guid.NewGuid().ToString("N")+ tempR + reName + "." +upOriFileExt;
                tempName = "T-" + tempName;
            }
            else
            {
                tempName = upOriFileName;
            }
            return tempName;
        }
        /// 判断图片是否伪装
        public enum FileExtension
        {
            JPG = 255216,
            GIF = 7173,
            PNG = 13780,
            //SWF = 6787,
            //RAR = 8297,
            //ZIP = 8075,
            //_7Z = 55122
        }
        /// 判断图片是否伪装
        public class FileValidation
        {
            public static bool IsAllowedExtension(HttpPostedFile fu, FileExtension[] fileEx)
            {
                int fileLen = fu.ContentLength;
                byte[] imgArray = new byte[fileLen];
                fu.InputStream.Read(imgArray, 0, fileLen);
                MemoryStream ms = new MemoryStream(imgArray);
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                string fileclass = "";
                byte buffer;
                try
                {
                    buffer = br.ReadByte();
                    fileclass = buffer.ToString();
                    buffer = br.ReadByte();
                    fileclass += buffer.ToString();
                }
                catch
                {
                }
                br.Close();
                ms.Close();
                foreach (FileExtension fe in fileEx)
                {
                    if (Int32.Parse(fileclass) == (int)fe)
                        return true;
                }
                return false;
            }
        }
        ///保存图片#region 保存图片
       
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="fpath">上传地址</param>
        /// <param name="myFileUpload">上传控件</param>
        /// <returns>处理结果</returns>
        public bool Upload(string fpath, HttpPostedFile myFileUpload)
        {
            string fileExtends = "";
            string fileName = myFileUpload.FileName.Trim();
            //判断是否选取上传图片或图片名为空
            if (!string.IsNullOrEmpty(fileName))
            {
                FileExtension[] fe = { FileExtension.GIF, FileExtension.JPG,FileExtension.PNG };
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
                            msg = "图片宽度应在（" + this.minwidth + " - " + this.maxwidth +"）之间，高度在（" + this.minheight + " - " + this.maxheight + "）之间!";
                            return false;
                        }
                        else
                        {
                            //重命名图片
                            OFullName = RenameFile(fileName, fileExtends);
                            TFullName = OFullName.Replace("T-","S-");
                            if (fpath.LastIndexOf(@"/") < 0 || fpath.LastIndexOf(@"") < 0)
                            {
                                fpath = fpath + "\\";
                            }
                            fpath = System.Web.HttpContext.Current.Server.MapPath("~") +fpath;
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
                                    aheight = (int)Math.Floor(Convert.ToDouble(theight) *(Convert.ToDouble(awidth) / Convert.ToDouble(twidth)));//等比设定高度
                                    //生成缩略原图
                                    Bitmap tImage = new Bitmap(awidth, aheight);
                                    Graphics g = Graphics.FromImage(tImage);
                                    g.InterpolationMode =System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法
                                    g.SmoothingMode =System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
                                    g.Clear(Color.Transparent); //清空画布并以透明背景色填充
                                    Stream oStream = myFileUpload.InputStream;
                                    System.Drawing.Image oImage =System.Drawing.Image.FromStream(oStream);
                                    g.DrawImage(oImage, new Rectangle(0, 0, awidth, aheight),new Rectangle(0, 0, twidth, theight), GraphicsUnit.Pixel);
                                    tImage.Save(file);
                                }
                                else
                                {
                                    myFileUpload.SaveAs(file);
                                }
                               
                                //添加水印
                                if (this.isAddWaterMark)
                                {
                                    this.addWaterMark(file, fpath, fileName,this.waterMarkMode);
                                }
                                //生成缩略图
                                if (this.isSuoImg)
                                {
                                    if (this.isSuowidth)
                                    {
                                        suoheight = (int)Math.Floor(Convert.ToDouble(theight)* (Convert.ToDouble(suowidth) / Convert.ToDouble(twidth)));//等比设定高度
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
                                    g.InterpolationMode =System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; //设置高质量插值法
                                    g.SmoothingMode =System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
                                    g.Clear(Color.Transparent); //清空画布并以透明背景色填充
                                    Stream oStream = myFileUpload.InputStream;
                                    System.Drawing.Image oImage =System.Drawing.Image.FromStream(oStream);
                                    g.DrawImage(oImage, new Rectangle(0, 0, suowidth,suoheight), new Rectangle(0, 0, twidth, theight), GraphicsUnit.Pixel);
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
        ///加入水印类型#region 加入水印类型
       
        /// <summary>
        /// 加入水印类型
        /// </summary>
        /// <param name="oldpath">原图片绝对地址(含图片名称)</param>
        /// <param name="newpath">新图片放置的绝对地址(不含图片名)</param>
        /// <param name="oldFileName">待加水印图片名</param>
        /// <param name="markMode">水印方式(图片/文字)</param>
        protected void addWaterMark(string oldpath, string newpath, string oldFileName, int markMode)
        {
            try
            {
                //解决"该进程无法访问图片,因为该图片正由另一进程使用"
                FileStream fs = new FileStream(oldpath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);

                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                switch (markMode)
                {
                    //是图片的话              
                    case 0:
                        this.addWatermarkImage(g, this.imageWaterMark, this.waterMarkPos,image.Width, image.Height);
                        break;
                    //如果是文字                   
                    case 1:
                        this.addWatermarkText(g, this.watermarkText, this.waterMarkPos,image.Width, image.Height);
                        break;
                }
                //重命名图片
                string waterMarkName = RenameFile(oldFileName, GetFileExtends(oldFileName));
                //确保图片唯一性，以免错误覆盖
                string waterMarkFile = BeSureOneFile(newpath, OFullName);
                b.Save(waterMarkFile);
                b.Dispose();
                image.Dispose();
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
                //if(File.Exists(oldpath))
                //{
                //    File.Delete(oldpath);
                //}
            }
            finally
            {
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
            }
        }
        ///加入图片水印#region 加入图片水印
       
        /// <summary>
        /// 加入图片水印
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        protected void addWatermarkImage(Graphics picture, string WaterMarkPicPath, int _watermarkPosition, int _width, int _height)
        {
            System.Drawing.Image watermark = new Bitmap(WaterMarkPicPath);
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            //设置透明度，数值有误则取默认值
            float tempAlpha;
            if (this.imageWaterMarkAlpha >= 0 && this.imageWaterMarkAlpha <= 1)
            {
                tempAlpha = this.imageWaterMarkAlpha;
            }
            else
            {
                tempAlpha = imageAlpha;
            }
            float[][] colorMatrixElements = {
                                            new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                            new float[] {0.0f,  1.0f,  0.0f,  0.0f,0.0f},
                                            new float[] {0.0f,  0.0f,  1.0f,  0.0f,0.0f},
                                            new float[] {0.0f,  0.0f,  0.0f,  tempAlpha, 0.0f},
                                            new float[] {0.0f,  0.0f,  0.0f,  0.0f,1.0f}
                                        };
            //矩阵说明：
            //          从左至右对角线4个数字依次对应红色、绿色、蓝色、alpha
            //      可按照需要自己设置，此处只对alpha进行全局设置
            //      自己设置时，应0-1之间float

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default,ColorAdjustType.Bitmap);
            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;
            //计算水印图片的比率
            //取背景的1/4宽度来比较
            if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((_width > watermark.Width * 4) && (_height < watermark.Height * 4))
            {
                bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
            }
            else
                if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
                {
                    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
                }
                else
                {
                    if ((_width * watermark.Height) > (_height * watermark.Width))
                    {
                        bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
                    }
                    else
                    {
                        bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
                    }
                }
            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);
            switch (_watermarkPosition)
            {
                case 0:
                    xpos = 10;
                    ypos = 10;
                    break;
                case 1:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case 2:
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case 3:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }
            picture.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth,
                WatermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel,
                imageAttributes);
            watermark.Dispose();
            imageAttributes.Dispose();
        }

        ///加入文字水印#region 加入文字水印
       
        /// <summary>
        ///  加入文字水印
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        protected void addWatermarkText(Graphics picture, string _watermarkText, int _watermarkPosition, int _width, int _height)
        {
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);
                if ((ushort)crSize.Width < (ushort)_width)
                    break;
            }
            float xpos = 0;
            float ypos = 0;
            switch (_watermarkPosition)
            {
                case 0:
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case 1:
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = (float)_height * (float).01;
                    break;
                case 2:
                    xpos = ((float)_width * (float).01) + (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
                case 3:
                    xpos = ((float)_width * (float).99) - (crSize.Width / 2);
                    ypos = ((float)_height * (float).99) - crSize.Height;
                    break;
            }
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(_watermarkText, crFont, semiTransBrush2, xpos + 1, ypos + 1,StrFormat);
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(_watermarkText, crFont, semiTransBrush, xpos, ypos,StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();
        }
    }
}