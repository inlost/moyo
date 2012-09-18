<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="moyu.Admin.living.info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet"href="../../Script/ueitor/themes/default/ueditor.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>分类：<asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" 
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="Data Source=localhost;Initial Catalog=moyu;Integrated Security=True" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [living_cat]"></asp:SqlDataSource>
        </p>
        <p>店铺：<asp:DropDownList ID="DropDownList2" runat="server" 
                DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="Data Source=localhost;Initial Catalog=moyu;Integrated Security=True" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [living_shops] WHERE ([cat] = @cat)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="cat" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </p>
        <p>标题：<asp:TextBox ID="TextTitle" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="发布" onclick="Button1_Click" /></p>
        <div><asp:TextBox ID="TextBody" runat="server" Height="392px" TextMode="MultiLine" 
            Width="677px"></asp:TextBox></div>
        <br />
    </div>
    </form>
<script type="text/javascript" src="../../Script/ueitor/editor_config.js"></script>
<script type="text/javascript" src="../../Script/ueitor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("TextBody");
</script>
</body>
</html>
