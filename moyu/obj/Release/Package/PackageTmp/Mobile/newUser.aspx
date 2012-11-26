<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newUser.aspx.cs" Inherits="moyu.Mobile.newUser" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>感谢_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <h1>推荐用户回馈</h1>
            <nav>
                <form action="../Services/Mobile_Main.ashx?action=giveThanks" method="post">
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li class="info">谁介绍左邻给你的</li>
                    <li><a href="index.aspx">跳过，回首页</a></li>
                    <li><input type="text" name="to" placeholder="Ta的用户名"/></li>
                    <li><input type="submit" value="谢谢Ta" /></li>
                </ul>
                </form>
            </nav>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
