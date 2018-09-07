

var pageIndex = 1;
var pageSize = 8;
layui.use(['layer', 'form', 'laypage'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form;

    initil(pageIndex, pageSize);

    //添加公众号
    $('#add').click(function () {
        var index = layer.load(1);
        $.ajax({
            type: 'get',
            url: '/admin/officialaccounts/add',
            success: function (data) {
                layer.close(index);
                $('#content').html(data);
                $('#BoxTitle').text("添加公众号");
                $('#Console').attr('style', 'display:none');
                $('#listBack').bind('click', function () {
                    initil(pageIndex, pageSize);
                });
                layui.form.render();
            },
            error: function (e) {
                layer.close(index);
                layer.msg(e.responseText);
            }
        });
    });
    $('#GetToken').click(function () {
        $.ajax({
            url: "/admin/officialaccounts/GetToken",
            type: "post",
            success: function (res) {
                if (res) {
                    layer.msg("更新成功！");
                } else {
                    layer.msg("更新成功！");
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
            }
        });
    });
    //监听启用
    form.on("checkbox(enable)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).val();
        $.ajax({
            url: '/admin/officialaccounts/setenable',
            type: 'post',
            data: { id: id },
            success: function (res) {
                if (res.Code == 1) {
                    
                } else {
                    data.elem.checked = !value;
                    form.render();
                    layer.msg(res.Message);
                }
            },
            error: function (e) {
                data.elem.checked = !value;
                form.render();
                layer.msg(e.responseText);
            }
        });

    })
 
    //监听默认
    form.on("checkbox(isDefault)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).val();
         
        if (value) {
            $.ajax({
                url: '/admin/officialaccounts/setdefault',
                type: 'post',
                data: { id: id },
                success: function (res) {
                    if (res.Code == 1) {
                        $(".layui-table input[name=isDefault]").attr("checked", false);
                        data.elem.checked = value;
                        form.render();
                    } else {
                        data.elem.checked = !value;
                        form.render();
                        layer.msg(res.Message);
                    }
                },
                error: function (e) {
                    data.elem.checked = !value;
                    form.render();
                    layer.msg(e.responseText);
                }
            });
        } else {
            data.elem.checked = !value;
            form.render();
        }
     

    })
    form.render();
});

//页数据初始化
//currentIndex：当前也下标
//pageSize：页容量（每页显示的条数）
function initil(pageindex, pagesize) {
    var index = layer.load(1);
    var $ = layui.jquery;
    var form = layui.form;
    var laypage = layui.laypage;
    $.ajax({
        type: 'post',
        url: '/admin/officialaccounts/List',
        data: { page: pageindex, rows: pagesize},
        dataType: 'json',
        success: function (res) {

            layer.close(index);
            var list = res.data;
            var dataHtml = '';
            dataHtml += '<table style="" class="layui-table" lay-even>';
            dataHtml += '<thead><tr><th>公众号Id</th><th>名称</th><th>公众号</th><th>说明</th><th colspan="2">选项</th><th>类别</th><th colspan="2">操作</th></tr></thead>';
            dataHtml += '<tbody>';
            
            if (list.length != 0) {
      
                $.each(list, function (index, value) {
                    dataHtml += '<tr>'
                        + '<td>' + value.OfficalId + '</td>'
                        + '<td>' + value.OfficalName + '</td>'
                        + '<td>' + value.OfficalCode + '</td>'
                   
                        + '<td>' + value.Remark + '</td>'
                        + '<td><input type="checkbox" name="enable" value="' + value.Id + '" lay-filter="enable" title="启用"' + checkBoxStatus(value.Enable) + ' ></td>'
                        + '<td><input type="checkbox" name="isDefault" value="' + value.Id + '" lay-filter="isDefault" title="默认"' + checkBoxStatus(value.IsDefault) + ' ></td>'
                        + '<td>' + initCategory(value.Category) + '</td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="edit(\'' + value.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="del(\'' + value.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                    dataHtml  + '</tr>';

                });

            } else {
                
                dataHtml += '<tr><td colspan="15">暂无数据</td></tr>';
            }
            dataHtml += '</tbody>';
            dataHtml += '</table>';
            dataHtml += '<div id="page"></div>';

            $('#content').html(dataHtml);

            form.render('checkbox');  //重新渲染CheckBox，编辑和添加的时候
            $('#Console,#List').attr('style', 'display:block'); //显示FiledBox
            $('#BoxTitle').text("公众号列表");    //FileBox标题改为文章列表
            laypage.render({
                elem: 'page'
                , count: res.count
                , limit: pagesize
                , curr: pageindex
                , limits: [8, 20, 30, 40, 50]
                , layout: ['prev', 'page', 'next', 'count', 'limit', 'skip']
                , jump: function (obj, first) {
                    if (!first) {
                        pageIndex = obj.curr;
                        pageSize = obj.limit;
                        initil(obj.curr, obj.limit);
                    }
                }
            });
            form.render();
        }
    })
}

//编辑公众号
function edit(id) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/officialaccounts/edit',
        data: 'id=' + id,
        success: function (data) {
            layer.close(index);
            $('#BoxTitle').text("修改公众号");
            $('#content').html(data);
            $('#Console').attr('style', 'display:none');
            $('#listBack').bind('click', function () {
                initil(pageIndex, pageSize);
            });
             form.render();
        },
        error: function (e) {
            layer.close(index);
            layer.msg(e.responseText);
        }
    });
}

//删除文章
function del(id) {
    var form = layui.form;
    layer.confirm('确定删除改公众号？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {

            $.ajax({
                url: "/admin/officialaccounts/Delete",
                type: "post",
                data: { id: id },
                success: function (res) {
                    if (res.Code == 1) {
                        initil(pageIndex, pageSize);
                        form.render();
                    }
                    layer.close(index);
                    layer.msg(data.Message);
                },
                error: function (e) {
                    layer.msg(e.responseText);
                }
            });
        }, 2000);
    })
}

//去掉显示null
function wipeNull(oldStr, newStr) {
    if (newStr == undefined) {
        newStr = "";

    }
    if (oldStr == null || oldStr == undefined) {
        return newStr;
    } else {
        return oldStr;
    }
}
//返回复选框状态
function checkBoxStatus(value) {
    if (value == true) {
        return "checked";
    } else {
        return "";
    }
}
function initCategory(value) {
    switch (value) {
        case 0: return "媒体号"; break;
        case 1: return "企业号"; break;
        case 2: return "个人号"; break;
        case 3: return "测试号"; break;
        default: return "未知类型";
    }
}