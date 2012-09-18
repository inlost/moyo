<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Living.index" %>
<h2 class="channelTitle">定西生活 <span>Living@DingXi</span></h2>
<div id="livingHolder">
    <%listCats(); %>
</div>
<script type="text/javascript">
    var moyo = new Moyo();
    moyo.Living.newShopHover();
    moyo.popSideBar();
</script>
