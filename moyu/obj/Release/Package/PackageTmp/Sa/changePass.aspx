<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePass.aspx.cs" Inherits="moyu.Sa.changePass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>系统检测到你在使用默认密码，请修改密码后继续</h1>
        <ul>
            <li>输入新密码：<asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox></li>
            <li>再次输入：<asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></li>
            <li>
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" /></li>
        </ul>
    </div>
    </form>
</body>
</html>
