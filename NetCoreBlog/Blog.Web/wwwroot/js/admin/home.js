layui.use(['jquery', 'layer', 'element'], function () {
    var $ = layui.$, element = layui.element, layer = layui.layer;
    layui.define(function (exports) {
        exports('navClick', function (a) {
            var url = $(a).attr("href");
            $("#mainFrame").attr("src", url);
        })
    });
});
