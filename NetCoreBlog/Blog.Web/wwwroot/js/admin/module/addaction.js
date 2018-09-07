//@ sourceUrl=module-addaction.js
layui.use(['jquery', 'layer', 'form'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form;
    form.render();
    form.on("submit(btnAddModuleAction)", function (data) {
        $.ajax({
            url: "/admin/module/addaction",
            type: 'post',
            dataType: 'json',
            data: $('#moduleAactionForm').serialize(),
            success: function (res) {
                if (res.Code == 1) {
                    layer.close(layer.index);
                    $.ajax({
                        url: "/admin/module/actionlist",
                        data: { moduleId: $("#ModuleId").val() },
                        dataType: "json",
                        success: function (data) { 
                            initlActionGrid(data.data)
                        }
                    });
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

