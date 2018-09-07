var firstPost = 0;
var pageIndex = 1;
var pageSize = 8
layui.use(['jquery','laypage', 'layer', 'form'], function () {
    var form = layui.form;
   
    initilFriendslinks(pageIndex, pageSize);

    form.on("checkbox(Enable)", function (data) {
        var index = layer.load(1);
        var value = data.elem.checked;
        $.ajax({
            url: '/admin/Friendslinks/SetEnable',
            type: 'post',
            data: { id: data.value },
            success: function (res) {
                if (res.Code == 0) {
                    if (data.elem.checked) {
                        data.elem.checked = false;   
                    }
                    else {
                        data.elem.checked = true;
                    }  
                    layer.msg(res.Message)
                }
                layer.closeAll();
                form.render();  //重新渲染        
            },
            error: function (e) {
                data.elem.checked = !value;
                layer.msg(e.responseText);
                form.render();
                layer.close(index);
            }
        });
    })
    
});
function initilFriendslinks(pageIndex, pageSize) {
    var laypage = layui.laypage, $ = layui.$, form = layui.form;
    var index = layer.load(1);
    var page = pageIndex, rows = pageSize;
    $.ajax({
        type: 'post',
        url: '/admin/friendslinks/List',
        data: { 'page': page, 'rows': rows },
        datatype: 'json',
        success: function (res) {
            layer.close(index);
            if (res.code == 0) {
                var data = res.data;
                var html = '';
                html += '<table class="layui-table" lay-even>';
                html += '<colgroup><col width="180"><col><col width="180"><col width="100"><col width="100"></colgroup>';
                html += '<thead><tr><th>链接名称</th><th>链接地址</th><th>启用</th><th colspan="2">操作</th></tr></thead>';
                html += '<tbody>';
                if (data.length <= 0) {
                    html += '<tr><td colspan="5">暂无数据</td></tr>';
                } else {
                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        html += "<tr>";
                        html += "<td>" + item.WebName + "</td>";
                        html += "<td>" + item.Url + "</td>";
                        html += "<td>" + "<form class='layui-form'><input type='checkbox' name='Enable' value='" + item.Id + "' lay-filter='Enable' title='启用'" + checkBoxStatus(item.Enable) + " /></form>" + "</td>";
                        html += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="editFriendslinks(\'' + item.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                        html += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="deleteFriendslinks(\'' + item.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                        html += "</tr>";
                    }
                }

                html += '</tbody>';
                html += '</table>';

                $('#friendslinksIndex').html(html);
                form.render();
                $('#friendslinksConsole,#friendslinksList').attr('style', 'display:block'); //显示FiledBox
                if (firstPost == 0) {
                    //第一次初始化加载分页
                    laypage.render({
                        elem: 'page'
                        , count: res.count
                        , limit: rows
                        , curr: page
                        , limits: [8]
                        , layout: ['count', 'prev', 'page', 'next', 'limit', 'skip']
                        , jump: function (obj, first) {
                            if (!first) {
                                initilFriendslinks(obj.curr, obj.limit);
                            }
                        }
                    });
                    firstPost++;
                }

            } else {
                layer.msg(res.Message);

            }
        },
        error: function (e) {
            layer.close(index);
            layer.msg(e.responseText);
        }
    });
}
//添加友链
function addFriendslinks() {
    var $ = layui.$;
    $(window).one("resize", function () {
        var index = layui.layer.open({
            title: "添加友链",
            type: 2,
            content: ["/admin/Friendslinks/Add",'no'],
            success: function (layero, index) {
                setTimeout(function () {
                    layui.layer.tips('点击此处返回友链列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
    }).resize();
}


//编辑友链
function editFriendslinks(friendslinksId) {
    var $ = layui.$;
    $(window).one("resize", function () {
        var index = layui.layer.open({
            title: "编辑友链",
            type: 2,
            content: ["/admin/Friendslinks/edit?id=" + friendslinksId, 'no'],
            success: function (layero, index) {
                setTimeout(function () {
                    layui.layer.tips('点击此处返回友链列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
    }).resize();
}

//删除友链
function deleteFriendslinks(friendslinksId) {
    var layer = layui.layer, $ = layui.$;
    layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {

            $.ajax({
                url: '/admin/friendslinks/Delete',
                data: { id: friendslinksId },
                type: 'post',
                success: function (res) {
                    if (res.Code == 1) {
                        firstPost = 0;
                        initilFriendslinks(pageIndex, pageSize);
                    } else {
                        layer.msg(res.Message);
                    }
                    layer.close(index);
                },
                error: function (e) {
                    layer.close(index);
                    layer.msg(e.responseText);
                }
            })

        }, 1000);
    });
}

//返回复选框状态
function checkBoxStatus(value) {
    if (value == true) {
        return "checked";
    } else {
        return "";
    }
};