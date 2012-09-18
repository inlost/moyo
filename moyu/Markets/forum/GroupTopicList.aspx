<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupTopicList.aspx.cs" Inherits="moyu.Markets.Informations.GroupTopicList" %>
<h2 class="channelTitle"><%getName(); %> <span>Group</span></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <div id="t_t_l_groupContent">
            <h2>这个圈子里的人正在说……</h2>
            <ul>
                <%listTopic(); %>
            </ul>
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h3 id="t_f_b_new" class="clearfix"><%getSideBar_firstFun(); %></h3>
    </div>
</div>
<script>
    var moyo = new Moyo();
    moyo.addPageJump();
    moyo.Information.getMoreTopic();
    moyo.addLoginListen();
    moyo.Group.addJoinGroupListen();
    moyo.addHoverClass($(".t_topicList_topic"));
</script>
