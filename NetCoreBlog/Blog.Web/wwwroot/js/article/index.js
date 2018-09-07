layui.use(['jquery', 'flow','form'], function () {
    var $ = layui.jquery;
    var flow = layui.flow;
    var form = layui.form;
    var categoryId = $('#hidCategoryId').val();
    var keyWords = $('#hidKeyWords').val();
    form.on("submit(formSearch)", function (data) {

        window.location.href = encodeURI(encodeURI("/article/Search?keywords=" + $('#keywords').val()));
    })
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
                url: '/article/GetArticlesByFlow',
                data: { currentIndex: page, pageSize: 10, categoryId: categoryId, keyWords: keyWords },
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