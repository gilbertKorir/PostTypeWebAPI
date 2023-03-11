$(document).ready(function () {
    fetchNames();
    getAccounts();
});

var AccountsData = null;

function fetchNames() {
    $.ajax({
        type: "POST",
        url: "/Accounts/GetAllIds",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (names) {
            var dropdown = "";
            for (let i = 0; i < names.length; i++) {
                dropdown = dropdown +
                    "<option value='"+ names[i].Id +"'>"+ names[i].Name + "</option>";
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

function getAccounts() {
    //alert("hello");
    $.ajax({
        url: "/Accounts/GetAllAccounts",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data));
            if (data) {
                AccountsData = data;
                var row = "";
                $("#tbodyAccounts").html('');
                for (let i = 0; i < data.length; i++) {
                    row = row
                        + "<tr>"
                        + "<td>" + data[i].AccountName + "</td>"
                        + "<td>" + data[i].KycId + "</td>"
                        + "<td>" + data[i].Active + "</td>"
                        + "<td><button class='btn btn-success' onclick='editAccount("+ i +")'>Edit</button></td>"
                        + "</tr>";
                }
                if (row != null) {
                    $("#tbodyAccounts").append(row);
                }
            }
        },
        error: function (msg) {
            alert("Accounts not retrieved");
        }
    });
}

function addAccount() {

    var objAccount = {};
    objAccount.AccountName = $("#txtName").val();
    objAccount.KycId = $("#txtKyc").val();

    var status = $("#txtActive").is(':checked');
    if (status) {
        objAccount.Active = 1;
    }
    else {
        objAccount.Active = 0;
    }
    $.ajax({
        url: "/Accounts/AddAccount",
        data: JSON.stringify(objAccount),
        dataType: "json",
        type:"POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            clear();
            getAccounts();
        },
        error: function (msg) {
            alert("Account not added");
        }
    });
}

function editAccount(i) {
    $(".accId").show();
    $("#txtIdAcc").show();
    $("#updateAccount").show();
    $("#txtName").val(AccountsData[i].AccountName);
    $("#txtKyc").val(AccountsData[i].KycId);
    $("#txtActive").val(AccountsData[i].Active);
    $("#txtIdAcc").val(AccountsData[i].Id);
}

//clear fields function
function clear() {
    $('#txtName').val('');
    $('#txtIdAcc').val('');
    $('.accId').hide('');
    $('#txtActive').prop('checked', false);
    $('#txtIdAcc').val('');
    $('#txtKyc').val('');
    $("#updateAccount").hide();
}

function updateAccount() {

    var objAccount = {};
    objAccount.AccountName = $("#txtName").val();
    objAccount.KycId = $("#txtKyc").val();
    objAccount.Id = $("#txtIdAcc").val();

    var status = $("#txtActive").is(':checked');
    if (status) {
        objAccount.Active = 1;
    }
    else {
        objAccount.Active = 0;
    }

    var accId = $("#txtIdAcc").val();

    if (accId) {
        //alert(JSON.stringify(objAccount));
        $.ajax({
            url: "/Accounts/UpdateAccounts",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(objAccount),
            dataType: "json",
            type: "POST",
            success: function (result) {
                getAccounts();
                clear();

            },
            error: function (msg) {
                alert("Account not updated");
            }
        });
    }
}




