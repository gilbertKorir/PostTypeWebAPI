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

    $.ajax({
        url:"/Person/AddEmployee",
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

function fetchData() {
    $.ajax({
        type: "POST",
        url: "/Person/GetAllEmployees",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data));
            if (data) {
                EmpData = data;
                $("#tbody").html("");
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
                }
            }
        },
        error: function (xhr, status, error) {
            alert("error in operation");
        }
    });
}
function editproduct(i) {
    $(".empId").show();
    $("#update").show();
    $("#adding").hide();

    $("#txtName").val(EmpData[i].Name);
    $("#txtAge").val(EmpData[i].Age);
    $("#txtActive").val(EmpData[i].Active);
    $("#txtId").val(EmpData[i].Id);

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


