<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="game_royal.aspx.cs" Inherits="moyu.Mobile.game_royal" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>三宫六院_沁辰左邻</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> 
    <link type="text/css" rel="stylesheet" href="../Style/game_royal.css" />
    <%Server.Execute("script-loader.aspx"); %>
</head>
<body class="page">
    <header>
        <%Server.Execute("header.aspx"); %>
    </header>
    <section id="content">
        <div id="royalMap">
            <header class="clearfix">
                <span id="royalOwnerIcon" style="background:url(..<% getInfo("avatar"); %>) no-repeat;background-size:cover;"></span>
                <div id="royalIntro"><%getInfo("name"); %>的三宫六院</div>
            </header>
            <section id="highPalace">
                <ul class="highThree clearfix">
                    <li class="palace build" data-palace="1" data-status="0"></li>
                    <li class="palace needLive" data-palace="2" data-status="1"></li>
                    <li class="palace build" data-palace="3" data-status="0"></li>
                </ul>
            </section>
            <section id="lowPalace" class="clearfix">
                <ul class="lowSix leftSix clearfix">
                    <li class="palace build" data-palace="4" data-status="0"></li>
                    <li class="palace build" data-palace="5" data-status="0"></li>
                    <li class="palace build" data-palace="6" data-status="0"></li>
                    <li class="palace build" data-palace="7" data-status="0"></li>
                    <li class="palace build" data-palace="8" data-status="0"></li>
                    <li class="palace build" data-palace="9" data-status="0"></li>
                </ul>
                <ul class="lowSix rightSix clearfix">
                    <li class="palace build" data-palace="10" data-status="0"></li>
                    <li class="palace build" data-palace="11" data-status="0"></li>
                    <li class="palace build" data-palace="12" data-status="0"></li>
                    <li class="palace build" data-palace="13" data-status="0"></li>
                    <li class="palace needLive" data-palace="14" data-status="1"></li>
                    <li class="palace needLive" data-palace="15" data-status="1"></li>
                </ul>
            </section>
        </div>
    </section>
    <div id="royalFun" class="hide">
        <div id="needBuild"><div class="needBuild">待建</div></div>
    </div>
    <footer id="pageFooter">
        <%Server.Execute("footer.aspx"); %>
    </footer>
    <script src="../Script/game_royal.js"></script>
</body>
</html>
