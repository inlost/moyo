﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewTopic.aspx.cs" Inherits="moyu.Markets.Informations.NewTopic" %>
<h2 class="channelTitle"><%getName(); %>-发表新贴 <span>NewTopic</span></h2>
<div id="topic_content" class="clearfix">
    <form>
        <input type="hidden" name="cid" value="<%getCid(); %>" />
        <ul>
            <li id="t_t_l_title" >标题：<input type="text" name="title"/></li>
            <li><textarea id="topicBody" name="topicBody"></textarea></li>
            <li><button type="submit">好了，发表</button></li>
        </ul>
    </form>
</div>
<script>
    var moyo = new Moyo();
    moyo.loadCss("../Script/ueditor/themes/default/ueditor.css");
    moyo.Information.newTopic();
</script>
<script type="text/javascript" src="../Script/ueditor/editor_config.js"></script>
<script type="text/javascript" src="../Script/ueditor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("topicBody");
</script>
