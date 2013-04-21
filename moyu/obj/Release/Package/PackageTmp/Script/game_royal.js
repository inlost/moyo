if(typeof MoyoGame){
    var MoyoGame=function(){};
}
MoyoGame.prototype.royal={
    init:function(){
        moyoGame.royal.setPalaceStatus();
    },
    setPalaceStatus:function(){
        var palaces=$(".palace");
        for(var i=0;i<palaces.length;i++){
            if(palaces[i].className.indexOf("build")>0){
                var buildTmp=$("#royalFun #needBuild").html();
                palaces[i].innerHTML=buildTmp;
            }else{
                var palaceStatus=palaces[i].getAttribute("data-status");

            }
        }
    }
};

var moyoGame=new MoyoGame;
moyoGame.royal.init();
