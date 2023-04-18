$(document).ready(function () {
    fetchData();
});

var EmpData = null;

//clear fields function
function clear() {
    $('#txtName').val('');
    $('#txtAge').val('');
    $('#txtActive').prop('checked', false);
    $('#txtId').val('');
    $('#update').hide();
}

//add employee
function Add() {

    var objEmp = {};

    objEmp.Name = $("#txtName").val();
    objEmp.Age = $("#txtAge").val();

    var checkBox = $("#txtActive").is(':checked');
    if (checkBox) {
        objEmp.Active = 1;
    }
    else {
        objEmp.Active = 0;
    }

    if (objEmp.Name == null || objEmp.Age == null || objEmp.Age == 0) {
        alert("Check your fields clearly");
    }
    else {
        $.ajax({
            url: "/Person/AddEmployee",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objEmp),
            type: "POST",
            success: function (data) {
                fetchData();
                clear();
            },
            error: function (msg) {
                alert("Error adding the data");
                // fetchData();
                //clear();
            }
        });
    }
}

function fetchData() {

    $.ajax({
        type: "POST",
        url: "/Person/GetAllEmployees",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
           // alert(response);
            if (response) {
                //EmpData = response;

                $("#tbody").jqGrid("GridUnload");

                $("#tbody").jqGrid({
                   
                    colNames: ['Employee Id', 'Name', 'Age', 'Active'],
                    colModel: [
                        { name: "Id", name: "Id", width: 150 },
                        { name: "Name", name: "Name", width: 150 },
                        { name: "Age", name: "Age", width: 150 },
                        { name: "Active", name: "Active", width: 140 }
                    ],
                    data: response,
                    rowNum: 20,
                    pager: '#jpager',
                    viewrecords: true,
                    caption: "Employee Table",
                    height: 'auto',

                    ondblClickRow: function (Id, iRow, iCol, e) {
                        // get the data from the row
                        $(".empId").show();
                        $("#update").show();
                        $("#adding").hide();

                        var rowData = $("#tbody").getRowData(Id);

                        // populate the required fields
                        $("#txtName").val(rowData.Name);
                         $("#txtAge").val(rowData.Age);
                         $("#txtActive").val(rowData.Active);
                         $("#txtId").val(rowData.Id);
                    }

                });

                $("#tbody").trigger("reloadGrid");




              /*  $("#tbody").html("");
                var row = "";
                for (let i = 0; i< data.length; i++) {
                    row = row
                        + "<tr>"
                        + "<td>" + data[i].Id + "</td>"
                        + "<td>" + data[i].Name + "</td>"
                        + "<td>" + data[i].Age + "</td>"
                        + "<td>" + data[i].Active + "</td>"
                        + "<td><button id='btnEdit' class='btn btn-success' onclick='editproduct(" + i + ")'>Edit</button></td>"
                        + "</tr>";
                }
                if (row != null) {
                    $("#tbody").append(row);
                }*/
            }
        },
        error: function (xhr, status, error) {
            alert("error in operation");
        }
    });
}


function editproduct(id) {
    $(".empId").show();
    $("#update").show();
    $("#adding").hide();

    // Get the index of the row in the data array
    //var index = $("#tbody").jqGrid('getInd', Id);

    // Get the data for the selected row
   // var rowData = $("#tbody").jqGrid('getGridParam', 'data')[index];

    

    var rowData = $("#tbody").jqGrid('getRowData', id);

    alert(rowData);
    //$("#txtName").val(Name);
   /* $("#txtAge").val(rowData.Age);
    $("#txtActive").val(rowData.Active);
    $("#txtId").val(rowData.Id);*/

}

function updateEmployee() {

    var objEmp = {};

    objEmp.Name = $("#txtName").val();
    objEmp.Age = $("#txtAge").val();
    objEmp.Id = $("#txtId").val();

    var checkBox = $("#txtActive").is(':checked');
    if (checkBox) {
        objEmp.Active = 1;
    }
    else {
        objEmp.Active = 0;
    }

    //alert(JSON.stringify(objEmp));
    var empId = $("#txtId").val();

    if (empId) {
        $.ajax({
            url: "/Person/UpdateEmp",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objEmp),
            type: "POST",
            success: function (data) {
                fetchData();
                $(".empId").hide();
                clear();
            },
            error: function (msg) {
                alert("Error updating the data");
                // fetchData();
                //clear();
            }
        });
    }
}


