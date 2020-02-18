layui.use(['laydate', 'laypage', 'layer', 'table', 'carousel', 'upload', 'element', 'slider', 'jquery', 'laytpl', 'util'], function(){
    var laydate = layui.laydate //日期
        , laytpl = layui.laytpl
        , util = layui.util
       
        ,laypage = layui.laypage //分页
        ,layer = layui.layer //弹层
        ,table = layui.table //表格
        ,carousel = layui.carousel //轮播
        ,upload = layui.upload //上传
        ,element = layui.element //元素操作
        ,slider = layui.slider //滑块
        , $ = layui.jquery;
   
    // $('[name="suerpassworld"]').blur(function (res) {
    //     if( suer===newps){
    //         alert(suer+newps);
    //         layer.msg("密码一致",{icon:1});
    //     }else{
    //         alert(suer);
    //         alert(newps);
    //         layer.msg("修改密码和确认密码不一致，请检查！",{icon:3});
    //     }
    // });
    
    $("#submit").click(function (res) {
        var suer=$("#suerpassworld").val();
        var newps=$("#newpassworld").val();
        var pass=$("#password").val()
        if(pass===""){
            layer.msg('旧密码不能为空', {icon: 3});
        }else if(newps===""){
            layer.msg('新密码不能为空', {icon: 3});
        }else if(newps===""){
            layer.msg('第二次确认新密码不能为空', {icon: 3});
        }else{
            if( suer=== newps){
                $.ajax({
                    url: "/Login/CheckAdmin",
                    type: "get",
                    dataType: "json",
                    data: {
                        username:$("#username").val(),
                        password:$("#password").val()
                    },
                    success: function (res) {
                        if (res) {
                            if (suer === newps) {
                                $.ajax({
                                    url: "/Login/UpdataAdmin",
                                    type: "get",
                                    dataType: "json",
                                    data: {
                                        username: $("#username").val(),
                                        password: suer
                                    },
                                    success: function (res) {
                                        if (res) {
                                            layer.msg('密码修改成功', {icon: 1});
                                            window.location.replace("/Login/Login");
                                        } else {
                                            layer.msg('旧密码错误', {icon: 2});
                                        }
                                    },
                                    error: function (res) {
                                        layer.msg('修改密码发生异常', {icon: 3});
                                    }
                                })
                            } else {
                                layer.msg('旧密码输入错误请重新输入', {icon: 2});
                            }
                        }else{
                            layer.msg('旧密码错误', {icon: 2});
                        }
                    }
                })
            }else{
                layer.msg("修改密码和确认密码不一致，请检查！",{icon:3});
            }
        }
            
    });
    
    laydate.render({
        elem: '#data'
        , value: new Date()
        //或 elem: document.getElementById('test')、elem: lay('#test') 等
    });
   var r= table.render({
       elem: '#test',
       id: 'test',
       skin: 'line' //行边框风格
       , even: true //开启隔行背景
       , size: 'sm',
       loading:true,
        url: "/Manager/FindManageList",
        // toolbar: "default",
       cellMinWidth:70,
    page: true,
        toolbar:"#tools",
        cols:[[
            { type: 'checkbox' }
            , { field: 'id', title: 'ID', sort: true }
            , { field: 'name', title: '姓名', minWidth: 100 }
           , { field: 'addres', title: '收件地址', sort: true, minWidth: 120 }
            , { field: 'phone', title: '电话', sort: true, minWidth: 120}
            , { field: 'subject', title: '任教科目', sort: true , minWidth: 90}
            , { field: 'class', title: '年级', sort: true }
            , { field: 'logDate', title: '时间', sort: true, minWidth: 200 }
            , { field: 'remarks', title: '备注', sort: true, minWidth: 600 }

        ]]
    });
    table.on('toolbar(test)', function (obj) { //注：tool 是工具条事件名，test 是 table 原始容器的属性 lay-filter="对应的值"
        var where = null;
        var data = obj.data //获得当前行数据
            ,layEvent = obj.event; //获得 lay-event 对应的值
        if (layEvent === 'export') {
            var where = $("#where").val();
            window.location.href = "/Manager/ExportExcle?where=" + where + " ";

        }else if (layEvent === 'password') {
            
            layer.open({
                type: 1,
                content: $("#movepass") //这里content是一个普通的String
            });

        }else if (layEvent === 'out') {
            window.location.replace("/Login/Login");

        }
        else if (layEvent === 'query') {
            var myReg = /^[\u4e00-\u9fa5]+$/;
             where= $("#where").val();
            if(where===""){
                layer.msg("正在刷新", { icon: 1 });
                table.reload('test', {
                    url: '/Manager/FindManageList'
                    , page: {
                        curr: 1
                    } //设定异步数据接口的额外参数
                    //,height: 300
                });
            } else {
                table.reload('test', {
                    url: '/Manager/FindManageList'
                    ,where:{
                        where:where
                    },page:{
                        curr:1
                    } //设定异步数据接口的额外参数
                    //,height: 300
                });
                $("#where").val(where);
            }
        }
    });
});

