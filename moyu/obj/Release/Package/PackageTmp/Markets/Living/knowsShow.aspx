<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="knowsShow.aspx.cs" Inherits="moyu.Markets.knowsShow" %>
<h2 class="channelTitle clearfix"><b class="left"><%getTitle(); %></b></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <div id="t_p_l_topic">
            <div id="t_p_l_t_content">
                <ul id="t_p_l_t_c_info" class="clearfix">

                </ul>
                <%getBody(); %>
            </div>
            <div id="t_p_l_t_comments">
                <div id="t_p_l_t_c_tip">回答</div>
                <div id="t_p_l_t_c_list">
                    <%anwerGet(); %>
                </div>
                <div id="t_p_l_t_c_status"></div>
                <div id="t_p_l_t_c_new">
                    <div id="t_p_l_c_n_title">
                        <h3>我来回答</h3>
                    </div>
                    <div id="t_p_l_c_n_content" class="clearfix">
                        <img class="userAvatar left" src="/Images/avatar.png"/>
                        <textarea id="t_p_l_t_comments_left" name="message" cols="60" rows="6" maxlength="300" placeholder="请输入你的回答……"></textarea>
                    </div>
                    <p class="act"><button type="submit" id="t_p_l_t_c_n_answer" data-tid="<%getTid(); %>">提交回答</button></p>                
                </div>
            </div>
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h2>等待回答的问题</h2>
        <ul id="t_f_bar_hotTopic">
            <%getWaitAnswer(); %>
        </ul>
        <h2>刚刚被解决的问题</h2>
        <ul id="t_f_bar_newTopic">
            <%getLastAnswer(); %>
        </ul>
    </div>
</div>
<script>
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.Living.newAnswer();
    moyo.Living.loadAnswer();
</script>
