function changeBackground() {
    var mydiv = document.getElementByclassName('inside-circle');
    if (mydiv.style.backgroundColor == "white") {
        mydiv.style.backgroundColor = "red";
    }
    else {
        mydiv.style.backgroundColor = "white"
    }
}

function ToAbout() {
    $.get("Home/About", null, function (data) {
        //你把data alert一下你瞬间就明白了,return View的Action返回的data就是整个html dom
        $("div[class=page]").html($(data).find("*").parent().html());
    });

}
