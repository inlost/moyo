var moyoHistory=[];
Moyo=function(){
    this.spm="?spm=08-29";
    var statusBar=$("#market-status-bar"),
        marketBox=$("#marketBox"),
        channelContent=$("#marketContent").parent();
    this.showStatus=ShowStatus;//显示状态条
    this.addHistory=function(url){
        if(!moyoHistory.length){
            moyoHistory.push(0);
        }
        if(url.length){
            moyoHistory.push(url);
        }
    };
    function ShowStatus(msg){
        statusBar.html("<h3 class='center'>"+msg+"</h3>");
        marketBox.hide();
        statusBar.slideDown();
    }
    this.hideStatus=HideStatus;//影藏状态条
    function HideStatus(showContent){
        statusBar.slideUp(function(){
            if(showContent){
                channelContent.fadeIn();
            }
        });
    };
    //为连接添加跳转事件
    this.addPageJump=function(elm){
        var jumpLinks=(arguments.length==0?$(".jump"):elm);
        jumpLinks.off();
        jumpLinks.on("click",function(){
            moyo.home.hideFirstHome();
            ShowStatus("载入中");
            var url=encodeURI($(this).attr("data-dst"));
            $.ajax({
                type:"GET",
                url:"../"+url,
                success:function(msg){
                    moyo.addHistory(url);
                    $("#marketContent").html(msg);
                    HideStatus(true);
                },error:function(e){
                    HideStatus(true);
                }
            });
            return false;
        });
    };
    //直接跳转
    this.jumpTo=function(url){
        ShowStatus("载入中");
        $.ajax({
            type:"GET",
            url:"../"+url,
            success:function(msg){
                $("#marketContent").html(msg);
                HideStatus(true);
            },error:function(e){
                HideStatus(true);
            }
        });
    },
    //跳回上一页
    this.jumpBack=function(){
        if(moyoHistory.length==2){
            location.reload();
        }
        var url="";
        url=moyoHistory.splice(moyoHistory.length-2,2)[0];
        moyo.addHistory(url);
        moyo.jumpTo(url);
    },
    //通用弹出对话框
    this.popMessage=function(title,message){
        var msgBox=$("#moyo-message"),
            msgContentHolder=msgBox.children("p");
        msgBox.attr("title",title);
        msgContentHolder.html(message);
        msgBox.dialog({modal: true,buttons:{
            关闭:function(){
                $(this).dialog("close");
            }
        }});
    };
    //通用弹出层
    this.popDiv=function(title,message){
        var msgBox=$("#moyo-message"),
            msgContentHolder=msgBox.children("p");
        msgBox.attr("title",title);
        msgContentHolder.html(message);
        return msgBox;
    };
    //加载CSS
    this.loadCss=function(file){
      var cssTag=document.getElementById('loadCss'),
　　    head=document.getElementsByTagName('head').item(0);
　　  if(cssTag)head.removeChild(cssTag);
        css = document.createElement('link');
        css.href = file;
　　  css.rel='stylesheet';
　　  css.type='text/css';
　　  css.id='loadCss';
　　  head.appendChild(css);
    };
    //弹出详细
    this.popSideBar=function(){
        var sideBarHolder=$("#moyo-sideBar-holder");
        var sideShow=$(".side");
        sideShow.on("click",function(){
            if($(this).hasClass("needLogin")){return false;}
            var url="../"+$(this).attr("href");
            if(document.getElementById("moyo-sideBar-clone")){
                $("#moyo-sideBar-clone").remove();
            }
            var sideBar=sideBarHolder.html().replace(/moyo-sideBar/,"moyo-sideBar-clone");
            $("#layout").append(sideBar);
            sideBar=$("#moyo-sideBar-clone");
            sideBar.animate({width:$(window).width()/2},260,function(){
                $.ajax({
                    type:"GET",
                    url:url,
                    success:function(msg){
                        $("#moyo-sideBar-clone .m-s-c-inner").html(msg);
                    }
                });
            });
            $(".m-s-content").css({
                height:$(window).height(),
                "margin-top":$(document).scrollTop()-($(document).scrollTop()>70?100:0)
            });
            var scrollFunc=function(e){
                var e = e || window.event,
                    upDown="";
                if(e.preventDefault) {
                    // Firefox,Chrome
                    if($.browser.mozilla){
                        upDown=e.detail>0?"down":"up";
                    }else{
                        upDown=event.wheelDelta>0?"up":"down";
                    }
                    e.preventDefault();
                    e.stopPropagation();
                } else {
                    // IE
                    upDown=event.wheelDelta>0?"up":"down";
                    e.cancelBubble=true;
                    e.returnValue = false;
                }
                var content=$("#moyo-sideBar-clone .m-s-content"),
                    close=$("#moyo-sideBar-clone .m-s-close");
                if(upDown=="down"){
                   content.css("margin-top",parseInt(content.css("margin-top"))-60);
                   close.css("top",parseInt(close.css("top"))+60);
                }else{
                   content.css("margin-top",parseInt(content.css("margin-top"))+60);
                   close.css("top",parseInt(close.css("top"))-60);
                }
            }
            $(".page").bind("mousewheel",scrollFunc);
            if(document.addEventListener && $.browser.mozilla)
            {
                document.addEventListener("DOMMouseScroll",scrollFunc,false);
            }
            sideBar.children(".m-s-content").children(".m-s-close").on("click",function(){
                $(".page").off("mousewheel");
                if($.browser.mozilla){
                    document.removeEventListener("DOMMouseScroll",scrollFunc,false);
                }
                sideBar.animate({width:0},260,function(){
                    $(this).remove();
                });
            });
            return false;
        });
    };
    this.addGotoTopListen=function(){
        $(window).scroll(function () { //回到顶部
            var windowHeight = $(window).height();
            var scrollTop = $(window).scrollTop();
            if (scrollTop > 500) {
                $("#gotop").fadeIn("slow");
            } else {
                $("#gotop").fadeOut("fast");
            }
        });
    };
    this.addHoverClass=function(selector){
        selector.hover(function(){
            $(this).addClass("hover");
        },function(){
            $(this).removeClass("hover");
        });
    };
    //登录注册
    this.addLoginListen=function(){
        var isLogined=false,
            niceName="",
            loginActior=$(".needLogin");
        $.ajax({
            url:"../Services/user.ashx",
            type:"POST",
            data:{action:"isLogined"},
            success:function(msg){
                if(msg!="false"){
                    isLogined=true;
                    niceName=msg;
                    loginActior.each(function(){
                        var theLink=$(this);
                        theLink.removeClass("needLogin");
                        if(!theLink.attr("data-dst")){
                            theLink.html(niceName);
                            return false;
                        }
                        if(theLink.attr("data-dst")=="notChange"){
                            return false;
                        }
                        theLink.addClass("jump");
                    });
                    moyo.addPageJump();
                }
            }
        });
        if(loginActior.length==0){return false;}
        loginActior.on("click",function(){
            var loginBt=$(this);
            if(!loginBt.hasClass("needLogin")){return;}
            $.ajax({
                url:"login.html",
                type:"get",
                success:function(msg){
                    var dlg=moyo.popDiv("用户登录",msg);
                    dlg.dialog({
                        modal: true,
                        width:600
                    });
                    moyo.addHoverClass($("#loginForm .textInput"));
                    moyo.addRegListen();
                    $("#loginForm-i-login").on("click",function(){
                        var uid=$("#loginForm input[name=loginForm-i-uid]").val();
                        var password=$("#loginForm input[name=loginForm-i-password]").val();
                        if(uid.length==0){
                            alert("请填写用户名");
                            return false;
                        }
                        if(password.length==0){
                            alert("请填写密码");
                            return false;
                        }
                        $("#loginForm ul").hide();
                        $("#loginForm .statusBar").addClass("loading-big");
                        moyo.userLogin(uid,password,function(msg){
                            if(msg=="True"){
                                dlg.dialog("close");
                                loginActior.each(function(){
                                    if(!$(this).attr("data-dst")){
                                        $(this).html(uid).removeClass("needLogin");
                                    }else{
                                        $(this).removeClass("needLogin");
                                        if($(this).attr("data-dst")!="notChange"){
                                            $(this).addClass("jump");
                                        }
                                    }
                                });
                                moyo.addPageJump();
                                loginBt.click();
                            }else{
                                $("#loginForm .statusBar").removeClass("loading-big");
                                alert("登录失败,请检查用户名或密码是否正确");
                                $("#loginForm ul").show();
                            }
                        });
                    });
                }
            });
        });
    };
    // input提示效果
    this.inputTip=function (elm) {
        if($(".ucinformation").length==0){
            $("body").prepend("<div class='ucinformation'><div class='ifBox'><div class='ifBoxCont'><div class='ifBoxTxt'></div></div><div class='boxSjOne'>◆</div></div></div>");
        }
        var tipElm=elm?elm:$("input");
        tipElm.on("hover", function () {
            if ($(this).attr("data-tip") && $(this).attr("data-tip") != "") { // input必须要有tip类型且要有内容
                var iFarg = $(this).attr("data-tip");
                var it = $(this).offset().top;
                var il = $(this).offset().left;
                $(".ucinformation .ifBoxTxt").html(iFarg);
                var ih = $(".ucinformation").height();
                $(".ucinformation").css({ "left": il, "top": it - ih - ($.browser.msie?1:100)});
                $(".ucinformation").addClass("informationBlock");

                $(this).bind("mouseout", function () {
                    $(".ucinformation").removeClass("informationBlock");

                });
                $(this).bind("keyup", function () {
                    $(".ucinformation").removeClass("informationBlock");

                });
            } else {
                $(this).off("hover").off("mouseout").off("keyup");
            }
        });
    };
    //注册
    this.addRegListen=function(){
        var regLink=$("#loginForm-i-reg");
        regLink.on("click",function(){
            $.ajax({
                url:"reg.html",
                type:"GET",
                success:function(msg){
                    var dlg=moyo.popDiv("轻松注册",msg);
                    dlg.dialog({
                        modal: true,
                        width:700
                    });
                    moyo.addHoverClass($("#loginForm .textInput"));
                    moyo.inputTip();
                    moyo.addLoginListen();
                    $("#loginForm-r-reg").on("click",function(){
                        var postData={
                            action:"reg",
                            niceName:$("#loginForm input[name=loginForm-i-uid]").val(),
                            realName:$("#loginForm input[name=loginForm-i-name]").val(),
                            sex:$("#loginForm input[name=sex]:checked").val(),
                            Birth:$("#loginForm input[name=birth]:checked").val(),
                            email:$("#loginForm input[name=loginForm-i-QQ]").val()+"@qq.com",
                            phone:$("#loginForm input[name=loginForm-i-phone]").val(),
                            password:$("#loginForm input[name=loginForm-i-password]").val()
                        };
                        for(var i in postData)
                        {
                            if(postData[i].length==0)
                            {
                                alert("有没有填写的项目，请填写完整后再提交");
                                return false;
                            }
                        }
                        if(postData.niceName.length>8||postData.niceName.length<2){
                            alert("用户名长度不合适么，应该是2-8位汉字或者英文");
                            return false;
                        }
                        if(postData.realName.length<2||postData.realName.length>6)
                        {
                            alert("请填写真实姓名");
                            return false;
                        }
                        if(isNaN($("#loginForm input[name=loginForm-i-QQ]").val())){
                            alert("请填写真实的QQ");
                            return false;
                        }
                        if(isNaN(postData.phone)||postData.phone.length!=11){
                            alert("请填写真实的手机号码");
                            return false;
                        }
                        if(postData.password!=$("#loginForm input[name=loginForm-i-repassword]").val()){
                            alert("两次输入的密码不一致，请重新输入");
                            return false;
                        }
                        $("#loginForm ul").hide();
                        $("#loginForm .statusBar").addClass("loading-big");
                        $.ajax({
                            url:"../Services/User.ashx",
                            type:"POST",
                            data:postData,
                            success:function(msg){
                                if(msg=="True"){
                                    dlg.dialog("close");
                                }else{
                                    $("#loginForm .statusBar").removeClass("loading-big");
                                    alert("用户名已经被别人占用啦！换一个吧");
                                    $("#loginForm ul").show();
                                }
                            }
                        });
                    });
                }
            });
            return false;
        });
    };
    //登录ing
    this.userLogin=function(uid,password,callBack){
        $.ajax({
            url:"../Services/User.ashx",
            type:"POST",
            data:{action:"login",uid:uid,password:password},
            success:function(msg){
                callBack(msg);
            }
        });
    };
    this.waitTip=function(){
        $(".wait").on("click",function(){
            moyo.popMessage("稍安勿躁","这个频道目前还没有开放，但是马上就要开放了.敬请期待哦亲~");
            return false;
        });
    };
};
Moyo.prototype.home={
    wheaterGet:function(){
        var date=new Date();
        $.ajax({
            url:"../Services/other.ashx?d="+date.getDate(),
            type:"POST",
            data:{action:"weather"},
            success:function(data){
                var dataJson=eval("("+data+")").weatherinfo;
                $("#rightHeaderInfo_data").html(dataJson.date_y+" "+dataJson.week);
                $("#rightHeaderInfo_temp").html(dataJson.temp1);
                $("#rightHeaderInfo_weather").html(dataJson.weather1);
                $("#rightHeaderInfo_index").html(dataJson.index_d);
                $("#rightHeaderInfo_indexCo").html("舒适指数："+dataJson.index_co);
            }
        });
    },
    hideFirstHome:function(){
        var splitBar=$("#splitBar");
        var home=$("#home");
        var market=$("#market");
        home.slideUp("normal").remove();
        splitBar.css("display","none").remove();
        market.removeClass("hide");
    },
    channelClick:function(){
        var channels=$(".channelItem");
        var channelContent=$("#marketContent");
        channels.on("click",function(){
            var nowChannel=$(this);
            if(nowChannel.attr("id")=="logo"||nowChannel.attr("id")=="homeChannel"){
                location.reload();
                return;
            }
            channels.removeClass("activeChannel");
            nowChannel.addClass("activeChannel");
            var moyo=new Moyo();
            moyo.home.hideFirstHome();
            moyo.showStatus("载入中……");
            moyo.addHistory(getUrl(nowChannel));
            $.ajax({
                url:getUrl(nowChannel),
                type:"GET",
                success:function(msg){
                    channelContent.html(msg);
                    moyo.hideStatus(true);
                },error:function(e){
                    console.log(e);
                }
            });
            $(window).off("scroll");
            moyo.addGotoTopListen();
            moyo.home.switchToContent();
            return false;
        });
        function getUrl(channelElm)
        {
            var url="";
            switch(channelElm.attr("id"))
            {
                case "doorChannel":
                    url= "../Markets/door/index.html";
                    break;
                case "infosChannel":
                    url= "../Markets/infos/index.aspx";
                    break;
                case "saleChannel":
                    url= "../Markets/sale/index.aspx";
                    break;
                case "forumChannel":
                    url= "../Markets/forum/index.aspx";
                    break;
                case "livingChannel":
                    url= "../Markets/Living/index.aspx";
                    break;
            }
            return url+moyo.spm;
        }
    },
    switchToContent:function(){
        if($("#marketContent").hasClass("marketContentOnSubmit")){
            var content=$("#marketContent");
            var subForm=$("#subForm");
            subForm.fadeOut("slow",function(){
                content.removeClass("marketContentOnSubmit").animate({left:"0"},"normal");
            }).hide();
        }
    },
    //切换到订单页面
    //formfor 订单名称 data 附加数据
    switchToSubForm:function(formFor,data){
        var content=$("#marketContent");
        var subForm=$("#subForm");
        var formType=$("#formType");
        var orderGid=$("#order-gid");
        content.addClass("marketContentOnSubmit").animate({left:"-3000"},"normal",function(){
            subForm.fadeIn("slow");
            formType.val(formFor);
            if(!data){
                data=0;
            }
            orderGid.val(data);
        });
        var submitButton=$("#submitButton");
        submitButton.on("click",function(){
            var inputs=$("#formElm input");
            inputs.each(function(){
                var value=$(this).val();
                if(value.length==0){
                    $(this).css("background-color","red");
                    moyo.popMessage("提示","您的表单有没有填写的项目，请全部填写后重新提交");
                    return false;
                }else{
                    $(this).css("background-color","white");
                }
            });
            var postData={
                action:"add",
                title:$("#formType").val(),
                gid:$("#order-gid").val(),
                name:$("input[name=order-name]").val(),
                phone:$("input[name=order-phone]").val(),
                address:$("input[name=order-address]").val(),
                eid:$("input[name=order-eid]").val()
            };
            if(postData.eid==""){
                postData.eid=00000000000;
            }
            $.ajax({
                type:"POST",
                url:"../Services/Sale_Orders.ashx",
                data:postData,
                success:function(msg){
                    if(msg=="ok"){
                        moyo.popMessage("恭喜","提交成功，我们的工作人员会尽快与您取得联系。");
                    }else{
                        moyo.popMessage("出错啦","提交失败，请检查所输入的信息是否正确，如果您确定信息无误但无法提交，请致电：0932-8366225。");
                    }
                    moyo.home.switchToContent();
                }
            });
            return false;
        });
    },
    mallGoodFloat:function(){
        var holder=$("#h-b-item-sale ul");
        var iId=0;
        var start=function(){
            iId=setInterval(function(){
                if(!document.getElementById("h-b-item-sale")){return false;};
                var nowShow=$("#h-b-item-sale li:first");
                nowShow.fadeOut("slow",function(){
                    holder.append(nowShow);
                    $("#h-b-item-sale li:first").fadeIn();
                });
            },3000);
        }
        start();
        holder.hover(function(){
            clearInterval(iId);
        },start);
    },
    infoNumberAdd:function(){
        var numbers=$("#h-b-item-infos span");
        numbers.each(function(){
            var nowHolder=$(this);
            var nowNumber=0;
            var realNumber=Number(nowHolder.attr("data-number"));
            setInterval(function(){
                if(!document.getElementById("h-b-item-infos")){return false;};
                nowHolder.html(0);
                var iId=setInterval(function(){
                    nowNumber=Number(nowHolder.html());
                    if(nowNumber>=realNumber){
                        clearInterval(iId);
                    }else{
                        nowHolder.html(nowNumber+1);
                    }
                },1300/realNumber);
            },3800);
        });
    }
};
Moyo.prototype.setting={
    avatarUpload:function(){
        var inputFile=$("#setting-c-h-avatar input[type=file]");
        var imgElm=$("#setting-c-h-a-bigShow img");
        var saveButton=$("#setting-c-h-avatar button");
        inputFile.on("change",function(){
            imgElm.attr("src","images/loading-big.gif");
            saveButton.attr("disabled","disabled");
            $("#setting-c-h-avatar form").submit();
        });
    },
    avatarUploadBack:function(url){
        var areaPoint={x1:60,y1:60,x2:280,y2:280};
        if(url==0){
            moyo.popMessage("啊哦","上传失败了");
        }else{
            var imgElm=$("#setting-c-h-a-bigShow img");
            var saveButton=$("#setting-c-h-avatar button");
            imgElm.attr("src",url);
            imgElm.imgAreaSelect({
                aspectRatio:'4:4',
                handles: true,
                show:true,
                x1: 60, y1: 60, x2: 280, y2: 280,
                parent:$("#setting-c-h-a-bigShow"),
                onSelectEnd:function(img,selection){
                    areaPoint=selection;
                    areaPoint.img=url;
                    areaPoint.action="avatarUpBack";
                    saveButton.removeAttr("disabled");
                }
            });
            saveButton.on("click",function(){
                $.ajax({
                    url:"../Services/User.ashx",
                    type:"POST",
                    data:areaPoint,
                    success:function(msg){
                        alert(msg);
                    }
                });
            });
        }
    }
};
Moyo.prototype.computer={
    bindSubmit:function(){
        var submitButton=$(".submitForm");
        submitButton.on("click",function(){
            $(".m-s-close").click();
            var nowClick=$(this);
            moyo.home.switchToSubForm(nowClick.attr("data-for"));
            return false;
        });
    }
};
Moyo.prototype.ecard={
    bindSchoolLogin:function(){
        var loginButton=$("#ecSchoolMk-serviceList .e-s-login");
        var loginForm=$("#eCard-login-form");
        var permission=$("#eCard-login-form input[name=permission]");
        loginButton.on("click",function(){
            //登录入口点击
            var nowClick=$(this);
            if(nowClick.hasClass("selected")){
                loginForm.fadeOut("fast");
                loginButton.removeClass("selected");
                return false;
            }
            var inputs=$(".ec-l-f-r input[type=text],.ec-l-f-r input[type=password]");
            inputs.val("");
            permission.val(nowClick.attr("data-permission"));
            if(permission.val()=="2"){
                $("#safeKey").css("display","none");
            }else{
                $("#safeKey").css("display","");
            }
            loginButton.removeClass("selected");
            $(this).addClass("selected");
            var holder=$(this).parent();
            loginForm.fadeOut("fast",function(){
                loginForm.appendTo(holder);
                loginForm.fadeIn("fast");
            });
            return false;
        });
        loginForm.children("form").on("submit",function(){
            //表单提交
            var moyo=new Moyo();
            moyo.showStatus("正在登录……");
            submitData={
                action:"login",
                id:$("#eCard-login-form input[name=uid]").val(),
                key:$("#eCard-login-form input[name=password]").val(),
                permission:permission.val()
            };
            $.ajax({
                dataType:"json",
                url:"../Services/School_User.ashx",
                type:"POST",
                data:submitData,
                success:function(msg){
                    if(msg.rst){
                        moyo.showStatus("登录成功，请稍后……");
                        $.ajax({
                            type:"GET",
                            url:"../Markets/Ecard/school/teacher_home.aspx",
                            success:function(msg){
                               $("#marketContent").html(msg);
                               moyo.hideStatus(true);
                            }
                        });
                    }else{
                        moyo.hideStatus(true);
                        $("#ecard-loginFail").dialog({"height":"170",modal: true,buttons:{
                            确定:function(){
                                $(this).dialog("close");
                            }
                        }});
                    }
                }
            });
            return false;
        });
    },
    addExchangeListen:function(){
        var exchangeBtn=$(".mC-e-g-item button");
        exchangeBtn.on("click",function(){
            var gid=$(this).attr("data-gid");
            $.ajax({
                url:"../Services/Union.ashx?r="+Math.random(),
                type:"POST",
                data:{action:"userExchangeAdd",gid:gid},
                success:function(msg){
                    if(msg=="True"){
                        moyo.popMessage("恭喜","兑换成功！");
                        moyo.jumpTo("Markets/Ecard/live/exchange.aspx?r="+Math.random());
                    }else{
                        moyo.popMessage("错误","您的积分不足以兑换这件礼品");
                    }
                }
            });
        });
    }
};
Moyo.prototype.school={
    teacherDesktopSortabale:function(){
        var sortBox=$("#schoolEcard-c-functionBox");
        sortBox.sortable().disableSelection();
    },
    teacherDocumentInit:function(){
        var newDocArchAddListen=function(){
            var newArch=$("#schoolEcard-c-n-s-architecture");
            var newArchType=$("#schoolEcard-c-n-s-architecture_type");
            var newArchPreElm=$("#schoolEcard-c-n-architectureAddBox").parent();
            var newArchAddButton=$("#schoolEcard-c-n-s-architecture-add");
            newArchAddButton.on("click",function(){
                if(newArch.val()=="-1"){
                    moyo.popMessage("提示","没有可以添加的构架");
                    return;
                }
                if(newArch.children("option").length==0){
                    moyo.popMessage("提示","没有能添加的构架了。");    
                    return;
                }
                var optionArch=newArch.children("option").length==1?newArch.children("option"):newArch.children("option[selected]");
                console.log(optionArch.text());
                var newArchLi="<li data-value=\""+newArch.val()+"\" data-type=\""+newArchType.val()+"\" class=\"schoolEcard-c-n-s-archs\">"+optionArch.text()+"</li>"
                optionArch.remove();
                newArchPreElm.before(newArchLi);
            });
        };
        var addDocPostNewListen=function(){
            $("#schoolEcard-c-n-submit button[type=button]").on("click",function(){
                postNewDocuments(0);
            });
            $("#schoolEcard-c-n-submit button[type=submit]").on("click",function(){
                postNewDocuments(1);
            });
            function postNewDocuments(type){
                editor.sync();
                var archs=$(".schoolEcard-c-n-s-archs");
                var strArchs="";
                archs.each(function(){
                    strArchs+=$(this).attr("data-value")+"|"+$(this).attr("data-type")+";";
                });
                if(strArchs.length==0){
                    moyo.popMessage("错误","请先选择发送对象后再提交");
                    return false;
                }
                strArchs=strArchs.substring(0,strArchs.length-1);
                var title=$("#schoolEcard-c-n-b-title input");
                var body=$("textarea[name=schoolEcard-c-n-b-holder]");
                var postData={
                        action:"addNew",
                        title:title.val(),
                        body:body.val(),
                        status:type==1?true:false,
                        receivers:strArchs
                };
                moyo.showStatus("提交中");
                $.ajax({
                    dataType:"json",
                    type:"POST",
                    url:"../Services/School_Documents.ashx",
                    data:postData,
                    success:function(msg){
                        if(msg.rst){
                            moyo.popMessage("恭喜","公文发布成功~");    
                        }else{
                            moyo.popMessage("错误","公文发布失败");    
                        }
                        moyo.jumpTo("Markets/Ecard/school/teacher_documents.aspx");
                    }
                });
                return false;
            }
        };
        newDocArchAddListen();
        addDocPostNewListen();
    },
    mailInit:function(){
        var mailBoxes=$("#schoolEcard-c-f-tabs-list");
        mailBoxes.sortable().disableSelection();
        var contacts=$("#schoolEcard-c-f-t-n-sendTo li");
        contacts.hover(function(){
            $(this).addClass("ui-state-hover");
        },function(){
            $(this).removeClass("ui-state-hover");
        });
        var receiversLi=$("#schoolEcard-c-f-t-n-sendTo li");
        var receivers=[];
        receiversLi.each(function(){
            receivers.push({
                label:$(this).html(),
                val:$(this).attr("data-uid")
            });
        });
        var receiverSendAdd=$("#schoolEcard-c-f-t-n-m-r-addBox input");
        receiverSendAdd.autocomplete({
            source:receivers,
            select:function(event,ui){
                addReceivers(ui.item.label,ui.item.val);
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
                .data("item.autocomplete", item)
                .append("<a>" + item.label + "-" + item.val + "</a>")
                .appendTo(ul);
        };
        //添加收件人
        function addReceivers(name,uid){
            if($(".theReceiver[data-uid="+uid+"]").length>0){
                moyo.popMessage("提示","不能重复添加联系人");
                return;
            }
            var receiveBeforeElm=$("#schoolEcard-c-n-sendToBox-title");
            var newReceiver="<li class=\"theReceiver\" data-uid=\""+uid+"\">"+name+"</li>";
            receiveBeforeElm.after(newReceiver);
        }
        //联系人点击
        receiversLi.on("click",function(){
            addReceivers($(this).html(),$(this).attr("data-uid"));
        });
        //发送邮件
        $("#schoolEcard-c-f-t-n-m-submit button").on("click",function(){
            editor.sync();
            var addedReceivers="";
            $("#schoolEcard-c-f-t-n-m-receivers .theReceiver").each(function(){
                addedReceivers+=$(this).attr("data-uid")+";";
            });
            if(addedReceivers.length==0){
                moyo.popMessage("提示","请选择发送对象后重新提交");
                return;
            }
            addedReceivers=addedReceivers.substring(0,addedReceivers.length-1);
            moyo.showStatus("发送中");
            $.ajax({
                dataType:"json",
                type:"POST",
                url:"../Services/School_mail.ashx",
                data:{
                    action:"addNew",
                    title:$("#schoolEcard-c-f-t-n-b-title input").val(),
                    body:$("textarea[name=schoolEcard-c-f-t-n-b-holder]").val(),
                    receivers:addedReceivers
                },success:function(msg){
                        if(msg.rst){
                            moyo.popMessage("恭喜","邮件发送成功~");
                        }else{
                            moyo.popMessage("错误","邮件发送失败");
                        }
                        moyo.jumpTo("Markets/Ecard/school/teacher_mail.aspx");
                }
            });
            return false;
        });
    },
    filesInit:function(){
        var uploadButton=$("#newUpload");

    },
    classInit:function(){
        var classes=$(".schoolEcard-c-f-s-architecture");
        classes.hover(function(){
            $(this).addClass("hover");
        },function(){
            $(this).removeClass("hover");
        });
        classes.on("click",function(){
            moyo.jumpTo("Markets/Ecard/school/teacher_class.aspx?class="+$(this).attr("data-value"));
        });
        $(".student-List-student").draggable({
            revert: "invalid"
        });
        $("#student-List,#student-late-list,#student-leave-list,#student-leaveEarly-list").droppable({
            activeClass:"ui-state-error ui-corner-all",
            drop:function(event,ui){
                var newStuElm=ui.draggable.clone();
                newStuElm.removeClass("ui-draggable-dragging").attr("style","position: relative; ");
                newStuElm.draggable({
                    revert: "invalid"
                });
                ui.draggable.remove();
                $(this).append(newStuElm);
        }});
    },
    coursewareInit:function(){
       $("#schoolEcard-c-f-tabs").tabs();
       $("#submitCourseware").on("click",function(){
            editor.sync();
           var title=$("#schoolEcard-c-f-t-n-f-c-title input").val();
           var body=$("textarea[name=schoolEcard-c-f-t-n-f-holder]").val();
           var grade=$("#schoolEcard-c-f-t-n-f-c-grade select").val();
           var subject=$("#schoolEcard-c-f-t-n-f-c-subject select").val();
           if(!title.length){
                moyo.popMessage("错误","标题不能为空");
                return;
            }
            if(!body.length){
                moyo.popMessage("错误","内容不能为空");
                return;
            }
            $.ajax({
                dataType:"json",
                url:"../Services/School_Courseware.ashx",
                type:"POST",
                data:{
                    action:"addNew",
                    title:title,
                    body:body,
                    grade:grade,
                    subject:subject
                },success:function(msg){
                    if(msg.rst){
                        moyo.popMessage("恭喜","发表成功~");
                    }else{
                        moyo.popMessage("错误","发表失败");
                    }
                    moyo.jumpTo("Markets/Ecard/school/teacher_courseware.aspx");
                }
            });
        });
    }
};
Moyo.prototype.Information={
    newTopic:function(){
        var title=$("#t_t_l_title input");
        var cid=$("#topic_content input[name=cid]").val();
        $("#topic_content button[type=submit]").on("click",function(){
            var subButton=$(this);
            subButton.hide();
            editor.sync();
            var body=$("textarea[name=topicBody]");
            if(title.val().length==0||body.val().length<20){
                moyo.popMessage("错误",title.val().length==0?"标题不能为空":"正文内容不得少于10字");
                subButton.show();
                return false;
            }
            $.ajax({
                dataType:"json",
                type:"POST",
                url:"../Services/Information_Topic.ashx",
                data:{
                    action:"addNew",
                    cid:cid,
                    title:title.val(),
                    body:body.val()
                },
                success:function(msg){
                    if(!msg.rst){
                        moyo.popMessage("错误","发表新帖子失败！");
                    }else{
                        moyo.jumpTo("Markets/forum/TopicShow.aspx?id="+msg.message);
                    }
                    subButton.show();
                }
            });
            return false;
        });
    },
    commentsNewListen:function(){
        var body=$("#t_p_l_t_comments textarea");
        var submit=$("#t_p_l_t_comments button");
        var holder=$("#t_p_l_t_c_new");
        var status=$("#t_p_l_t_c_status");
        submit.on("click",function(){
            if(body.val().length==0){
                moyo.popMessage("提示","评论内容不能为空");
                body.focus();
                return;
            }
            holder.hide();
            status.addClass("loading-big");
            $.ajax({
                dataType:"json",
                type:"POST",
                url:"../Services/Information_Topic.ashx",
                data:{
                    action:"commentsNew",
                    tid:submit.attr("data-tid"),
                    body:body.val()
                },
                success:function(msg){
                    status.removeClass("loading-big");
                    holder.show();
                    if(msg.rst){
                        body.val("");
                        moyo.popMessage("恭喜","评论添加成功！");
                        $.ajax({
                            url:"../Services/Information_Topic.ashx",
                            type:"POST",
                            data:{
                                action:"commentsGet",
                                pid:submit.attr("data-tid")
                            },
                            success:function(msg){
                                $("#t_p_l_t_c_list").html(msg);
                            }
                        });
                    }else{
                        moyo.popMessage("提示","评论添加失败");
                    }
                }
            });
            return false;
        });
    },
    getMoreTopic:function(){
        var moreButton=$("#loadMoreTopic");
        moreButton.on("click",function(){
            if(moreButton.html()=="载入更多帖子中……"){
                return false;
            }else{
                moreButton.html("载入更多帖子中……");
            }
            var cid=$("#t_topic_list table").attr("data-cid"),
                last=$("#t_topic_list tbody tr:last-child").attr("data-tid");
            $.ajax({
                type:"POST",
                url:"../Services/Information_Topic.ashx",
                data:{
                    action:"moreTopic",
                    cid:cid,
                    last:last
                },
                success:function(msg){
                    if(msg==""){
                        moyo.popMessage("啊哦","没有更多内容了");
                        moreButton.remove();
                        return false;
                    }
                    moreButton.html("点击查看更多帖子");
                    $("#t_topic_list .newLoad").removeClass("newLoad");
                    $("#t_topic_list tbody").append(msg);
                    moyo.addPageJump();
                }
            });
            return false;
        });
    }
};
Moyo.prototype.siteBar={
    init:function(){
        moyo.siteBar.homeListen();
        moyo.siteBar.backListen();
        moyo.siteBar.joinGroupListen();
        moyo.siteBar.sslListen();
    },
    homeListen:function(){
        $("#s-i-home").on("click",function(){
            location.reload();
        });
    },
    backListen:function(){
        $("#s-i-back").on("click",function(){
            moyo.jumpBack();
        });
    },
    sslListen:function(){
        if(document.location.protocol=="https:"){
            var dlg=moyo.popDiv("现在是SSL加密模式","<img src=\"tipPage\\images\\ssl.jpg\" alt=\"关于SSL加密模式\"/>");
             dlg.dialog({
                modal:true,
                width:740,
                height:497
            });
            $("#s-i-ssl").attr("title","点击退出SSL安全加密模式");
        }
        $("#s-i-ssl").on("click",function(){
            var url="";
            if(document.location.protocol=="http:"){
                url="https://"+location.hostname;
            }else{
                url="http://"+location.hostname;
            }
            window.location=url;
        });
    },
    joinGroupListen:function(){
        $("#s-i-groupJoin").on("click",function(){
            var dlg=moyo.popDiv("提示","复制下面的地址到地址栏即可快速加入定西左邻QQ群\<br\/\>\<br\/\>http://qun.qq.com/#jointhegroup/gid/95396686\<br\/\>\<br\/\>群号：95396686 V3定西超级群，就差你了,快来吧!");
            dlg.dialog({
                modal: true,
                width:500,
                buttons:{
                    关闭:function(){
                        $(this).dialog("close");
                    }
                }
            });
            return false;
        });
    }
};
Moyo.prototype.Group={
    applyBack:function(id){
        if(id==0){
            moyo.popMessage("提示","提交申请失败。<br/>请先登录并请确保所有项目已经填写,小组图标已选择。");
            return;
        }
        moyo.popMessage("恭喜","圈子创建成功!");
        $(".m-s-close").click();
        moyo.jumpTo("Markets/forum/GroupTopicList.aspx?id="+id);
    },
    addJoinGroupListen:function(){
        var joinBt=$(".groupJoin");
        joinBt.on("click",function(){
            if(joinBt.hasClass("needLogin")){
                return false;
            }else{
                var clickedBt=$(this);
                var joinType=clickedBt.attr("data-isAllow");
                if(joinType==0){
                    moyo.popMessage("提示","该小组管理员设置不允许任何人加入");
                    return;
                }
                var data={
                    action:"join_group_noNeed",
                    gid:$(this).attr("data-gid")
                };
                if(joinType==1){
                    $.ajax({
                        url:"../Services/Information_group.ashx",
                        type:"POST",
                        data:data,
                        success:function(){
                            moyo.popMessage("恭喜","加入小组成功");
                            moyo.jumpTo(moyoHistory[moyoHistory.length-1]);
                        },error:function(e){
                            moyo.popMessage("啊哦","加入小组失败");
                            moyo.jumpTo(moyoHistory[moyoHistory.length-1]);
                        }
                    });
                }else{
                    
                }
            }
        });
    },
    newTopicTagListen:function(){
        var addTag=$("#group_n_c_p_tag a");
        addTag.on("click",function(){
            $(this).addClass("hide");
            $("#group_n_c_p_tag div").removeClass("hide");
            return false;
        });
    },
    newTopicListen:function(){
        $("#group_n_c_p_submit button").on("click",function(){
            $(this).hide();
            var postData={
                tag:$("input[name=tag]").val(),
                title:$("input[name=title]").val(),
                body:$("textarea[name=body]").val(),
                gid:$("input[name=gid]").val(),
                action:"topic_new"
            };
            var checkFailed=function(){
            }
            if(postData.title.length<5){
                moyo.popMessage("提示","标题不能少于5个字");
                $(this).show();
                return false;
            }
            if(postData.body.length<20){
                moyo.popMessage("提示","正文不能少于20个字");
                $(this).show();
                return false;
            }
            if(postData.tag.length!=0 && postData.tag.length!=2){
                $(this).show();
                return false;
            }
            $.ajax({
                type:'POST',
                url:'../Services/Information_group.ashx',
                data:postData,
                success:function(msg){
                        moyo.jumpTo("Markets/forum/GroupTopicShow.aspx?id="+msg);
                }
            });
        });
    },
    newCommentListen:function(){
        $("#group_comment_new button").on("click",function(){
            var submitBt=$(this);
            submitBt.hide();
            var commentBox=$("textarea");
            var postData={
                action:"commentNew",
                comment:commentBox.val(),
                tid:commentBox.attr("data-tid")
            };
            if(postData.comment.length<5){
                moyo.popMessage("提示","评论必须大于5个字");
                submitBt.show();
                return;
            }
            var commentRef=function(){
                postData.action="commentGet";
                $.ajax({
                    type:'POST',
                    url:'../Services/Information_group.ashx',
                    data:postData,
                    success:function(msg){
                        $("#group_comments_list ul").html(msg);
                    }
                });
            }
            $.ajax({
                type:'POST',
                url:'../Services/Information_group.ashx',
                data:postData,
                success:function(msg){
                    submitBt.show();
                    moyo.popMessage("提示","回应成功！");
                    commentBox.val("");
                    commentRef();
                }
            });
        });
    }
};
Moyo.prototype.Info={
    newInfoListen:function(){
        var newButton=$("#infoM-catList button");
        newButton.on("click",function(){
            moyo.jumpTo("Markets/infos/new.aspx");
        });
    },
    newInfoAddListen:function(){
        var subBt=$("#infoM-new-form button");
        subBt.on("click",function(){
            var postData={
                action:'new',
                cid:$("#infoM-new-main input[name=catId]").val(),
                title:$("#infoM-new-form input[name=title]").val(),
                name:$("#infoM-new-form input[name=name]").val(),
                price:$("#infoM-new-form input[name=price]").val(),
                body:$("#infoM-new-form textarea").val(),
                phone:$("#infoM-new-form input[name=phone]").val(),
                pass:$("#infoM-new-form input[name=pass]").val()
            };
            if(postData.title.length<2){
                moyo.popMessage("提示","标题不能少于2个字");
                return false;
            }
            if(postData.body.length<20){
                moyo.popMessage("提示","内容不能小于20个字");
                return false;
            }
            if(postData.phone.length!=7&&postData.phone.length!=11){
                moyo.popMessage("提示","请填写正确的固定电话或手机号码");
                return false;
            }
            if(postData.pass.length<5){
                moyo.popMessage("提示","管理密码不能小于五位");
                return false;
            }
            if(postData.price.length==0){postData.price=0;}
            $.ajax({
                url:"Services/Info.ashx",
                type:"POST",
                data:postData,
                success:function(msg){
                    moyo.popMessage("恭喜","信息发布成功");
                    moyo.jumpTo("Markets/Infos/index.aspx");
                }
            });
        });
    }
};
Moyo.prototype.Message={
    uid:"",
    init:function(){
        moyo.Message.newComet();
        moyo.Message.messageSend();
    },
    newComet:function(){
        var url="";
        if(moyo.Message.uid==""){
            url="../Services/message.ashx?r="+Math.random();
        }else{
            url="../Services/Message_Comet.aspx?r="+Math.random();
        }
        $.ajax({
            url:url,
            type:"POST",
            data:{action:(moyo.Message.uid==""?"newUserOnLine":"loop"),uid:moyo.Message.uid},
            success:function(msg){
                if(moyo.Message.uid==""){
                    moyo.Message.uid=msg;
                }else{
                    if(typeof(msg)=="string" && msg.length>0){
                        var newMsg=eval("("+msg+")");
                        $("#th-t-onlineNumber").html(newMsg.onlineCount+"人听到了你说的话");
                        function addMsg(msg){
                            var strClass=(moyo.Message.uid==msg.source||msg.type==-1)?"highlight":"normal";
                            strClass+=" msg"+msg.id;
                            var msgTp=$("#th-messageTpHolder").html();
                            msgTp=msgTp.replace(/\[message\]/,msg.message)
                            .replace(/\[class\]/,strClass)
                            .replace(/\[by\]/,msg.sourceName)
                            .replace(/\[time\]/,msg.time);
                            $("#th-messageHolder").prepend(msgTp);
                            moyo.addPageJump($(".msg"+msg.id + " a"));
                            setTimeout(function(){
                                $(".msg"+msg.id).fadeOut("slow",function(){
                                    $(this).remove();
                                });
                            },8000);

                        }
                        addMsg(newMsg);
                    }
                }
                moyo.Message.newComet();
            },error:function(){
                moyo.Message.newComet();
            }
        });
    },
    messageSend:function(){
        var message="";
        $("#th-t-message").on("keydown",function(e){
            if(e.which==13){
                message=$("#th-t-message").val();
                $("#th-t-message").val("");
                $.ajax({
                    url:"../Services/Message.ashx?r="+Math.random(),
                    type:"POST",
                    data:{action:"newMessage",message:message,uid:moyo.Message.uid},
                    success:function(msg){
                    }
                });
                return false;
            }
        });
    }
};
Moyo.prototype.Living={
    newShopHover:function(){
        var newShops=$(".t-c-newShop ul li");
        newShops.on("hover",function(){
            var shopHolder=$(this);
            $(".t-c-newShop .show").removeClass("show").addClass("hide");
            $(".t-c-newShop ul li h4.hide").removeClass("hide");
            shopHolder.children("h4").addClass("hide");
            shopHolder.children(".t-c-i-detal").removeClass("hide").addClass("show");
        });
    },
    rateInit:function(){
        $(".rates").raty({
            score: function() {
                return $(this).attr('data-rated');
            },
            half:true,
            hints:['糟糕透了','糟糕','不行','非常一般','一般','还凑合','不错','挺好的','棒','太棒了'],
            number:10,
            click: function(score, evt) {
                $(this).parent().children("input").val(score);
            }
        });
        $("#d-s-m-r-submit").on("click",function(){
            var rateData={
                action:"add",
                sid:$("#detal-shop").attr("data-sid"),
                point:$("#d-s-m-r-s-point").val(),
                price:$("#d-s-m-r-s-price").val(),
                service:$("#d-s-m-r-s-service").val(),
                circumstance:$("#d-s-m-r-s-circumstance").val(),
                comment:$("#d-s-m-r-comment").val()
            };
            for(var key in rateData)
            {
                if(rateData[key].length==0){
                    moyo.popMessage("提示","所有评分项都要评分，评语也不能为空哦~评完了再提交吧。");
                    return;
                }
            }
            $(this).hide();
            $.ajax({
                type:"POST",
                url:"../Services/living_shop_comment.ashx",
                data:rateData,
                success:function(msg){
                    moyo.popMessage("恭喜","评论成功");
                    $(".m-s-close").click();
                }
            });
        })
    },
    mapInit:function(){
        if(typeof(soso)=="undefined"){
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://api.map.soso.com/v1.0/main.js?callback=mapCallBack";
            document.body.appendChild(script);
        }else{
            mapCallBack();
        }
    },
    mapCallBack:function(){
        var mapHolder=$("#d-s-m-map");
        var myLatlng = new soso.maps.LatLng(mapHolder.attr("data-lng"),mapHolder.attr("data-lat"));
        var myOptions = {
            zoom: 8,
            center: myLatlng,
            mapTypeId: soso.maps.MapTypeId.ROADMAP,
            zoomLevel: 15
        };
        var map = new soso.maps.Map(mapHolder[0], myOptions);
        var latLng=new soso.maps.LatLng(mapHolder.attr("data-lng"),mapHolder.attr("data-lat"));
        var marker = new soso.maps.Marker({
            position:latLng,
            map: map
        });
    }
};
$(document).ready(function(){
    moyo=new Moyo();
    moyo.home.channelClick();
    moyo.home.wheaterGet();
    moyo.home.mallGoodFloat();
    moyo.home.infoNumberAdd();
    moyo.addPageJump();
    moyo.addLoginListen();
    moyo.popSideBar();
    moyo.inputTip($(".needTip"));
    moyo.siteBar.init();
    //moyo.Message.init();
    moyo.addHoverClass($("#order-form li"));
});
