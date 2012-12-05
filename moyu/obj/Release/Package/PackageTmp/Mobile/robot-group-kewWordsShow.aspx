<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-group-kewWordsShow.aspx.cs" Inherits="moyu.Mobile.robot_group_kewWordsShow" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%getTitle(); %>_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header class="header">
        <h1 id="activity-name"><%getTitle(); %></h1>
		<span id="post-date"><%getTime(); %></span>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li><a href="help.aspx">怎么玩儿？（查看帮助）</a></li>
                    <li><a href="index.aspx" data-ajax="false">返回首页</a></li>
                </ul>
            </nav>
        </section>
        <nav data-role="navbar">
            <ul>
                <li><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1">最新</a></li>
                <li><a href="robot-group-kewWordsShow.aspx?type=atMe">@我<%getNotRead(); %></a></li>
                <li><a href="robot-group-kewWordsShow.aspx?type=user">我的</a></li>
            </ul>
        </nav>
        <section class="page-content">
            <ul id="postItemHolder">
                <%getContent(); %>
            </ul>
            <%setMessageRead(); %>
            <div id="loadMore">
                <a data-type="<%getType(); %>" data-tag="<%getTag(); %>" data-role="button" href="javascript:void(0);">点击查看更多</a>
            </div>
        </section>
        <section class="sharebtn" onclick="weixinShare('<%getTitle(); %>','真是太有意思啦');">
			<span>分享到朋友圈</span>
		</section>
        <section id="functionsHolder" class="hide">
            <div id="commentsBox" class="ui-body ui-body-c" style="overflow:visible;">
                <input type="hidden" id="atUid" value="0" />
                <a id="commentsBoxClose" href="#" style="text-indent:0;float:right;position:absolute;top:-18px;right:-8px;" data-icon="delete" data-iconpos="notext" class="ui-btn-left ui-btn ui-shadow ui-btn-corner-all ui-btn-icon-notext ui-btn-up-d" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="d" title="Close"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Close</span><span class="ui-icon ui-icon-delete ui-icon-shadow">&nbsp;</span></span></a>
                <ul id="commentsList"></ul>
                <textarea name="textarea" id="commentText"></textarea>
                <a id="commentNew" data-role="button" href="javascript:void(0);">发表评论</a>
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
            commentNewTexHolder = $("#commentNew .ui-btn-textc");
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
        var data = {},
            loadBtn = $("#loadMore a");
        var isLoading = false;
        loadBtn.on("click", function () {
            if (isLoading) { return false; }
            isLoading = true;
            var textHolder = $("#loadMore .ui-btn-text");
            textHolder.html("正在努力加载……");
            data.type = loadBtn.attr("data-type");
            data.action = "getMore";
            data.tag = loadBtn.attr("data-tag");
            data.last = $(".postItem:last").attr("data-id");
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
        });
    </script>
</body>
</html>