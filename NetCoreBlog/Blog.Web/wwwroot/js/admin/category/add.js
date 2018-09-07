//@ sourceURL=category-add.js
var time = new Date().Format("yyyyMMddHHmmss");
layui.use(['form', 'layedit', 'jquery', 'layer'], function () {
    var form = layui.form, $ = layui.$, layedit = layui.layedit, layer = layui.layer;
    var edit = layedit.build('BodyContent', { uploadImage: { url: '/api/pictures', type: 'post' } });

    $(function () {
        $.post("/admin/category/GetSelectItem", function (data) {
            $.each(data, function (index, value) {
                if (value.Id == selectId) {
                    $('#Pid').append("<option selected value='" + value.Id + "'>" + value.Name + "</option>");

                } else {

                    $('#Pid').append("<option value='" + value.Id + "'>" + value.Name + "</option>");
                }

            })
            form.render('select');
            var selectItem = $('#CategoryType').next().children('.layui-anim').children('.layui-this').attr('lay-value');

            InitForm(selectItem);
        })

    });
    form.on('submit(*)', function (data) {
    //console.log(data.elem) //被执行事件的元素DOM对象，一般为button对象
    //console.log(data.form) //被执行提交的form对象，一般在存在form标签时才会返回
    //console.log(data.field) //当前容器的全部表单字段，名值对形式：{name: value}
        layedit.sync(edit);

        $.ajax({
            url: '/admin/category/add',
            type: 'post',
            data: $("form").serialize(),
            success: function (res) {
                if (res.Code == 1) {
                    parent.location.reload();
                    var index = parent.layer.getFrameIndex(window.name);
                    parent.layer.close(index);

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
form.on('select(test)', function (data) {
    var item = data.value;
    if (item == categoryType.General) {
        getGeneralForm();
    } else if (item == categoryType.Page) {
        getPageForm();

    } else if (item == categoryType.Link) {
        getLinkForm();
    }
});
form.on('switch', function (data) {
    //console.log(data.elem); //得到checkbox原始DOM对象
    //console.log(data.elem.checked); //开关是否开启，true或者false
    //console.log(data.value); //开关value值，也可以通过data.elem.value得到
    //console.log(data.othis); //得到美化后的DOM对象
    var value = data.elem.checked;
    $(data.elem).val(value)

});  
function InitForm(item) {
    if (item == categoryType.General) {
        getGeneralForm();
    } else if (item == categoryType.Page) {
        getPageForm();

    } else if (item == categoryType.Link) {
        getLinkForm();
    }

}

function getGeneralForm() {
    $('#Pid').removeAttr("disabled");
    form.render('select');
    addDisplaynonne(['BodyContent', 'LinkUrl']);
    $('#LinkUrl').val("");
    $('#BodyContent').val("");
};
function getPageForm() {
    $('#Pid').val('0');
    $('#Pid').attr("disabled", "disabled");
    form.render('select');
    removeDisplaynonne(["BodyContent"])
    addDisplaynonne(['LinkUrl']);
    $('#LinkUrl').val("");
};
function getLinkForm() {
    $('#Pid').val('0');
    $('#Pid').attr("disabled", "disabled");
    form.render('select');
    removeDisplaynonne(["LinkUrl"])
    addDisplaynonne(['BodyContent', 'Meta_Description', 'Meta_Keywords']);
    $('#BodyContent').val("");
    $('#Meta_Description').val("");
    $('#Meta_Keywords').val("");
};

function addDisplaynonne(inputs) {
    if ($.isArray(inputs)) {
        $.each(inputs, function (index, value) {
            $('#' + value).parent().parent().addClass("displaynonne");
        });
    } else {

        $('#' + inputs).parent().parent().addClass("displaynonne");
    }

};
function removeDisplaynonne(inputs) {
    if ($.isArray(inputs)) {
        $.each(inputs, function (index, value) {
            $('#' + value).parent().parent().removeClass("displaynonne");
        });
    } else {

        $('#' + inputs).parent().parent().removeClass("displaynonne");
    }
};
})

layui.use(['upload', 'layer', 'jquery'], function () {
    var upload = layui.upload, layer = layui.layer, $ = layui.$;

    //执行实例
    var uploadInst = upload.render({
        elem: '#upImg' //绑定元素
        , url: '/api/Pictures' //上传接口
        , method: 'post'
        , exts:'jpg|png|gif|bmp|jpeg'
        , auto: true 
        , data: { isThumbnail: 1, directoryName: "categoryCover", filename: time }//接口1代表启用压缩
        , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            layer.load(); //上传loading
        }
        , done: function (res) {
            //上传完毕回调
            if (res.code == 0) {
              
                $('#ImgUrl').val(res.data.src);
                $('#img').attr('src', res.data.src);
            }
            layer.closeAll('loading'); //关闭loading
        }
        , error: function () {
            //请求异常回调
            layer.closeAll('loading'); //关闭loading
        }
    });
});