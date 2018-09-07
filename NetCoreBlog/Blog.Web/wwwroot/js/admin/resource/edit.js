var time = new Date().Format("yyyyMMddHHmmss");
layui.use(['form', 'layer', 'upload'], function () {
    var $ = layui.jquery;
    var form = layui.form;
    var layer = layui.layer;
    var upload = layui.upload;

    var uploadInst = upload.render({
        elem: '#upImg' //绑定元素
        , url: '/api/Pictures' //上传接口
        , method: 'post'
        , exts: 'jpg|png|gif|bmp|jpeg'
        , auto: true
        , data: { isThumbnail: 1, directoryName: "resourceCover", filename: time,width:256,height:148 }//接口1代表启用压缩
        , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            layer.load(); //上传loading
        }
        , done: function (res) {
            //上传完毕回调
            if (res.code == 0) {

                $('#ResourceCoverSrc').val(res.data.src);
                $('#resourceCoverImg').attr('src', res.data.src);
            }
            layer.closeAll('loading'); //关闭loading
        }
        , error: function () {
            //请求异常回调
            layer.closeAll('loading'); //关闭loading
        }
    });

    //监听添加提交
    form.on('submit(*)', function (data) {
        var index = layer.load(1);

        $.ajax({
            type: "POST",
            url: '/admin/resource/edit',
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