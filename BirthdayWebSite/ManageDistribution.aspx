<%@ Page Title="Manage Recipients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ManageDistribution.aspx.cs" Inherits="ManageDistribution" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <style>
        .info {
            background-color: darkolivegreen;
            font-size: medium;
            border: 1px solid black;
            color: white;
            padding: 5px 7px 2px 4px;
        }
    </style>


    <div class="">
        <div class="info">
            Showing distribution list details for department:
            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
        </div>
        <div class="row"></div>
        <div class="row">
            <div class="col-md-12">
                <br />
                Include added users :
                <asp:CheckBox ID="chkIncludeAddedUsers" runat="server" />
                <br />
                <div class="alert-info">
                    Selecting this option will include users added into application under current department.<br />
                    If you choose to add a distribution list which already includes all users from the department, you can unselect this option.
                </div>

            </div>
        </div>
        <br />
        <br />

        <div class="row">
            <div class="col-md-12">
                Add Recipients List: 
                <asp:TextBox ID="txtUSers" runat="server" TextMode="MultiLine" Columns="15" Rows="3" CssClass="form-control"></asp:TextBox>

            </div>
            <div class="alert-info" style="border: 1px solid gold">
                <ul>
                    <li>To add distribution list, please include Email address of the distribution list
                        <br />
                        for example, to add list "DL-IN-PUN-CWS"  add the email address of the list i.e. "DL-IN-PUN-CWS@ege.faurecia.com" (which you can reteieve from Outlook).
                    </li>
                    <li>You can add multiple addresses, just seperate them by semicolon (;) </li>
                </ul>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />


            </div>
        </div>
    </div>


</asp:Content>
