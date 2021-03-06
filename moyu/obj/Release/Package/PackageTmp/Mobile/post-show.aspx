﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post-show.aspx.cs" Inherits="moyu.Mobile.post_show" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title><%getTitle(); %>_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header class="header">
        <%Server.Execute("header.aspx"); %>
        <h1 id="activity-name"><%getTitle(); %></h1>
		<span id="post-date"><%getTime(); %></span>
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

