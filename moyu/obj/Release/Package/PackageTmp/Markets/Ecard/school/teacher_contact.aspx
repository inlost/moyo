<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_contact.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-mail" class="jump" data-dst="Markets/Ecard/school/teacher_mail.aspx">邮件</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-class" class="jump" data-dst="Markets/Ecard/school/teacher_class.aspx">班级</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-courseware" class="jump" data-dst="Markets/Ecard/school/teacher_courseware.aspx">课件/教案</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-files" class="jump" data-dst="Markets/Ecard/school/teacher_cabinet.aspx">文件柜</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-addressList" data-dst="Markets/Ecard/school/teacher_contact.aspx" class="selected jump">通讯录</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-quit" class="jump">安全退出</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="schoolEcard_right" runat="server">
    <div id="schoolEcard-c-functionBox" class="ui-widget">
        <div id="schoolEcard-c-f-contack" class="ui-widget-content">
            <h3 class="ui-widget-header"><span class="ui-icon ui-icon-star"></span>通讯录</h3> 
            <ul id="schoolEcard-c-f-c-list" class="clearfix">
            <%contactGet(); %>
            </ul>
        </div>
    </div>
</asp:Content>
