<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lucky-gift.aspx.cs" Inherits="moyu.Mobile.lucky_gift" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>登陆_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="grid">
            <h3>好幸运，中奖啦！</h3>
            <form action="../Services/Mobile_Main.ashx?action=giftAdd" method="post">
                <ul style="list-style:none;">
                    <li>礼物将在第二日发放，注意查收哦</li>
                    <li  class="input-control select">
                        <select name="gift">
                            <option value="电话费">电话费</option>
                            <option value="QQ会员">QQ会员</option>
                            <option value="Q币">Q币</option>
                            <option value="黄钻">黄钻</option>
                            <option value="绿钻">绿钻</option>
                            <option value="粉钻">粉钻</option>
                            <option value="蓝钻">蓝钻</option>
                            <option value="迅雷会员">迅雷会员</option>
                        </select>
                    </li>
                    <li class="input-control text">
                        <input type="text" name="message" placeholder="手机号/QQ号/迅雷号"/>
                        <span class="helper"></span>
                    </li>
                    <li class="input-control text">
                        <input type="text" name="message2" placeholder="希望左邻提供哪些礼物"/>
                        <span class="helper"></span>
                    </li>
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