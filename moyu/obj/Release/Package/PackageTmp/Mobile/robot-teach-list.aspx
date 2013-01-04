<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-teach-list.aspx.cs" Inherits="moyu.Mobile.robot_teach_list" %>
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
                        <small>查看调教（机器人）帮助</small>
                    </button>
                </a>
            </div>
        </section>
        <div class="page-control" data-role="page-control">
            <ul>
                <li class="<%getTabClass("hasAnswer"); %>"><a href="robot-teach-list.aspx?type=hasAnswer">最新调教</a></li>
                <li class="<%getTabClass("noAnswer"); %>"><a href="robot-teach-list.aspx?type=noAnswer">等我调教</a></li>
                <li class="<%getTabClass("answerByMe"); %>"><a href="robot-teach-list.aspx?type=answerByMe">我的调教</a></li>
            </ul>
            <section class="frames page-content">
                <ul id="postItemHolder" style="list-style:none;">
                    <% getContent(); %>
                </ul>
                <div id="loadMore">
                    <a style="display:block;" data-type="<%getType(); %>" href="javascript:void(0);"><button class="command-button default" style="width:90%; text-align:center;">点击查看更多</button></a>
                </div>
            </section>
        </div>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			<span>分享到朋友圈</span>
		</section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
    <script>

        $(".goTop").on("click", function () {
            $(document).scrollTop(0);
        });

        var data = {},
            loadBtn = $("#loadMore a");
        var isLoading = false;
        loadBtn.on("click", function () {
            if (isLoading) { return false; }
            isLoading = true;
            var textHolder = $("#loadMore button");
            textHolder.html("正在努力加载……");
            data.type = loadBtn.attr("data-type");
            data.action = "loadTeach";
            data.last = $(".teachItems").last().attr("data-id");
            $.ajax({
                url: '../Services/Mobile_Main.ashx',
                type: 'POST',
                data: data,
                success: function (data) {
                    if (data == "") {
                        textHolder.html("没有更多的内容了");
                    } else {
                        textHolder.html("点击查看更多");
                        $("#postItemHolder").append(data);
                        $(".goTop").on("click", function () {
                            $(document).scrollTop(0);
                        });
                        isLoading = false;
                    }
                }
            });
        });
    </script>
</body>
</html>