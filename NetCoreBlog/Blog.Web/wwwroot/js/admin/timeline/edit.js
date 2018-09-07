//@ sourceURL=timeline-edit.js
layui.use(['form', 'layer', 'layedit'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;
    var editIndex = layedit.build('Content', {
        tool: ['strong', 'italic', 'underline', 'del', '|', 'left', 'center', 'right', 'link', 'unlink', 'face']
    });
   

    //监听添加提交
    form.on('submit(*)', function (data) {
        var index = layer.load(1);
        layedit.sync(editIndex);
        $.ajax({
            type: "POST",
            url: '/admin/timeline/edit',
            data: $("#timelineForm").serialize(),
            datatype: 'json',
            success: function (res) {
                layer.close(index);
                if (res.Code == 1) {
                    init(pageIndex, pageSize);
                } else {
                    layer.msg(res.Message);
                }
            },
            error: function (e) {
                layer.close(index);
                layer.msg(e.responseText);
            }
        });
        return false;
    });
});