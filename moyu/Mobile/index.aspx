<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Mobile.index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link rel="stylesheet" href="../Style/mobile.css" />
    <link rel="stylesheet" href="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.css" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script src="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.js"></script>
</head>
<body class="page" data-theme="c" data-role="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <header><h1><%getNiceName(); %>@左邻 <%getLogout(); %></h1></header>
            <section>
                <ul class="clear">
                    <%getUserPoint(); %>
                </ul>
            </section>
        </section>
        <section class="ui-body ui-body-c">
            <h1>功能区</h1>
            <nav data-role="navbar">
                <ul class="clear">
                    <li><a href="signIn.aspx" data-ajax="false">我要签到</a></li>
                    <li><a href="lucky.aspx" data-ajax="false">我要抽奖</a></li>
                    <li><a href="coupons.aspx" data-ajax="false">优惠券</a></li>
<%--                    <li><a href="topic-list.aspx?cid=9"><span>3  </span>定西贴吧</a></li>
                    <li><a href="index.aspx"><span>4  </span>定西知道</a></li>--%>
                </ul>
            </nav>
        </section>
        <section class="ui-body ui-body-c">
            <header><h1>幸运榜单</h1></header>
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <%getGift(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
