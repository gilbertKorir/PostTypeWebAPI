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
                    width: '100%',
                    colNames: ['Employee Id', 'Name', 'Age', 'Active'],
                    colModel: [
                        { name: "Id", name: "Id", hidden:true},
                        { name: "Name", name: "Name", editable: true, align:"center" },
                        { name: "Age", name: "Age", editable: true, align: "center" },
                        { name: "Active", name: "Active", editable: true, align: "center" }
                    ],
                    data: response,
                    rowNum: 20,
                    pager: '#jpager',
                    viewrecords: true,
                    rowList: [8, 12],
                    caption: "Employee Table",
                    height: '100%',
                    autowidth: true,
                    align: 'center',
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

                }).navGrid('#jpager', { edit: true, add: true, del: false, search: true, refresh: false },
                {
                    //update
                    edit:true,
                    width: 400,
                    url: '/Person/UpdateEmp',
                    closeAfterEdit: true,
                    afterComplete: function (response) {
                        console.log(response);
                    $('#tbody').setGridParam({ datatype: 'json', page: 1 }).trigger("reloadGrid");
                    alert(response.responseText);
                    }
             
                });

                //$("#tbody").trigger("reloadGrid");

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

    var rowData = $("#tbody").jqGrid('getRowData', id);

    alert(rowData);

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


