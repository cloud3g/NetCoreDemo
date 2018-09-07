
var pageIndex = 1;
var pageSize = 8;
layui.use(['jquery','laypage', 'layer', 'form'], function () {
    var form = layui.form;
   
    init(pageIndex, pageSize);

    //添加资源
    $('#add').click(function () {
        var index = layer.load(1);
        $.ajax({
            type: 'get',
            url: '/admin/resource/Add',
            success: function (data) {
                layer.close(index);
                $('#content').html(data);
                $('#BoxTitle').text("添加资源");
                $('#Console').attr('style', 'display:none');
                $('#listBack').bind('click', function () {
                    init(pageIndex, pageSize);
                });
                layui.form.render();
            },
            error: function (e) {
                layer.close(index);
                layer.msg(e.responseText);
            }
        });
    });
    
});
function init(pageindex, pagesize) {
    var index = layer.load(1);
    var $ = layui.jquery;
    var form = layui.form;
    var laypage = layui.laypage;
    $.ajax({
        type: 'post',
        url: '/admin/resource/List',
        data: { 'page': pageindex, 'rows': pagesize },
        datatype: 'json',
        success: function (res) {
            layer.close(index);
            var list = res.data;
            var dataHtml = '';
            dataHtml += '<table style="" class="layui-table" lay-even>';
            dataHtml += '<thead><tr><th>发表时间</th><th>标题</th><th>摘要</th><th>作者</th><th>类别</th><th  colspan="2">操作</th></tr></thead>';
            dataHtml += '<tbody>';
            //遍历文章集合
            if (list.length != 0) {
                $.each(list, function (index, value) {
                    dataHtml += '<tr>';
                    dataHtml += '<td>' + new Date(value.CreateTime).Format("yyyy-MM-dd HH:mm:ss"); + '</td>';
                    dataHtml += '<td>' + value.Name + '</td>';
                    dataHtml += '<td>' + value.Abstract + '</td>';
                    dataHtml += '<td>' + textInit(value.Submitter) + '</td>';
                    dataHtml += '<td>' + GetType(value.Type) + '</td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-normal" onclick="edit(\'' + value.Id + '\')"><i class="layui-icon">&#xe642;</i></button></td>';
                    dataHtml += '<td><button class="layui-btn layui-btn-small layui-btn-danger" onclick="del(\'' + value.Id + '\')"><i class="layui-icon">&#xe640;</i></button></td>';
                    dataHtml + '</tr>';

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
            $('#BoxTitle').text("资源列表");    //FileBox标题改为资源列表
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
                        init(obj.curr, obj.limit);
                    }
                }
            })
            form.render();
        },
        error: function (e) {
            layer.close(index);
            layer.msg(e.responseText);
        }
    });
}


//编辑资源
function edit(id) {
    var index = layui.layer.load(1);
    $.ajax({
        type: 'get',
        url: '/admin/resource/edit?id='+id,
        success: function (data) {
            layer.close(index);
            $('#content').html(data);
            $('#BoxTitle').text("编辑资源");
            $('#Console').attr('style', 'display:none');
            $('#listBack').bind('click', function () {
                init(pageIndex, pageSize);
            });
            layui.form.render();
        },
        error: function (e) {
            layer.close(index);
            layer.msg(e.responseText);
        }
    });
}

//删除资源
function del(id){
    var layer = layui.layer, $ = layui.$;
    layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
        var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            
            $.ajax({
                url: "/admin/resource/Delete",
                data: { id: id },
                type: "post",
                success: function (res) {
                    if (res.Code == 1) {
                        firstPost = 0;
                        init(pageIndex, pageSize);
                    } else {
                        layer.msg(data.Message);
                    }
                    layer.close(index);
                },
                error: function (e) {
                    layer.close(index);
                    layer.msg(e.responseText);
                }
            });

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
function GetType(value) {
    switch (value) {
        case 0: return "源码"; break;
        default: return "未知类型"; break;
    }
}
function textInit(value) {
    return value ? value : "";
}