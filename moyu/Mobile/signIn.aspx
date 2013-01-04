<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signIn.aspx.cs" Inherits="moyu.Mobile.signIn" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>签到_左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="grid">
            <h3>每日签到</h3>
            <div class="row">
                <a href="../Services/Mobile_Main.ashx?action=signin">
                    <button class="command-button default" style="width:100%;">
                        <%signInTexe(); %>
                        <small><%getUserPoint(); %></small>
                    </button>
                </a>
            </div>
        </section>
        <section>
            <h3><%getUserName(); %>的签到日志</h3>
            <ul class="lucky-list">
                <%getSignLog(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
