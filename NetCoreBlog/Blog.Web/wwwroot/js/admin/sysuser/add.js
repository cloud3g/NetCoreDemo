//@ sourceURL=user-add.js

layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;
    

    form.on('submit(btnSubmit)', function (data) {
        $.ajax({
            url: '/admin/sysuser/add',
            type: 'post',
            data: $('#userForm').serialize(),
            success: function (res) {
                if (res.Code == 1) {
                    initil(pageIndex, pageSize);
                } else {
                    layer.msg(res.Message);
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
            }
        });


        return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
    });
    
});