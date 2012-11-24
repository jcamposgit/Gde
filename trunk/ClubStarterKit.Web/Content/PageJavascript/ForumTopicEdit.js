$(document).ready(function () {
    $("a.cur-group").each(function () {
        var link = $(this);
        link.click(function () {
            $("input[name=Topic.Group]").val(link.html());
        });
    });
});