<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="moyu.Sa.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" action="../Services/Union.ashx" method="post">
    <div>
        <input type="hidden" value="webLogin" name="action" />
        <ul>
            <li>商家编号：<input type="text" name="sid" /></li>
            <li>商家密码：<input type="password" name="password" /></li>
            <li><button type="submit">登陆</button></li>
        </ul>
    </div>
    </form>
</body>
</html>
