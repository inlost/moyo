<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-group-topUser.aspx.cs" Inherits="moyu.Mobile.robot_group_topUser" %>
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
        <h3 id="activity-name"><%getTitle(); %></h3>
    </header>
    <section id="content">
        <section class="grid">
            <div class="row">
                <a href="help.aspx">
                    <button class="command-button default" style="width:100%;">
                        怎么玩儿
                        <small>查看贴吧帮助</small>
                    </button>
                </a>
            </div>
        </section>
        <div class="page-control" data-role="page-control">
            <ul>
                <li class="normal"><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1">最新</a></li>
                <li class="normal"><a href="robot-group-kewWordsShow.aspx?type=atMe">@我<%getNotRead(); %></a></li>
                <li class="normal"><a href="robot-group-kewWordsShow.aspx?type=user">我的</a></li>
                <li class="normal"><a href="robot-group-kewWordsShow.aspx?type=elite">精华</a></li>
                <li class="active"><a href="robot-group-topUser.aspx">排行</a></li>
            </ul>
            <section class="frames page-content">
                <ul id="postItemHolder" style="list-style:none;">
                    <li class="postItem">
                        <h2 class="group-post-info clear" style="text-indent:0;">
                            <span class="left group-post-info-tag group_tag_7">15天内贴吧积分前5名将自动成为贴吧管理员</span>
                        </h2>
                    </li>
                    <% getUserPoint();  %>
                    <%getContent(); %>
                </ul>
            </section>
        </div>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			<span>分享到朋友圈</span>
		</section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
