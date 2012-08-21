<%@ Page Title="" Language="C#" MasterPageFile="~/Templete/ecard_school.Master" AutoEventWireup="true" CodeBehind="teacher_courseware.aspx.cs" Inherits="moyu.Markets.Ecard.school.teacher_courseware" %>
<asp:Content ID="Content1" ContentPlaceHolderID="schoolEcard_left" runat="server">
    <ul id="schoolEcard-f-functionList">
        <li><a href="#marketBox" id="schoolEcard-f-f-disktop" class="jump" data-dst="Markets/Ecard/school/teacher_home.aspx">桌面</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-documents" class="jump" data-dst="Markets/Ecard/school/teacher_documents.aspx">公文</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-mail" class="jump" data-dst="Markets/Ecard/school/teacher_mail.aspx">邮件</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-class" class="jump" data-dst="Markets/Ecard/school/teacher_class.aspx">班级</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-courseware" class="selected jump" data-dst="Markets/Ecard/school/teacher_courseware.aspx">课件/教案</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-files" class="jump" data-dst="Markets/Ecard/school/teacher_cabinet.aspx">文件柜</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-addressList" data-dst="Markets/Ecard/school/teacher_contact.aspx" class="jump">通讯录</a></li>
        <li><a href="#marketBox" id="schoolEcard-f-f-quit" class="jump">安全退出</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="schoolEcard_right" runat="server">
<div id="schoolEcard-c-functionBox" class="ui-widget">
    <div id="schoolEcard-c-f-tabs">
	    <ul>
        	<li><a href="#schoolEcard-c-f-tabs-new">新建</a></li>
		    <li><a href="#schoolEcard-c-f-tabs-list">浏览</a></li>
	    </ul>
	    <div id="schoolEcard-c-f-tabs-new" class="clearfix">
            <form>
                <ul id="schoolEcard-c-f-t-n-from" class="clearfix">
                    <li>
                        <ul id="schoolEcard-c-f-t-n-f-coursePars" class="clearfix">
                            <li id="schoolEcard-c-f-t-n-f-c-title">标题：<input type="text" name="title"/></li>
                            <li id="schoolEcard-c-f-t-n-f-c-grade"">年级：
                                <select>
                                    <option value="12">高三</option>
                                    <option value="11">高二</option>
                                    <option value="10">高一</option>
                                    <option value="9">九年级</option>
                                    <option value="8">八年级</option>
                                    <option value="7">七年级</option>
                                    <option value="6">六年级</option>
                                    <option value="5">五年级</option>
                                    <option value="4">四年级</option>
                                    <option value="3">三年级</option>
                                    <option value="2">二年级</option>
                                    <option value="1">一年级</option>
                                </select>
                            </li>
                            <li id="schoolEcard-c-f-t-n-f-c-subject">科目
                                <select>
                                    <option value="1">语文</option>
                                    <option value="2">数学</option>
                                    <option value="3">英语</option>
                                    <option value="4">生物</option>
                                    <option value="5">化学</option>
                                    <option value="6">物理</option>
                                    <option value="7">政治</option>
                                    <option value="8">地理</option>
                                    <option value="9">历史</option>
                                    <option value="10">思想品德</option>
                                    <option value="11">计算机</option>
                                    <option value="12">美术</option>
                                    <option value="13">音乐</option>
                                    <option value="14">体育</option>
                                    <option value="15">其它</option>
                                </select>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <textarea id="schoolEcard-c-f-t-n-f-holder" name="schoolEcard-c-f-t-n-f-holder"></textarea>
                    </li>
                    <li><button id="submitCourseware" type="button">发表</button></li>
                </ul>
            </form>
	    </div>
	    <div id="schoolEcard-c-f-tabs-list" class="clearfix">

	    </div>
    </div>
</div>
<script type="text/javascript">
    var moyo = new Moyo();
    moyo.loadCss("../Script/ueitor/themes/default/ueditor.css");
    moyo.school.coursewareInit();
</script>
<script type="text/javascript" src="../Script/ueitor/editor_config.js"></script>
<script type="text/javascript" src="../Script/ueitor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("schoolEcard-c-f-t-n-f-holder");
</script>
</asp:Content>
