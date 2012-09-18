<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Infos.index" %>
<h2 class="channelTitle">信息港   <span>Information</span></h2>
<div id="infoMarket">
    <ul id="infoM-catList" class="clearfix">
        <li class="infoM-catList-title">分类：</li>
        <%listCat(1); %>
        <li><button type="button">免费发布信息</button></li>
    </ul>
    <ul id="infoM-subCatList" class="clearfix">
        <li class="infoM-catList-title">详细：</li>
        <%listCat(2); %>
    </ul>
    <ul id="infoM-infoList" class="clearfix">
        <%listPost(); %>
    </ul>
</div>
<script>
    $("#infoM-catList button").button();
    moyo.addPageJump();
    moyo.Info.newInfoListen();
</script>