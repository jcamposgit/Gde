/// <reference path="../Javascript/jquery-1.3.2.js" />

$(document).ready(function () {
    $.historyInit(LoadPageFromHash, document.location);
    replacePagerHrefs();
});

function LoadPageFromHash(hash) {
    hash = hash || $(".paged-link-first").attr("title");
    var storageDiv = $("#ajax-page-list");
    var metadata = storageDiv.metadata();
    var blockingElem = $("#" + (metadata.block || storageDiv));
    var link = $("a[title='" + hash + "']");

    // the default is loc, but it might not have been loaded
    var page = link.attr("loc") || link.attr("href");

    blockingElem.block();
    // ajax load content
    storageDiv.load(page, null, function () {
        // scroll to top
        $('html, body').animate({ scrollTop: 0 }, 'fast');
        blockingElem.unblock();
        replacePagerHrefs(); // the new pager needs to be set

        if (metadata.afterChange && jQuery.isFunction(metadata.afterChange)) {
            metadata.afterChange();
        }
    });    
}

function replacePagerHrefs() {
    // replace all pager links
    $(".pager a").each(function () {
        var elem = $(this);
        var link = elem.attr("href");

        // if href already has a "#", no need to exchange
        if (link.charAt(0) == '#')
            return;
            
        elem.attr("href", "#" + elem.attr("title"));
        elem.attr("loc", link);
    });
}