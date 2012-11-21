<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="moyu.Markets.Informations.TopicList" %>
<h2 class="channelTitle"><%getName(); %> <span>Forum</span></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <table data-cid="<%getCid(); %>">
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
                <%getTopicList(); %>
            </tbody>
        </table>
        <div id="getMoreTopic">
            <%getMoreLink(); %>
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h2>返回[<a class="jump" data-dst="Markets/forum/index.aspx" href="/定西吧_沁辰左邻/Markets---forum---index@aspx" >定西吧</a>]</h2>
        <h3 id="t_f_b_new" class="clearfix"><span class="left">想说点儿什么？</span> <a class="right <% isNeedLogin();%>" id="t_f_b_newTopic" data-dst="Markets/forum/NewTopic.aspx?<%getPar(); %>" href="javascript:void(0);">发表新帖子</a></h3>
    </div>
</div>
<script>
    var moyo = new Moyo();
    moyo.addPageJump();
    moyo.Information.getMoreTopic();
    moyo.addLoginListen();
</script>