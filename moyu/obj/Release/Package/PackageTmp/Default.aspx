﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="moyu.Default" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html class="ie ie6"> <![endif]-->
<!--[if IE 7 ]>    <html class="ie ie7"> <![endif]-->
<!--[if IE 8 ]>    <html class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <html class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--><html lang="zh-CN" xmlns="http://www.w3.org/1999/xhtml"><!--<![endif]-->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>沁辰左邻-定西人的网上家园</title>
    <meta name="description" content="定西人自己的网上家园，定西的信息平台，提供定西网络购物、团购，定西社交社区，定西电脑上门服务，定西一卡通等信息化互联网服务。" />
    <meta name="keywords" content="定西,信息平台,定西网购,定西电脑上面维修,定西信息港,定西一卡通,定西商家联盟,定西社区,定西贴吧" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link rel="Stylesheet" href="Style/main.css?spm=9-18-4" />
    <link rel="Stylesheet" href="Script/jquery-ui/css/flick/jquery-ui-1.8.23.custom.css"/>
    <!--[if lt IE 7]>
    <script>
        try { document.execCommand('BackgroundImageCache', false, true); } catch (e) {}
    </script>
    <![endif]-->
</head>
<body class="page">
    <div id="layout">
        <div id="header-navigation">
            <div id="channels" class="center">
                <ul id="channelList" class="clearfix">
					<li class="channelItem" id="logo">
					</li>
                    <li class="channelItem activeChannel clearfix" id="homeChannel">
						<span class="left"></span>
                        <h1 class="left">定西上网导航</h1>
                    </li>
                    <li class="channelItem clearfix" id="forumChannel">
						<span class="left"></span>
						<h1 class="left">定西贴吧</h1>						
                    </li>
                    <li class="channelItem clearfix" id="saleChannel">
						<span class="left"></span>
						<h1 class="left">定西网上商城</h1>					
                    </li>
                    <li class="channelItem clearfix" id="livingChannel">
						<span class="left"></span>
						<h1 class="left">定西生活</h1>						
                    </li>
                    <li class="channelItem clearfix" id="infosChannel">
						<span class="left"></span>
                        <h1 class="left">定西信息港</h1>
                    </li>
                    <li class="channelItem clearfix" id="doorChannel">
						<span class="left"></span>
                        <h1 class="left">定西上门服务</h1>
                    </li>
                </ul>
            </div>
            <div id="siteBar">
                <ul id="siteBar-itemList">
                    <li><a id="s-i-home" href="javascript:void(0);">主页</a></li>
                    <li><a id="s-i-login" class="needLogin" href="javascript:void(0);">登录</a></li>
                    <li><a id="s-i-back" href="javascript:void(0);">返回</a></li>
                    <li><a id="s-i-message" href="javascript:void(0);">站内信</a></li>
                    <li><a id="s-i-settings" class="side needLogin" data-dst="notChange" href="PopupPage/setting.aspx">设置</a></li>
                    <li><a id="s-i-ssl" href="javascript:void(0);" title="点击进入SSL安全加密模式">SSL</a></li>
                    <li><a id="s-i-groupJoin" href="javascript:void(0);">群：95396686<img border="0" src="Images/gp.gif" alt="点击这里加入此群" title="点击这里加入此群"></a></li>
                </ul>
            </div>
        </div>
        <div id="home" class="center">
            <ul id="home-boxes" class="clearfix">
                <li class="fullSizeBox needTip" data-tip="从今天起，关心天气粮食和蔬菜">
                    <div id="rightHeaderInfo" class="clearfix">
                        <div id="rightHeaderInfo_data">日期</div>
                        <div id="rightHeaderInfo_temp">温度</div>
                        <div id="rightHeaderInfo_weather">天气</div>
                        <div id="rightHeaderInfo_index">穿衣指数</div>
                        <div id="rightHeaderInfo_indexCo">舒适指数</div>
                    </div>
                </li>
                <li class="halfSizeBox needTip" data-tip="看新闻，星座"><a  id="h-b-item-qq" href="http://www.qq.com/" target="_blank">腾讯网</a></li>
				<li class="halfSizeBox needTip" data-tip="看新闻，网易新闻的评论很亮"><a  id="h-b-item-163" href="http://www.163.com/" target="_blank">网易新闻</a></li>
                <li class="fullSizeBox clearfix" id="h-b-item-forum">
                    <div id="h-b-i-f-count" class="left">
                        <%getForumInfo(); %>
                    </div>
                    <div id="h-b-i-f-detal" class="left">
                        <div id="h-b-i-f-d-forum">
                            <h2>正在发生：</h2>
                            <ul>
                                <%getForumTopicGet(); %>
                                <li><a class="jump" href="#" data-dst="Markets\forum\index.aspx">点此查看更多</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
				<li class="halfSizeBox needTip" data-tip="中国最大的微博之一，不解释"><a id="h-b-item-weibo" href="http://www.weibo.com/" target="_blank">新浪微博</a></li>
				<li class="halfSizeBox needTip" data-tip="中国最大的微博之一，不解释"><a id="h-b-item-QQweibo" href="http://t.qq.com/" target="_blank">腾讯微博</a></li>
				<li class="halfSizeBox needTip" data-tip="市长信箱，书记信箱"><a id="h-b-item-dingxi" href="http://www.dx.gansu.gov.cn/portal/zmhdl/index.htm" target="_blank">定西党政</a></li>
				<li class="halfSizeBox needTip" data-tip="最懂你的音乐电台"><a id="h-b-item-doubanFm" href="http://www.douban.fm/" target="_blank">豆瓣电台</a></li>
                <li class="halfSizeBox needTip" data-tip="扣扣空间，你懂的"><a id="h-b-item-qzone" href="http://www.Qzone.com/" target="_blank">QQ空间</a></li>
				<li class="halfSizeBox needTip" data-tip="网页版的QQ"><a id="h-b-item-webQQ" href="http://web.qq.com/" target="_blank">WebQQ</a></li>
				<li class="halfSizeBox needTip" data-tip="土豆的老板"><a id="h-b-item-youku" href="http://www.youku.com/" target="_blank">优酷</a></li>
				<li class="halfSizeBox needTip" data-tip="电视动漫，娱乐节目"><a id="h-b-item-tudou" href="http://www.tudou.com/" target="_blank">土豆</a></li>
                <li class="fullSizeBox" id="h-b-item-sale">
                    <ul>
                        <%getBigGoods(); %>
                    </ul>
                </li> 
				<li class="halfSizeBox needTip" data-tip="电视，电影，动漫"><a id="h-b-item-qy" href="http://www.iqiyi.com/" target="_blank">爱奇艺</a></li>
				<li class="halfSizeBox needTip" data-tip="低保，小额贷款，下岗证"><a id="h-b-item-dingxiRen" href="http://www.dxsrsj.gov.cn/" target="_blank">定西人力资源和社会保障局</a></li>
                <li class="fullSizeBox clearfix" id="h-b-item-infos">
                    <div id="h-b-i-i-shops" class="left">
                        <a href="#" class="jump" data-dst="Markets/Living/index.aspx">
                            <b>优惠、打折、点评</b><span data-number="<%shopCountGet(); %>"><%shopCountGet(); %></span>个定西的店铺。
                        </a>
                    </div>
                    <div id="h-b-i-i-infos" class="left">
                        <a href="#" class="jump" data-dst="Markets/Infos/index.aspx">
                            <b>招聘、二手、房产</b><span data-number="<%infoCountGet(); %>"><%infoCountGet(); %></span>条定西生活信息
                        </a>
                    </div>
                </li>
				<li class="halfSizeBox needTip" data-tip="各种小游戏"><a id="h-b-item-game" href="http://www.4399.com/" target="_blank">小游戏</a></li>
				<li class="halfSizeBox needTip" data-tip="高清MTV"><a id="h-b-item-mtv" href="http://www.yinyuetai.com/" target="_blank">音乐台</a></li>
				<li class="halfSizeBox needTip" data-tip="汽车违章信息查询"><a id="h-b-item-dingxiJJ" href="http://www.gsgajt.gov.cn:9999/InfoSerchs/Vehvioinfo.aspx" target="_blank">违章查询</a></li>
				<li class="halfSizeBox needTip" data-tip="不解释"><a id="h-b-item-train" href="http://www.12306.cn/mormhweb/" target="_blank">火车票</a></li>
				<li class="halfSizeBox needTip" data-tip="不解释"><a id="h-b-item-calender" href="http://app.baidu.com/app/enter?appid=114629" target="_blank">万年历</a></li>
				<li class="halfSizeBox needTip" data-tip="在线照片美化，修改"><a id="h-b-item-photo" href="http://xiuxiu.web.meitu.com/" target="_blank">照片美化</a></li> 
				<li class="halfSizeBox needTip" data-tip="护照、报警、身份证办理"><a id="h-b-item-dingxiGA" href="http://www.dxsgaj.gov.cn/" target="_blank">定西市公安局</a></li>
                <li class="halfSizeBox needTip" data-tip="定西上门服务"><a id="h-b-item-service" class="jump" data-dst="Markets/door/index.html" href="#" target="_blank">上门服务</a></li>
            </ul>
        </div>
        <div id="market" class="hide">
            <div id="market-status-bar" class="loading-big"></div>
            <div id="marketBox" class="hide">
                <div id="marketContent" class="center"></div>
                <div id="subForm" class="center hide">
                    <h2 class="channelTitle">请填写您的联系信息</h2>
                    <form action="#" id="order-form" method="Post" onSubmit="return false;">
                        <input type="hidden" name="formType" value="" id="formType"/>
                        <input type="hidden" name="gid" value="" id="order-gid" />
                        <ul id="formElm">
                            <li>
                                <div class="methodLine clearfix">
                                    <div class="ml-l">您的名字：</div>
                                    <div class="ml-c"><input type="text" name="order-name" autocomplete="off"/></div>
                                    <div class="ml-r"><span>*</span>个人请填姓名，公司填写公司名或联系人名</div>
                                </div>
                            </li>
                            <li>
                                <div class="methodLine clearfix">
                                    <div class="ml-l">您的电话：</div>
                                    <div class="ml-c"><input type="text" name="order-phone" autocomplete="off"/></div>
                                    <div class="ml-r"><span>*</span>最好填写手机号码，请尽量确保电话有人接听</div>
                                </div>
                            </li>
                            <li>
                                <div class="methodLine clearfix">
                                    <div class="ml-l">您的联系地址：</div>
                                    <div class="ml-c"><input type="text" name="order-address" autocomplete="off"/></div>
                                    <div class="ml-r"><span>*</span>请填写详细的联系地址</div>
                                </div>
                            </li>
                            <li>
                                <div class="methodLine clearfix">
                                    <div class="ml-l">您的会员号码(可不填)：</div>
                                    <div class="ml-c"><input type="text" value="000000000000" name="order-eid" autocomplete="off"/></div>
                                    <div class="ml-r">如果是平台会员，请重填此项，以便积分和取得优惠</div>
                                </div>
                            </li>
                            <li>
                                <div class="methodLine clearfix">
                                    <div class="ml-l">&nbsp;</div>
                                    <div class="ml-c"><button id="submitButton" type="submit">提交</button></div>
                                    <div class="ml-r">请确认填写项准确无误后再提交表单</div>
                                </div>
                            </li>
                        </ul>
                    </form>
                </div>
            </div>
        </div>
        <div id="footer">
                <a href="#" id="gotop" target="_self" title="回到顶部"></a>
                <div id="footerContent" class="clearfix">
					<div id="footerMark">爱 @ <span>沁辰左邻</span></div>
                    <ul id="footerC-QRIntroduce" class="left">
                        <li>在微信关注左邻</li>
                        <li>在掌中知晓身边 de 一切</li>
                        <li>在添加朋友里扫描二维码</li>
                        <li>或查找号码：ZuoLinSpeaker</li>
                        <li>左邻，一直都在你身边</li>
                    </ul>
                    <div id="footerC-QRHolder" class="left"></div>
                    <div id="footerC-about" class="left">
                        <p id="footerC-introduce">我们在努力做一些实实在在的事儿，希望通过左邻，能为大家的生活提供些实实在在的帮助和便利。TA应该就像原来我们住在平房时候你的邻居一样，甭管大事儿小情，喜怒哀乐，都在你身边。如果看到这儿你忍不住想和我们一起，做这样一件有意义的事情，你又足够靠谱，那就来吧~猛戳<a href="#">加入我们</a>。我们稀罕你！</p>
                        <ul class="clearfix">
                            <li><a href="#">关于我们</a></li>
                            <li><a href="#" class="jump" data-dst="Markets/forum/GroupTopicList.aspx?id=1">左邻每天在进步</a></li>
                            <li><a href="#">意见建议</a></li>
                            <li><a href="#">提供帮助</a></li>
                            <li><a href="#">广告投放</a></li>
                            <li><a href="#">联系我们</a></li>
                        </ul>
                        <p>Copyright © 2009-2012 Ai0932.cOm. All Rights Reserved</p>
                    </div>
                </div>
        </div>
        <div id="talkBar-holder" class="clearfix">
            <div id="th-guider" class="needTip left" data-tip="客官久等啦，我是如花，嘿嘿……"></div>
            <div id="th-talkBox" class="left">
                <input id="th-t-message" type="text" class="needTip" data-tip="都看见你了，来和大家打个招呼说说话吧"/>
                <span id="th-t-onlineNumber"></span>
            </div>
            <div id="th-messageHolder" class="left clearfix"></div>
            <div id="th-messageTpHolder" class="hide">
                <div class="th-message left [class] clearfix">
                    <div class="th-m-message">[message]<b></b></div>
                    <div class="th-m-by left">[by]</div>
                    <div class="th-m-time left">[time]</div>
                </div>
            </div>
        </div>
    </div>
    <div id="functions" class="hide">
        <div id="moyo-message" title=""><p></p></div>
        <div id="moyo-sideBar-holder">
            <div id="moyo-sideBar">
                <div class="m-s-content">
                    <div class="m-s-c-inner"></div>
                    <div class="m-s-close"><a href="javascript:void(0);">关闭</a></div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Script/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="Script/main.js?spm=9-18-2"></script>
    <script type="text/javascript" src="Script/jquery-ui/js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-33269870-1']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
<!--    
    <script type="text/javascript">
        var cjsscript = document.createElement('script');
        cjsscript.src = "Script/control.js";
        var cjssib = document.getElementsByTagName('script')[0];
        cjssib.parentNode.insertBefore(cjsscript, cjssib);
    </script>
-->
</body>
</html>