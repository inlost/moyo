using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
namespace moyu.Data
{
    public class Storage
    {
        /// <summary>
        /// 密钥对结构
        /// </summary>
        private struct accessPar{
            public string id;
            public string key;
        }
        /// <summary>
        /// 多密钥对随机
        /// </summary>
        /// <returns>密钥对</returns>
        private accessPar getAccessPar()
        {
            accessPar[] theAccessPar = new accessPar[1];
            theAccessPar[0].id = "d8pu794grkxok9e37nyaxoy6";
            theAccessPar[0].key = "M3wAYYBUFV40F5o/B2smwiHjnis=";
            return theAccessPar[0];
        }
        /// <summary>
        /// 命名空间常量结构
        /// </summary>
        public struct Bucket
        {
            public const string image = "/imges/";
            public const string swf = "/swf/";
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="bucket">命名空间</param>
        /// <param name="fileName">文件在OSS上的名字</param>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="contentType">文件类型</param>
        /// <returns>成功返回URL，失败返回服务器错误信息</returns>
        public string uploadFile(string bucket,string fileName, string filePath,string contentType)
        {
            FileStream theFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            WebClient theWebClient = new WebClient();
            string GMTime = DateTime.Now.ToUniversalTime().ToString("r");
            MD5CryptoServiceProvider theMD5Hash = new MD5CryptoServiceProvider();
            byte[] hashDatas;
            hashDatas = theMD5Hash.ComputeHash(new byte[(int)theFileStream.Length]);
            string contentMD5 = Convert.ToBase64String(hashDatas);
            HMACSHA1 theHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(getAccessPar().key));
            string headerStr = "PUT\n"+
                contentMD5+"\n"+
                contentType+"\n"+
                GMTime+"\n"+
                "x-oss-date:"+GMTime+"\n"+
                bucket+ fileName;
            byte[] rstRes = theHMACSHA1.ComputeHash(Encoding.Default.GetBytes(headerStr));
            string strSig = Convert.ToBase64String(rstRes);
            theWebClient.Headers.Add(HttpRequestHeader.Authorization,"OSS "+getAccessPar().id+":"+strSig);
            theWebClient.Headers.Add(HttpRequestHeader.ContentMd5, contentMD5);
            theWebClient.Headers.Add(HttpRequestHeader.ContentType, contentType);
            theWebClient.Headers.Add("X-OSS-Date", GMTime);
            try
            {
                byte[] ret = theWebClient.UploadFile("http://storage.aliyun.com/" +bucket+ fileName, "PUT", filePath);
                string strMessage = Encoding.ASCII.GetString(ret);
                return "http://storage.aliyun.com" +bucket+ fileName;
            }
            catch (WebException e)
            {
                Stream stream = e.Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
    }
}