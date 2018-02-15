$(document).ready(function () {

    $("#spUser").click(function () {

        $('#tblreport').dataTable();
        $("#" + btnAutoUpdate).click();
    });



});

function EditRow(id) {

    $.ajax({
        type: 'POST',
        url: "ManageHRUser.aspx/GetLoginUserById",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessEdit,
        failure: function (response) {
            alert(response.d);

        }
    });


}


function DeleteRow(id) {
    $.ajax({
        type: 'POST',
        url: "ManageHRUser.aspx/DeleteLoginUser",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessDelete,
        failure: function (response) {
            alert(response.d);

        }
    });
}

function OnSuccessEdit(response) {

    var empdata = $.parseJSON(response.d);
    console.log(empdata[0]);
    $("#collapseOne").collapse('show');
    $("#MainContent_userCode").val(empdata[0].UserCode);
    $("#MainContent_password").val(empdata[0].Password);
    $("#" + ddlDepartment).val(empdata[0].DepartmentId);


    $("#" + hidID).val(empdata[0].ID);


}


function OnSuccessDelete(response) {

    if (response.d == "success") {
        ShowAlert("success", "Delete Successfully.");
        $("#" + btnAutoUpdate).click();
    }
    else {
        ShowAlert("danger", "Error occurred in deleteing the records.");
    }
}


function OpenModal() {
    $('#myModal').modal("show");
}


function CreateDepartment() {
    var Departmentname = $("#DepartmentName").val();
    var shortname = $("#shortName").val();

    if (Departmentname != "" && shortname != "") {
        $.ajax({
            type: 'POST',
            url: "ManageHRUser.aspx/AddDepartment",
            data: '{"DepartmentName":"' + Departmentname + '","shortName":"' + shortname + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            }
        });
    }
    else {
        alert("Please check department and department shortname values are filled.");
    }

}

function OnSuccess(response) {
    if (response.d > 0) {
        ShowAlert("success", "Department added successfully!");
        $('#myModal').modal("hide");
        $("#" + btnUpdateddl).click();
    }
    else {
        alert("Same department is already present. Please check names.");
    }
}
function ShowAlert(val, msg) {

    $("#Spalert").text(msg);
    $(".alert").addClass("alert-" + val);
    $(".alert").hide().show();
    window.setTimeout(function () { $(".alert").hide(); }, 4000);

}
function ResetCreateBox() {
    $("#MainContent_userCode").val("");
    $("#MainContent_password").val("");
    $("#" + hidID).val("");
}
function BindListTable(vData) {

    var testdata = $.parseJSON(vData);
    $('#tblreport').dataTable({
        destroy: true,
        "aaData": testdata,
        "aoColumns": [
            { "mData": "ID" },
            { "mData": "UserCode" },
            { "mData": "Password" },
            { "mData": "Department" },
             {
                 "mRender": function (data, type, row) {
                     var editbtn = '<span id=' + row.ID + ' class="glyphicon glyphicon-edit " title="Edit" style="cursor:pointer" onclick="EditRow(' + row.ID + ');"></span> &nbsp;&nbsp;';
                     var deletebtn = '<span id=' + row.ID + ' class="glyphicon glyphicon-remove " title="delete"  style="cursor:pointer" onclick="DeleteRow(' + row.ID + ');"></span>'
                     return editbtn + deletebtn;
                 }
             }
        ]
    });

}
