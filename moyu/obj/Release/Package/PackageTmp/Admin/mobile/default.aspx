<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="moyu.Admin.mobile._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:1024px;margin:0 auto;">
        <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
            <AlternatingItemTemplate>
                <tr style="background-color: #FAFAD2;color: #284775;">
                    <td>
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="dateLabel" runat="server" Text='<%# Eval("date") %>' />
                    </td>
                    <td>
                        <asp:Label ID="giftLabel" runat="server" Text='<%# Eval("gift") %>' />
                    </td>
                    <td>
                        <asp:Label ID="uidLabel" runat="server" Text='<%# Eval("uid") %>' />
                    </td>
                    <td>
                        <asp:Label ID="messageLabel" runat="server" Text='<%# Eval("message") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #FFCC66;color: #000080;">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                    </td>
                    <td>
                        <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="dateTextBox" runat="server" Text='<%# Bind("date") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="giftTextBox" runat="server" Text='<%# Bind("gift") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="uidTextBox" runat="server" Text='<%# Bind("uid") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="messageTextBox" runat="server" Text='<%# Bind("message") %>' />
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                    <tr>
                        <td>未返回数据。</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="dateTextBox" runat="server" Text='<%# Bind("date") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="giftTextBox" runat="server" Text='<%# Bind("gift") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="uidTextBox" runat="server" Text='<%# Bind("uid") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="messageTextBox" runat="server" Text='<%# Bind("message") %>' />
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #FFFBD6;color: #333333;">
                    <td>
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="dateLabel" runat="server" Text='<%# Eval("date") %>' />
                    </td>
                    <td>
                        <asp:Label ID="giftLabel" runat="server" Text='<%# Eval("gift") %>' />
                    </td>
                    <td>
                        <asp:Label ID="uidLabel" runat="server" Text='<%# Eval("uid") %>' />
                    </td>
                    <td>
                        <asp:Label ID="messageLabel" runat="server" Text='<%# Eval("message") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr runat="server" style="background-color: #FFFBD6;color: #333333;">
                                    <th runat="server">id</th>
                                    <th runat="server">date</th>
                                    <th runat="server">gift</th>
                                    <th runat="server">uid</th>
                                    <th runat="server">message</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center;background-color: #FFCC66;font-family: Verdana, Arial, Helvetica, sans-serif;color: #333333;">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #FFCC66;font-weight: bold;color: #000080;">
                    <td>
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="dateLabel" runat="server" Text='<%# Eval("date") %>' />
                    </td>
                    <td>
                        <asp:Label ID="giftLabel" runat="server" Text='<%# Eval("gift") %>' />
                    </td>
                    <td>
                        <asp:Label ID="uidLabel" runat="server" Text='<%# Eval("uid") %>' />
                    </td>
                    <td>
                        <asp:Label ID="messageLabel" runat="server" Text='<%# Eval("message") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="SELECT [id], [date], [gift], [uid], [message] FROM [users_gifts] ORDER BY [id] DESC"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
