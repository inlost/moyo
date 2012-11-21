<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newQuestion.aspx.cs" Inherits="moyu.Markets.Living.newQuestion" %>
<h2 class="channelTitle">定西知道-新问题 <span>newQ@Knows</span></h2>
<div id="group_newTopic_content" class="clearfix">
    <div id="group_n_c_postContent" class="left">
        <div id="group_n_c_p_title" class="clearfix">
            <img src="<%Response .Write (Session["avatar"].ToString().Replace("320_320","32_32")); %>" class="left" />
            <input class="left" type="text" name="title" maxlength="30" />
            <span class="left">标题不少于5个字</span>
        </div>
        <div id="group_n_c_p_body">
            <textarea name="body" rows="30" cols="80"></textarea>
        </div>
        <div id="group_n_c_p_submit">
            <button>提问</button>
        </div>
    </div>
    <div id="group_n_c_sideBar" class="left">
        <div id="group_n_c_s_back" class="clearfix">
            <a href="" class="jump" data-dst="Markets/Living/index.aspx">返回列表</a>
        </div>
    </div>
</div>
<script>
    moyo.Living.newQuestion();
    moyo.addPageJump();
</script>