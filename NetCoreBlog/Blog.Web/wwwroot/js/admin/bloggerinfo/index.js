var time = new Date().Format("yyyyMMddHHmmss");
layui.use(['element', 'layer', 'form','upload'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , form = layui.form
        , element = layui.element //Tab的切换功能，切换事件监听等，需要依赖element模块
        , upload = layui.upload;
    var uploadInst = upload.render({
        elem: '#upImg',
        url: '/api/pictures/',
        data: { directoryName: "bloggerCover", filename: time, isThumbnail: 1, width:200,height:200 },
        exts: 'jpg|png|gif|bmp|jpeg',
        before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            layer.load(); //上传loading
        },
        done: function (res) {
            if (res.code == 0) {
                $('#Icon').val(res.data.src);
                $('#img').attr('src', res.data.src);
            }
            layer.closeAll('loading');
        },
        error: function (e) {
            layer.msg(e.responseText);
            layer.closeAll('loading');
        }
    });
    form.on('submit(changeUser)', function (data) {
        
        $.ajax({
            url: '/admin/bloggerinfo/save',
            type: 'post',
            data: $(".layui-form").serialize(),
            success: function (res) {
                layer.msg(res.Message);
            },
            error: function (e) {
                layer.msg(e.responseText);
            }
        });
        return false;
    })
})