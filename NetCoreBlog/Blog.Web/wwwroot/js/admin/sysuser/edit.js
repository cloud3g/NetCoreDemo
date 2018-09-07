//@ sourceURL=user-edit.js
layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;


    form.on('submit(btnSubmit)', function (data) {
        $.ajax({
            url: '/admin/sysuser/edit',
            type: 'post',
            data: $('#userForm').serialize(),
            success: function (res) {
                if (data.Code == 1) {
                    initil(pageIndex, pageSize);
                } else {
                    layer.msg(data.Message);
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
            }
        });
        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    });

});