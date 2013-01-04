<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="footer.aspx.cs" Inherits="moyu.Mobile.footer" %>
<p>Copyright © 2009-2012 Ai0932.cOm. All Rights Reserved.</p>
<script>
    function weixinShare(title, body,link) {
        var imgSrc = "";
        if ($("#content img").length != 0)
        {
            imgSrc = $($("#content img")[0]).attr("src");
        }
        var url = link || window.location.href;
        WeixinJSBridge.invoke('shareTimeline', {
            "title": title,
            "link": url,
            "desc": body,
            "img_url": imgSrc
        });
    }
</script>
