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
                //alert(response)
                if (response) {
                    $("#bal").val(response);
                }
                else {
                   $("#bal").val(0);
                }
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

    if (obj.AccountNo == null || obj.Type == null || obj.TransDate == null || obj.Amount == null) {
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
                //fetchTransactions();
            },
            error: function (jqXHR, textStatus, errorThrown) {
               
                    alert('Withdrawal amount exceeds total deposit.');
                
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
            //alert(response);
            if (response) {
                $("#myTb").jqGrid("GridUnload");
                $("#myTb").jqGrid({
                    width: '100%',
                    colNames: ['Account No Id', 'Type', 'Date', 'Amount'],
                    colModel: [
                        { name: "AccountNo", name: "AccountNo"},
                        { name: "Type", name: "Type"},
                        { name: "TransDate", name: "TransDate"},
                        { name: "Amount", name: "Amount"}
                    ],
                    data: response,
                    rowNum: 12,
                    pager: '#jpager',
                    viewrecords: true,
                    rowList: [12],
                    caption: "Transactions",
                    height: '100%',
                    autowidth: true,
                    align: 'center',

                    ondblClickRow: function (Id, iRow, iCol, e) {

                        var rowData = $("#myTb").getRowData(Id);

                        // populate the required fields
                        /*  $("#txtName").val(rowData.AccountName);
                          $("#txtKyc").val(rowData.KycId);
                          $("#txtActive").val(rowData.Active);
                          $("#txtIdAcc").val(rowData.KycId);*/
                    }
                });
                //$("#myTb").jqGrid('navGrid', '#jpager', {edit:false, add:false, del:false});
                $("#myTb").trigger("reloadGrid");
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