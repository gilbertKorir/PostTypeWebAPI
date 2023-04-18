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
            //var dropdown = "<option value=''>-- select --</option>";
            var dropdown = "";
            //$("#txtKycid").html('');
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
    if (obj.AccountNo == null) {
        alert("No selected Account Number");
    } else if (new Date(obj.startDate) > dt) {
        alert("Start date has an error");
        return false;
    } else if (new Date(obj.endDate) > dt || new Date(obj.endDate) < new Date(obj.startDate)) {
        alert("End date has an error");
        return false;
    } else {
        $.ajax({
            url: "/Reports/GetCashStatement",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(obj),
            success: function (response) {
                console.log(response);
                var data = response.map(function (obj) {
                    return {
                        Type: obj.Type,
                        TransDate: obj.TransDate.slice(0, 10), // Get the first 10 characters of the TransDate value (the date part)
                        Debit: obj.Debit,
                        Credit: obj.Credit,
                        Balance: obj.Balance
                    };

                });

                $("#jptb").jqGrid("GridUnload");

                $("#jptb").jqGrid({
                    //type: "POST",
                    //data: JSON.stringify(obj),
                    colNames: ['Type', 'TransDate', 'Debit', 'Credit', 'Balance'],
                    colModel: [
                        { name: "Type", name: "Type", width: 150 },
                        { name: "TransDate", name: "TransDate", width: 150 },
                        { name: "Debit", name: "Debit", width: 150 },
                        { name: "Credit", name: "Credit", width: 140 },
                        { name: "Balance", name: "Balance", width: 140 }
                    ],
                    data: data,
                    rowNum: 20,
                    //sortname: "TransDate",
                   /* sortdirection: 'asc',*/
                   // rowList: [10, 20, 30],
                    pager: '#jpager',
                    viewrecords: true,
                    caption: "Statement",
                    height: 'auto'
                });

                $("#jptb").trigger("reloadGrid");
            },
        });
    }
}


    function clearFields() {
        $("#txtKycid").val("");
        $("#accNm").val("");
        $("#type").val("");
        $("#sdate").val("");
        $("#edate").val("");

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
















//function generateStatement() {
//    var obj = {};
//    obj.AccountNo = $("#accNm").val();
//    obj.startDate = $("#sdate").val();
//    obj.endDate = $("#edate").val();

//    var dt = new Date();
//    if (obj.AccountNo == null) {
//        alert("No selected Account Number");
//    }
//    else if (new Date(obj.startDate) > dt) {
//        alert("Start date has an error");
//        return false;
//    }
//    else if (new Date(obj.endDate) > dt || new Date(obj.endDate) < new Date(obj.startDate)) {
//        alert("End date has an error");
//        return false;
//    }
//    else {
//        $.ajax({
//            url: "/Reports/GetCashStatement",
//            //type: "POST",
//            //dataType: "json",
//            //contentType: "application/json; charset=utf-8",
//            data: JSON.stringify(obj),
//            success: function (response) {
//                $("#jptb").jqGrid({
//                    dataType: "json",
//                    type: "POST",
//                    data: JSON.stringify(obj),
//                    colNames: ['Type', 'TransDate', 'Debit', 'Credit', 'Balance'],
//                    colModel: [
//                        { name: "Type", index: "Type", width: 150 },
//                        { name: "TransDate", index: "TransDate", width: 150 },
//                        { name: "Debit", index: "Debit", width: 150 },
//                        { name: "Credit", index: "Credit", width: 150 },
//                        { name: "Balance", index: "Balance", width: 150 }
//                    ],
//                    data: response,
//                    rowNum: 10,
//                    sortname: "TransDate",
//                    sortorder: "desc"
//                });

//                //alert(JSON.stringify(response));
//                /* var tbl = "";
//                 $("#statementTbl").html('');
//                 for (let i = 0; i < response.length; i++) {
//                     tbl = tbl
//                         + "<tr>"
//                         + "<td>" + response[i].Type + "</td>"
//                         + "<td>" + (response[i].TransDate ? response[i].TransDate.slice(0, 10) : '') + "</td>"
//                         + "<td>" + (response[i].Debit || 0) + "</td>"
//                         + "<td>" + (response[i].Credit || 0) + "</td>"
//                         + "<td>" + (response[i].Balance) + "</td>"
//                         + "</tr>";
//                 }
//                 if (tbl != null) {

//                     $("#statementTbl").append(tbl);
//                 }
//             },
//             error: function (msg) {
//                 alert("cannot fetch the statement");
//             }*/
//            },
//            error: function (xhr, status, error) {
//                console.log(xhr.responseText);
//            }
//        });
//    }
//}





/*  $("#jptb").jqGrid({
      ajaxGridOptions: {
          url: "/Reports/GetCashStatement",
          type: "POST",
          dataType: "json",
          contentType: "application/json; charset=utf-8",
          data: JSON.stringify(obj),
          success: function (response) {
              console.log(JSON.stringify(response));
          },
          error: function (xhr, status, error) {
              console.log(xhr.responseText);
          }
      },
      datatype: "json",
      colNames: ['Type', 'TransDate', 'Debit', 'Credit', 'Balance'],
      colModel: [
          { name: "Type", index: "Type", width: 150 },
          { name: "TransDate", index: "TransDate", width: 150 },
          { name: "Debit", index: "Debit", width: 150 },
          { name: "Credit", index: "Credit", width: 150 },
          { name: "Balance", index: "Balance", width: 150 }
      ],
      rowNum: 10,
      sortname: "TransDate",
      sortorder: "desc",
      rowList: [10, 20, 30],
      pager: '#jpager',
      viewrecords: true,
      caption: "Statement"
*/











/*
        url: "/Reports/GetCashStatement",
        postData: {
            id: function () { return $("#accNm").val(); },
            startDate: function () {
                return moment($("#sdate").val()).format("YYYY-MM-DD");
            },
            endDate: function () {
                return moment($("#edate").val()).format("YYYY-MM-DD");
            }
        },

        success: function (response) {
            console.log(response); // log the response object
        },

        datatype: "json",
        colNames: ['Type', 'TransDate', 'Debit', 'Credit', 'Balance'],
        colModel: [
            { name: "Type", index: "Type", width: 150 },
            { name: "TransDate", index: "TransDate", width: 150 },
            { name: "Debit", index: "Debit", width: 150 },
            { name: "Credit", index: "Credit", width: 150 },
            { name: "Balance", index: "Balance", width: 150 }
        ],
        rowNum: 10,
        sortname: "TransDate",
        sortorder: "desc",
        rowList: [10, 20, 30],
        pager: '#jpager',
        viewrecords: true,
        caption: "Statement"
    });
*/
/*var obj = {};
obj.AccountNo = $("#accNm").val();
obj.startDate = $("#sdate").val();
obj.endDate = $("#edate").val();

var dt = new Date();
if (obj.AccountNo == null) {
    alert("No selected Account Number");
} else if (new Date(obj.startDate) > dt) {
    alert("Start date has an error");
    return false;
} else if (new Date(obj.endDate) > dt || new Date(obj.endDate) < new Date(obj.startDate)) {
    alert("End date has an error");
    return false;
} else {*/
        //alert(JSON.stringify(obj))
        //$.ajax({
/*  url: "/Reports/GetCashStatement",
  type: "POST",
  dataType: "json",
  contentType: "application/json; charset=utf-8",
 // data: JSON.stringify(obj),
  postData: {
      id: function () { return $("#accNm").val(); },
      startDate: function () { return $("#sdate").val(); },
      endDate: function () { return $("#edate").val(); }
  },
  success: function (response) {
      console.log(JSON.stringify(response));
      $("#jptb").jqGrid({
          datatype: "json",
          colNames: ['Type', 'TransDate', 'Debit', 'Credit', 'Balance'],
          colModel: [
              { name: "Type", index: "Type", width: 150 },
              { name: "TransDate", index: "TransDate", width: 150 },
              { name: "Debit", index: "Debit", width: 150 },
              { name: "Credit", index: "Credit", width: 150 },
              { name: "Balance", index: "Balance", width: 150 }
          ],
          data: response,
          rowNum: 10,
          sortname: "TransDate",
          sortorder: "desc",
          //rowList: [10, 20, 30],
          pager: '#jpager',
          viewrecords: true,
          caption: "Statement"
      });
      //clearFields();
  },
  error: function (xhr, status, error) {
      console.log(xhr.responseText);


  }*/
        //});

        //}













