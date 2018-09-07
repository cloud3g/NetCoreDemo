//@ sourceURL=user-index.js

var pageIndex = 1;
var pageSize = 8;
layui.use(['layer', 'form','laypage'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form;

    initil(pageIndex, pageSize);

    //添加用户
    $('#addUser').click(function () {
        var index = layer.load(1);
        $.ajax({
            type: 'get',
            url: '/admin/sysuser/add',
            success: function (data) {
                layer.close(index);
                $('#content').html(data);
                $('#BoxTitle').text("添加用户");
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

 

    form.render();
});

//页数据初始化
//currentIndex：当前也下标
//pageSize：页容量（每页显示的条数）
function initil(pageindex, pagesize, query) {
    var index = layer.load(1);
    var $ = layui.jquery;
    var form = layui.form;
    var laypage = layui.laypage;
    $.ajax({
        type: 'post',
        url: '/admin/sysuser/getlist',
        data: { page: pageindex, rows: pagesize, query: query },
        dataType: 'json',
        success: function (res) {
            layer.close(index);
            var list = res.data;
            var dataHtml = '';
            dataHtml += '<table style="" class="layui-table" lay-even>';
         //   dataHtml += '<colgroup><col width="20%"><col width="10%"><col width="10%"><col width="10%"><col width="10%"><col width="15%"></colgroup>';
            dataHtml += '<thead><tr><th>昵称</th><th>用户名</th><th>密码</th><th>角色</th><th>最后登陆IP</th><th colspan="3">操作</th></tr></thead>';
            dataHtml += '<tbody>';
            //遍历文章集合
            if (list.length != 0) {
                $.each(list, function (index, value) {
                    dataHtml += '<tr>'
                        + '<td>' + value.RealName + '</td>'
                        + '<td>' + value.LoginName + '</td>'
                        + '<td>' + value.Password + '</td>';
                    dataHtml += '<td>' + wipeNull(value.Roles, "") + '</td>';
                    dataHtml += '<td>' + wipeNull(value.IP,"") + '</td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="editUser(\'' + value.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small" onclick="allotRole(\'' + value.Id + '\')"><i class="layui-icon">&#xe613;</i></button></td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="delUser(\'' + value.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                    dataHtml  + '</tr>';

                });

            } else {
                dataHtml += '<tr><td colspan="7">暂无数据</td></tr>';
            }
            dataHtml += '</tbody>';
            dataHtml += '</table>';
            dataHtml += '<div id="page"></div>';

            $('#content').html(dataHtml);

            form.render('checkbox');  //重新渲染CheckBox，编辑和添加的时候
            $('#Console,#List').attr('style', 'display:block'); //显示FiledBox
            $('#BoxTitle').text("用户列表");    //FileBox标题改为文章列表
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

//编辑用户
function editUser(userId) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/sysuser/edit',
        data: 'id=' + userId,
        success: function (data) {
            layer.close(index);
            $('#BoxTitle').text("修改文章");
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
function delUser(userId) {
    var form = layui.form;
    layer.confirm('确定删除改用户？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {

            $.ajax({
                url: "/admin/sysuser/Delete",
                type: "post",
                data: { id: userId },
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

function allotRole(userId) {
    var layer = layui.layer, $ = layui.$;
    var index = layer.load(1);
    $.ajax({
        url: "/admin/sysuser/allotrole?userId=" + userId,
        type: "get",
        success: function (res) {
            layer.close(index);
            var allotRole = layer.open({
                type: 1,
                title: '分配角色',
                area: '500px',
                anim: 2,
                content: res,
                btn: ['保存'],
                yes: function (index, layero) {
                    var cbValue = "";
                    $(layero).find(":checkbox:checked").each(function () {
                        cbValue += $(this).val() + ","
                    });
                    var ids = new Array(cbValue.substring(0, cbValue.length - 1));
                    $.ajax({
                        url: "/admin/sysuser/allotrole",
                        type: 'post',
                        dataType: 'json',
                        data: { roleIds: ids, userId: userId },
                        success: function (res) {
                            if (res.Code == 1) {
                                layer.close(allotRole);
                                initil(pageIndex, pageSize);

                            } else {
                                layer.msg(res.Message);
                            }
                        },
                        error: function (e) {
                            layer.close(allotRole);
                            layer.msg(e.responseText);
                        }
                    });
                }
            });
        },
        error: function (e) {
            layer.msg(e.responseText);
            layer.close(index);
        }
    });
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