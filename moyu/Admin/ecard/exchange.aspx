<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exchange.aspx.cs" Inherits="moyu.Admin.ecard.exchange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        名称：<asp:TextBox ID="textName" runat="server"></asp:TextBox>
        <br />
        数量：<asp:TextBox ID="textNumber" runat="server"></asp:TextBox>
        <br />
        图片：<asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        所需积分：<asp:TextBox ID="textJf" runat="server"></asp:TextBox>
    
        <br />
        <asp:Button ID="Button1" runat="server" Text="发布" onclick="Button1_Click" />
    
    </div>
    </form>
</body>
</html>
