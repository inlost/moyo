<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addPicIntroduce.aspx.cs" Inherits="moyu.Mobile.addPicIntroduce" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
    <style>        
        .page {
            overflow-x:hidden;
        }
    </style>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section>
            <div class="page-content">
                <%getBody(); %>
            </div>
            <form action="../Services/Information_group.ashx" method="post">
                <input type="hidden" name="action" value="addPicPostIntroduce" />
                <input type="hidden" name="pid" value="<%getPid(); %>" />
                <input type="hidden" name="tid" value="<%getTid(); %>" />
                <div class="input-control textare" style="width:100%;">
                    <textarea name="introduce"></textarea>
                </div>
                <button type="submit"><%getBtnText(); %></button>
            </form>
<% if(Convert.ToInt32(Request.Params["pid"])!=0){ %>
            <section class="grid">
                <div class="row">
                    <form action="../Services/Information_group.ashx" method="post">
                        <input type="hidden" name="action" value="setAvatar" />
                        <input type="hidden" name="pid" value="<%getPid(); %>" />
                        <input type="hidden" name="tid" value="<%getTid(); %>" />
                        <input type="hidden" name="introduce" value="我悄悄的换了一个新头像，大家觉得怎么样呀？" />
                        <button class="command-button default" style="width:100%;" type="submit">
                            设置成头像
                            <small>上传正方形图片效果最佳</small>
                        </button>
                    </form>
                </div>
            </section>
<%} %>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
