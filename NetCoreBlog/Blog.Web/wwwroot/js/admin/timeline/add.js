//@ sourceURL=timeline-add.js
layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;

    var editIndex = layedit.build('Content', {
        tool: ['strong', 'italic', 'underline', 'del', '|', 'left', 'center', 'right', 'link', 'unlink', 'face']
    });
    //监听添加提交
    form.on('submit(*)', function (data) {
        layedit.sync(editIndex);
        var index = layer.load(1);
        $.ajax({
            type: "POST",
            url: '/admin/timeline/Add',
            data: $("#timelineForm").serialize(),
            datatype: 'json',
            success: function (res) {
                if (res.Code == 1) {
                    init(pageIndex, pageSize);
                } else {
                    layer.msg(res.Message);
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
            }
        });
        return false;
    });
});