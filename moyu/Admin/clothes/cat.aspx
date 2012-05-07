<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cat.aspx.cs" Inherits="moyu.Admin.clothes.cat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" 
            Height="235px" Width="198px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="Data Source=INLOST-PC;Initial Catalog=moyu;Persist Security Info=True;User ID=sa;Password=thisislaobai" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE (([deep] = @deep) AND ([father] = @father))">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="deep" Type="Int16" />
                <asp:Parameter DefaultValue="0" Name="father" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ListBox ID="ListBox2" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id" 
            Height="236px" Width="204px"></asp:ListBox>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="Data Source=INLOST-PC;Initial Catalog=moyu;Persist Security Info=True;User ID=sa;Password=thisislaobai" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE (([deep] = @deep) AND ([father] = @father))">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="deep" Type="Int16" />
                <asp:ControlParameter ControlID="ListBox1" Name="father" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="86px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="删除" />
&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server" Width="90px"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="添加" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="删除" />
    </div>
    </form>
</body>
</html>
