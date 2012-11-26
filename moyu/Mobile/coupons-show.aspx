<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="coupons-show.aspx.cs" Inherits="moyu.Mobile.coupons_show" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%getTitle(); %>_电子优惠券_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page" data-theme="c" data-role="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <li><a href="coupons.aspx" data-ajax="false">返回我的优惠券</a></li>
                <li><a href="index.aspx" data-ajax="false">返回首页</a></li>
            </ul>
        </section>
        <section class="ui-body ui-body-c">
            <h1><%getTitle(); %></h1>
            <section class="ui-bar ui-bar-e">
                    <%getBody(); %>
            </section>
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <%getNo(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
