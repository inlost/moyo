<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clothes.aspx.cs" Inherits="moyu.Admin.clothes.clothes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="../../Script/ueitor/themes/default/ueditor.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            分类：<asp:DropDownList ID="DropDownCatOne" 
                runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="Data Source=INLOST-PC;Initial Catalog=moyu;Persist Security Info=True;User ID=sa;Password=thisislaobai" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE (([deep] = @deep) AND ([father] = @father))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="deep" Type="Int16" />
                    <asp:Parameter DefaultValue="0" Name="father" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:DropDownList ID="DropDownCat" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="Data Source=INLOST-PC;Initial Catalog=moyu;Persist Security Info=True;User ID=sa;Password=thisislaobai" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE (([deep] = @deep) AND ([father] = @father))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="2" Name="deep" Type="Int16" />
                    <asp:ControlParameter ControlID="DropDownCatOne" Name="father" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div>
            原价：<asp:TextBox ID="TextOldPrice" runat="server"></asp:TextBox>
            卖价：<asp:TextBox ID="TextSalePrice" runat="server"></asp:TextBox>
        </div>
        <div>库存：<asp:TextBox ID="TextInventory" runat="server"></asp:TextBox></div>
        <div>名称：<asp:TextBox ID="TextName" runat="server" Width="358px"></asp:TextBox></div>
        <div>图片：<asp:FileUpload ID="FileUpload1" runat="server" /></div>
        <div>
            <asp:TextBox ID="TextIntroduce" runat="server"
                TextMode="MultiLine" Width="733px"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="发布" onclick="Button1_Click" /></div>
    </div>
    </form>

<script type="text/javascript" src="../../Script/ueitor/editor_config.js"></script>
<script type="text/javascript" src="../../Script/ueitor/editor_all_min.js"></script>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("TextIntroduce");
</script>
</body>
</html>
