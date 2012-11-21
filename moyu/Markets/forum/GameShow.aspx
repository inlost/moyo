<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameShow.aspx.cs" Inherits="moyu.Markets.forum.GameShow" %>
<h2 class="channelTitle"><%getName(); %> </h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <div id="t_p_l_topic">
            <embed type="application/x-shockwave-flash" class="gam-qc-video" pluginspage="http://www.macromedia.com/go/getflashplayer" src="<%getUrl(); %>" width="620" height="480" play="true" loop="false" menu="false" allowscriptaccess="never" allowfullscreen="true">
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h2>游戏介绍</h2>
        <div id="gameShow_introduce">
            <p><%getIntroduce(); %></p>
        </div>
    </div>
</div>
