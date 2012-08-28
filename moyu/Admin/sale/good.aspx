<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="good.aspx.cs" Inherits="moyu.Admin.sale.good" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="../../Script/ueitor/themes/default/ueditor.css" />
    <script src="../../Script/ueitor/editor_config.js"></script>
    <script src="../../Script/ueitor/editor_all_min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul>
            <li>分类：<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id" AutoPostBack="True"></asp:DropDownList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:conStr %>' SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE ([deep] = @deep)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="1" Name="deep" Type="Int16"></asp:Parameter>
                </SelectParameters>
            </asp:SqlDataSource>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="SELECT [id], [name] FROM [sale_cat] WHERE ([father] = @father)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="father" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </li>
            <li>名称：<asp:TextBox ID="TextName" runat="server"></asp:TextBox></li>
            <li>图片：<asp:FileUpload ID="FileUpload1" runat="server" /></li>
            <li>卖家：<asp:TextBox ID="TextSeller" runat="server"></asp:TextBox></li>
            <li>价格：<asp:TextBox ID="TextPrice" runat="server"></asp:TextBox></li>
            <li>销售类型：<asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem Value="1">大图展示</asp:ListItem>
                <asp:ListItem Value="2">小图展示</asp:ListItem>
            </asp:DropDownList></li>
            <li>
                <p>介绍：    <asp:Button ID="Button1" runat="server" Text="发布" OnClick="Button1_Click"/></p>
                <asp:TextBox ID="TextIntroduce" runat="server" TextMode="MultiLine" Height="424px" Width="620px"></asp:TextBox>
            </li>
        </ul>
    </div>
    </form>
<script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ initialContent: "" });
    editor.render("TextIntroduce");
</script>
</body>
</html>
