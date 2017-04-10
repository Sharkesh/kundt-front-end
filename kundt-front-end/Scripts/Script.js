$(document).ready(function () {
    if ($("#squaredTwo").attr("checked")) {
        $("#squaredTwo").click(function () {
            $("#hiddensquaredTwo").val("true");
        });
    } else {
        $("#squaredTwo").click(function () {
            $("#hiddensquaredTwo").val("false");
        });
    }
});
