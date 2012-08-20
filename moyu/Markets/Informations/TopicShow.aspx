<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopicShow.aspx.cs" Inherits="moyu.Markets.Informations.TopicShow" %>
<h2 class="channelTitle"><%getName(); %> </h2>
<div id="topic_content" class="clearfix">
    <div id="t_topic_list" class="left">
        <div id="t_p_l_topic">
            <div id="t_p_l_t_content">
                <ul id="t_p_l_t_c_info" class="clearfix">
                    <%getInfo(); %>
                </ul>
                <%getContent(); %>
            </div>
            <div id="t_p_l_t_comments">
                <div id="t_p_l_t_c_tip">评论</div>
                <div id="t_p_l_t_c_list">
                    <%commentsGet(); %>
                </div>
                <div id="t_p_l_t_c_status"></div>
                <div id="t_p_l_t_c_new">
                    <div id="t_p_l_c_n_title">
                        <h3>我来说两句<%isNeedLogin(); %></h3>
                    </div>
                    <div id="t_p_l_c_n_content" class="clearfix">
                        <img class="userAvatar left" src="/Images/avatar.png"/>
                        <textarea id="t_p_l_t_comments left" name="message" cols="60" rows="6" maxlength="300" placeholder="请输入你的评论……"></textarea>
                    </div>
                    <p class="act"><span class="type_counts"><em>0</em> / 300</span><button type="submit" id="t_p_l_t_c_n_submit" data-tid="<%getTid(); %>">评论</button></p>                
                </div>
            </div>
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
    moyo.addLoginListen();
    moyo.Information.commentsNewListen();
</script>
