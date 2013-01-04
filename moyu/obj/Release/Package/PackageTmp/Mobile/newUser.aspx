<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newUser.aspx.cs" Inherits="moyu.Mobile.newUser" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>感谢_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section>
            <h3>谁介绍你来左邻的？</h3>
                <form action="../Services/Mobile_Main.ashx?action=giveThanks" method="post">
                    <div class="input-control text span3">
                        <input type="text" name="to" placeholder="Ta的用户名"/>
                        <span class="helper"></span>
                    </div>
                    <button  class="command-button default" type="submit" style="width:100%;">
                        点击这里为介绍人增加一个贡献
                        <small>提示：贡献可是很值钱的哦</small>
                    </button>
                </form>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
