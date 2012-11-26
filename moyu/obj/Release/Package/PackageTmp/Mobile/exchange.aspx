<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exchange.aspx.cs" Inherits="moyu.Mobile.exchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>贡献兑换_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul  class="lucky-list" data-role="listview" data-inset="true">
                    <li class="info">1贡献可兑换30积分</li>
                    <li><a href="lucky.aspx">返回抽奖</a></li>
                    <li><a href="index.aspx">返回首页</a></li>
                </ul>
            </nav>
            <form action="../Services/Mobile_Main.ashx?action=exchange" method="post">
                <ul>
                    <li class="functionList-half left"><input type="number" min="1" style="width:10em;" name="jf"/></li>
                    <li class="functionList-half left"><input type="submit" value="兑换" /></li>
                </ul>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
