<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exchange.aspx.cs" Inherits="moyu.Markets.Ecard.live.exchange" %>
<h2 class="channelTitle">积分兑换 <span>Exchange</span></h2>
<div id="mC-exchange" class="clearfix">
    <div id="mC-e-goodList" class="left clearfix">
        <%getGoods(); %>
    </div>
    <div id="mC-e-integralLog" class="left">
        <h3>我的积分：<%myJfGet(); %></h3>
        <div id="mC-e-i-exchanges">
            <h3>兑换到的礼品</h3>
            <ul>
                <%exchangeLogGet(); %>
            </ul>
        </div>
        <div id="mC-e-i-integralGetLog">
            <h3>积分获取记录</h3>
            <ul>
                <%jfLogGet(); %>
            </ul>
        </div>
    </div>
</div>
<script type="text/javascript">
    var moyo = new Moyo();
    moyo.ecard.addExchangeListen();
</script>
