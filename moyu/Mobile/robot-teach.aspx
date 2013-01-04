<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="robot-teach.aspx.cs" Inherits="moyu.Mobile.robot_teach" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <h3>调教左邻</h3>
        <div class="row">
            <a href="robot-teach-list.aspx?type=hasAnswer">
                <button class="command-button default" style="width:100%;">
                    返回调教列表
                    <small><strong>去看看别人都教了左邻什么</strong></small>
                </button>
            </a>
        </div>
        <section class="bg-color-blueLight padding20 fg-color-darken">
<% if(isShowTeach){ %>
            <form action="../Services/Mobile_Main.ashx?action=teachRobot" method="post">
                <section class="bg-color-pink fg-color-white padding10" style="margin:6px 6px 0;">
                    你刚对左邻说“<span style="color:red;"><%getQuestion(); %></span>”，我不知道要怎么回答你了，亲爱的教教我该怎么回答吧！
                </section>
                <input type="hidden" name="q" value="<%getRobotQuestion(); %>" />
                <ul class="bg-color-blue fg-color-white" style="list-style:none;margin:0 6px 6px;">
                    <li class="input-control textarea" style="margin:0;"><textarea name="a"></textarea></li>
                    <%isShowAdd(); %>
                </ul>
            </form>
<%}else{ %>
            <section class="bg-color-pink fg-color-white padding10" style="margin:6px 6px 0;">
                <%getRuleBody(); %>
            </section>
            <section class="bg-color-blue fg-color-white padding10" style="margin:0 6px 6px">
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
