<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="coupons-show.aspx.cs" Inherits="moyu.Mobile.coupons_show" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title><%getTitle(); %>_电子优惠券_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="grid">
            <h3><%getTitle(); %></h3>
            <div class="row">
                <a href="coupons.aspx">
                    <button class="command-button default" style="width:100%;">
                        返回我的优惠券
                        <small>点这里返回我的优惠券</small>
                    </button>
                </a>
            </div>
        </section>
        <section class="grid">
            <section class="row bg-color-blueLight padding10">
                    <%getBody(); %>
            </section>
            <%getNo(); %>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
