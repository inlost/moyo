<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="moyu.Markets.Informations.index" %>
<h2 class="channelTitle">贴吧  <span>Forum</span></h2>
<ul id="ifMk-serviceList" class="channelServiceList clearfix">
    <li id="ifMk-tb-kids">
        <a data-dst="Markets/forum/TopicList.aspx?cid=1&name=孩子" class="jump" href="javascript:void(0);">
            <h3>少年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-young">
        <a data-dst="Markets/forum/TopicList.aspx?cid=2&name=青年" class="jump" href="javascript:void(0);">
            <h3>青年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-person">
        <a data-dst="Markets/forum/TopicList.aspx?cid=3&name=中年" class="jump" href="javascript:void(0);">
            <h3>中年吧</h3>
        </a>
    </li>
    <li id="ifMk-tb-software">
        <a data-dst="Markets/forum/TopicList.aspx?cid=7&name=软件街" class="jump" href="javascript:void(0);">
            <h3>软件街</h3>
        </a>
    </li>
    <li id="ifMk-tb-games">
        <a data-dst="Markets/forum/TopicList.aspx?cid=8&name=游戏盒子" class="jump" href="javascript:void(0);">
            <h3>游戏盒子</h3>
        </a>
    </li>
    <li id="ifMk-tb-kitchen">
        <a data-dst="Markets/forum/TopicList.aspx?cid=9&name=下厨房" class="jump" href="javascript:void(0);">
            <h3>下厨房</h3>
        </a>
    </li>
</ul>
<h2 class="channelTitle">圈子  <span>Group</span></h2>
<ul id="ifMk-groupList" class="clearfix">
    <%getGroupList(); %>
    <li class="ifMk-g-newGroup left">
        <a class="side needLogin" data-dst="notChange" href="../../PopupPage/Group_new.html">
            申请建立一个圈子
        </a>
    </li>
</ul>
<script>
    var moyo = new Moyo();
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.popSideBar();
</script>