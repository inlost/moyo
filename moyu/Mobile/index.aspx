<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Mobile.index" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="grid">
            <header><h3><%getNiceName(); %>@左邻 <%getLogout(); %></h3></header>
            <section class="row">
                <ul class="spanN">
                    <%getUserPoint(); %>
                </ul>
            </section>
        </section>
        <section class="grid bg-color-blueLight">
            <h3>功能区</h3>
            <nav class="row">
                <ul>
                    <li class="left"><a class="button bg-color-blue" href="signIn.aspx" >签到</a></li>
                    <li class="left"><a class="button bg-color-green" href="lucky.aspx">抽奖</a></li>
                    <li class="left"><a class="button bg-color-pink" href="newUser.aspx">贡献</a></li>
                </ul>
            </nav>
            <h3>社区</h3>
            <nav class="row">
                <ul>
                    <li class="left"><a class="button bg-color-greenLight" href="robot-group-kewWordsShow.aspx?type=group&tag=-1">贴吧</a></li>
                    <li class="left"><a class="button bg-color-red" href="robot-teach-list.aspx?type=hasAnswer">调教</a></li>
                </ul>
            </nav>
            <h3>商城</h3>
            <nav class="row">
                <ul>
                    <li class="left"><a class="button bg-color-yellow" href="coupons.aspx" >优惠券</a></li>
                    <li class="left"><a class="button bg-color-purple" href="mall-newShop.aspx">店铺入驻</a></li>
                </ul>
            </nav>
        </section>
        <section class="grid">
            <header><h3>幸运榜单</h3></header>
            <ul class="lucky-list">
                <%getGift(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
