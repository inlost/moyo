<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits="moyu.Mobile.header" %>
<% if (Session["isLogin"] == null || Session["isLogin"].ToString()=="false")
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
