layui.use(['laydate', 'laypage', 'layer', 'table', 'carousel', 'upload', 'element', 'slider','jquery','form'], function() {
    var laydate = layui.laydate //日期
        , laypage = layui.laypage //分页
        , layer = layui.layer //弹层
        , table = layui.table //表格
        , carousel = layui.carousel //轮播
        , upload = layui.upload //上传
        , element = layui.element //元素操作
        , slider = layui.slider //滑块
        , form = layui.form
        , $ = layui.jquery;


    $("#submit").click(function (res) {
        var json=$("#form").serialize();
        $.ajax({
            url: "/Login/LoginCheck",
            type: "get",
            dataType: "json",
            data: json,
            success: function (res) {
                if (res) {
                    layer.msg('登入成功', {icon: 1});
                    window.location.replace("/Manager/Manager");
                } else {
                    layer.msg('密码错误！亲.请检查密码', {icon: 3});
                }
            }
        })
    })
});