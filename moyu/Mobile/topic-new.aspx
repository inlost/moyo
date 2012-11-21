<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic-new.aspx.cs" Inherits="moyu.Mobile.topic_new" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>发表新帖子_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link rel="stylesheet" href="../Style/mobile.css" />
    <link rel="stylesheet" href="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.css" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script src="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.js"></script>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="functionList">
            <a class="newTopic" href="topic-list.aspx?cid=<%getCid(); %>">返回</a>
            <header><h1>发表帖子</h1></header>
            <form action="../Services/Information_Topic.ashx" method="post" enctype="multipart/form-data">
                <input type="hidden" name="action" value="topicNew_mobile" />
                <input type="hidden" name="cid" value="<%getCid(); %>" />
                <ul class="formElmList">
                    <li>标题：<input style="width:70%;" type="text" name="title"/></li>
                    <li><textarea rows="8" cols="15" name="body"></textarea></li>
                    <!--<li>图片：<input type="file" name="pic" /></li>-->
                    <li><button type="submit">发表</button></li>
                </ul>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
