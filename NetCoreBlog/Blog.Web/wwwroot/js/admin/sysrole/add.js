//@ sourceURL=role-add.js
layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;
     

    form.on('submit(btnSubmit)', function (data) {
        //console.log(data.elem) //被执行事件的元素DOM对象，一般为button对象
        //console.log(data.form) //被执行提交的form对象，一般在存在form标签时才会返回
        //console.log(data.field) //当前容器的全部表单字段，名值对形式：{name: value}
        
        $.ajax({
            url: '/admin/sysrole/add',
            data: $('#roleForm').serialize(),
            type: 'post',
            success: function (res) {
                layer.closeAll('page');
                if (res.Code == 1) {
                    
                    initilSysRole(pageIndex, pageSize);
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