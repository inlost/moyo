<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="moyu.Mobile.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登陆_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link rel="stylesheet" href="../Style/mobile.css" />
    <link rel="stylesheet" href="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.css" />
    <script src="../Script/jquery-1.8.2.min.js"></script>
    <script src="../Script/jquery.mobile-1.1.1/jquery.mobile-1.1.1.min.js"></script>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <div data-role="navbar">
	        <ul id="pageNav">
		        <li class="loginBtn"><a href="#" class="ui-btn-active ui-state-persist">登录</a></li>
		        <li class="regBtn"><a href="#">注册</a></li>
	        </ul>
        </div>
        <section id="loginFormHolder" class="functionList loginForm ui-body ui-body-c">
            <form action="../Services/User.ashx" method="post">
                <input type="hidden" name="action" value="mobileLogin" />
                <input type="hidden" name="rdUrl" value="<%getUrl(); %>" />
                <input type="hidden" name="wu" value="<%getWu(); %>" />
                <ul class="clear">
                    <%getLoginMsg(); %>
                    <li class="functionList-half"><span>用户名：</span><input type="text" name="uid" /></li>
                    <li class="functionList-half"><span>密码：</span><input type="password" name="password" /></li>
                    <li class="functionList-half"><input type="submit" value="登录" /></li>
                </ul>
            </form>
        </section>
        <section id="regFormHolder" class="functionList loginForm ui-body ui-body-c hide">
            <h1 id="userRegForm"> 请填写真实的注册信息</h1>
            <form action="../Services/User.ashx" method="post">
                <input type="hidden" name="action" value="mobileReg" />
                <input type="hidden" name="rdUrl" value="<%getUrl(); %>" />
                <input type="hidden" name="wu" value="<%getWu(); %>" />
                <ul class="clear">
                    <li class="functionList-half"><span>用户名：</span><input type="text" name="niceName" /></li>
                    <li class="functionList-half"><span>姓名：</span><input type="text" name="realName" /></li>
                    <li class="functionList-half left">
                        <span>性别：</span>
                        <select name="sex">
                            <option value="boy">男</option>
                            <option value="girl">女</option>
                        </select>
                    </li>
                    <li class="functionList-half left">
                        <span>年龄：</span>
                        <select name="birth">
                            <option value="1950-1-1">50后</option>
                            <option value="1960-1-1">60后</option>
                            <option value="1970-1-1">70后</option>
                            <option value="1980-1-1" selected="selected">80后</option>
                            <option value="1990-1-1">90后</option>
                            <option value="2000-1-1">00后</option>
                        </select>
                    </li>
                    <li class="functionList-half"><span>QQ：</span><input type="text" name="email" /></li>
                    <li class="functionList-half"><span>电话：</span><input type="text" name="phone" /></li>
                    <li class="functionList-half"><span>密码：</span><input type="password" name="password" /></li>
                    <li class="functionList-half"><input type="submit" value="注册" /></li>
                </ul>
            </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
    <script>
        var navHolder = $("#pageNav li");
        navHolder.on("click", function () {
            $(".functionList").addClass("hide");
            if ($(this).hasClass("regBtn")) {
                $("#regFormHolder").removeClass("hide");
            } else {
                $("#loginFormHolder").removeClass("hide");
            }
        });
    </script>
</body>
</html>
