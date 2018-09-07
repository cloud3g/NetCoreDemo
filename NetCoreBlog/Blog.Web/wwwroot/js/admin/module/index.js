var tree1;
var actionGrid;
layui.config({
    base: '/lib/layui/extend/'
}).use(['laytpl', 'treegrid', 'jquery', 'layer','grid','form'], function () {
    var laytpl = layui.laytpl,
        treegrid = layui.treegrid,
        layer = layui.layer,
        $ = layui.$,
        grid = layui.grid,
        form = layui.form;
    treegrid.config.render = function (viewid, data) {
        var view = document.getElementById(viewid).innerHTML;
        return laytpl(view).render(data) || '';
    };

    initilSysModule();
    initlActionGrid();
    //监听模块菜单点击
    form.on("checkbox(isMenu)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).attr("data-id");
        $.ajax({
            url: "/admin/module/setMenu",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (data) {
                if (data.Code == 1) {

                } else {
                    layer.msg(data.Message);
                    data.elem.checked = !value;
                    form.render();
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
                data.elem.checked = !value;
                form.render();

            }
        });
        layui.stope(event);
    });
    //监听标识启用点击
    form.on("checkbox(actionEnable)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).attr("data-id");
        $.ajax({
            url: "/admin/module/SetActionEnable",
            type: "post",
            dataType: "json",
            data: { id: id },
            success: function (data) {
                if (data.Code == 1) {

                } else {
                    layer.msg(data.Message);
                    data.elem.checked = !value;
                    form.render();
                }
            },
            error: function (e) {
                layer.msg(e.responseText);
                data.elem.checked = !value;
                form.render();

            }
        });
    });

});

function initilSysModule() {
    var laypage = layui.laypage, $ = layui.$, treegrid = layui.treegrid, form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'post',
        url: '/admin/module/list',
        datatype: 'json',
        success: function (res) {
            layer.close(index);
            if (res.code == 0) {
                var data = res.data;
                if (data.length > 0) {
                        tree1 = treegrid.createNew({
                        id: 'id',
                        elem: 'modeuleIndex',
                        view: 'view',
                        data: { rows: data },
                        parentid: 'pid',
                        singleSelect: true,
                        rowClick: function (row) {     //行点击事件
                            rowClick(row);
                        }
                    });
                    tree1.build();
                    var row = tree1.getRow();                    
                    var id = row == null ? undefined : row.id;          
                    tree1.collapseAll(id);
                    form.render();
                } else {
                    layer.msg("没有数据！");
                }
            
                
            }
        }
    });


};
function initlActionGrid(data) {
    var grid = layui.grid, form = layui.form;
    grid.createNew({
        elem: 'grid_test',
        view: 'grid',
        data: { rows: data },
    }).build();
    form.render();
}
function add() {
    var row = tree1.getRow();
    var id = row == null ? 0 : row.id;
    var $ = layui.jquery;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/module/addmodule',
        data: { id: id },
        success: function (data) {
            layer.close(index);
          var addmodule=   layer.open({
                type: 1,
                area: '700px',
               
                title: '添加模块',
                closeBtn: 1,
                shade: 0.6,
                shadeClose: false,
                skin: 'layer-css',
                content: data
            });
          
        },
        error: function (e) {
            layer.close(index);
            //layer.msg(e.responseText);;
        }
    });
}
function addAction() {
    var row = tree1.getRow();
    if (!row) {
        layer.msg("请选择要添加标识的模块！");
        return;
    };
    if (row.Type == 0) {
        layer.msg("目录项不能添加标识！");
        return;
    };
    var index = layer.load(1);
    $.ajax({
        url: "/admin/module/addAction?moduleId=" + row.id,
        type: "get",
        success: function (res) {
            layer.close(index);
            layer.open({
                type: 1,
                area: '300px',
                title: '添加[' + row.Name + ']标识',
                closeBtn: 1,
                shade: 0.6,
                shadeClose: false,
                skin: 'layer-css',
                content: res
            });
 
        },
        error: function (r, s, t) {
            layer.close(index);
            layer.msg("请求出错,请联系管理员！错误码:" + r.status);
        }
    });
}

function rowClick(row) {
    var layer = layui.layer, $ = layui.$;
    //如果是目录，则清除action表
    if (row.Type == 0) {
        initlActionGrid("");
        return;
    }
    var index = layer.load(1);
    $.ajax({
        url: "/admin/module/actionlist",
        data: { moduleId: row.id },
        dataType: "json",
        success: function (res) {
            layer.close(index);
            initlActionGrid(res.data)
        }
    });


}
function edit(id) {
    var $ = layui.jquery;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/module/editmodule',
        data: { id: id },
        success: function (data) {
            layer.close(index);
            var addmodule = layer.open({
                type: 1,
                area: '700px',

                title: '编辑模块',
                closeBtn: 1,
                shade: 0.6,
                shadeClose: false,
                skin: 'layer-css',
                content: data
            });

        },
        error: function (e) {
            layer.close(index);
            //layer.msg(e.responseText);;
        }
    });
}
function editAction(id) {
    var $ = layui.jquery;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/module/editaction',
        data: { id: id },
        success: function (data) {
            layer.close(index);
            var addmodule = layer.open({
                type: 1,
                area: '700px',
                title: '编辑标识',
                closeBtn: 1,
                shade: 0.6,
                shadeClose: false,
                skin: 'layer-css',
                content: data
            });

        },
        error: function (e) {
            layer.close(index);
            //layer.msg(e.responseText);;
        }
    });
}
function del(id) {
    var layer = layui.layer, $ = layui.$;
    layer.confirm("确定删除该模块吗？", function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            $.ajax({
                url: "/admin/module/delete",
                type: "post",
                dataType: "json",
                data: { id: id },
                success: function (res) {
                    if (res.Code == 1) {
                        window.location.reload();

                    } else {
                        layer.msg(res.Message);
                    }
                    layer.close(index);
                },
                error: function (e) {
                    layer.msg(e.responseText);
                    layer.close(index);
                }
            });

        }, 2000);

    })
}

function delAction(id) {
    var layer = layui.layer, $ = layui.$;
    layer.confirm("确定删除该标识吗？", function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            $.ajax({
                url: "/admin/module/DeleteAction",
                type: "post",
                dataType: "json",
                data: { id: id },
                success: function (res) {
                    if (res.Code == 1) {
                        window.location.reload();

                    } else {
                        layer.msg(res.Message);
                    }
                    layer.close(index);
                },
                error: function (e) {
                    layer.msg(e.responseText);
                    layer.close(index);
                }
            });

        }, 2000);

    })
}