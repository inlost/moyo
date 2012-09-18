<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupTopicShow.aspx.cs" Inherits="moyu.Markets.forum.GroupTopicShow" %>
<h2 class="channelTitle"><%getName(); %>- <span>TopicShow</span></h2>
<div id="group_newTopic_content" class="clearfix">
    <div id="group_n_c_postContent" class="left">
        <%getTopicTitle(); %>
        <div id="group_n_c_p_bodyShow">
            <%getBody(); %>
        </div>
        <div id="group_topic_comments">
            <div id="group_comments_list">
                <ul>
                    <%commentGet(); %>
                </ul>
            </div>
            <div id="group_comment_new">
                <div class="clearfix">
                    <span class="left">你的回应</span>
                    <a href="#group_n_c_postContent" class="right">回到顶部</a>
                </div>
                <textarea rows="30" cols="80" data-tid="<%getTid(); %>"></textarea>
                <button>回应</button>
            </div>
        </div>
    </div>
    <div id="group_n_c_sideBar" class="left">
        <div id="group_n_c_s_back" class="clearfix">
            <img class="left" src="<%getGroupIcon(); %>" />
            <a href="#" class="jump" data-dst="Markets/forum/GroupTopicList.aspx?id=<%getGid(); %>">返回<%getName(); %>小组</a>
        </div>
    </div>
</div>
<script>
    moyo.addPageJump();
    moyo.Group.newCommentListen();
</script>
