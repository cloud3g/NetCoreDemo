layui.use(['flow', 'jquery'], function () {
    var $ = layui.jquery, flow = layui.flow;;
    $(function () {
        //播放公告
        playAnnouncement(3000);
    });
    function playAnnouncement(interval) {
        var index = 0;
        var $announcement = $('.home-tips-container>span');
        //自动轮换
        setInterval(function () {
            index++;    //下标更新
            if (index >= $announcement.length) {
                index = 0;
            }
            $announcement.eq(index).stop(true, true).fadeIn().siblings('span').fadeOut();  //下标对应的图片显示，同辈元素隐藏
        }, interval);
    }
    //信息流
    flow.load({
        elem: '.blog-main-left', //指定列表容器
        isAuto: true,
        end: '没有更多了',
        mb: 200,
        done: function (page, next) {
            var pages;  //总页数
            var lis = [];   //html
            if (page == 1) {
                next(lis.join(''), page < 999999);
                return;
            }
            $.ajax({
                type: 'post',
                url: '/home/GetArticlesByFlow',
                data: { currentIndex: page, pageSize: 10 },
                datatype: 'json',
                success: function (res) {
                    if (res.Success) {
                        lis.push(res.Data);
                        pages = res.SubCode;
                        next(lis.join(''), page < pages);
                    } else {
                        layer.msg('获取数据失败', { icon: 2 });
                    }
                },
                error: function (e) {
                    layer.msg(e.responseText);
                }
            });
        }
    });
});