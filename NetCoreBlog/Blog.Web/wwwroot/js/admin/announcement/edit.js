layui.use(['layedit', 'form','layer'], function () {
    var layedit = layui.layedit;
    var $ = layui.jquery;
    var form = layui.form;
    var layer = layui.layer;
    var editIndex = layedit.build('announcementContent', {
        tool: ['strong', 'italic', 'underline', 'del', '|', 'left', 'center', 'right', 'link', 'unlink', 'face'],
        height: '150px'
    }); //建立编辑器

    //自定义验证规则
    form.verify({
        announcementContent: function (value) {
            value = $.trim(layedit.getText(editIndex));
            if (value == "") return "至少得有一个字吧";
            layedit.sync(editIndex);
        }
    });

    //监听添加提交
    form.on('submit(formAnnouncement)', function (data) {
        var index = layer.load(1);
        layedit.sync(editIndex);
        $.ajax({
            type: "POST",
            url: '/admin/Announcement/edit',
            data: $(".layui-form").serialize(),
            datatype: 'json',
            async: false,
            success: function (res) {
                layer.close(index);
                if (res.Code==1) {
                    layer.msg(res.Message);
                    layer.closeAll(); //关闭所有页面层
                    parent.window.location.reload();
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