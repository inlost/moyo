Moyo=function(){

}
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
            console.log("go to "+iIndex);
            floatBox.animate({"margin-top":-(iIndex-1)*432},800,function(){
                console.log("go to "+iIndex+" success");
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
            channels.each(function(){
               nowChannel.removeClass("activeChannel");
            });
            nowChannel.addClass("activeChannel");
            market.removeClass("hide");
            $.ajax({
                url:getUrl(nowChannel)+"index.html",
                type:"GET",
                success:function(msg){
                    $("#market-status-bar").hide();
                    channelContent.html(msg);
                    channelContent.parent().removeClass("hide").show();
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
$(document).ready(function(){
    moyo=new Moyo();
    moyo.home.headImgFloat();
    moyo.home.channelHover();
    moyo.home.focusOnWhat();
    moyo.home.channelClick();
});
