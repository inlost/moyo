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
            $(this).animate({"opacity":"1"},500);
        },function(){
            $(this).animate({"opacity":"0.6"},100);
        });
    },
    focusOnWhat:function(){
        var pointer=$("#focusOnWhat");
        $("#content").on("mousemove",function(e){
            pointer.css("left",e.pageX-28);
        });
    }
};
$(document).ready(function(){
    var moyo=new Moyo();
    moyo.home.headImgFloat();
    moyo.home.channelHover();
    moyo.home.focusOnWhat();
});
