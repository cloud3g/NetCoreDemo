//@ sourceURL=article-edit.js
var time = new Date().Format("yyyyMMddHHmmss");
layui.use(['jquery', 'layedit', 'form'], function () {
    var $ = layui.$, layedit = layui.layedit, form = layui.form;
    layedit.set({
        uploadImage: {
            url: '/api/pictures' //接口url
            , type: 'post' //默认post
        }
    });
    var eidt = layedit.build('Content');
    form.render();
    var categoryRootId = $('#rootCategory').val();
    getSelete(categoryRootId);

    form.on('select(rootCategory)', function (data) {
        $('#childCategory').html("");
        getSelete(data.value);  
        //console.log(data.elem); //得到select原始DOM对象
        //console.log(data.value); //得到被选中的值
        //console.log(data.othis); //得到美化后的DOM对象
    });  

    form.on('submit(btnSubmit)', function (data) {
        //console.log(data.elem) //被执行事件的元素DOM对象，一般为button对象
        //console.log(data.form) //被执行提交的form对象，一般在存在form标签时才会返回
        //console.log(data.field) //当前容器的全部表单字段，名值对形式：{name: value}
        if ($('#childCategory').val() == null) {
            $("#CategoryId").val($('#rootCategory').val())
            $("#CategoryName").val($('#rootCategory').find("option:selected").text())
        } else {
            $("#CategoryId").val($('#childCategory').val())
            $("#CategoryName").val($('#childCategory').find("option:selected").text())
        }
        layedit.sync(eidt);
        $.ajax({
            url: '/admin/blogmanager/edit',
            data: $('#articleForm').serialize(),
            type: 'post',
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

    function getSelete(pid) {
        
        if (pid && pid != 0) {
            $.post("/admin/blogmanager/getselect", { pid: pid }, function (data) {
                if (data) {
                    var html = '';
                    $.each(data, function (index, obj) {
                        html += '<option value="' + obj.Value + '">' + obj.Text + '</option>';
                    })
                    $('#childCategory').html(html);
                    form.render('select');
                }
            }, 'json')
        }
    }
});

layui.use(['upload', 'layer', 'jquery'], function () {
    var upload = layui.upload, layer = layui.layer, $ = layui.$;
    var imgUrl = $('#img').attr("src");
    var temp = imgUrl.split('/');
    var imgName = temp[temp.length - 1].split('.')[0] == "cover_default" ? time : temp[temp.length - 1].split('.')[0];
    //执行实例
    var uploadInst = upload.render({
        elem: '#upImg' //绑定元素
        , url: '/api/Pictures' //上传接口
        , method: 'post'
        , exts: 'jpg|png|gif|bmp|jpeg'
        , auto: true
        , data: { directoryName: "articleCover", filename: time }
        , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            layer.load(); //上传loading
        }
        , done: function (res) {
            //上传完毕回调
            if (res.code == 0) {

                $('#ImgUrl').val(res.data.src);
                $('#img').attr('src', res.data.src + "?"+new Date());
            }
            layer.closeAll('loading'); //关闭loading
        }
        , error: function () {
            //请求异常回调
            layer.closeAll('loading'); //关闭loading
        }
    });
});