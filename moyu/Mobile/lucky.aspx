<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lucky.aspx.cs" Inherits="moyu.Mobile.lucky" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>抽奖_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link rel="stylesheet" href="../Style/mobile.css" />
    <link rel="stylesheet" href="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.css" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script src="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.js"></script>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li class="info"><%getMessage(); %></li>
                    <li><a href="../Services/Mobile_Main.ashx?action=luckyMe">抽奖</a></li>
                    <li><a href="exchange.aspx">贡献兑换积分</a></li>
                    <li><a href="index.aspx">返回首页</a></li>
                </ul>
            </nav>
        </section>
        <section class="ui-body ui-body-c">
            <h1>幸运榜单</h1>
            <ul class="clear lucky-list" data-role="listview" >
                <%getGift(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
