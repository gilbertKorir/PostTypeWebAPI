$(document).ready(function () {
    fetchData();
});


//clear fields function
function clear() {
    $('#txtName').val('');
    $('#txtAge').val('');
    $('#txtActive').prop('checked', false);
    $('#txtId').val('');
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
    //alert(JSON.stringify(objEmp));
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
            alert("dice");
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

function Edit(id) {
    if (id > 0) {
        $.ajax({
            url: "/Person/UpdateEmp?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                $(".empId").show();

            $("#txtName").val();
              $("#txtAge").val();
              $("#txtActive").val();
               $("#txtId").val();

            },
            error: function (msg) {
                fetchData();
                clear();
            }
        });
    }
}


$(document).ready(function () {
    fetchData();
});


//clear fields function
function clear() {
    $('#txtName').val('');
    $('#txtAge').val('');
    $('#txtActive').prop('checked', false);
    $('#txtId').val('');
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
    //alert(JSON.stringify(objEmp));
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
            fetchData();
            clear();
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
                        + "<td><button id='btnEdit' class='btn btn-success' onclick='editproduct(" + data[i].Id + ")'>Edit</button></td>"
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

function editproduct(id) {
    $(".empId").show();
 
    if (id > 0) {
        $.ajax({
            url: "/Person/UpdateEmp",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                //console.log(data); 
                $("#txtName").val(data.Name);
                $("#txtAge").val(data.Age);
                $("#txtActive").val(data.Active);
                $("#txtId").val(data.Id);

            },
            error: function (msg) {
                alert("error");
                //fetchData();
                //clear();
            }
        });
    }
}


