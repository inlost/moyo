<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_home.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="selected jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
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
        <div id="schoolEcard-c-todo" class="ui-widget-content schoolEcard-c-fb">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>最新需要处理</h3>
        </div>
        <div id="schoolEcard-c-document" class="ui-widget-content schoolEcard-c-fb">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>最新公文</h3>
        </div>
        <div id="schoolEcard-c-mail" class="ui-widget-content schoolEcard-c-fb">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>最新邮件</h3>
        </div>
        <div id="schoolEcard-c-courseware" class="ui-widget-content schoolEcard-c-fb">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>教案/课件</h3>
        </div>
    </div>
<script>
    var moyo = new Moyo();
    moyo.school.teacherDesktopSortabale();
</script>
</asp:Content>
