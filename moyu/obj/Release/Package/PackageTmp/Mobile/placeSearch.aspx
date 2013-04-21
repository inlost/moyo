<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="placeSearch.aspx.cs" Inherits="moyu.Mobile.placeSearch" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>搜索_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section>
            <h2><%getName(); %></h2>
            <img style="width:100%;" src="<%getMapUrl(); %>" />
            <p><%getAddress(); %></p>
            <p><%getTel(); %></p>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
