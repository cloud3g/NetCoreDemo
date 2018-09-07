var time = new Date().Format("yyyyMMddHHmmss");
layui.use(['form', 'layer', 'upload'], function () {
    var $ = layui.jquery;
    var form = layui.form;
    var layer = layui.layer,upload = layui.upload;


    var uploadInst = upload.render({
        elem: '#upImg',
        url: '/api/pictures/',
        data: { directoryName: "friendslinksCover", filename: time, isThumbnail: 1, width: 40, height: 40 },
        exts: 'jpg|png|gif|bmp|jpeg',
        before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            layer.load(); //上传loading
        },
        done: function (res) {
            if (res.code == 0) {
                $('#Logo').val(res.data.src);
                $('#img').attr('src', res.data.src + "?" + new Date());
                form.render();
            }
            layer.closeAll('loading');
        },
        error: function (res) {
            layer.closeAll('loading');
        }
    });
    //监听开关
    form.on('switch(enable)', function (data) {
        $('#Enable').val(data.elem.checked);
    });
    //监听添加提交
    form.on('submit(*)', function (data) {
        var index = layer.load(1);

        $.ajax({
            type: "POST",
            url: '/admin/friendslinks/edit',
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