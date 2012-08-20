<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Clothes.fashion.index" %>
<h2 class="channelTitle">服装城 -时装 <span>Clothes-Fashion</span></h2>
<div id="clothes-content" class="clearfix">
    <div id="c-t-row1" class="clothes-c-row left">
        <%getRow(1); %>
    </div>
    <div id="c-t-row2" class="clothes-c-row left">
        <%getRow(2); %>
    </div>
    <div id="c-t-row3" class="clothes-c-row left">
         <%getRow(3); %>   
    </div>
    <div id="c-t-row4" class="clothes-c-row left">
         <%getRow(4); %>
    </div>
<div id="fallItem-tp" class="hide" >
    <a class="hide popDetail" data-canBuy="true"  data-service="Sale_Clothes.ashx?id=[id]"  data-action="getDetail" data-pid="[id]" data-left="[inventory]">
        <div class="fallImages"><img src="[img]" title="[title]"/><h3 class="fallTitle">[title]</h3>
        </div>
        <ul class="clearfix fall-item-info"><li class="left  fall-item-inventory">还有 [inventory] 件</li><li class="left fall-itenm-hot"> [like] 人喜欢</li><li class="fall-item-price">￥:[price]</li></ul>
    </a>
</div>
<script>
    var moyo = new Moyo();
    moyo.clothes.fallInit();
    moyo.addPopDetail();
</script>
</div>
