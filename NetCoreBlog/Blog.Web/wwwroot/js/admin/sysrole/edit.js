//@ sourceURL=role-edit.js
layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;


    form.on('submit(btnSubmit)', function (data) {
        $.ajax({
            url: '/admin/sysrole/edit',
            data: $('#roleForm').serialize(),
            type: 'post',
            success: function (res) {
                if (res.Code == 1) {
                    layer.closeAll('page');
                    window.location.reload();
                } else {
                    layer.msg(res.Message);
                }
            },
            error: function (e) {
                layer.closeAll('page');
                layer.msg(e.responseText);
            }
        });

        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    });

});
