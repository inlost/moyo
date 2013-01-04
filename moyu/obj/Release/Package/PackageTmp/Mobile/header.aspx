<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits="moyu.Mobile.header" %>
<%if(Request.Params["place"]==null){ %>
<div class="nav-bar">
    <div class="nav-bar-inner padding10">
        <span class="pull-menu"></span>

        <a href="index.aspx"><span class="element brand">
            左邻
        </span></a>

        <div class="divider"></div>

        <ul class="menu" style="overflow: visible;">
            <li><a href="index.aspx"><i class="icon-home"></i> 首页</a></li>
            <li><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1"><i class="icon-github"></i> 贴吧</a></li>
            <li><a href="help.aspx"><i class="icon-help-2"></i> 帮助</a></li>
        </ul>

    </div>
</div>
<%}
    if (Session["isLogin"] == null || Session["isLogin"].ToString()=="false")
   { %>
<a id="biz-link" href="weixin://profile/gh_cd769e8f6b1a" class="btn">
	<div class="arrow">
		<div class="icons arrow-r"></div>
	</div>
	<div class="logo">
		<div class="circle"></div>
		<img id="img" src="../images/brand_profileinweb_logo_HL@2x.png" onerror="this.src='http://res.wx.qq.com/mpres/htmledition/mobile/images/default_avator100b96.png'">
	</div>
	<div id="nickname">		点击关注左邻				</div>
	<div id="weixinid">微信号:ZuoLinSpeaker</div>				
</a>
<%}else{ %>
    <%getNotRead(); %>
<%} %>
