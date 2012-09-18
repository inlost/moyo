<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_new.aspx.cs" Inherits="moyu.Markets.forum.group_new" %>
<h2 class="channelTitle"><%getName(); %>-发表新话题 <span>NewTopic</span></h2>
<div id="group_newTopic_content" class="clearfix">
    <div id="group_n_c_postContent" class="left">
        <div id="group_n_c_p_title" class="clearfix">
            <input type="hidden" name="gid" value="<%getGid(); %>" />
            <img src="<%Response .Write (Session["avatar"].ToString().Replace("320_320","32_32")); %>" class="left" />
            <input class="left" type="text" name="title" maxlength="30" />
            <span class="left">标题不少于5个字</span>
        </div>
        <div id="group_n_c_p_body">
            <textarea name="body" rows="30" cols="80"></textarea>
        </div>
        <div id="group_n_c_p_tag">
            <a href="#">添加标签</a>
            <div class="clearfix hide">
                <input type="text" maxlength="2" name="tag"  class="left"/>
                <span class="left">标签为两个汉字（如电影、工作等，清晰、合理的标签可以让话题更有价值。）</span>
            </div>
        </div>
        <div id="group_n_c_p_submit">
            <button>创建</button>
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
    moyo.Group.newTopicTagListen();
    moyo.Group.newTopicListen();
    moyo.addPageJump();
</script>