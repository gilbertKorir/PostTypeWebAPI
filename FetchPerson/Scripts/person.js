$(document).ready(function () {
    fetchData();
});

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
    //alert(JSON.stringify(objEmp));
    $.ajax({
        url: "/Person/AddEmployee",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(objEmp),
        type: "POST",
        success: function (data) {
            clearFields();
            fetchData();
        },
        error: function (msg) {
            alert(msg);
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
                $("#tbody").html("");
                var row = "";
                for (let i = 0; i< data.length; i++) {
                    row = row
                        + "<tr>"
                        + "<td>" + data[i].Name + "</td>"
                        + "<td>" + data[i].Age + "</td>"
                        + "<td>" + data[i].Active + "</td>"
                        + "<td><button id='btnEdit' class='btn btn-success' onclick='Edit(" + data[i].Id + ")'>Edit</button></td>"
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

//clear fields function
function clearFields() {
    $('#txtName').val('');
    $('#txtAge').val('');
    $('#txtActive').val('');
    $('#txtId').val('');
}
