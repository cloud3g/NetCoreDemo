//时间格式化
Date.prototype.Format = function (formatStr) {
    var str = formatStr;
    var Week = ['日', '一', '二', '三', '四', '五', '六'];

    str = str.replace(/yyyy|YYYY/, this.getFullYear());
    str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));
    str = str.replace(/MM/, (this.getMonth() + 1) > 9 ? (this.getMonth() + 1).toString() : '0' + (this.getMonth() + 1));
    str = str.replace(/M/g, (this.getMonth() + 1));

    str = str.replace(/w|W/g, Week[this.getDay()]);

    str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
    str = str.replace(/d|D/g, this.getDate());

    str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
    str = str.replace(/h|H/g, this.getHours());
    str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
    str = str.replace(/m/g, this.getMinutes());

    str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
    str = str.replace(/s|S/g, this.getSeconds());

    return str;
}


function uploadImg(elem,input,img,filename, directoryName, isThumbnail, width, height) {
      /// <summary>上传图片</summary>     
     ///<param name="elem" type="String">绑定上传按钮id</param>
    ///<param name="input" type="String">保存返回路径的input</param>
    ///<param name="img" type="String">显示返回图片的元素</param>
     /// <param name="filename" type="String">上传的图片名字</param>
    /// <param name="directoryName" type="String">上传图片保存文件夹</param>
    ///<param name="isThumbnail" type="bool">是否压缩,传非0数为压缩</param>
    ///<param name="width" type="int">压缩宽度</param>
    ///<param name="height" type="int">压缩高度</param>

    layui.use(['upload', 'layer', 'jquery'], function () {
      
        var upload = layui.upload, layer = layui.layer, $ = layui.$;
        //执行实例
        var uploadInst = upload.render({
            elem: '#' + elem //绑定元素
            , url: '/api/Pictures' //上传接口
            , method: 'post'
            , exts: 'jpg|png|gif|bmp|jpeg'
            , auto: true
            , data: { isThumbnail: isThumbnail, directoryName: directoryName, filename: filename, width: width, height: height}//接口1代表启用压缩
            , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
                layer.load(); //上传loading
            }
            , done: function (res) {
                //上传完毕回调
                if (res.code == 0) {
                    if (input) {
                        $('#' + input).val(res.data.src);
                    }
                    if (img) {
                        $('#' + img).attr('src', res.data.src);
                    }
                    
                }
                layer.closeAll('loading'); //关闭loading
            }
            , error: function () {
                //请求异常回调
                layer.closeAll('loading'); //关闭loading
            }
        });
    });
}

layui.use('jquery', function () {
    var $ = layui.$;
    //全局的ajax访问，处理ajax清求时sesion超时
    $(document).ajaxComplete(function (event, xhr, settings) {
        //通过XMLHttpRequest取得响应头，sessionstatus，
        var sessionstatus = xhr.getResponseHeader("sessionstatus");
        if (sessionstatus == "timeout") {
            //如果超时就处理 ，指定要跳转的页面
            
            top.location.href = "/admin/account";
        }
    });
})