$(document).ready(function () {
    getUserName()
    getCount()
    getInventory()
    getitemerror()
    getCreateItemWorning()
})

function getInventory() {
    $.ajax({
        url: '/Inventory/GetInventory',
        data: {},
        success: function (result) {
            $("#content-block1").html(result);
        }
    })
}
function getUserName() {
    $.ajax({
        url: '/Layout/GetUserName',
        data: {},
        success: function (userName) {
            document.getElementById("UserName").innerHTML = userName;
        }
    })
}

function getCount() {
    $.ajax({
        url: '/Layout/GetCount',
        data: {},
        success: function (Count) {
            document.getElementById("InventoryCount").innerHTML = Count[0];
            document.getElementById("finishItemCount").innerHTML = Count[1];
            document.getElementById("OverdueItemCount").innerHTML = Count[2];
            document.getElementById("notFinishItemCount").innerHTML = Count[3];
        }
    })
}

function viewCreateInventory() {
    $.ajax({
        url: '/Inventory/ViewCreateInventory',
        data: {},
        success: function (result) {
            $("#content-block1").html(result);
        }
    })
}
function showInventoryItems(inventoryId) {
    showNoFinishItems(inventoryId)
    showFinishItems(inventoryId)
    showOverdueItems(inventoryId)
    getCount()
}
function showNoFinishItems(inventoryId) {
    $.ajax({
        url: '/Item/ShowNoFinishItems',
        data: {inventoryId },
        success: function (result) {
            $("#content-block2").html(result);
        }
    })
}
function showFinishItems(inventoryId) {
    $.ajax({
        url: '/Item/ShowFinishItems',
        data: {inventoryId},
        success: function (result) {
            $("#content-block3").html(result);
        }
    })
}
function showOverdueItems(inventoryId) {
    $.ajax({
        url: '/Item/ShowOverdueItems',
        data: {inventoryId},
        success: function (result) {
            $("#content-block4").html(result);
        }
    })
}

function AfterViewCreateInventory() {
    var code = event.keyCode;
    if (code == 13) {
        inventoryName=$("#create-inventory-name").val();
        $.ajax({
            type: 'post',
            url: '/Inventory/CreateInventory',
            data: { 'inventoryName': inventoryName},
            success: function (result) {
                $("#content-block1").html(result);
                getCount()
            }
        });
    }
}

function DeleteInventory() {
    var inventoryId = $("#input").val();
    $.ajax({
        type: 'post',
        url: '/Inventory/RemoveInventory',
        data: { inventoryId: inventoryId },
        success: function (result) {
            $(".modal-backdrop").css("display","none");
            $("#content-block1").html(result);
            getCount()
            showInventoryItems(inventoryId)
        }
    });
}

function ChangeStatue(itemId,inventoryId) {
    $.ajax({
        type:'POST',
        url: '/Item/ChangeStatueInInventory',
        data: { 'itemId': itemId },
        dataType:"json", 
        success: function (data) {
            showNoFinishItems(inventoryId)
            showFinishItems(inventoryId)
            getCount()
        },
    })
}

function deleteNofinishItem() {
    var itemId = $("#itemId").val();
    var inventoryId = $("#inventoryId").val();
    $.ajax({
        url: '/Item/DeleteItemeInInventory',
        type: 'post',
        data: { "itemId": itemId, "inventoryId": inventoryId},
        success: function (result) {
            $(".modal-backdrop").css("display", "none");
            showInventoryItems(result)
        }
    })
}


function getitemerror() {

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
        $("#content-error").html("请输入50字以内。");
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
        $("#mark-error").html("请输入50字以内。");
        return;
    })

}

function getCreateItemWorning() {
    $("#item-create-content").blur(function () {
        var name = $("#item-create-content").val();
        if (name == "") {
            $("#create-content-warning").html("内容不能为空!");
            return;
        }
        else if (name != "") {
            $("#create-content-warning").html("")
            return;
        }
    })
    $("#item-create-content").keypress(function () {
        $("#create-content-warning").html("请输入50字以内。");
        return;
    })
    $("#item-create-mark").blur(function () {
        var name = $("#item-create-mark").val();
        if (name == "") {
            $("#create-mark-warning").html("内容不能为空!");
            return;
        }
        else if (name != "") {

            $("#create-mark-warning").html("")
            return;
        }
    })
    $("#item-create-mark").keypress(function () {
        var a = $("#create-mark-warning").text();
        $("#create-mark-warning").html("请输入50字以内。");
        return;
    })
}