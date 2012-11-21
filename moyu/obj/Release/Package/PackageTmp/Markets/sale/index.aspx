<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.sale.index" %>
<h2 class="channelTitle">定西商城 <span>Sale</span></h2>
<div id="saleHolder">
    <%listCats(); %>
</div>
<script>
    moyo.addHoverClass($(".saleH-c-good"));
    moyo.popSideBar();
</script>
