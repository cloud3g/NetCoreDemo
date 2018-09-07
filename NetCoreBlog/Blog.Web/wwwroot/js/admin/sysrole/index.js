//@ sourceURL=role-index.js

var moduleGrid;
var actionGrid;
var pageIndex = 1;
var pageSize = 8;
var firstPage = 1;
layui.config({
    base: '/lib/layui/extend/'
}).use(['laytpl','laypage', 'treegrid', 'jquery', 'layer', 'grid', 'form'], function () {
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
    initilSysRole(pageIndex, pageSize);
    initilSysModule();
    initlActionGrid();
    //监听操作点击
    form.on("checkbox(ckOpt)", function (data) {
        var value = data.elem.checked;
        var id = $(data.elem).attr("data-id");
        var ids;
     
        if ($('#actionIds').val() == "") {
            ids = new Array();
        } else {
            ids = $('#actionIds').val().split(",")
        }
        if (value) {
            if ($.inArray(id, ids) == -1) {
                ids.push(id);
            }
        } else {
            ids.splice($.inArray(id, ids), 1); 
        }
        $('#actionIds').val(ids.join(","));

    });

    $("#savePerm").click(function () {
        var roleId = $("#roleId").val();
        var moduleId = $("#moduleId").val();
        var actionIds = $("#actionIds").val();
    
        if (roleId == "" || roleId == null) {
            layer.msg("请选择角色！");
            return;
        }
        if (moduleId == "" || moduleId == null) {
            layer.msg("请选择模块！");
            return;
        }
        var moduleRow = moduleGrid.getRow();
        if (moduleRow.Type == 0) {
            layer.msg("目录模块不能设置权限！");
            return;
        }
        var index = layer.load(1);
        $.ajax({
            url: "/admin/sysrole/allot",
            type: 'post',
            data: { roleId: roleId, moduleId: moduleId, actionIds: actionIds },
            dataType: "json",
            success: function (res) {
                layer.msg(res.Message);
                layer.close(index);
            },
            error: function (e) {
                layer.close(index);
                layer.msg(e.responseText);
            }

        });
    })
    $("#addRole").click(function () {
        var index = layer.load(1);
        $.ajax({
            url: '/admin/sysrole/add',
            type: 'get',
            success: function (data) {
                layer.close(index);
                layer.open({
                    type: 1,
                    area: '400px',
                    title: '添加角色',
                    shade: 0.6,
                    skin: 'layer-css',
                    content: data
                });
            },
            error: function (e) {
                layer.close(index);
                layer.msg(e.responseText);
            }
        });
    })
});


function initilSysRole(pageindex,pagesize) {
    var grid = layui.grid, form = layui.form, laypage = layui.laypage;
    var index = layer.load(1);
    
    $.ajax({
        type: 'post',
        url: '/admin/sysrole/RoleList',
        data: { page: pageindex, rows: pagesize },
        datatype: 'json',
        success: function (res) {    
            form.render();
            layer.close(index);
            if (res.code == 0) {
                var data = res.data;
                if (data.length > 0) {
                    var roleList = grid.createNew({
                        id:'Id',
                        elem: 'roleList',
                        view: 'roleView',
                        data: { rows: data },
                        singleSelect: true,
                        rowClick: function (row) {     //行点击事件
                            roleRowClick(row);
                        }
                    }).build();
                    var pegDiv = "<div id='page'></div>";
                     
                    $('#roleList .grid-content').append(pegDiv)
                    laypage.render({
                        elem: 'page'
                        , count: res.count
                        , limit: pagesize
                        , curr: pageindex
                        , layout: ['count', 'prev', 'page', 'next',  'skip']
                        , jump: function (obj, first) {
                            if (!first) {
                                pageIndex = obj.curr;
                                initilSysRole(obj.curr, obj.limit);
                            }
                        }
                    });

                    form.render();
                } else {
                    layer.msg("没有数据！");
                }
            }
        }
    });


};

function initilSysModule() {
    var laypage = layui.laypage, $ = layui.$, treegrid = layui.treegrid, form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'post',
        url: '/admin/sysrole/modulelist',
        datatype: 'json',
        success: function (res) {
            layer.close(index);
            if (res.code == 0) {
                var data = res.data;
                if (data.length > 0) {
                    moduleGrid = treegrid.createNew({
                        id: 'id',
                        elem: 'moduleList',
                        view: 'moduleView',
                        data: { rows: data },
                        parentid: 'pid',
                        singleSelect: true,
                        rowClick: function (row) {     //行点击事件
                            moduleRowClick(row);
                        }
                    });
                    moduleGrid.build();
                    var row = moduleGrid.getRow();
                    var id = row == null ? undefined : row.id;
                    moduleGrid.collapseAll(id);
                    form.render();
                    
                } else {
                    layer.msg("没有数据！");
                }


            }
        }
    });


};
function initlActionGrid(data) {
    var grid = layui.grid, form = layui.form, $ = layui.$;
    $('#actionIds').val("");
    actionGrid=  grid.createNew({
        elem: 'actionList',
        view: 'actionView',
        data: { rows: data },
    }).build();
    form.render();
    if ($('div input[type=checkbox]:checked').length > 0) {
        var cbs = $('div input[type=checkbox]:checked');
        var ids = new Array();
        $.each(cbs, function (index, value) {
            var id = $(value).attr("data-id");
            ids.push(id);
        })
        $('#actionIds').val(ids.join(","));
    }
}
function moduleRowClick(row) {
    var layer = layui.layer, $ = layui.$;
    var roleName = $('.grid-body .roleName').html();
    if ($('#roleId').val() == "") {
        $('.grid-body .moduleName').html(row.Name);
        $('#moduleId').val(row.id);
        return;
    }

    var index = layer.load(1);
    $.ajax({
        url: "/admin/sysrole/actionlist",
        data: { moduleId: row.id, roleId: $('#roleId').val() },
        dataType: "json",
        success: function (res) {
            layer.close(index);
            initlActionGrid(res.data);
            $('.grid-body .moduleName').html(row.Name);
            $('.grid-body .roleName').html(roleName);
            $('#moduleId').val(row.id);
        }
    });
}

function roleRowClick(row) {
    var layer = layui.layer, $ = layui.$;

    var moduleRow = moduleGrid.getRow();
    if (!moduleRow) {
        $('.grid-body .roleName').html(row.Name);
    } else {
        var index = layer.load(1);
        $.ajax({
            url: "/admin/sysrole/actionlist",
            data: { moduleId: moduleRow.id, roleId: row.Id },
            dataType: "json",
            success: function (res) {
                layer.close(index);
                initlActionGrid(res.data);
                $('.grid-body .moduleName').html(moduleRow.Name);
                $('.grid-body .roleName').html(row.Name);
                $('#moduleId').val(moduleRow.id);
            }
        });
    }
   
    $('#roleId').val(row.Id);
}
function edit(id, e) {
    var index = layer.load(1);
    $.ajax({
        url: '/admin/sysrole/edit',
        data: { id: id },
        type: 'get',
        success: function (data) {
            layer.close(index);
            layer.open({
                type: 1,
                area: '400px',
                title: '编辑角色',
                shade: 0.6,
                skin: 'layer-css',
                content: data
            });
        },
        error: function (e) {
            layer.close(index);
            layer.msg(e.responseText);
        }
    });
    //阻止冒泡
    layui.stope(e);
}

function del(id) {
    var layer = layui.layer, $ = layui.$;
    layer.confirm("确定删除该角色吗？", function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            $.ajax({
                url: "/admin/sysrole/delete",
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
