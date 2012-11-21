<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="routes.aspx.cs" Inherits="moyu.routes" %>
<!DOCTYPE html>
<!--[if lt IE 7 ]> <html class="ie ie6"> <![endif]-->
<!--[if IE 7 ]>    <html class="ie ie7"> <![endif]-->
<!--[if IE 8 ]>    <html class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <html class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--><html lang="zh-CN" xmlns="http://www.w3.org/1999/xhtml"><!--<![endif]-->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%getTitle(); %></title>
    <meta name="description" content="<%getTitle(); %>" />
    <meta name="keywords" content="定西,<%getKeyWords(); %>" />
    <link rel="shortcut icon" href="<%deepFix(); %>images/favicon.ico" />
    <link rel="Stylesheet" href="<%deepFix(); %>Style/main.css?spm=9-18-4" />
    <link rel="Stylesheet" href="<%deepFix(); %>Script/jquery-ui/css/flick/jquery-ui-1.8.23.custom.css"/>
    <script type="text/javascript" src="<%deepFix(); %>Script/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="<%deepFix(); %>Script/jquery-ui/js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="<%deepFix(); %>Script/main.js?spm=9-18-2"></script>
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
                        <h1 class="left"><a href="http://www.ai0932.com/">首页</a></h1>
                    </li>
                    <li class="channelItem clearfix" id="forumChannel">
						<span class="left"></span>
						<h1 class="left"><a href="/定西吧_沁辰左邻/Markets---forum---index@aspx">定西贴吧</a></h1>						
                    </li>
                    <li class="channelItem clearfix" id="saleChannel">
						<span class="left"></span>
						<h1 class="left"><a href="/定西网上商城_沁辰左邻/Markets---sale---index@aspx">定西网上商城</a></h1>					
                    </li>
                    <li class="channelItem clearfix" id="livingChannel">
						<span class="left"></span>
						<h1 class="left"><a href="/定西生活_沁辰左邻/Markets---Living---index@aspx">定西生活</a></h1>						
                    </li>
                    <li class="channelItem clearfix" id="infosChannel">
						<span class="left"></span>
                        <h1 class="left"><a href="/定西信息港_沁辰左邻/Markets---Infos---index@aspx">定西信息港</a></h1>
                    </li>
                    <li class="channelItem clearfix" id="doorChannel">
						<span class="left"></span>
                        <h1 class="left"><a href="/定西上门服务_沁辰左邻/Markets---door---index@html">定西上门服务</a></h1>
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
        <div id="market">
            <div id="market-status-bar" class="loading-big hide"></div>
            <div id="marketBox">
                <div id="marketContent" class="center">
					<%getContent(); %>
				</div>
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
                            <li><a href="#" class="jump" data-dst="Markets/forum/TopicList.aspx?cid=11&name=意见建议">意见建议</a></li>
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