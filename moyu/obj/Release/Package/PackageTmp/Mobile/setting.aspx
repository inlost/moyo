<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="moyu.Mobile.setting" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>设置_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
        <script src="../Style/olton-Metro-UI/javascript/pagecontrol.js"></script>
    </header>
    <section id="content">
        <section class="grid">
                <h3>设置</h3>
                <div class="row">
                    <a href="index.aspx">
                        <button class="command-button default" style="width:100%;">
                            返回首页
                            <small><strong>点击这里返回首页</strong></small>
                        </button>
                    </a>
                </div>
            </section>
        <div class="page-control" data-role="page-control">
	        <ul>
		        <li class="active"><a href="#loginFormHolder">基本信息</a></li>
	        </ul>
            <div class="frames">
                <section id="loginFormHolder" class="frame active">
                    <ul class="funList">
                        <li id="userNameModify">用户名：<span><%getInfos("userName"); %></span>  <a href="javascript:void(0);" >修改</a></li>
                    </ul>
                </section>
            </div>
        </div>
        <section class="hide">
            <ul>
                <li class="userNameModify">用户名：<input style="width:80px;" type ="text" /> <a href="javascript:void(0);">保存</a></li>
            </ul>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
    <script>
        $("#userNameModify").on("click", function () {
            $(this).replaceWith($(".userNameModify"));
        });
        $(".userNameModify a").on("click", function () {
            var newUserName = $(".userNameModify input").val();
            if (newUserName.length < 2) {
                alert("用户名长度必须大于或等于两个汉字或字母");
                return;
            }
            if (confirm("成功修改用户名需要消耗36个积分，您确定修改么？"))
            {
                $.ajax({
                    url: "../Services/User.ashx",
                    type: "POST",
                    data:{action:"changeName",newName:newUserName},
                    success: function (msg) {
                        switch (Number(msg))
                        {
                            case -1:
                                alert("这个用户名已经被被人使用了，再换一个试试吧~");
                                break;
                            case -2:
                                alert("用户名长度必须大于或等于两个汉字或字母");
                                break;
                            case 0:
                                alert("服务器内部错误");
                                break;
                            default:
                                location.reload();
                                break;
                        }
                    }
                });
            }
        });
    </script>
</body>
</html>
