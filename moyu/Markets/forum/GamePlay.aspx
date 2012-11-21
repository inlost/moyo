<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamePlay.aspx.cs" Inherits="moyu.Markets.forum.GamePlay" %>
<h2 class="channelTitle">游戏盒子 <span>GameBox-Forum</span></h2>
<div id="forumGameContent" class="clearfix">
    <div id="f-hotGames">
        <h3>热门游戏</h3>
        <ul class="clearfix">
            <%getHotGames(); %>
        </ul>
    </div>
    <div id="f-gameHolder" class="left">
        <div id="f-gameHolder-catList">
            <h3>游戏列表</h3>
            <ul class="clearfix gameCatList">
                <%listCat(); %>
            </ul>
            <ul class="clearfix gameList">
                <%listGames(); %>
            </ul>
        </div>
        <div class="loadMore"><a href="/游戏吧_定西吧_沁辰左邻/Markets---forum---GamePlay@aspx/cid=8&name=游戏盒子&gameCat=<%getCat(); %>&gameLast=<%getLast(); %>" class="gameJump" data-cat="0" data-last="<%getLast(); %>">换一批</a></div>
    </div>
    <div id="f-gameTopic" class="left">
        <h3 id="t_f_b_new" class="clearfix"><span class="left">帖子列表</span><a class="needLogin right" id="t_f_b_newTopic" data-dst="Markets/forum/NewTopic.aspx?cid=8&amp;name=游戏盒子" href="javascript:void(0);">发表新帖子</a></h3>
        <ul>
            <%getTopicList(); %>
        </ul>
    </div>
</div>
<script>
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.Information.gameLoaderInit();
</script>