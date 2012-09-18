<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new.aspx.cs" Inherits="moyu.Markets.Infos._new" %>
<%if (Request .Params ["action"]==null){ %>
<h2 class="channelTitle">信息港-发布信息-请选择分类 <span>Information</span></h2>
<div id="infoMarket">
    <ul id="infoM-new-catLists">
        <%listCats(); %>
    </ul>
</div>
<%}else{ %>
<h2 class="channelTitle">信息港-发布信息 <span>Information</span></h2>
<div id="infoMarket" class="clearfix">
    <div id="infoM-new-main" class="left">
        <input type="hidden" name="catId" value="<%getCid(); %>" />
        <h3 class="clearfix"><span class="left">填写详细信息：</span><span class="right">带<b class="redStar">*</b>为必填</span></h3>
        <ul id="infoM-new-form">
            <li class="clearfix"><span><b class="redStar">*</b>标题：</span><input class="needTip" type="text" name="title"/></li>
            <li class="clearfix"><span>联系人：</span><input type="text" name="name" class="needTip" data-tip="不填将显示为匿名" /></li>
            <li class="clearfix"><span>价格：</span><input type="text" name="price" class="needTip" data-tip="不填将显示为面议"/></li>
            <li class="clearfix"><span><b class="redStar">*</b>内容：</span><textarea name="body"></textarea></li>
            <li class="clearfix"><span><b class="redStar">*</b>电话：</span><input type="text" name="phone" /></li>
            <li class="clearfix"><span><b class="redStar">*</b>管理密码：</span><input type="password" name="pass" class="needTip" data-tip="凭此密码可以删除这条信息"/></li>
            <li class="clearfix"><span>&nbsp;</span><button>发布信息</button></li>
        </ul>
    </div>
    <div id="infoM-new-side" class="left">
        <h3>信息置顶说明</h3>
    </div>
</div>
<%} %>
<script>
    moyo.addPageJump();
    moyo.inputTip($(".needTip"));
    moyo.Info.newInfoAddListen();
</script>