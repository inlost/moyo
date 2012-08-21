<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_mail.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_mail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-mail" class="selected jump" data-dst="Markets/Ecard/school/teacher_mail.aspx">邮件</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-class" class="jump" data-dst="Markets/Ecard/school/teacher_class.aspx">班级</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-courseware" class="jump" data-dst="Markets/Ecard/school/teacher_courseware.aspx">课件/教案</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-files" class="jump" data-dst="Markets/Ecard/school/teacher_cabinet.aspx">文件柜</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-addressList" data-dst="Markets/Ecard/school/teacher_contact.aspx" class="jump">通讯录</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-quit" class="jump">安全退出</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="schoolEcard_right" runat="server">
    <div id="schoolEcard-c-functionBox" class="ui-widget">
        <div id="schoolEcard-c-f-tabs">
	        <ul>
		        <li><a href="#schoolEcard-c-f-tabs-list">邮件列表</a></li>
		        <li><a href="#schoolEcard-c-f-tabs-new">新邮件</a></li>
	        </ul>
	        <div id="schoolEcard-c-f-tabs-list" class="clearfix">
                <div id="schoolEcard-c-f-t-l-students" class="ui-widget-content schoolEcard-c-fb">
                    <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>学生邮件</h3>
                </div>
                <div id="schoolEcard-c-f-t-l-home" class="ui-widget-content schoolEcard-c-fb">
                    <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>家长邮件</h3>
                </div>
                <div id="schoolEcard-c-f-t-l-cshool" class="ui-widget-content schoolEcard-c-fb">
                    <h3 class="ui-widget-header"><span class="ui-icon ui-icon-check"></span>同事邮件</h3>
                </div>
	        </div>
	        <div id="schoolEcard-c-f-tabs-new" class="clearfix">
		        <div id="schoolEcard-c-f-t-n-main" class="left">
                    <form>
                        <ul id="schoolEcard-c-f-t-n-m-receivers" class="clearfix">
                            <li id="schoolEcard-c-n-sendToBox-title">发送给：</li>
                            <li>
                                <div id="schoolEcard-c-f-t-n-m-r-addBox">
                                    <input type="text" />
                                </div>
                            </li>
                        </ul>
                        <div id="schoolEcard-c-f-t-n-body">
                            <div id="schoolEcard-c-f-t-n-b-title">
                                标题：<input type="text" />
                            </div>
                            <div><textarea id="schoolEcard-c-f-t-n-b-holder" name="schoolEcard-c-f-t-n-b-holder"></textarea></div> 
                        </div>
                    </form>
                    <div id="schoolEcard-c-f-t-n-m-submit">
                        <button type="submit">发送</button>
                    </div>
                </div>
                <div id="schoolEcard-c-f-t-n-sendTo" class="left ui-widget-content">
                    <h3 class="ui-widget-header"><span class="ui-icon ui-icon-star"></span>添加联系人</h3> 
                    <ul>
                        <%contactGet(); %>
                    </ul>
                </div>
	        </div>
        </div>
    </div>
<script>
    $("#schoolEcard-c-f-tabs").tabs();
    var moyo = new Moyo();
    moyo.loadCss("../Script/ueitor/themes/default/ueditor.css");
    moyo.school.mailInit();
</script>
<script type="text/javascript" src="../Script/ueitor/editor_config.js"></script>
<script type="text/javascript" src="../Script/ueitor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("schoolEcard-c-f-t-n-b-holder");
</script>
</asp:Content>
