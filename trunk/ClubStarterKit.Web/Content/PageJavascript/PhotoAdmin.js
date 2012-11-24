/// <reference path="../Javascript/jquery-1.3.2.js" />
/// <reference path="../Javascript/jquery-blockui.js" />

$(document).ready(function () {
    $("a.deletePhoto").click(function () {
        var elem = $(this);
        elem.block();
        $.post(elem.attr("loc"), null, function (data) {
            if (data.success) {
                $.growlUI("Photo Deletion", "Photo deleted.");
                elem.unblock();
            }
            else {
                $.growlUI("Photo Deletion", "There was an error deleting your photo.");
                elem.unblock();
            }
        }, "json");
        return true;
    });
});