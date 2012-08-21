<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicList.aspx.cs" Inherits="moyu.Markets.Informations.TopicList" %>
<h2 class="channelTitle"><%getName(); %> <span>Forum</span><%showLogin(); %></h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <h3>帖子列表---<a data-dst="Markets/Informations/NewTopic.aspx?<%getPar(); %>" class="<%isNeedLogin(); %>" href="javascript:void(0);">发表新帖子</a></h3>
        <table data-cid="<%getCid(); %>">
            <thead>
                <tr>
                    <th id="t_t_l_table_title">标题</th>
                    <th id="t_t_l_table_postBy">作者</th>
                    <th id="t_t_l_table_view">浏览</th>
                    <th id="t_t_l_table_reply">回应</th>
                    <th id="t_t_l_table_lastUpdate">最后回应</th>
                </tr>
            </thead>
            <tbody>
                <%getTopicList(); %>
            </tbody>
        </table>
<%--
        <ul class="pageList clearfix">
            <%getPages(); %>
        </ul>
--%>
        <div id="getMoreTopic">
            <%getMoreLink(); %>
        </div>
    </div>
    <div id="t_function_bar" class="left">
        <h3>板块列表</h3>
        <table id="forum_list">
            <thead>
                <tr>
                    <th class="forum"></th>
                    <th class="topics">帖子数</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <a href="javascript:void(0);" data-dst="Markets/Informations/TopicList.aspx?cid=1&name=孩子" class="jump">
                            孩子
                            <div>中学生们谈天灌水的地方</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(1); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0); " data-dst="Markets/Informations/TopicList.aspx?cid=2&name=青年" class="jump">
                            青年
                            <div>大江东去浪淘尽</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(2); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0);" data-dst="Markets/Informations/TopicList.aspx?cid=3&name=中年" class="jump">
                            中年
                            <div>风流人物</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(3); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0);" data-dst="Markets/Informations/TopicList.aspx?cid=7&name=软件街" class="jump">
                            软件街
                            <div>新奇实用的集中营</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(7); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0);" data-dst="Markets/Informations/TopicList.aspx?cid=8&name=游戏盒子" class="jump">
                            游戏盒子
                            <div>最好玩、最休闲</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(8); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0);" data-dst="Markets/Informations/TopicList.aspx?cid=9&name=下厨房" class="jump">
                            下厨房
                            <div>无需付出的岁月并不是真正的生活</div>
                        </a>
                    </td>
                    <td>
                        <%getTopicCount(9); %>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<script>
    var moyo = new Moyo();
    moyo.addPageJump();
    moyo.Information.getMoreTopic();
    moyo.addLoginListen();
</script>