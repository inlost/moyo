<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lucky-gift.aspx.cs" Inherits="moyu.Mobile.lucky_gift" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登陆_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <h1>好幸运，中奖啦！</h1>
            <form action="../Services/Mobile_Main.ashx?action=giftAdd" method="post">
                <ul>
                    <li>
                        <span>请选择礼物：</span>
                        <select name="gift">
                            <option value="电话费">电话费</option>
                            <option value="QQ会员">QQ会员</option>
                            <option value="黄钻">黄钻</option>
                            <option value="绿钻">绿钻</option>
                            <option value="粉钻">粉钻</option>
                            <option value="蓝钻">蓝钻</option>
                            <option value="迅雷会员">迅雷会员</option>
                        </select>
                    </li>
                    <li>礼物将在第二日发放，注意查收哦</li>
                    <li><span>礼物备注：</span><input type="text" name="message" placeholder="手机号/QQ号/迅雷号"/></li>
                    <li><input type="text" name="message2" placeholder="希望左邻提供哪些礼物"/></li>
                    <li><input type="submit" value="提交" /></li>
                </ul>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>