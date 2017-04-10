$(document).ready(function () {
    $("#squaredTwo").click(function () {
        if ($(this).is(":checked"))
        {
            $("#hiddensquaredTwo").val("true");
        }
        else {
            $("#hiddensquaredTwo").val("false");
        }
    });
});
