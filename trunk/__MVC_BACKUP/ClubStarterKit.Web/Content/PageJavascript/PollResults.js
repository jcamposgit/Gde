$(document).ready(function () {
    // load the results as progress bars
    $(".poll-result").each(function () {
        var i = $(this);
        var percent = i.attr("value");
        i.progressbar({
            value: percent
        });
    });
});