layui.use(['element', 'layer', 'form', 'layedit','upload'], function () {
    var $ = layui.jquery
        , layer = layui.layer
        , form = layui.form
        , element = layui.element //Tab的切换功能，切换事件监听等，需要依赖element模块
        , layedit = layui.layedit
        , upload = layui.upload;
    var editIndex = layedit.build('Abstract', {
        tool: ['strong', 'italic', 'underline', 'del', '|', 'left', 'center', 'right', 'link', 'unlink', 'face']
    });

    var uploadInst = upload.render({
        elem: '#upImg',
        url: '/api/pictures/',
        data: { directoryName: "global", filename: "Logo_40", isThumbnail: 1, width: 40, height: 40 },
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
        error: function (e) {
            layer.msg(e.responseText);
            layer.closeAll('loading');
        }
    });
    form.on('submit(*)', function (data) {
        layedit.sync(editIndex);
        $.ajax({
            url: '/admin/blogsettings/save',
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