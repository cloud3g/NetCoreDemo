//@ sourceURL:category-index.js


layui.config({
    base: '/lib/layui/extend/'
}).use(['laytpl', 'treegrid', 'jquery', 'layer','form'], function () {
    var laytpl = layui.laytpl,
        treegrid = layui.treegrid,
        layer = layui.layer,
        $ = layui.$, form = layui.form;
    treegrid.config.render = function (viewid, data) {
        var view = document.getElementById(viewid).innerHTML;
        return laytpl(view).render(data) || '';
    };

    $.post("/admin/category/getlist", function (rows) {
        var tree1 = treegrid.createNew({
            elem: 'List',
            view: 'view',
            data: { rows: rows },
            parentid: 'pid',
            singleSelect: true
        });
        tree1.build();
        form.render();
        tree1.collapseAll(undefined);
        $('.layui-btn').on('click', function () {
            var id,
                row = tree1.getRow();
            if (row !== null)
                id = row.id;
            switch ($(this).attr('lay-filter')) {
                case 'add': {
                    add(id);
                } break;
                case 'edit': {
                    edit(id);
                } break;
                case 'delete': {
                    del(id);
                } break;
            }
        });
    });

    form.on("checkbox(enable)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).attr("data-id");
        $.ajax({
            url: '/admin/category/setenable',
            data: { id: id, value: value },
            type: 'post',
            success: function (res) {
                if (res.Code != 1) {
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
        })
    })
    form.on("checkbox(isNav)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).attr("data-id");
        $.ajax({
            url: '/admin/category/setnav',
            data: { id: id, value: value },
            type: 'post',
            success: function (res) {
                if (res.Code != 1) {
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
        })
    })
    function edit(id) {
        if (id) {
            var index = layui.layer.open({
                title: " ",
                type: 2,
                content: '/admin/category/edit?id=' + id,
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回栏目列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                }
            })
            layui.layer.full(index);
        } else {
            layer.msg('请选择要编辑的项', {
                icon: 2,
                time: 2000 //2秒关闭（如果不配置，默认是3秒）
            });
        };
    };
    function add(id) {
        id = id ? id : 0;
        var index = layui.layer.open({
            title: " ",
            type: 2,
            content: '/admin/category/add?id=' + id,
            success: function (layero, index) {
                setTimeout(function () {
                    layui.layer.tips('点击此处返回栏目列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)
            }
        })
        layui.layer.full(index);
        
        
    };
    function del(id) {
        if (id) {
            layer.confirm("确定删除？", { icon: 3, title: '提示' }, function (index) {
                $.ajax({
                    url: '/admin/category/delete',
                    data: { id: id },
                    type: 'post',
                    success: function (res) {
                        layer.close(index);
                        if (res.Code == 1) {
                            window.location.reload();
                            
                        } else {
                            layer.msg(res.Message, {
                                time: 2000 //2秒关闭（如果不配置，默认是3秒）
                            });
                        }
                    },
                    error: function (e) {
                        layer.close(index);
                        layer.msg(e.responseText);
                    }
                });
                
            })
           
        } else {
            layer.msg('请选择要删除的项', {
                icon: 2,
                time: 2000 //2秒关闭（如果不配置，默认是3秒）
            });
        };
    };
});
