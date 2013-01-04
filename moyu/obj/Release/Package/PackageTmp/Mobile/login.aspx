<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="moyu.Mobile.login" %>
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
        <script src="../Style/olton-Metro-UI/javascript/pagecontrol.js"></script>
    </header>
    <section id="content">
        <div class="page-control" data-role="page-control">
	        <ul>
		        <li class="active"><a href="#loginFormHolder">登录</a></li>
		        <li class="regBtn"><a href="#regFormHolder">注册</a></li>
	        </ul>
            <div class="frames">

                <section id="loginFormHolder" class="frame active">
                    <form action="../Services/User.ashx" method="post">
                        <input type="hidden" name="action" value="mobileLogin" />
                        <input type="hidden" name="rdUrl" value="<%getUrl(); %>" />
                        <input type="hidden" name="wu" value="<%getWu(); %>" />
                        <ul style="list-style:none;">
                            <%getLoginMsg(); %>
                            <li class="input-control text span3">
                                <input type="text" name="uid" placeholder="用户名" />
                                <span class="helper"></span>
                            </li>
                            <li class="input-control password span3">
                                <input type="password" name="password" placeholder="密码" />
                                <span class="helper"></span>
                            </li>
                            <li><input type="submit" value="登录" /></li>
                        </ul>
                    </form>
                </section>
                <section id="regFormHolder" class="frame">
                    <form action="../Services/User.ashx" method="post">
                        <input type="hidden" name="action" value="mobileReg" />
                        <input type="hidden" name="rdUrl" value="<%getUrl(); %>" />
                        <input type="hidden" name="wu" value="<%getWu(); %>" />
                        <ul style="list-style:none;">
                            <li class="input-control text span3">
                                <input type="text" name="niceName" required="required" placeholder="用户名" />
                                <span class="helper"></span>
                            </li>
                            <li class="input-control text span3">
                                <input type="text" name="realName" required="required" placeholder="真实姓名"/>
                                <span class="helper"></span>
                            </li>
                            <li class="left clear">
                                <span class="left" style="line-height:30px;">性别：</span>
                                <span class="left input-control select" style="width:60px;">
                                    <select name="sex">
                                        <option value="boy">男</option>
                                        <option value="girl">女</option>
                                    </select>
                                </span>
                            </li>
                            <li class="left clear">
                                <span class="left" style="line-height:30px;">年龄：</span>
                                <span class="left input-control select">
                                    <select name="birth" style="width:74px;">
                                        <option value="1950-1-1">50后</option>
                                        <option value="1960-1-1">60后</option>
                                        <option value="1970-1-1">70后</option>
                                        <option value="1980-1-1" selected="selected">80后</option>
                                        <option value="1990-1-1">90后</option>
                                        <option value="2000-1-1">00后</option>
                                    </select>
                                </span>
                            </li>
                            <li style="clear:both;" class="input-control text span3">
                                <input type="text" name="email" required="required"  placeholder="QQ"/>
                                <span class="helper"></span>
                            </li>
                            <li class="input-control text span3">
                                <input type="text" name="phone" required="required" placeholder="手机" />
                                <span class="helper"></span>
                            </li>
                            <li class="input-control password span3">
                                <input type="password" name="password" required="required" placeholder="密码"/>
                                <span class="helper"></span>
                            </li>
                            <li><input type="submit" value="注册" /></li>
                        </ul>
                    </form>
                </section>

            </div>
        </div>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
