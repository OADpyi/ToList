var date = new Date();
var year = date.getFullYear();
var m = date.getMonth() + 1;
var d = date.getDate();
function add() {
    d = d + 1;

    if ((m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12) && (d > 31)) {
        m = m + 1;
        d = 0;
        d = d + 1;
    }
    else if ((m == 4 || m == 6 || m == 9 || m == 11) && d > 30) {
        m = m + 1;
        d = 0;
        d = d + 1;
    }
    else if (m == 2 && d > 28) {
        m = m + 1;
        d = 0;
        d = d + 1;
    }
    if (m > 12) {
        year = year + 1;
        m = 1;
    }
    var str = year + "-" + m + "-" + d;
    document.getElementById('dianjicishu').innerHTML = str;
}

function subtract() {

    d = d - 1;
    if ((m == 1 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12) && (d < 1)) {
        m = m - 1;
        d = 31;
        d = d - 1;
    }
    else if (m == 3 && d < 1) {
        m = m - 1;
        d = 29;
        d = d - 1;
    }
    else if ((m == 4 || m == 6 || m == 9 || m == 11) && d < 1) {
        m = m - 1;
        d = 32;
        d = d - 1;
    }
    else if (m == 2 && d < 1) {
        m = m - 1;
        d = 29;
        d = d - 1;
    }
    if (m < 1) {
        year = year - 1;
        m = 12;
        d = 31;
    }
    var str = year + "-" + m + "-" + d;
    document.getElementById('dianjicishu').innerHTML = str;
}
