﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-teach.aspx.cs" Inherits="moyu.Mobile.robot_teach" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page" data-theme="c" data-role="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li><a href="help.aspx" data-ajax="false">机器人说明书</a></li>
                    <li><a href="index.aspx" data-ajax="false">返回首页</a></li>
                </ul>
            </nav>
        </section>
        <section class="ui-body ui-body-c">
<% if(isShowTeach){ %>
            <form action="../Services/Mobile_Main.ashx?action=teachRobot" method="post">
                <section class="ui-bar ui-bar-e">
                    你刚对左邻说“<span style="color:red;"><%getQuestion(); %></span>”，我不知道要怎么回答你了，亲爱的教教我该怎么回答吧！
                </section>
                <input type="hidden" name="q" value="<%getRobotQuestion(); %>" />
                <ul>
                    <li>大家不要把左邻教坏了哦…</li>
                    <li><textarea name="a"></textarea></li>
                    <%isShowAdd(); %>
                </ul>
            </form>
<%}else{ %>
            <section>
                <%getRuleBody(); %>
            </section>
            <section class="ui-bar ui-bar-e">
            “<span style="color:red;"><%getQuestion(); %></span>”这个问题<span style="color:red;"><%getNiceName(); %></span>已经教会我了，我厉害吧【得意】~
            </section>
<%} %>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
