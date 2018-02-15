$(document).ready(function () {

});

function SetMenu(userprofile)
{
    if (userprofile == "HR User")
    {
        $(".GlobalHRprofile").addClass("hidden");
    }
    else if(userprofile=="Global HR Admin")
    {
        $(".HRprofile").addClass("hidden");
    }
}