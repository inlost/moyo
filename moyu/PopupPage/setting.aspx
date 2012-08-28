<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="moyu.PopupPage.setting" %>
<div class="PopPage_Content">
    <h1 id="setting-header"><img src="<%getInfos("avatar"); %>" alt="<%getInfos("name"); %>" /><%getInfos("title"); %></h1>
    <div id="setting-content">
        <ul class="clearfix">
            <li class="left active" id="setting-c-avatar">头像</li>
            <li class="left active" id="setting-c-account">账号</li>
            <li class="left" id="setting-c-password">密码</li>
        </ul>
        <ul id="setting-c-holder">
            <li id="setting-c-h-avatar">
                <form enctype="multipart/form-data" method="post" action="../Services/User.ashx" target="submitFrame">
                    <input type="hidden" name="action" value="avatarUp" />
                    <ul>
                        <li class="clearfix"><span class="left">选择图片：</span><input type="file" name="pic" /></li>
                        <li id="setting-c-h-a-bigShow" class="clearfix"><span class="left">我的头像：</span><img class="left" alt="<%getInfos("name"); %>" src="<%getInfos("avatar-big"); %>" /></li>
                    </ul>
                </form>
                <iframe id="submitFrame" name="submitFrame" style="display:none;"></iframe>
                <button>保存</button>
            </li>
            <li id="setting-c-h-account" class="hide">

            </li>
            <li id="setting-c-h-password" class="hide">

            </li>
        </ul>
    </div>
</div>
<script src="Script/jquery.imgareaselect-0.9.8/scripts/jquery.imgareaselect.min.js"></script>
<script>
    moyo.setting.avatarUpload();
    moyo.loadCss("Script/jquery.imgareaselect-0.9.8/css/imgareaselect-default.css");
</script>
