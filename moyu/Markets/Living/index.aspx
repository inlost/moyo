<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Living.index" %>
<h2 class="channelTitle">定西生活 <span>Living@DingXi</span></h2>
<div id="livingHolder">
    <div id="livingKnows" class="l-catHolder">
        <h2 class="l-catTitle clearfix">定西知道</h2>
        <div class="l-catContent clearfix">
            <div id="knowsList" class="left">
                <ul class="clearfix"><%getKnows(); %></ul>
            </div>
            <div id="newQ" class="right">
                <a data-dst="Markets/Living/newQuestion.aspx" class="<%getLoginClass(); %>" href="#">我要提问题</a>
            </div>
        </div>
    </div>
    <%listCats(); %>
</div>
<script type="text/javascript">
    moyo.Living.newShopHover();
    moyo.addHoverClass($("#knowsList li"));
    moyo.popSideBar();
    moyo.addPageJump();
    moyo.addLoginListen();
</script>
