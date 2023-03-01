$(document).ready(function () {

});


function saveAccount() {
    $.ajax({
        type: "POST",
        url: "/Accounts/GetAllNames",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(JSON.stringify(data));
        },
        error: function (msg) {
            alert("Names not retrieved");
        }
    });
}