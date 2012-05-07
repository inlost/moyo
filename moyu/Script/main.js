Moyo=function(){
    var statusBar=$("#market-status-bar");
    var marketBox=$("#marketBox");
    var channelContent=$("#marketContent").parent();
    this.showStatus=ShowStatus;//显示状态条
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
    this.addPageJump=function(){
        var jumpLinks=$(".jump");
        jumpLinks.on("click",function(){
            ShowStatus("载入中");
            $.ajax({
                type:"GET",
                url:"../"+$(this).attr("data-dst"),
                success:function(msg){
                    $("#marketContent").html(msg);
                    HideStatus(true);
                },error:function(e){
                    HideStatus(true);
                }
            });
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
    //通用弹出对话框
    this.popMessage=function(title,message){
        var msgBox=$("#moyo-message");
        var msgContentHolder=msgBox.children("p");
        msgBox.attr("title",title);
        msgContentHolder.html(message);
        msgBox.dialog({modal: true,buttons:{
            确定:function(){
                $(this).dialog("close");
            }
        }});
    };
    //加载CSS
    this.loadCss=function(file){ 
        var cssTag=document.getElementById('loadCss'); 
　　  var head=document.getElementsByTagName('head').item(0); 
　　  if(cssTag)head.removeChild(cssTag);
        css = document.createElement('link'); 
        css.href = file;
　　  css.rel='stylesheet'; 
　　  css.type='text/css'; 
　　  css.id='loadCss'; 
　　  head.appendChild(css); 
    };
};
Moyo.prototype.home={
    headImgFloat:function(){
        var floatBox=$("#adImgBox ul");
        var iIndex=2;
        var introduces=$("#adIntroduce li");
        var intervalId=setInterval(go,5500);
        function startFloat(){
            intervalId=setInterval(go,5500);
        }
        $("#adIntroduce a").on("click",function(){
            iIndex=$(this).attr("data-index");
            clearInterval(intervalId);
            go();
            startFloat();
            return false;
        });
        function go(){
            $(".ad-now").removeClass();
            introduces.eq(iIndex-1).addClass("ad-now");
            //console.log("go to "+iIndex);
            floatBox.animate({"margin-top":-(iIndex-1)*432},800,function(){
                //console.log("go to "+iIndex+" success");
                iIndex=iIndex==4?1:Number(iIndex)+1;
            });
        }
    },
    channelHover:function(){
        var channels=$(".channelItem");
        channels.hover(function(){
            var channelName=$(this).children(".channelName");
            channelName.css({"display":"block",
                                "background-color":$(this).css("background-color")});
            $(this).animate({"opacity":"1"},500);
        },function(){
            var channelName=$(this).children(".channelName");
            channelName.css("display","none");
            if(!$(this).hasClass("activeChannel")){
                $(this).animate({"opacity":"0.6"},100);
            }
        });
    },
    focusOnWhat:function(){
        var pointer=$("#focusOnWhat");
        $("#content").on("mousemove",function(e){
            pointer.css("left",e.pageX-28);
        });
    },
    channelClick:function(){
        var channels=$(".channelItem");
        var header=$("#header");
        var splitBar=$("#splitBar");
        var market=$("#market");
        var channelContent=$("#marketContent");
        channels.on("click",function(){
            var nowChannel=$(this);
            splitBar.css("display","none");
            header.slideUp("normal");
            channels.removeClass("activeChannel").animate({"opacity":"0.6"},100);
            nowChannel.addClass("activeChannel").animate({"opacity":"1.0"},100);;
            market.removeClass("hide");
            var moyo=new Moyo();
            moyo.showStatus("载入中……");
            $.ajax({
                url:getUrl(nowChannel)+"index.html",
                type:"GET",
                success:function(msg){
                    channelContent.html(msg);
                    moyo.hideStatus(true);
                },error:function(e){
                    console.log(e);
                }
            });
            return false;
        });
        function getUrl(channelElm)
        {
            switch(channelElm.attr("id"))
            {
                case "cpChannel":
                    return "../Markets/Computer/";
                case "ecChannel":
                    return "../Markets/Ecard/";
                case "clChannel":
                    return "../Markets/Clothes/";
            }
        }
    },
    switchToSubForm:function(formFor,data){
        var content=$("#marketContent");
        var subForm=$("#subForm");
        var formType=$("#formType");
        content.addClass("marketContentOnSubmit").animate({left:"-3000"},"normal",function(){
            subForm.fadeIn("slow");
            formType.val(formFor);
        });
    }
};
Moyo.prototype.computer={
    bindSubmit:function(){
        var submitButton=$(".submitForm");
        submitButton.on("click",function(){
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
$(document).ready(function(){
    moyo=new Moyo();
    moyo.home.headImgFloat();
    moyo.home.channelHover();
    moyo.home.focusOnWhat();
    moyo.home.channelClick();
});
