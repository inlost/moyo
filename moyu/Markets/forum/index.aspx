<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Informations.index" %>
<h1 class="channelTitle">定西贴吧  <span>Forum</span></h1>
<ul id="ifMk-serviceList" class="channelServiceList clearfix">
    <li id="ifMk-tb-kids">
        <a data-dst="Markets/forum/TopicList.aspx?cid=1&name=少年吧" class="jump" href="/少年吧_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=1&name=少年吧">
            <h3>少年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-young">
        <a data-dst="Markets/forum/TopicList.aspx?cid=2&name=青年吧" class="jump" href="/青年吧_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=2&name=青年吧">
            <h3>青年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-person">
        <a data-dst="Markets/forum/TopicList.aspx?cid=3&name=中年吧" class="jump" href="/中年吧_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=3&name=中年吧">
            <h3>中年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-software">
        <a data-dst="Markets/forum/TopicList.aspx?cid=7&name=软件街" class="jump" href="/软件街_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=7&name=软件街">
            <h3>软件街</h3>
        </a>
    </li>
    <li id="ifMk-tb-games">
        <a data-dst="Markets/forum/GamePlay.aspx?cid=8&name=游戏盒子" class="jump" href="/游戏盒子_定西吧_沁辰左邻/Markets---forum---GamePlay@aspx/cid=8&name=游戏盒子">
            <h3>游戏盒子</h3>
        </a>
    </li>
    <li id="ifMk-tb-kitchen">
        <a data-dst="Markets/forum/TopicList.aspx?cid=9&name=掌上贴吧" class="jump" href="/掌上贴吧_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=9&name=掌上贴吧">
            <h3>掌上贴吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-phone">
        <a data-dst="Markets/forum/TopicList.aspx?cid=10&name=手机数码" class="jump" href="/手机数码_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=10&name=手机数码">
            <h3>手机数码</h3>
        </a>
    </li>
    <li id="ifMk-tb-fadeBack">
        <a data-dst="Markets/forum/TopicList.aspx?cid=11&name=意见建议" class="jump" href="/意见建议_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=11&name=意见建议">
            <h3>意见建议</h3>
        </a>
    </li>
    <li id="ifMk-tb-news">
        <a data-dst="Markets/forum/TopicList.aspx?cid=12&name=定西新闻" class="jump" href="/定西新闻_定西吧_沁辰左邻/Markets---forum---TopicList@aspx/cid=12&name=定西新闻">
            <h3>定西新闻</h3>
        </a>
    </li>
</ul>
<h2 class="channelTitle">定西圈子  <span>Group</span></h2>
<ul id="ifMk-groupList" class="clearfix">
    <%getGroupList(); %>
    <li class="ifMk-g-newGroup left">
        <a class="side needLogin" data-dst="notChange" href="../../PopupPage/Group_new.html">
            申请建立一个圈子
        </a>
    </li>
</ul>
<script>
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.popSideBar();
</script>