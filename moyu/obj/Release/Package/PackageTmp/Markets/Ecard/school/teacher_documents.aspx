<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_documents.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_documents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="selected jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-mail" class="jump" data-dst="Markets/Ecard/school/teacher_mail.aspx">邮件</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-class" class="jump" data-dst="Markets/Ecard/school/teacher_class.aspx">班级</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-courseware" class="jump" data-dst="Markets/Ecard/school/teacher_courseware.aspx">课件/教案</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-files" class="jump" data-dst="Markets/Ecard/school/teacher_cabinet.aspx">文件柜</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-addressList" data-dst="Markets/Ecard/school/teacher_contact.aspx" class="jump">通讯录</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-quit" class="jump">安全退出</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="schoolEcard_right" runat="server">
    <div id="schoolEcard-c-functionBox" class="ui-widget clearfix">
        <div id="schoolEcard-c-documentsList" class="ui-widget-content left">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>学校公文</h3>
        </div>
        <div id="schoolEcard-c-myDocumentsList" class="ui-widget-content left">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>我发的公文</h3>
        </div>
        <div id="schoolEcard-c-draftDocumentsList" class="ui-widget-content left">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>公文草稿</h3>
        </div>
        <div id="schoolEcard-c-newDocuments" class="ui-widget-content left">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>发新公文</h3>
            <ul id="schoolEcard-c-n-sendToBox" class="clearfix">
                <li id="schoolEcard-c-n-sendToBox-title">发送给：</li>
                <li>
                        <div id="schoolEcard-c-n-architectureAddBox">
                        <select id="schoolEcard-c-n-s-architecture"><%architecture_get(); %></select>
                        <select id="schoolEcard-c-n-s-architecture_type">
                            <option value="0">全部</option>
                            <option value="1">老师</option>
                            <option value="2">学生</option>
                            <option value="4">家长</option>
                        </select>
                        <button type="button" id="schoolEcard-c-n-s-architecture-add">添加</button>
                    </div>               
                </li>
            </ul>
            <form id="schoolEcard-c-n-body">
                <div id="schoolEcard-c-n-b-title">标题：<input type="text" /></div>
                <div><textarea id="schoolEcard-c-n-b-holder" name="schoolEcard-c-n-b-holder"></textarea></div> 
            </form>
            <div id="schoolEcard-c-n-submit">
                <button type="button">存草稿</button>
                <button type="submit">发布</button>
            </div>
        </div>
    </div>
<script type="text/javascript">
    var moyo = new Moyo();
    moyo.loadCss("../Script/ueitor/themes/default/ueditor.css");
    moyo.school.teacherDocumentInit();
</script>
<script type="text/javascript" src="../Script/ueitor/editor_config.js"></script>
<script type="text/javascript" src="../Script/ueitor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({initialContent:""});
    editor.render("schoolEcard-c-n-b-holder");
</script>
</asp:Content>
