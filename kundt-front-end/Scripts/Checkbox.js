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

$(document).ready(function () {
    $("#squaredTwo2").click(function () {
        if ($(this).is(":checked")) {
            $("#hiddensquaredTwo2").val("true");
        }
        else {
            $("#hiddensquaredTwo2").val("false");
        }
    });
});