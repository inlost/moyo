<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cat.aspx.cs" Inherits="moyu.Admin.living.cat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>名称:<asp:TextBox ID="TextName" runat="server"></asp:TextBox></p>
        <p>排序:<asp:TextBox ID="TextOrder" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="Button1" runat="server" Text="添加" onclick="Button1_Click" /></p>
    </div>
    <hr />
    <div>
        <p>分类：<asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="Data Source=LocalHost;Initial Catalog=moyu;Integrated Security=True" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [living_cat]"></asp:SqlDataSource>
        </p>
        <p>名称：<asp:TextBox ID="TextAdTitle" runat="server"></asp:TextBox></p>
        <p>图片：<asp:FileUpload ID="FileUpload1" runat="server" /></p>
        <p>连接：<asp:TextBox ID="TextAdUrl" runat="server"></asp:TextBox></p>
        <p>
            <asp:Button ID="Button2" runat="server" Text="添加" onclick="Button2_Click" /></p>
    </div>
    </form>
</body>
</html>
