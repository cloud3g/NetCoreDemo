layui.use(['element', 'layer', 'util', 'form', "jquery"], function () {
    var $ = layui.$;
    var ua = navigator.userAgent;
    var ipad = ua.match(/(iPad).*OS\s([\d_]+)/),
        isIphone = !ipad && ua.match(/(iPhone\sOS)\s([\d_]+)/),
        isAndroid = ua.match(/(Android)\s+([\d.]+)/),
        isMobile = isIphone || isAndroid;
    if (isMobile) {
        layui.util.fixbar({
           
            bgcolor: '#009E94',
        });
        removejscssfile("/fly/css/full.css", "css");
    } else {
        layui.util.fixbar({
            bar1: "&#xe603;",
            bgcolor: '#009E94',
            click: function (type) {

                if (type === 'bar1') {
                    if (isInclude("full.css")) {

                        setCookie('full', "", 365);
                        removejscssfile("/fly/css/full.css", "css");
                        $(".layui-fixbar li[lay-type='bar1']").html("&#xe603;");

                    } else {

                        setCookie('full', "true", 365);
                        loadjscssfile("/fly/css/full.css", "css");
                        $(".layui-fixbar li[lay-type='bar1']").html("&#xe619;");
                        leftOut();
                    }
                }
            }
        });
    };
    
    $(function () {

        $("#" + navId).addClass("nav-this");
        $('.blog-navicon').click(function () {
            var sear = new RegExp('layui-hide');
            if (sear.test($('.blog-nav-left').attr('class'))) {
                leftIn();
            } else {
                leftOut();
            }
        });
        $('.blog-mask').click(function () {
            leftOut();
        });
        //文章栏目导航按钮
        $('.category-toggle').click(function (e) {
            e.stopPropagation();
            categroyIn();
        });

        $('.article-category').click(function () {
            categoryOut();
        });

        $('.article-category > a').click(function (e) {
            e.stopPropagation();
        });

    });
    function leftIn() {
        $('.blog-mask').unbind('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend');
        $('.blog-nav-left').unbind('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend');

        $('.blog-mask').removeClass('maskOut');
        $('.blog-mask').addClass('maskIn');
        $('.blog-mask').removeClass('layui-hide');
        $('.blog-mask').addClass('layui-show');

        $('.blog-nav-left').removeClass('leftOut');
        $('.blog-nav-left').addClass('leftIn');
        $('.blog-nav-left').removeClass('layui-hide');
        $('.blog-nav-left').addClass('layui-show');
    }

    function leftOut() {
        $('.blog-mask').on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $('.blog-mask').addClass('layui-hide');
        });
        $('.blog-nav-left').on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $('.blog-nav-left').addClass('layui-hide');
        });

        $('.blog-mask').removeClass('maskIn');
        $('.blog-mask').addClass('maskOut');
        $('.blog-mask').removeClass('layui-show');

        $('.blog-nav-left').removeClass('leftIn');
        $('.blog-nav-left').addClass('leftOut');
        $('.blog-nav-left').removeClass('layui-show');
    }
    function loadjscssfile(filename, filetype) {
        if (filetype == "js") {
            var fileref = document.createElement('script');
            fileref.setAttribute("type", "text/javascript");
            fileref.setAttribute("src", filename);
        }
        else if (filetype == "css") {
            var fileref = document.createElement("link");
            fileref.setAttribute("rel", "stylesheet");
            fileref.setAttribute("type", "text/css");
            fileref.setAttribute("href", filename);
        }
        if (typeof fileref != "undefined")
            document.getElementsByTagName("head")[0].appendChild(fileref);
    }
    function removejscssfile(filename, filetype) {
        var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none";
        var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none";
        var allsuspects = document.getElementsByTagName(targetelement);
        for (var i = allsuspects.length; i >= 0; i--) {
            if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
                allsuspects[i].parentNode.removeChild(allsuspects[i]);
        }
    }
    function isInclude(name) {
        var js = /js$/i.test(name);
        var es = document.getElementsByTagName(js ? 'script' : 'link');
        for (var i = 0; i < es.length; i++)
            if (es[i][js ? 'src' : 'href'].indexOf(name) != -1) return true;
        return false;
    }
    function categroyIn() {
        $('.category-toggle').addClass('layui-hide');
        $('.article-category').unbind('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend');

        $('.article-category').removeClass('categoryOut');
        $('.article-category').addClass('categoryIn');
        $('.article-category').addClass('layui-show');
    }

    function categoryOut() {
        $('.article-category').on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $('.article-category').removeClass('layui-show');
            $('.category-toggle').removeClass('layui-hide');
        });

        $('.article-category').removeClass('categoryIn');
        $('.article-category').addClass('categoryOut');
    }

    function setCookie(c_name, value, expiredays) {
        console.log(c_name + ":" + value);
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + expiredays);
        document.cookie = c_name + "=" + escape(value) +
            ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString()) +"; path=/";
    }
    function getCookie(c_name) {
        if (document.cookie.length > 0) {
            c_start = document.cookie.indexOf(c_name + "=");
            if (c_start != -1) {
                c_start = c_start + c_name.length + 1;
                c_end = document.cookie.indexOf(";", c_start);
                if (c_end == -1) c_end = document.cookie.length;
                return unescape(document.cookie.substring(c_start, c_end));
            }
        }
        return "";
    }
    function checkCookie() {
        isfull = getCookie('full');
        if (isfull != null && isfull != "") {
            loadjscssfile("/fly/css/full.css", "css");
        }
        else {
            removejscssfile("/fly/css/full.css", "css");
        }
        return true;
    }
});
