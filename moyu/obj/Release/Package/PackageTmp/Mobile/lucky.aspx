<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lucky.aspx.cs" Inherits="moyu.Mobile.lucky" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>抽奖_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="grid">
            <h3>抽奖</h3>
            <div class="row">
                <a href="../Services/Mobile_Main.ashx?action=luckyMe">
                    <button class="command-button default" style="width:100%;">
                        积分抽奖
                        <small><strong><%getMessage(); %></strong></small>
                    </button>
                </a>
                <a href="exchange.aspx">
                    <button class="command-button default" style="width:100%;">
                        贡献兑换积分
                        <small>1个贡献可以兑换30个积分</small>
                    </button>
                </a>
            </div>
        </section>
        <section class="ui-body ui-body-c">
            <h3>我的幸运日历</h3>
            <ul class="clear lucky-list">
                <%getGift(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
