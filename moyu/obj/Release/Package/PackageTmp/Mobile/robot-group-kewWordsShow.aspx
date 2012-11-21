<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-group-kewWordsShow.aspx.cs" Inherits="moyu.Mobile.robot_group_kewWordsShow" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%getTitle(); %>_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link rel="stylesheet" href="../Style/mobile.css" />
    <link rel="stylesheet" href="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.css" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script src="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.js"></script>
</head>
<body class="page">
    <header class="header">
        <h1 id="activity-name"><%getTitle(); %></h1>
		<span id="post-date"><%getTime(); %></span>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="page-content">
            <ul>
                <%getContent(); %>
            </ul>
        </section>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			<span>分享到朋友圈</span>
		</section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>