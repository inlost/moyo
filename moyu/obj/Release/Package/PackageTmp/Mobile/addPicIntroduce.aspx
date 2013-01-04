<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addPicIntroduce.aspx.cs" Inherits="moyu.Mobile.addPicIntroduce" %>
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
        <section>
            <div class="page-content">
                <%getBody(); %>
            </div>
            <form action="../Services/Information_group.ashx" method="post">
                <input type="hidden" name="action" value="addPicPostIntroduce" />
                <input type="hidden" name="pid" value="<%getPid(); %>" />
                <input type="hidden" name="tid" value="<%getTid(); %>" />
                <div class="input-control textare" style="width:90%;">
                    <textarea name="introduce"></textarea>
                </div>
                <button type="submit"><%getBtnText(); %></button>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
