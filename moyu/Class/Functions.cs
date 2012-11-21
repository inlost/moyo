using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moyu
{
    public class Functions
    {
        /// <summary>
        /// 获取时间的友好显示
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>友好显示</returns>
        public string kindTime(DateTime time)
        {
            DateTime nowTime=DateTime.Now;
            Int64 ct = timeToSeconds(nowTime) - timeToSeconds(time);
            if (ct == 0)
            { return "刚刚"; }
            else if (ct < 60)
            { return ct + "秒前"; }
            else if (ct < 3600)
            { return (nowTime.Minute - time.Minute) + "分钟前"; }
            else if (ct < 86400)
            { return (nowTime.Hour - time.Hour) + "小时前"; }
            else if (ct < 2678400)
            { return (nowTime.Day - time.Day) + "天前"; }
            else if (ct < 31536000)
            { return (nowTime.Month - time.Month) + "个月前"; }
            else
            { return (nowTime.Year - time.Year) + "年前"; }
        }
        private Int64 timeToSeconds(DateTime time)
        {
            return time.Year * 31536000 + time.Month * 2678400 + time.Day * 86400 + time.Hour * 3600 + time.Minute * 60 + time.Second;
        }
    }
}