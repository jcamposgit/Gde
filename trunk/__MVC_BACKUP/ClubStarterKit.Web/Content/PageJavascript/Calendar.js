/// <reference path="../Javascript/jquery-1.3.2.js" />

$(document).ready(function () {
    $.historyInit(LoadCalFromHash, document.location);
    replaceCalHrefs();
});

function LoadCalFromHash(hash) {
    hash = hash || $("#currentLink").attr("value");
    var storageDiv = $("#calendar");

    storageDiv.block();
    // ajax load content
    storageDiv.load('/events/calendar/' + hash, null, function () {
        // scroll to top
        $('html, body').animate({ scrollTop: 0 }, 'fast');
        storageDiv.unblock();
        replaceCalHrefs(); // the new links need to be set
    });
}

function replaceCalHrefs() {
    // replace all links with rev attribute
    $("a[rev]").each(function () {
        var elem = $(this);
        var link = elem.attr("href");

        // if href already has a "#", no need to exchange
        if (link.charAt(0) == '#')
            return;

        elem.attr("href", "#" + elem.attr("rev"));
        elem.attr("loc", link);
    });
}