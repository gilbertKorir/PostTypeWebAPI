$(document).ready(function () {
    getAllKycid();
    fetchTransactions();

    //$("#myTb").DataTable();

    //populate

    $("#txtKycid").change(function () {
        var id = $("#txtKycid").val();
        $.ajax({
            url: "/Transactions/GetAccountsForId/" + id,
            type: "POST",
            dataType: "json",
            success: function (data) {
                var dropdown = "<option value=''> --select-- </option>";
                $("#accNm").empty();
                for (var i = 0; i < data.length; i++) {
                   /* $dropdown.append("<option value='" + data[i].Id + "'>" + data[i].AccountName + "</option>");*/
                    dropdown = dropdown + "<option value='" + data[i].Id + "'>" + data[i].AccountName + "</option>";
                }
                if (dropdown != null) {
                    $("#accNm").append(dropdown);
                }
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });

    //get currrent balance
    $('#accNm').change(function () {
        var id = $("#accNm").val();
        $.ajax({
            url: "/Transactions/CurrentBalance/" + id,
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response) {
                   // alert(response);
                    $("#bal").val(response);
                }
                else {
                    $("#bal").val(0);
                }
                //alert("The current balance for this account is :" + response);
            },
            error: function (xhr, status, error) {
                alert("Current balance for the account is 0");
                $("#bal").val(0);
            }
        });

    });

});

function getAllKycid() {
    $.ajax({
        url: "/Transactions/GetAllKycid",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dropdown = "<option value=''>-- select --</option>";
            $("#txtKycid").html('');
            for (let i = 0; i < data.length; i++) {
                dropdown = dropdown
                    + "<option value='" + data[i].Id + "'>" + data[i].Name + "</option>";
            }
            if (dropdown != null) {
                $("#txtKycid").append(dropdown);
            }
        },
        error: function () {
            alert("KycIds not retrieved");
        }
    })
}

function addTransaction() {
    var obj = {};

    obj.AccountNo = $("#accNm").val();
    obj.Type = $("#type").val();
    obj.TransDate = $("#date").val();
    obj.Amount = $("#amount").val();

    if (obj.AccountNo == null || obj.Type == null || obj.TransDate == null || obj.Amount == 0) {
        alert("Fields cannot be empty");
    }
    else {

        $.ajax({
            url: "/Transactions/AddTransaction",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(obj),
            success: function (response) {
                alert(response);
                clearFields();
                fetchTransactions();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR == 'Withdrawal amount exceeds total deposit.') {
                    // Handle withdrawal exceeds total deposit error
                    alert('Withdrawal amount exceeds total deposit.');
                }
                else {
                    alert('Withdrawal amount exceeds total deposit.');
                }
            }

        });

    }
}

function fetchTransactions() {
    $.ajax({
        url: "/Transactions/GetTransaction",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var dropdown = "";
            $("#tTrans").html('');
            for (let i = 0; i < response.length; i++) {
                dropdown = dropdown
                    + "<tr>"
                    //+ "<td>" + response[i].TransId + "</td>"
                    + "<td>" + response[i].AccountNo + "</td>"
                    + "<td>" + response[i].Type + "</td>"
                    + "<td>" + response[i].TransDate + "</td>"
                    + "<td>" + response[i].Amount + "</td>"
                    + "</tr>";
            }
            if (dropdown != null) {
                $("#tTrans").append(dropdown);
            }
        },
        error: function (msg) {
            alert("cannot fetch the transactions");
        }

    });
}

function clearFields() {
    $("#txtKycid").val("");
    $("#accNm").val("");
    $("#type").val("");
    $("#date").val("");
    $("#amount").val("");
    $("#bal").val("");
}

function showPopup() {
    var popupBox = document.getElementById("popupBox");
    popupBox.style.display = "block";
}

function hidePopup() {
    var popupBox = document.getElementById("popupBox");
    popupBox.style.display = "none";
}

function validate() {
    let v1 = $("#txtKycid").val();
    let v2 = $("#accNm").val();
    let v3 = $("#type").val();
    let v4 = $("#date").val();
    let v5 = $("#amount").val();

    if (v1 == null || v2 == null || v3 == null || v4 == null || v5 == null) {
        alert("Fields cannot be empty");
    }
}