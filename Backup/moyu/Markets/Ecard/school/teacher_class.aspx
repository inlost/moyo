<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_class.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-mail" class="jump" data-dst="Markets/Ecard/school/teacher_mail.aspx">邮件</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-class" class="selected jump" data-dst="Markets/Ecard/school/teacher_class.aspx">班级</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-courseware" class="jump" data-dst="Markets/Ecard/school/teacher_courseware.aspx">课件/教案</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-files" class="jump" data-dst="Markets/Ecard/school/teacher_cabinet.aspx">文件柜</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-addressList" data-dst="Markets/Ecard/school/teacher_contact.aspx" class="jump">通讯录</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-quit" class="jump">安全退出</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="schoolEcard_right" runat="server">
    <div id="schoolEcard-c-functionBox" class="ui-widget">
        <h3 class="ui-widget-header"><span class="ui-icon ui-icon-star"></span>班级</h3> 
        <div id="schoolEcard-c-f" class="ui-widget-content clearfix">
            <div id="schoolEcard-c-f-switchClass">
                <ul id="schoolEcard-c-f-s-list" class="clearfix">
                    <li>请选择班级</li>
                    <%architecture_get(); %>
                </ul>
            </div>
            <ul class="left" id="student-List">
                <li class="functionListTitle">学生列表</li>
                <%studentGet(); %>
            </ul>
            <ul class="left" id="student-late-list">
                <li class="functionListTitle">迟到</li>
            </ul>
            <ul class="left" id="student-leave-list">
                <li class="functionListTitle">请假</li>
            </ul>
            <ul class="left" id="student-leaveEarly-list">
                <li class="functionListTitle">早退</li>
            </ul>        
        </div>
    </div>
<script type="text/javascript">
    var moyo = new Moyo();
    moyo.school.classInit();
</script>
</asp:Content>
