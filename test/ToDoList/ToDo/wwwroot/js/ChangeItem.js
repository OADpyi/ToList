$(document).ready(function () {

    $("#item-content").blur(function () {
        var name = $("#item-content").val();
        if (name == "") {
            $("#content-error").html("内容不能为空!");
            return;
        }
        else if (name != "") {
            var a = $("#content-error").text();
            $("#content-error").html("")
            return;
        }
    })
    $("#item-content").keypress(function () {
        var a = $("#content-error").text();
        $("#content-error").html("请输入30字以内。");
        return;
    })
    $("#item-mark").blur(function () {
        var name = $("#item-mark").val();
        if (name == "") {
            $("#mark-error").html("内容不能为空!");
            return;
        }
        else if (name != "") {
            var a = $("#mark-error").text();
            $("#mark-error").html("")
            return;
        }
    })
    $("#item-mark").keypress(function () {
        var a = $("#content-error").text();
        $("#mark-error").html("请输入30字以内。");
        return;
    })

})