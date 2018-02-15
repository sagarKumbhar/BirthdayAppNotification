$(document).ready(function () {


    //#######################################################

    $("#upExcel").click(function () {
        $("#upExcelBlock").toggle();
    });

    $('#dp2').datepicker(
          {
              startView: "decade"
          });
    $('#dp3').datepicker(
         {
             startView: "decade"
         });
    $('#dp4').datepicker(
         {
             startView: "decade"
         });
    $("#spUser").click(function () {

        $('#tblreport').dataTable();
        $("#" + btnAutoUpdate).click();
    });
    $('#lnkDownloadTemplate').click(function () {
        var filepath = $(this).attr('data-filepath');
        top.location.href = filepath;
    });

});

function EditRow(id) {
    $.ajax({
        type: 'POST',
        url: "ManageUser.aspx/GetUserById",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessEdit,
        failure: function (response) {
            alert(response.d);

        }
    });
}

function OnSuccessEdit(response) {

    var empdata = $.parseJSON(response.d);
    console.log(empdata[0]);
    $("#collapseOne").collapse('show');
    $("#MainContent_name").val(empdata[0].EmployeeName);
    $("#MainContent_EmpID").val(empdata[0].EmployeeId);
    $("#MainContent_Email").val(empdata[0].Email);
    $("#Department").val(empdata[0].DepartmentId);
    $("#MainContent_bdate").val(empdata[0].Birthdate);
    if (empdata[0].PhotoLocation != "")
        $("#MainContent_imgPhoto").attr("src", (empdata[0].PhotoLocation));
    else
        $("#MainContent_imgPhoto").attr("src", "Images/noData.png");
    $("#" + hidID).val(empdata[0].ID);


}
function DeleteRow(id) {
    $.ajax({
        type: 'POST',
        url: "ManageUser.aspx/DeleteEmployee",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessDelete,
        failure: function (response) {
            alert(response.d);

        }
    });
}

function OnSuccessDelete(response) {

    if (response.d == "success") {
        ShowAlert("success", "Deleted Successfully.");
        $("#" + btnAutoUpdate).click();
    }
    else {
        ShowAlert("danger", "Error occurred in deleteing the records.");
    }
}
function setUploadButtonState() {

    var maxFileSize = 1048576; // 1MB -> 1 * 1024 * 1024
    var fileUpload = $('#' + fuploadEmpPic);

    var isValid = false
    var msg = "";

    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test($("#" + Email).val())) {
        isValid = true;
    }
    else {
        msg += "Please check Email format";
        isValid = false;
    }
    if (isValid) {

        if (fileUpload.val() == '') {
            isValid = true;
        }
        else {

            var ext = fileUpload.val().split('.').pop().toLowerCase();
            if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
                msg = "Invalid photo format. Use '.png','jpg' or 'jpeg' files.";
                isValid = false;
            }
            if (!isValid) {
                if (fileUpload[0].files[0].size < maxFileSize) {

                    isValid = true;
                } else {
                    msg = "Please check size of the photo.";
                    isValid = false;
                }
            }
        }

    }


    if (isValid == false) {

        ShowAlert("warning", msg);
        return false;
    }
    else {
        return true;
    }
}
function ShowAlert(val, msg) {
    //  alert(msg);
    $("#Spalert").text(msg);
    $(".alert").addClass("alert-" + val);
    $(".alert").hide().show();
    window.setTimeout(function () { $(".alert").hide(); }, 6000);

}

function ShowAlertExcel(val, msg) {
    //  alert(msg);
    $("#Spalert").html(msg);
    $(".alert").addClass("alert-" + val);
    $(".alert").hide().show();


}

function ResetCreateBox() {
    $("#MainContent_name").val("");
    $("#MainContent_EmpID").val("");
    $("#MainContent_Email").val("");
    $("#Department").val("");
    $("#MainContent_bdate").val("");
    $("#MainContent_imgPhoto").attr("src", "Images/noData.png");
    $("#" + hidID).val("");
}
function BindListTable(vData) {

    var testdata = $.parseJSON(vData);
    $('#tblreport').dataTable({
        responsive: true,
        "aaData": testdata,
        "aoColumns": [
            { "mData": "ID" },
            { "mData": "EmployeeName" },
            { "mData": "Birthdate" },
            { "mData": "JoiningDate" },
            { "mData": "MarriageDate" },
            { "mData": "Email" },
            { "mData": "EmployeeId" },
            { "mData": "PhotoUploaded" },
             {
                 "mRender": function (data, type, row) {
                     var editbtn = '<span id=' + row.ID + ' class="glyphicon glyphicon-edit" title="Edit" style="cursor:pointer" onclick="EditRow(' + row.ID + ');"></span> &nbsp;&nbsp;';
                     var deletebtn = '<span id=' + row.ID + ' class="glyphicon glyphicon-remove" title="delete" style="cursor:pointer" onclick="DeleteRow(' + row.ID + ');"></span>'
                     return editbtn + deletebtn;
                 }
             }
        ]
    });

}
