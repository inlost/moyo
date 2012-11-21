<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post-show.aspx.cs" Inherits="moyu.Mobile.post_show" %>
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
            <%getContent(); %>
        </section>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			    <span>分享到朋友圈</span>
		</section>
<%--        <section class="page-comment" id="comment">
            <ul>
                <li>
                    <section class="page-comment-new">
                        <form action="../Services/Information_Topic.ashx" method="post">
                            <input type="hidden" name="action" value="commentsNew_mobile" />
                            <input type="hidden" name="tid" value="<%getTid(); %>" />
                            <div id="newComment" class="clear">
                                <div id="newCommentBody" class="left"><input type="text" name="body" /></div>
                                <div id="newCommentSumbit" class="left"><button type="submit">评论</button></div>
                            </div>
                        </form>
                    </section>
                </li>
                <%getComment(); %>
            </ul>
        </section>--%>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>

