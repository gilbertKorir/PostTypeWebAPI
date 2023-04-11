$(document).ready(function () {
    getAllKycid();

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



function generateStatement() {
    var obj = {};
   obj.AccountNo = $("#accNm").val();
   obj.startDate = $("#sdate").val();
   obj.endDate = $("#edate").val();

    var dt = new Date();
       
    //alert(JSON.stringify(obj));
    if (obj.AccountNo == null) {
        alert("No selected Account Number");
    }
    else if (new Date(obj.startDate) > dt) {
        alert("Start date should not be past today");
        return false;
    }
    else if (new Date(obj.endDate) > dt) {
        alert("End date should not be past today");
        return false;
    }
    else {
        $.ajax({
            url: "/Reports/GetCashStatement",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(obj),
            success: function (response) {
                //alert(JSON.stringify(response));
                var tbl = "";
                $("#statementTbl").html('');
                for (let i = 0; i < response.length; i++) {
                    tbl = tbl
                        + "<tr>"
                        + "<td>" + response[i].Type + "</td>"
                        + "<td>" + (response[i].TransDate ? response[i].TransDate.slice(0, 10) : '') + "</td>"
                        + "<td>" + (response[i].Debit || 0) + "</td>"
                        + "<td>" + (response[i].Credit || 0) + "</td>"
                        + "<td>" + (response[i].Balance) + "</td>"
                        + "</tr>";

                }
                if (tbl != null) {

                    $("#statementTbl").append(tbl);
                }
            },
            error: function (msg) {
                alert("cannot fetch the statement");
            }

        });
    }
  
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