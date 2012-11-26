<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic-list.aspx.cs" Inherits="moyu.Mobile.forum.topic_list" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>贴吧_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="functionList">
            <a class="newTopic" href="topic-new.aspx?cid=<%getCid(); %>">发新帖</a>
            <header><h1>帖子列表</h1></header>
                <ul class="clear">
                    <%getTopicList(); %>
                </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>

