<%@ Page Title="Manage HR Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ManageHRUser.aspx.cs" Inherits="ManageHRUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <link href="Content/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
    <script src="Scripts/SiteScripts/ManageHRUser.js"></script>
    <script type="text/javascript">
        var btnAutoUpdate = "<%=btnAutoUpdate.ClientID%>";
        var ddlDepartment = "<%=ddlDepartment.ClientID%>";
        var hidID = "<%=hidID.ClientID%>";
        var btnUpdateddl = "<%=btnUpdateddl.ClientID%>";
    </script>
    <style>
        .alert {
            display: none;
        }

        .modal-dialog {
            display: inline-block;
            text-align: left;
            vertical-align: middle;
        }
    </style>

    <div class="alert  alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <span id="Spalert">test </span>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="myModal" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">Add new Department</h4>
                </div>
                <div class="modal-body" id="idDivBody">

                    <div class="row">
                        <div class="col-md-12 ">
                            <div class="form-group col-md-6">
                                Department Name:
                                        <input type="text" class="form-control" placeholder="Name of Department" id="DepartmentName" maxlength="20" />
                            </div>
                            <div class="form-group col-md-6">
                                ShortName:
                                        <input type="text" class="form-control" id="shortName" placeholder="Acronymn" maxlength="7" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" onclick="CreateDepartment();">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <div class="row">
        <div class="col-md-12">
            <div class="panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><span class="glyphicon glyphicon-file"></span>Add new HR User</a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <asp:HiddenField runat="server" ID="hidID"></asp:HiddenField>
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upCreateLoginUser">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 ">
                                            <div class="form-group col-md-6">
                                                User Code:
                                        <input type="text" class="form-control" placeholder="User code" id="userCode" required runat="server" maxlength="10" />
                                            </div>
                                            <div class="form-group col-md-6">
                                                Password:
                                        <input type="text" class="form-control" id="password" placeholder="Password" required runat="server" maxlength="10" />
                                            </div>
                                            <div class="form-group col-md-6">
                                                Department:
                                            <div class=" input-group ">

                                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                                                <span class="input-group-addon add-on" onclick="OpenModal();" style="cursor: pointer; width: inherit !important"><span class="glyphicon glyphicon-plus"></span></span>

                                            </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                                Profile:
                                        <asp:DropDownList ID="ddlProfile" CssClass="form-control" Width="100%" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <asp:Button ID="btnSaveData" runat="server" Text="Save" OnClick="btnSaveData_Click" CssClass="btn btn-primary" />
                                            <button class="btn btn-warning" onclick="ResetCreateBox();" type="button">Reset</button>
                                        </div>
                                    </div>
                                    <input type="submit" formnovalidate style="display: none" onserverclick="UpdateDdl" id="btnUpdateddl" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><span class="glyphicon glyphicon-th-list"></span>List Registered HR Users</a>
                        </h4>
                    </div>
                    <div id="collapseTwo" class="panel-collapse collapse">
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="upDatatable" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div>
                                        <span class="glyphicon glyphicon-refresh" id="spUser" title="Refresh" style="cursor: pointer"></span>
                                        <div class="block-content">
                                            <div class="table-responsive">
                                                <table class="table table-hover table-striped table-iconed table-sortable" id="tblreport">
                                                    <thead>
                                                        <tr>
                                                            <th>ID</th>
                                                            <th>UserCode</th>
                                                            <th>Password</th>
                                                            <th>Department</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <input type="submit" formnovalidate style="display: none" onserverclick="btnAutoUpdate_Click" id="btnAutoUpdate" runat="server" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
