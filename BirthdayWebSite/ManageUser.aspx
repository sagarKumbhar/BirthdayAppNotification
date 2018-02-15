<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" Inherits="ManageUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <link href="Content/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/bootstrap-datepicker.js"></script>
    <link href="Content/datepicker.css" rel="stylesheet" />
    <script src="Scripts/SiteScripts/ManageUser.js"></script>

    <script type="text/javascript">
        var hidID = "<%=hidID.ClientID%>";
        var btnAutoUpdate = "<%=btnAutoUpdate.ClientID%>";
        var name = "<%=name.ClientID%>";
        var Email = "<%=Email.ClientID%>";
        var fuploadEmpPic = "<%=fuploadEmpPic.ClientID%>";
        var imgPhoto = "<%=imgPhoto.ClientID%>";
    </script>
    <style>
        .alert {
            display: none;
        }
    </style>
    <div class="alert  alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <span id="Spalert">test </span>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><span class="glyphicon glyphicon-file"></span>Add new Employee</a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 ">
                                    <asp:HiddenField runat="server" ID="hidID"></asp:HiddenField>
                                    <div class="form-group col-md-6">
                                        Name:
                                        <input type="text" class="form-control" placeholder="Name of Employee" name="UserName" id="name" runat="server" required maxlength="80" />

                                    </div>
                                    <div class="form-group col-md-6">
                                        EmployeeId/GlobalView ID:
                                        <input type="text" pattern="\d*" class="form-control" maxlength="10" id="EmpID" placeholder="Employee ID/ GV ID" runat="server" required title="Please enter numeric character." />
                                    </div>
                                    <div class="form-group col-md-6">
                                        Email:
                                        <input type="text" class="form-control" id="Email" placeholder="Email" runat="server" title="Please enter valid email." required />
                                    </div>
                                    <div class="form-group col-md-6">
                                        Department:
                                        <input type="text" class="form-control" id="txtDepartment" placeholder="Department" readonly runat="server" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        Birthdate:
                                        <div class="input-group date" id="dp2" data-date-format="dd/mm/yyyy">
                                            <input type='text' class="form-control" name='txtCalendar' style="background-color: lightgray; padding: 0" id='bdate' runat="server" required>
                                            <span class="input-group-addon add-on" style="cursor: pointer; width: inherit !important"><span class="glyphicon glyphicon-calendar"></span></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Joining Date:
                                        <div class="input-group date" id="dp3" data-date-format="dd/mm/yyyy">
                                            <input type='text' class="form-control" name='txtCalendarJD' style="background-color: lightgray; padding: 0" id='txtJoiningDate' runat="server">
                                            <span class="input-group-addon add-on" style="cursor: pointer; width: inherit !important"><span class="glyphicon glyphicon-calendar"></span></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Marriage Anniversary:
                                        <div class="input-group date" id="dp4" data-date-format="dd/mm/yyyy">
                                            <input type='text' class="form-control" name='txtCalendarMD' style="background-color: lightgray; padding: 0" id='txtMarriageDate' runat="server">
                                            <span class="input-group-addon add-on" style="cursor: pointer; width: inherit !important"><span class="glyphicon glyphicon-calendar"></span></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <div>
                                            <asp:Image ID="imgPhoto" runat="server" ImageUrl="~/Images/noData.png" Style="max-height: 150px!important; max-width: 150px !important" />
                                        </div>
                                        Upload Employee photo:
                                        <asp:FileUpload ID="fuploadEmpPic" runat="server" CssClass="form-control" />
                                        <div class="alert-info">
                                            <ul>
                                                <li>Max size 1 mb</li>
                                                <li>Supported formats '.png', '.jpg' and 'jpeg'.</li>
                                            </ul>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success " OnClick="btnSave_Click" OnClientClick="return setUploadButtonState();" />
                                    <button data-bind="" class="btn btn-danger" onclick="ResetCreateBox();">Reset</button>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><span class="glyphicon glyphicon-upload"></span>Upload bulk using Excel file</a>
                        </h4>
                    </div>
                    <div id="collapseTwo" class="panel-collapse collapse">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="form-group col-md-12">
                                        <span class="glyphicon glyphicon-download-alt">
                                            <asp:LinkButton ID="lnkDownloadTemplate" Font-Names="century gothic" runat="server" OnClick="lnkDownloadTemplate_Click">Download Template</asp:LinkButton>
                                        </span>
                                    </div>
                                    <div class="form-group col-md-12">
                                        Upload Excel:
                                        <asp:FileUpload ID="fuploadExcel" runat="server" CssClass="form-control" /><br />
                                        <%--<asp:Button ID="btnUplaodExcel" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUplaodExcel_Click" />--%>
                                        <input type="submit" formnovalidate class="btn btn-primary" onserverclick="btnUplaodExcel_Click" id="btnUplaodExcel" value="Upload" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#collapseThree"><span class="glyphicon glyphicon-th-list "></span>List Registered employees</a>
                        </h4>
                    </div>
                    <div id="collapseThree" class="panel-collapse collapse">
                        <div class="panel-body">

                            <asp:UpdatePanel runat="server" ID="upDatatable" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <span class="glyphicon glyphicon-refresh" id="spUser" title="Refresh" style="cursor: pointer"></span>
                                    <div class="block-content">
                                        <div class="table-responsive">

                                            <table class="display table-hover table-striped table-iconed table-sortable table-responsive" width="100%" id="tblreport">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>EmployeeName</th>
                                                        <th>Birthdate</th>
                                                        <th>Joining Date</th>
                                                        <th>Marriage Annivarsary</th>
                                                        <th>Email</th>
                                                        <th>EmployeeId</th>
                                                        <th>Photo</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>

                                            </table>
                                        </div>
                                    </div>
                                    <input type="submit" formnovalidate style="display: none" onserverclick="btnAutoUpdate_Click" id="btnAutoUpdate" runat="server" />

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            // $('#myTable').dataTable();
            //$('#tblreport').dataTable();
        });
    </script>
    <%--    <script src="JS_Datatable/jquery.min.js"></script>
    <link href="JS_Datatable/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="JS_Datatable/jquery.dataTables.min.js"></script>--%>
</asp:Content>
