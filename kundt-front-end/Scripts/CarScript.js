$(document).on("mousemove", function (event) {
    if (document.title == "Kundt Autoverleih Fehler!") {
        $("#log").text("pageX: " + event.pageX + ", pageY: " + event.pageY);

        //rotierendes Element!
        var img = $('.eye1');
        var offset = img.offset();
        var center_x = (offset.left) + (img.width() / 2);
        var center_y = (offset.top) + (img.height() / 2);
        var mouse_x = event.pageX;
        var mouse_y = event.pageY;
        var radians = Math.atan2(mouse_x - center_x, mouse_y - center_y);
        var degree = (radians * (180 / Math.PI) * -1) + 90;

        $(".eye1").css({ 'transform': 'rotate(' + (degree + 270) + 'deg)' });
        //$(".eye1").css({ '-moz-transform': 'rotate(' + (degree + 270) + 'deg)' });
        //$(".eye1").css({ '-webkit-transform': 'rotate(' + (degree + 270) + 'deg)' });
        //$(".eye1").css({ '-o-transform': 'rotate(' + (degree + 270) + 'deg)' });
        //$(".eye1").css({ '-ms-transform': 'rotate(' + (degree + 270) + 'deg)' });

        var img2 = $('.eye2');
        var offset2 = img2.offset();
        var center_x2 = (offset2.left) + (img2.width() / 2);
        var center_y2 = (offset2.top) + (img2.height() / 2);
        var mouse_x2 = event.pageX;
        var mouse_y2 = event.pageY;
        var radians2 = Math.atan2(mouse_x2 - center_x2, mouse_y2 - center_y2);
        var degree2 = (radians2 * (180 / Math.PI) * -1) + 90;

        $(".eye2").css({ 'transform': 'rotate(' + (degree2 + 270) + 'deg)' });
        //$(".eye2").css({ '-webkit-transform': 'rotate(' + (degree2 + 270) + 'deg)' });
    }
});

//Soundeffect
//$(document).ready(function () {
//    var audioElement = document.createElement('audio');
//    audioElement.setAttribute('src', 'car+horn+x.mp3');

//    audioElement.addEventListener('ended', function () {
//        this.play();
//    }, false);

//    audioElement.addEventListener("canplay", function () {
//        $("#length").text("Duration:" + audioElement.duration + " seconds");
//        $("#source").text("Source:" + audioElement.src);
//        //$("#status").text("Status: Ready to play").css("color", "green");
//    });

//    audioElement.addEventListener("timeupdate", function () {
//        $("#currentTime").text("Current second:" + audioElement.currentTime);
//    });

//    $('#play').click(function () {
//        audioElement.play();
//        //$("#status").text("Status: Playing");
//    });

//    //$('#pause').click(function () {
//    //    audioElement.pause();
//    //    $("#status").text("Status: Paused");
//    //});

//    //$('#restart').click(function () {
//    //    audioElement.currentTime = 0;
//    //});
//});
//didnt't work at all..?

function playSound() {
    if (document.title == "Kundt Autoverleih Fehler!") {
        document.getElementById('audiofile').play();
    }
}