//@ sourceUrl=module-edit.js
layui.use(['jquery', 'layer', 'form'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form;


    getSeleteItem($('#Type').val());
    //监听选择
    form.on("select(type)", function (data) {
        // alert(data.value);
        getSeleteItem(data.value);
    });

    form.on("submit(btnEditModule)", function (data) {
        $.ajax({
            url: "/admin/module/editmodule",
            type: 'post',
            dataType: 'json',
            data: $('#moduleForm').serialize(),
            success: function (res) {
                if (res.Code == 1) {
                    layer.close(layer.index);
                    initilSysModule();
                    initlActionGrid();
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

function getSeleteItem(type) {
    var form = layui.form;
    var $ = layui.$, form = layui.form;
    $('#Pid').html("<option value='0'>顶级目录</option>");
    form.render();
    if (type == moduleType.Page) {
        $.post("/admin/module/getseleteitem",  function (res) {

            var moduleHtml = "<option value='0'>顶级目录</option>";
            $.each(res, function (index, item) {
                moduleHtml += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                $('#Pid').html(moduleHtml);
                
            });
            $('#Pid').val($("#selectPid").val())
            form.render();
        })
    }
    
}