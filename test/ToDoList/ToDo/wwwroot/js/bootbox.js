function deleteInventory(id) {
    //1.给input赋值
    $("#input").val(id);
    //2.打开模态框
    //$(function () {
    //    $('#myModal').modal({
    //        keyboard: true
    //    })
    //});

}
//function test() {
//    alert("dsadsa" + $("#input").val())
//}

function deleteItemInInventory(itemId, inventoryId) {
    $("#itemId").val(itemId);
    $("#inventoryId").val(inventoryId);
}