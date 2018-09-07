var firstPost = 0;
var pageIndex = 1;
var pageSize = 8
layui.use(['jquery','laypage', 'layer', 'form'], function () {
    var laypage = layui.laypage, $ = layui.$, form = layui.form;
    
    initilAnnouncement(pageIndex, pageSize);

    //监听状态
    form.on("checkbox(Enable)", function (data) {
        var index = layer.load(1);
        var value = data.elem.checked;
        $.ajax({
            dataType: "json",
            type: "post",
            url: "/admin/announcement/setenable",
            data: { id: data.value },
            success: function (data) {
                if (data.Code == 0) {
                    if (data.elem.checked) {
                        data.elem.checked = false;   
                    } else {
                        data.elem.checked = true;   
                    }
                    layer.msg(data.Message);
                };
                layer.closeAll();
                form.render();  //重新渲染  
            },
            error: function (e) {
                data.elem.checked = !value;
                form.render();
                layer.msg(e.responseText);
                layer.close(index);
            }
        });
    })
    
});
//加载数据
function initilAnnouncement(pageIndex, pageSize) {
    var laypage = layui.laypage, $ = layui.$, form = layui.form;
    var index = layer.load(1);
    var page = pageIndex, rows = pageSize
    $.ajax({
        type: 'post',
        url: '/admin/Announcement/List',
        data: { 'page': page, 'rows': rows },
        datatype: 'json',
        success: function (res) {
            layer.close(index);
            if (res.code == 0) {
                var data = res.data;
                var html = '';
                html += '<table class="layui-table" lay-even>';
                html += '<colgroup><col width="180"><col><col width="180"><col width="180"><col width="100"><col width="100"></colgroup>';
                html += '<thead><tr><th>发布日期</th><th>内容</th><th>级别</th><th>启用</th><th colspan="2">操作</th></tr></thead>';
                html += '<tbody>';
                if (data.length <= 0) {
                    html += '<tr><td>暂无数据</td><td>暂无数据</td><td>暂无数据</td><td>暂无数据</td><td>暂无数据</td><td>暂无数据</td></tr>';
                } else {
                    for (var i = 0; i < data.length; i++) {
                        var item = data[i];
                        html += "<tr>";
                        html += "<td>" + new Date(item.CreateTime).Format("yyyy-MM-dd") + "</td>";
                        html += "<td>" + item.Content + "</td>";
                        var leave;
                        switch (item.Level) {
                            case 0:
                                leave = "普通";
                                break;
                            case 1:
                                leave = "一般";
                                break;
                            case 2:
                                leave = "重要";
                                break;
                            default:
                                leave = "未知";
                                break;
                        }
                        html += "<td>" + leave + "</td>";
                        html += "<td>" + "<form class='layui-form'><input type='checkbox' name='Enable' value='" + item.Id + "' lay-filter='Enable' title='启用'" + checkBoxStatus(item.Enable) + " /></form>" + "</td>";
                        html += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="editAnnouncement(\'' + item.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                        html += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="deleteAnnouncement(\'' + item.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                        html += "</tr>";
                    }
                }

                html += '</tbody>';
                html += '</table>';

                $('#announcementIndex').html(html);
                form.render();
                $('#announcementConsole,#announcementList').attr('style', 'display:block'); //显示FiledBox
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
                                initilAnnouncement(obj.curr, obj.limit);
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
//发布公告
//改变窗口大小时，重置弹窗的高度，防止超出可视区域（如F12调出debug的操作）
function addAnnouncement() {
    var $ = layui.$;
    $(window).one("resize", function () {
        var index = layui.layer.open({
            title: " ",
            type: 2,
            content: ["/admin/Announcement/Add",'no'],
            success: function (layero, index) {
                setTimeout(function () {
                    layui.layer.tips('点击此处返回公告列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
    }).resize();
}


//编辑公告
function editAnnouncement(announcementId) {
    var $ = layui.$;
    $(window).one("resize", function () {
        var index = layui.layer.open({
            title: " ",
            type: 2,
            content: ["/admin/announcement/edit?id=" + announcementId, 'no'],
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

//删除公告
function deleteAnnouncement(announcementId) {
    var layer = layui.layer, $ = layui.$;
    layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            $.post("/admin/announcement/Delete", { id: announcementId }, function (data) {
                
            })
            $.ajax({
                url: '/admin/announcement/Delete',
                data: { id: announcementId },
                type: 'post',
                success: function (res) {
                    if (res.Code == 1) {
                        firstPost = 0;
                        initilAnnouncement(pageIndex, pageSize);

                    } else {
                        layer.msg(res.Message);
                    }
                    layer.close(index);
                },
                error: function (e) {
                    layer.msg(e.responseText);
                }
            })

        }, 2000);
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