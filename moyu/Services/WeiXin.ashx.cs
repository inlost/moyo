using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
namespace moyu.Services
{
    /// <summary>
    /// WeiXin 的摘要说明
    /// </summary>
    public class WeiXin : IHttpHandler
    {
        private HttpContext theContext;
        private Api.WeiXin myWeiXin = new Api.WeiXin();
        public void ProcessRequest(HttpContext context)
        {
            theContext = context;
            theContext.Response.ContentType = "text/XML";
            if (checkSig())
            {
                getResponse();
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private bool checkSig()
        {
            string signature = theContext.Request.Params["signature"];
            string timestamp = theContext.Request.Params["timestamp"];
            string nonce =theContext.Request.Params["nonce"];
            if (myWeiXin.checkSignature(signature, timestamp, nonce))
            {
                //theContext.Response.Write(theContext.Request.Params["echostr"]);
                return true;
            }
            return false;
        }
        private void getResponse()
        {
            StreamReader sreader = new StreamReader(theContext.Request.InputStream);
            Hashtable data= myWeiXin.getRequestData(sreader);
            theContext.Response.Write(myWeiXin.getResponse(data));
        }
    }
}