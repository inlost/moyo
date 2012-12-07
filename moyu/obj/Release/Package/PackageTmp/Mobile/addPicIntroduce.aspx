<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addPicIntroduce.aspx.cs" Inherits="moyu.Mobile.addPicIntroduce" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page" data-theme="c" data-role="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1">返回贴吧</a></li>
                    <li><a href="index.aspx" data-ajax="false">返回首页</a></li>
                </ul>
            </nav>
        </section>
        <section class="ui-body ui-body-c">
            <div class="page-content">
                <%getBody(); %>
            </div>
            <form action="../Services/Information_group.ashx" method="post">
                <input type="hidden" name="action" value="addPicPostIntroduce" />
                <input type="hidden" name="pid" value="<%getPid(); %>" />
                <input type="hidden" name="tid" value="<%getTid(); %>" />
                <textarea name="introduce"></textarea>
                <button type="submit"><%getBtnText(); %></button>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
