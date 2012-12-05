<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Mobile.index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
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
                <ul>
                    <li><a href="signIn.aspx" data-ajax="false">我要签到</a></li>
                    <li><a href="lucky.aspx" data-ajax="false">我要抽奖</a></li>
                    <li><a href="coupons.aspx" data-ajax="false">优惠券</a></li>
<%--                    <li><a href="topic-list.aspx?cid=9"><span>3  </span>定西贴吧</a></li>
                    <li><a href="index.aspx"><span>4  </span>定西知道</a></li>--%>
                </ul>
            </nav>
            <h1>贴吧</h1>
            <nav data-role="navbar">
                <ul>
                    <li><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1" data-ajax="false" style="color:red;">看看大家在说啥</a></li>
                    <li><a href="help.aspx">贴吧使用帮助</a></li>
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
