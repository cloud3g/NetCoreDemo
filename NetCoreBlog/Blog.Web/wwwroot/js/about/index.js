layui.use(['flow', 'layer', 'form', 'element','layedit'], function () {
    var $ = layui.jquery;
    var flow = layui.flow;
    var form = layui.form;
    var element = layui.element;
    var layedit = layui.layedit;
    var editIndex = layedit.build('remarkEditor', {
        tool: ['strong', 'italic', 'underline', 'del', '|', 'left', 'center', 'right', 'link', 'unlink', 'face'],
        height: '150px'
    }); //建立编辑器
    element.tabChange('tabAbout', '1');
    var layid = location.hash.replace(/^#tabIndex=/, '');
    element.tabChange('tabAbout', layid); 

    //监听Tab切换，以改变地址hash值
    element.on('tab(tabAbout)', function () {
        location.hash = 'tabIndex=' + this.getAttribute('lay-id');
    });
    

    ////信息流
    //flow.load({
    //    elem: 'ul.blog-comment', //指定列表容器
    //    isAuto: true,
    //    end: '没有更多了',
    //    mb: 200,
    //    done: function (page, next) {
    //        var pages;  //总页数
    //        var lis = [];   //html
    //        $.ajax({
    //            type: 'post',
    //            url: '/api/leavemessage/GetLeaveMessageByPage',
    //            contentType: 'application/json',
    //            data: JSON.stringify({ "currentIndex": page, "pageSize": 15 }),
    //            datatype: 'json',
    //            success: function (res) {
    //                if (res.Success) {
    //                    lis.push(res.Data);
    //                    pages = res.SubCode;
    //                    next(lis.join(''), page < pages);
    //                    bindReplyBtn();
    //                } else {
    //                    layer.msg('获取数据失败', { icon: 2 });
    //                }
    //            },
    //            error: function (e) {
    //                layer.msg(e.responseText);
    //            }
    //        });
    //    }
    //});
    ////监听留言提交
    //form.on('submit(formLeaveMessage)', function (data) {
    //    var index = layer.load(1);
    //    $.ajax({
    //        type: 'post',
    //        url: '/About/LeaveMessage',
    //        async: false,
    //        data: "content=" + data.field.editorContent,
    //        success: function (outResult) {
    //            layer.close(index);
    //            if (outResult.Success) {
    //                layer.msg(outResult.Message, { icon: 6 });
    //                setTimeout(function () {
    //                    location.reload(true);
    //                }, 500);
    //            } else {
    //                if (outResult.Message != undefined) {
    //                    layer.msg(outResult.Message, { icon: 5 });
    //                } else {
    //                    layer.msg('程序异常，请重试或联系作者', { icon: 5 });
    //                }
    //            }
    //        },
    //        error: function (outResult) {
    //            layer.close(index);
    //            layer.msg("请求异常", { icon: 2 });
    //        }
    //    });
    //    return false;
    //});

    //function bindReplyBtn() {
    //    $('.btn-reply').unbind('click');
    //    $('.btn-reply').click(function () {
    //        $(this).parent('p').parent('.comment-parent').siblings('.replycontainer').toggleClass('layui-hide');
    //        if ($(this).text() == '回复') {
    //            $(this).text('收起')
    //        } else {
    //            $(this).text('回复')
    //        }
    //    });

    //    //监听留言回复提交
    //    form.on('submit(formReply)', function (data) {
    //        var index = layer.load(1);
    //        $.ajax({
    //            type: 'post',
    //            url: '/About/LeaveMessageReply',
    //            async: false,
    //            data: JSON.stringify(data.field),
    //            success: function (outResult) {
    //                layer.close(index);
    //                if (outResult.Success) {
    //                    layer.msg(outResult.Message, { icon: 6 });
    //                    setTimeout(function () {
    //                        location.reload(true);
    //                    }, 500);
    //                } else {
    //                    if (outResult.Message != undefined) {
    //                        layer.msg(outResult.Message, { icon: 5 });
    //                    } else {
    //                        layer.msg('程序异常，请重试或联系作者', { icon: 5 });
    //                    }
    //                }
    //            },
    //            error: function (outResult) {
    //                layer.close(index);
    //                layer.msg("请求异常", { icon: 2 });
    //            }
    //        });
    //        return false;
    //    });
    //}
});