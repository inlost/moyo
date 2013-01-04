<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-group-kewWordsShow.aspx.cs" Inherits="moyu.Mobile.robot_group_kewWordsShow" %>
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
		<span id="post-date"><%getTime(); %></span>
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
                <li class="<%getTabClass("all"); %>"><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1">最新</a></li>
                <li class="<%getTabClass("atMe"); %>"><a href="robot-group-kewWordsShow.aspx?type=atMe">@我</a></li>
                <li class="<%getTabClass("user"); %>"><a href="robot-group-kewWordsShow.aspx?type=user">我的</a></li>
                <li class="<%getTabClass("elite"); %>"><a href="robot-group-kewWordsShow.aspx?type=elite">精华</a></li>
                <li class="<%getTabClass("admin"); %>"><a href="robot-group-topUser.aspx">排行</a></li>
            </ul>
            <section class="frames page-content">
                <ul id="postItemHolder" style="list-style:none;">
                    <%getContent(); %>
                </ul>
                <%setMessageRead(); %>
                <div id="loadMore">
                    <a style="display:block;" data-type="<%getType(); %>" data-tag="<%getTag(); %>" data-role="button" href="javascript:void(0);"><button class="command-button default" style="width:90%; text-align:center;">点击查看更多</button></a>
                </div>
            </section>
        </div>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			<span>分享到朋友圈</span>
		</section>
        <section id="functionsHolder" class="hide">
            <div id="commentsBox" style="overflow:visible;">
                <input type="hidden" id="atUid" value="0" />
                <a id="commentsBoxClose" href="#" style="text-indent:0;float:right;position:absolute;top:-12px;right:-6px;" ><i class="icon-cancel"></i></a>
                <ul id="commentsList" style="list-style:none;"></ul>
                    <div class="input-control textarea" style="margin-top:10px;margin-right:50px;">
                        <textarea name="textarea" id="commentText"></textarea>
                    </div>
                <a id="commentNew" href="javascript:void(0);"><button class="bg-color-blue command-button" style="width:200px;text-align:center;color:white;font-weight:bold;">发表评论</button></a>
            </div>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
    <script>
        $.fn.setCursorPosition = function (position) {
            if (this.lengh == 0) return this;
            return $(this).setSelection(position, position);
        }

        $.fn.setSelection = function (selectionStart, selectionEnd) {
            if (this.lengh == 0) return this;
            input = this[0];

            if (input.createTextRange) {
                var range = input.createTextRange();
                range.collapse(true);
                range.moveEnd('character', selectionEnd);
                range.moveStart('character', selectionStart);
                range.select();
            } else if (input.setSelectionRange) {
                input.focus();
                input.setSelectionRange(selectionStart, selectionEnd);
            }

            return this;
        }

        $.fn.focusEnd = function () {
            this.setCursorPosition(this.val().length);
        }


        $(".goTop").on("click", function () {
            $(document).scrollTop(0);
        });
        var commentBox = $("#commentsBox"),
        commentsList = $("#commentsList");
        var isCommentPosting = false,
            commentNewTexHolder = $("#commentNew button");
        function commentNew() {
            if (isCommentPosting) { return false; }
            isCommentPosting = true;
            commentNewTexHolder.html("正在提交评论……");
            var data = {},
                holder = $(this).parent().parent();
            data.action = "commentNew";
            data.tid = holder.attr("data-id");
            data.atUid = $("#atUid").val();
            data.comment = $("#commentText").val();
            if (data.comment.length == 0)
            {
                isCommentPosting = false;
                alert("评论不能为空");
                return false;
            }
            $.ajax({
                url: '../Services/Information_group.ashx',
                type: 'POST',
                data: data,
                success: function (msg) {
                    var viewComment = holder.children(".group-post-functions").children(".viewComments");
                    viewComment.click();
                    commentNewTexHolder.html("发表评论");
                    isCommentPosting = false;
                    $("#commentText").val("");
                }
            });
        }
        $("#commentNew").on("click", commentNew);

        var isCommentsLoading = false;
        function viewComments() {
            if (isCommentsLoading) { return; }
            isCommentsLoading = true;
            $("#atUid").val("0");
            $("#commentText").val("");
            var viewComments = $(this);
            var oldHtml = viewComments.html();
            viewComments.html("载入中");
            var holder = $(this).parent().parent();
            commentBox.appendTo(holder);
            commentBox.show();
            var data = {};
            data.action = "commentGet";
            data.tid = holder.attr("data-id");
            commentsList.html("<li>正在加载评论……</li>");
            $.ajax({
                url: '../Services/Information_group.ashx',
                type: 'POST',
                data: data,
                success: function (msg) {
                    commentsList.html(msg == "" ? "暂时还没有评论" : msg);
                    viewComments.html(oldHtml);
                    $("#commentsBoxClose").on("click", function () {
                        commentBox.hide();
                        return false;
                    });
                    $(".commentM_i_at").on("click", function () {
                        var atBtn = $(this);
                        var commentArea = $("#commentText");
                        commentArea.val(commentArea.val() + "@" + $(this).attr("data-name") + " ");
                        commentArea.focusEnd();
                        $("#atUid") .val(atBtn.attr("data-uid"));
                    });
                    isCommentsLoading = false;
                }
            });
        }
        $(".viewComments").on("click", viewComments);
        var loadBtn = $("#loadMore a");
        var isLoading = false;
        loadBtn.on("click", function () {
            var data = {};
            if (isLoading) { return false; }
            isLoading = true;
            var textHolder = $("#loadMore button");
            textHolder.html("正在努力加载……");
            data.type = loadBtn.attr("data-type");
            data.action = "getMore";
            data.tag = loadBtn.attr("data-tag");
            data.last = $(".postItem").last().attr("data-id");
            $.ajax({
                url: '../Services/Information_group.ashx',
                type: 'POST',
                data: data,
                success: function (data) {
                    if (data == "") {
                        textHolder.html("没有更多的内容了");
                    } else {
                        textHolder.html("点击查看更多");
                        $("#postItemHolder").append(data);
                        $(".viewComments").on("click", viewComments);
                        $(".goTop").on("click", function () {
                            $(document).scrollTop(0);
                        });
                        isLoading = false;
                    }
                }
            });
            return false;
        });
    </script>
</body>
</html>