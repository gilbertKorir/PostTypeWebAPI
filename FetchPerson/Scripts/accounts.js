$(document).ready(function () {
    saveAccount();
});


function saveAccount() {
    $.ajax({
        type: "POST",
        url: "/Accounts/GetAllNames",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (names) {
            //alert(JSON.stringify(names));

            var dropdown = "";
            for (let i = 0; i < names.length; i++) {
                dropdown = dropdown +
                    "<option>" + names[i].Name + "</option>";
            }
            if (dropdown != null) {
                $('#txtKyc').append(dropdown);
            }
        },
        error: function (msg) {
            alert("Names not retrieved");
        }
    });
}






