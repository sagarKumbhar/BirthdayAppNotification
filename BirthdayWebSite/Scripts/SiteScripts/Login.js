$(function () {
    var LoginModel = function () {
        this.UserID = "";
        this.Password = "";

        this.Login = function () {
            //alert(this.UserID + " " + this.Password);
            //window.location.href = "ManageUser.aspx";

            $.ajax({
                type: 'POST',
                url: "Login.aspx/AuthenticateUser",
                data: '{"UserID":"' + this.UserID +'","Password":"'+ this.Password +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        $('.input').keypress(function (e) {
            if (e.which == 13) {
                $('form#login').submit();
                return false;    //<---- Add this line
            }
        });

    };
    function OnSuccess(response) {

        if (response.d == "failed")
            alert("Authentication failed !");
        else
        {
            window.location.href = "default.aspx";
        }
    }
    ko.applyBindings(new LoginModel());
});