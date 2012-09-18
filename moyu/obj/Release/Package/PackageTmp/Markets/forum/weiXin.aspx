<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weiXin.aspx.cs" Inherits="moyu.Markets.forum.weiXin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%getTitle(); %></title>
    <style>
        h1
        {
            font-size:18px;
            font-weight:bold;
            border-bottom:1px solid #DDD;
            line-height:30px;
        }
        div
        {
            font-size:16px;
            line-height:24px;
        }
        #tip
        {
            font-size:12px;
            color:#888;
        }
    </style>
</head>
<body>
    <h1><%getTitle(); %></h1>
    <div><p id="tip">左邻在你身边。www.ai0932.CoM——定西人的网上家园</p><%getContent(); %></div>
</body>
</html>
