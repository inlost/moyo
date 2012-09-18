<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop.aspx.cs" Inherits="moyu.Admin.living.shop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Script/jquery-1.8.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>分类：<asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="Data Source=localhost;Initial Catalog=moyu;Integrated Security=True" 
                ProviderName="System.Data.SqlClient" 
                SelectCommand="SELECT [id], [name] FROM [living_cat]"></asp:SqlDataSource>
        </p>
        <p>图片：<asp:FileUpload ID="FileUpload1" runat="server" /></p>
        <p>名称：<asp:TextBox ID="TextName" runat="server"></asp:TextBox></p>
        <p>介绍：<asp:TextBox ID="TextIntroduce" runat="server" Height="91px" TextMode="MultiLine" 
                Width="274px"></asp:TextBox></p>
        <p>地址：<asp:TextBox ID="TextAddress" runat="server"></asp:TextBox></p>
        <p>营业时间：<asp:TextBox ID="TextTime" runat="server"></asp:TextBox></p>
        <p>是否新店展示:：<asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem Value="1">展示</asp:ListItem>
            <asp:ListItem Selected="True" Value="0">不展示</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p>电话：<asp:TextBox ID="TextPhone" runat="server"></asp:TextBox>地图：<asp:TextBox 
                ID="TextX" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextY" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="添加" onclick="Button1_Click" />
        </p>
        <div id="map" style="width:603px;height:300px">
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function init() {
            var myLatlng = new soso.maps.LatLng(35.580834465188175, 104.62905764579773);
            var myOptions = {
                zoom: 8,
                center: myLatlng,
                mapTypeId: soso.maps.MapTypeId.ROADMAP,
                zoomLevel: 15
            }
            var map = new soso.maps.Map(document.getElementById("map"), myOptions);
            soso.maps.Event.addListener(
                map,
                'click',
                function (event) {
                    var marker = new soso.maps.Marker({
                        position: event.latLng,
                        map: map
                    });
                    $("#TextX").val(event.latLng.getLng());
                    $("#TextY").val(event.latLng.getLat());
                }
            );
        }
        (function loadScript() {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://api.map.soso.com/v1.0/main.js?callback=init";
            document.body.appendChild(script);
        })();
    </script>
</body>
</html>
