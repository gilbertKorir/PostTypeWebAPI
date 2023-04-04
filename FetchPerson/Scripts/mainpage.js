$(document).ready(function () {

    $("#emp").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: "https://localhost:44386/Person/PersonAction",
            success: function (data) {
                $("#items").html(data);
            }
        });
    });


    $("#acc").click(function (e) {
        e.preventDefault();
        $.ajax({
            url:"https://localhost:44386/Accounts/AccountsAction",
            success: function (data) {
                $("#items").html(data);
            }
        });
    })


    $("#transaction").click(function (e) {
        e.preventDefault();
        $.ajax({
            url:"https://localhost:44386/Transactions/TransactionsAction",
            success: function (data) {
                $("#items").html(data);
            }
        });
    })

    $("#reports").click(function (e) {
        e.preventDefault();
        $.ajax({
            url:"https://localhost:44386/Reports/ReportsAction",
            success: function (data) {
                $("#items").html(data);
            }
        });
    })

});

function employeecount() {
    $.ajax({
        type: "POST",
        url: "/Home/GetAllEmployees",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //alert(JSON.stringify(data));
                //for (let i = 0; i < data.length; i++) {
            const count = data.length;
            document.getElementById('employeeCount').innerHTML = `Total count: ${count}`;
               // }
        
        },
        error: function (xhr, status, error) {
            alert("error in operation");
        }
    });
}


