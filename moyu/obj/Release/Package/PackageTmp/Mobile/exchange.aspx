<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exchange.aspx.cs" Inherits="moyu.Mobile.exchange" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>贡献兑换_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <h3>贡献兑换积分</h3>
        <section class="grid">
            <form action="../Services/Mobile_Main.ashx?action=exchange" method="post">
                <ul style="list-style:none;margin-left:0;">
                    <li class="input-control text"><input style="width:100%;" type="number" min="1" name="jf"/></li>
                    <li><button style="width:100%;" type="submit" class="command-button default">兑换<small>1贡献兑换30积分</small> </button></li>
                </ul>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
