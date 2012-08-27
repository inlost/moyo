<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupTopicList.aspx.cs" Inherits="moyu.Markets.Informations.GroupTopicList" %>
<h2 class="channelTitle"><%getName(); %> <span>Group</span></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <table data-gid="<%getGid(); %>">
            <thead>
                <tr>
                    <th id="t_t_l_table_title">标题</th>
                    <th id="t_t_l_table_postBy">作者</th>
                    <th id="t_t_l_table_view">浏览</th>
                    <th id="t_t_l_table_reply">回应</th>
                    <th id="t_t_l_table_lastUpdate">最后回应</th>
                </tr>
            </thead>
            <tbody>
                <% //getTopicList(); %>
            </tbody>
        </table>
        <div id="getMoreTopic">
            <% //getMoreLink(); %>
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
</script>
