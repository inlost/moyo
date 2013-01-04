<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mall-newShop.aspx.cs" Inherits="moyu.Mobile.mall_newShop" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>店铺入驻_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <h3>我是老板</h3>
        <a href="../sa/Default.aspx">
            <button class="command-button default" style="width:100%;">
                商家登陆
            </button>
        </a>
        <section class="grid bg-color-blueLight padding10" style="margin:10px;">
<%if(Session["power"]==null || Convert.ToInt32(Session["power"])<5){ %>
            <article>
                <p>如果你是店铺的老板，想让更多人发现你的店铺，请填写下面的表单，入驻左邻。</p>
                <p>店铺加入左邻后，大家就可以方便的通过手机随时随地获取到你店铺的信息了。</p>
                <p>这一切都是完全免费的，快来入驻吧~</p>
                <form action="../Services/Mall.ashx" method="post">
                    <input type="hidden" name="action" value="addApply" />
                    <input type="hidden" name="what" value="入驻" />
                    <ul style="list-style:none;">
                        <li class="input-control text span3">
                            <input type="text" name="shopName" placeholder="店铺名称" />
                            <span class="helper"></span>
                        </li>
                        <li class="input-control text span3">
                            <input type="text" name="bossName" placeholder="怎么称呼你" />
                            <span class="helper"></span>
                        </li>
                        <li class="input-control text span3">
                            <input type="text" name="tel" placeholder="联系电话" />
                            <span class="helper"></span>
                        </li>
                        <li><input type="submit" value="提交" /></li>
                    </ul>
                </form>
            </article>
<%}else{ %>
            <ul>
                <%getApplys(); %>
            </ul>
<%} %>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>
