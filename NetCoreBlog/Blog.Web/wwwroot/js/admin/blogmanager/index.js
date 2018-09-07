//@ sourceURL=article-index.js

var pageIndex = 1;
var pageSize = 8;
layui.use(['layer', 'form','laypage'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form;

    initil(pageIndex, pageSize);

    //    //全选
    form.on('checkbox(allChoose)', function (data) {
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"][lay-filter="choose"]');
        child.each(function (index, item) {
            item.checked = data.elem.checked;
        });
        form.render('checkbox');
    });

     //置顶CheckBox监控
    form.on('checkbox(top)', function (data) {
        $.ajax({
            type: 'post',
            url: '/admin/blogmanager/SetTopOrNot',
            data: { id: data.value },
            success: function (res) {
                if (res.Code==0) {
                    if (data.elem.checked) {
                        data.elem.checked = false;
                    }
                    else {
                        data.elem.checked = true;
                    }
                    layer.msg(res.Message);
                }
                form.render();  //重新渲染        
            },
            error: function (e) {
              
                data.elem.checked = !data.elem.checked;
                form.render(); 
                layer.msg(e.responseText);
            }
        });
    });
    //推荐CheckBox监控
    form.on('checkbox(recommend)', function (data) {
        $.ajax({
            type: 'post',
            url: '/admin/blogmanager/SetRecommendOrNot',
            data: { id: data.value },
            success: function (res) {
                if (res.Code == 0) {
                    if (data.elem.checked) {
                        data.elem.checked = false;
                    }
                    else {
                        data.elem.checked = true;
                    }
                    layer.msg(res.Message);
                }
                form.render();  //重新渲染        
            },
            error: function (e) {
                data.elem.checked = !data.elem.checked;
                form.render(); 
                layer.msg(e.responseText);
            }
        });
    });

    //搜索
    form.on('submit(formSearch)', function (data) {
        var query = $("#keywords").val();
        console.log(query);
        initil(pageIndex, pageSize, query);
        return false;
    });

    //添加文章
    $('#addArticle').click(function () {
        var index = layer.load(1);
        $.ajax({
            type: 'get',
            url: '/admin/blogmanager/add',
            success: function (data) {
                layer.close(index);
                $('#content').html(data);
                $('#BoxTitle').text("添加文章");
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
    //删除选中文章
    $('#delArticleList').click(function () {
        delArticleList();
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
        url: '/admin/blogmanager/getlist',
        data: { page: pageindex, rows: pagesize, query: query },
        dataType: 'json',
        success: function (res) {
            layer.close(index);
            var list = res.data;
            var dataHtml = '';
            dataHtml += '<table style="" class="layui-table" lay-even>';
            dataHtml += '<colgroup><col><col width="20%"><col width="10%"><col width="10%"><col width="10%"><col width="10%"><col width="15%"></colgroup>';
            dataHtml += '<thead><tr><th><input type="checkbox" name="" lay-skin="primary" lay-filter="allChoose" id="allChoose"></th><th>标题</th><th>类别</th><th>访问量</th><th colspan="2">选项</th><th>发布时间</th><th colspan="2">操作</th></tr></thead>';
            dataHtml += '<tbody>';

            //遍历文章集合
            if (list.length != 0) {
                $.each(list, function (index, value) {
                    dataHtml += '<tr>'
                        + '<td><input type="checkbox" name="checked" lay-skin="primary" data-id="' + value.Id + '" lay-filter="choose" ></td>'
                        + '<td align="left">' + value.Title + '</td>'
                        + '<td>' + value.CategoryName + '</td>';
                    dataHtml += '<td>' + wipeNull(value.Traffic, 0) + '</td>';
                    dataHtml += '<td><input type="checkbox" name="top" value="' + value.Id + '" lay-filter="top" title="置顶"' + checkBoxStatus(value.Stick) + ' ></td><td><form class="layui-form"><input type="checkbox" name="top"  value="' + value.Id + '" lay-filter="recommend" title="推荐" ' + checkBoxStatus(value.Recommend) + '></form></td>'
                        + '<td>' + new Date(value.CreateTime).Format("yyyy-MM-dd HH:mm:ss") + '</td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="editArticle(\'' + value.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="delArticle(\'' + value.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                    dataHtml + '</tr>';
                } )   

            }else {
                dataHtml += '<tr><td colspan="8">暂无数据</td></tr>';
        
            }
            dataHtml += '</tbody>';
            dataHtml += '</table>';
            dataHtml += '<div id="page"></div>';

            $('#content').html(dataHtml);

            form.render('checkbox');  //重新渲染CheckBox，编辑和添加的时候
            $('#Console,#List').attr('style', 'display:block'); //显示FiledBox
            $('#BoxTitle').text("文章列表");    //FileBox标题改为文章列表
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

//编辑文章
function editArticle(articleId) {
    var $ = layui.jquery;
    var layer = layui.layer;
    var form = layui.form;
    var index = layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/blogmanager/edit?id=' + articleId,
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
//删除选中文章
function delArticleList() {
    var form = layui.form;
    var $checkbox = $('#content table tbody input[type="checkbox"][name="checked"]');
    var $checked = $('#content table tbody input[type="checkbox"][name="checked"]:checked');
        if ($checkbox.is(":checked")) {
            layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
                var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
                setTimeout(function () {
                    var ids = new Array();
                    for (var j = 0; j < $checked.length; j++) {
                        var id = $checked.eq(j).attr('data-id')
                        ids.push(id);
                    }
                    $.ajax({
                        url: "/admin/blogmanager/Delete",
                        type: "post",
                        data: { ids: ids },
                        success: function (res) {
                            if (res.Code == 1) {
                                $('#content table thead input[type="checkbox"]').prop("checked", false);

                                initil(pageIndex, pageSize, $(".search_input").val());
                                form.render();
                            }
                            layer.close(index);
                            layer.msg(res.Message);
                        },
                        error: function (e) {
                            layer.close(index);
                            layer.msg(e.responseText);
                        }
                    }); 

                }, 2000);
            })
        } else {
            layer.msg("请选择需要删除的文章");
        }
}
//删除文章
function delArticle(articleId) {
    var form = layui.form;
    layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            $.ajax({
                url: "/admin/blogmanager/Delete",
                type: "post",
                data: { ids: articleId },
                success: function (res) {
                    if (res.Code == 1) {
                        $('#content table thead input[type="checkbox"]').prop("checked", false);
              
                        initil(pageIndex, pageSize, $(".search_input").val());
                        form.render();
                    }
                    layer.close(index);
                    layer.msg(res.Message);
                },
                error: function (e) {
                    layer.close(index);
                    layer.msg(e.responseText);
                }
            });     

        }, 2000);
    })
}

//返回复选框状态
function checkBoxStatus(value) {
    if (value == true) {
        return "checked";
    } else {
        return "";
    }
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