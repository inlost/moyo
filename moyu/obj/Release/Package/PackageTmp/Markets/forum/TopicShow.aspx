<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicShow.aspx.cs" Inherits="moyu.Markets.Informations.TopicShow" %>
<h2 class="channelTitle clearfix"><b class="left"><%getName(); %></b></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <div id="t_p_l_topic">
            <div id="t_p_l_t_content">
                <ul id="t_p_l_t_c_info" class="clearfix">
                    <%getInfo(); %>
                </ul>
                <%getContent(); %>
            </div>
            <div id="t_p_l_t_comments">
                <div id="t_p_l_t_c_tip">评论</div>
                <div id="t_p_l_t_c_list">
                    <%commentsGet(); %>
                </div>
                <div id="t_p_l_t_c_status"></div>
                <div id="t_p_l_t_c_new">
                    <div id="t_p_l_c_n_title">
                        <h3>我来说两句<%isNeedLogin(); %></h3>
                    </div>
                    <div id="t_p_l_c_n_content" class="clearfix">
                        <img class="userAvatar left" src="/Images/avatar.png"/>
                        <textarea id="t_p_l_t_comments left" name="message" cols="60" rows="6" maxlength="300" placeholder="请输入你的评论……"></textarea>
                    </div>
                    <p class="act"><span class="type_counts"><em>0</em> / 300</span><button type="submit" id="t_p_l_t_c_n_submit" data-tid="<%getTid(); %>">评论</button></p>                
                </div>
            </div>
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h2>返回[<a class="jump" data-dst="Markets/forum/index.aspx" href="/定西吧_沁辰左邻/Markets---forum---index@aspx" >定西吧</a>]—[<a class="jump" data-dst="Markets/forum/TopicList.aspx?cid=<%getForumId(); %>&name=<%getForumName(); %>" href="/<%getForumName(); %>_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=<%getForumId(); %>&name=<%getForumName(); %>"><%getForumName(); %></a>]</h2>
        <h2>最热</h2>
        <ul id="t_f_bar_hotTopic">
            <%getForumHotTopic(); %>
        </ul>
        <h2>最新发表</h2>
        <ul id="t_f_bar_newTopic">
            <%getForumTopicGet(); %>
        </ul>
    </div>
</div>
<script>
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.Information.commentsNewListen();
</script>
