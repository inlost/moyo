<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupons.aspx.cs" Inherits="moyu.Mobile.coupons" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>电子优惠券_沁辰左邻</title>
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
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <li><a href="index.aspx" data-ajax="false">返回首页</a></li>
            </ul>
        </section>
        <section class="ui-body ui-body-c">
            <h1><%getNiceName(); %>的优惠券</h1>
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <%getUserCoupons(); %>
            </ul>
        </section>
        <section class="ui-body ui-body-c">
            <h1>正在发放的优惠券</h1>
            <ul class="lucky-list" data-role="listview" data-inset="true">
                <%getCoupons(); %>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
