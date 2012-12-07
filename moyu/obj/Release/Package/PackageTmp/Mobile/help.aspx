﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="moyu.Mobile.help" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page" data-theme="c" data-role="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <section class="ui-body ui-body-c">
            <nav>
                <ul class="lucky-list" data-role="listview" data-inset="true">
                    <li><a href="index.aspx">返回首页</a></li>
                    <li><a href="robot-group-kewWordsShow.aspx?type=group&tag=-1" data-ajax="false" style="color:red;">返回贴吧</a></li>
                </ul>
            </nav>
        </section>
        <section class="ui-body ui-body-c">
            <h1>左邻帮助</h1>
            <section data-role="collapsible-set" data-theme="c" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-d">
                <div data-role="collapsible" data-collapsed="false">
	                <h2>欢迎来到左邻</h2>
	                <p>左邻是定西人自己微信平台，定西的年轻朋友在这里汇聚。</p>
                    <p>我们在这里分享着自己的见闻发现和心情，并乐于帮助别人，构建着一个和谐安定的定西人自己的微信社区。</p>
                    <p><span style="font-weight:bold;">这里没有陌路，你从不曾孤单。</span> 欢迎加入左邻！</p>
                    <p>点击下面的条目，了解如何更好的使用左邻。</p>
	            </div>
	            <div data-role="collapsible">
	                <h2>注册登录和账号绑定</h2>
	                <h3 class="bold">1.注册左邻账号有什么好处？</h3>
                    <p>如你所见，和大多数网站一样，左邻也实行会员制度，在第一次使用左邻的时候，你需要注册一个属于你自己的左邻账号。</p>
                    <p>注册左邻账号之后，你可以方便的和左邻上的朋友进行交流，参与左邻为大家准备的各种活动，并可以很方便的领取到活动奖品。</p>
                    <h3 class="bold">2.如何注册？</h3>
                    <p>在微信对左邻发文字消息“<span class="sendMessage">注册</span>”，左邻就会给出账号绑定向导，按照向导提示的完成注册即可。</p>
                    <p>tip：<span style="color:red;">为了以后顺利的参与活动和领取奖品，请填写真实的注册信息。</span></p>
	                <h3 class="bold">3.登录</h3>
                    <p>注册之后，左邻会自动帮你登录左邻账号，无需重复登陆。</p>
                    <p>请尽量不要用别人的微信登陆你自己的左邻账号，如果你已经用别人的微信登陆或绑定了自己的左邻账号，请尽快用自己的微信重新登陆左邻账号一次。</p>
                </div>
	            <div data-role="collapsible">
	                <h2>签到积分和抽奖</h2>
                    <h3 class="bold">签到</h3>
	                <p>左邻鼓励大家每天都来签到，正确的完成注册和账号绑定的同学，可以通过以下两种方式签到：</p>
                    <ol>
                        <li>在微信对左邻发文字消息“<span class="sendMessage">签到</span>”即可自动完成签到。</li>
                        <li>在微信对左邻发文字消息“<span class="sendMessage">芝麻开门</span>”打开首页之后手动点击签到按钮完成签到。</li>
                    </ol>
                    <p>连续签到15天以上，在贴吧发帖时，用户名会显示为<span style="color:red;">尊贵的红色</span>，让你的帖子一眼就能被别人看到。</p>
                    <h3 class="bold">积分</h3>
                    <p>左邻的积分分为两种，一种为普通积分，一种为贡献积分，1个贡献积分可以兑换成30个普通积分。</p>
                    <p>普通积分可以用来抽奖，兑换电子优惠券，购买社区道具等，普通积分可以通过签到和在社区发帖回帖来得到。</p>
                    <p>贡献积分可以用来兑换普通积分，兑换现金代金券，兑换实物礼品等，贡献积分可以通过介绍朋友加入左邻来得到。</p>
                    <h3 class="bold">如何获得积分</h3>
                    <p>1.每日签到可以获得5个积分，连续签到会获得额外的两个奖励积分。</p>
                    <p>2.在贴吧发文字帖，每帖可以得到1个积分。</p>
                    <p>3.在贴吧发图片帖，每帖可以得到2个积分。</p>
                    <p>4.在贴吧回复别人的帖子，每次可以得到1个积分。</p>
                    <p>5.每天通过2，3，4途径获得的积分总和超过6分之后，当天不再增加积分。</p>
                    <h3 class="bold">抽奖</h3>
                    <p>我们在每天都为大家准备了一些小礼物，大家可以通过抽奖来拿到这些礼物。</p>
                    <p>每次抽奖需要消耗10个积分。</p>
                    <p>大家可以对左邻发消息“<span class="sendMessage">芝麻开门</span>”打开首页，在首页点击抽奖按钮即可进行抽奖。</p>
                    <p>礼物发放：礼物会在第二个工作日发放给获得礼物的同学。</p>
	            </div>
    	        <div data-role="collapsible">
	                <h2>电子优惠券</h2>
	                <p>我们会不定期和定西的商家联合举办一些活动，届时将会为大家提供代金券和优惠券。</p>
                    <p>兑换优惠券和代金券需要消耗一定的积分和贡献，大家可以通过对左邻发消息“<span class="sendMessage">芝麻开门</span>”打开首页，在首页点击优惠券按钮来查看正在发放的优惠券和代金券，并兑换它们。</p>
	            </div>
    	        <div data-role="collapsible" id="help-postBar">
	                <h2>和大家分享心情信息(贴吧)</h2>
	                <h3 class="bold">如何发帖子</h3>
                    <p>有什么新鲜发现想告诉大家？难过无聊开心寂寞的时候想找人说话？你可以在微信对左邻发消息“<span class="sendMessage">标签：内容</span>”</p>
                    <p>例如“<span class="sendMessage">开心：今天在东门口捡了1块钱。</span>”，在这个例子里，“开心”就是标签，“今天在东门口捡了1块钱”就是内容。</p>
                    <p>好的标签能让你分享的消息被更多的人看到，当别人在给左邻发的消息里含有你发表帖子时用过的标签的时候，左邻会把你发的帖子推送给他。</p>
                    <p>例如有人对左邻说“<span class="sendMessage">今天不开心了</span>”，因为他发的消息里包含你的标签“开心”，所以他就会收到你发的那篇开心的帖子。</p>
                    <h3 class="bold">如何查看帖子</h3>
                    <p>你可以对左邻发消息“<span class="sendMessage">贴吧</span>”，打开贴吧查看帖子。</p>
                    <p>也可以对左邻发消息“<span class="sendMessage">新鲜事儿</span>”，“<span class="sendMessage">大家在干嘛</span>”，“<span class="sendMessage">凑热闹</span>”，“<span class="sendMessage">无聊</span>”，打开贴吧查看帖子。</p>
                    <p>当你给左邻发的消息里包含大家说过的标签的时候，你也会看到大家说过的关于这个标签的所有帖子。</p>
                    <h3 class="bold">和大家分享照片（图片贴）</h3>
	                <p>在微信直接发图片给左邻，左邻上的朋友就都会看到你分享的照片了。</p>
                    <p>分享照片之后，你可以把它分享到朋友圈让更多的人看到。</p>
                    <p>你也可以为图片添加文字说明，做到有图有真相。</p>
                    <h3 class="bold">和大家分享秘密（匿名贴）</h3>
	                <p>帖子标签为“秘密”时，帖子上不会显示发帖人的名字，如果有什么说不出口但又不吐不快的秘密，就来发匿名帖吧。</p>
                    <p>匿名帖会在贴吧高亮显示，但是每次发匿名贴需要消耗3个积分。</p>
                    <h3 class="bold">贴吧红名</h3>
                    <p>连续签到15天以上，在贴吧发帖时，用户名会显示为<span style="color:red;">尊贵的红色</span>，让你的帖子一眼就能被别人看到。</p>
	            </div>
    	        <div data-role="collapsible">
	                <h2>左邻机器人</h2>
	                <p>左邻机器人是一个正在慢慢长大的机器人，你可以和她聊聊天，也可以教她一些知识。</p>
                    <p>当你跟左邻讲一些她能听的懂得话的时候，她就会把她知道的告诉你。</p>
                    <p>当你跟左邻讲一些她听不懂的话的时候，她就会让你教教她，在你教会了左邻之后，当再有人和她说同样的话的时候，她就会照你教她的回答别人了。</p>
                    <p>例如你可以跟左邻打招呼“<span class="sendMessage">你好吗</span>”</p>
                    <p>也可以问她“<span class="sendMessage">定西哪儿有买的营地灯</span>”</p>
                    <p>还可以问她“<span class="sendMessage">老白是谁</span>”</p>
                    <p>tip:当你想故意教会左邻一些东西的时候，尽量使用简单的词语效果会比较好，比如你是照相馆老板，只需要对左邻发消息“<span class="sendMessage">照相</span>”，然后左邻会让你教他如何回答这个问题，然后你可以留下自己店铺的名字，地址，还有联系电话，以后只要有人说的话里含有照相两个字，左邻就会告诉他你的店铺的名字，地址和联系电话了。</p>
                    <p>比如有人说“<span class="sendMessage">定西哪儿照相照的好</span>”，“<span class="sendMessage">今天去照相了</span>”，就都会收到你的店铺的名称地址和联系电话了。</p>
	            </div>
    	        <div data-role="collapsible">
	                <h2>帮助左邻做的更好</h2>
                    <p>如果你觉得左邻不错，想要帮助左邻，那么你可以把她告诉更多的身边的朋友，或者在微信里选择”在我的名片里显示“</p>
                    <p>如果有任何问题建议，欢迎大家对左邻发消息<span class="sendMessage">意见反馈：内容</span>来告诉我们。</p>
                    <p>你们的肯定是对我们最大的支持。</p>
	                <p>一路精彩即将揭晓，左邻与大家一起在成长。</p>
                    <p>不管啥时候，左邻，一直都在你身边</p>
	            </div>
            </section>
        </section>
    </section>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
</body>
</html>