$(document).ready(function () {
    getAllKycid();
    fetchTransactions();

    //populate

    $("#txtKycid").change(function () {
        var id = $("#txtKycid").val();
        $.ajax({
            url: "/Transactions/GetAccountsForId/" + id,
            type: "POST",
            dataType: "json",
            success: function (data) {


                var $dropdown = $("#accNm");
                $dropdown.empty();
                for (var i = 0; i < data.length; i++) {
                    $dropdown.append("<option value='" + data[i].Id + "'>" + data[i].AccountName + "</option>");
                }
            },
            error: function (xhr, status, error) {
                console.log("Error: " + error);
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
            var dropdown = "";
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

    obj.TransId = $("#txtKycid").val();
    obj.AccountNo = $("#accNm").val();
    obj.Type = $("#type").val();
    obj.TransDate = $("#date").val();
    obj.Amount = $("#amount").val();

    $.ajax({
        url: "/Transactions/AddTransaction",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(obj),
        success: function (response) {
            showPopup();
            clearFields();
            fetchTransactions();
            
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //if (jqXHR.status === 400 && jqXHR.responseText === 'Withdrawal amount exceeds total deposit.') {
                // Handle withdrawal exceeds total deposit error
                alert('Withdrawal amount exceeds total deposit.');
            //}
           // else {
               // alert("An error occured");
            //}
        }

    });
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
                    + "<td>" + response[i].TransId + "</td>"
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
}

function showPopup() {
    var popupBox = document.getElementById("popupBox");
    popupBox.style.display = "block";
}

function hidePopup() {
    var popupBox = document.getElementById("popupBox");
    popupBox.style.display = "none";
}