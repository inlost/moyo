<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupons.aspx.cs" Inherits="moyu.Mobile.coupons" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>电子优惠券_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section>
            <h3><%getNiceName(); %>的优惠券</h3>
            <%getUserCoupons(); %>
        </section>
        <section>
            <h3>正在发放的优惠券</h3>
            <%getCoupons(); %>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
