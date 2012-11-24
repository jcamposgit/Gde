// 8
/// <reference path="jquery-1.3.2.js" />
/// <reference path="jquery-blockui.js" />
/// <reference path="jquery-ui.js" />
/// <reference path="jquery-metadata.js" />

$(document).ready(function () {
    $("a.confirmation").each(function () {
        setupConfirmation($(this));
    });
});

function setupConfirmation(link) {
    // extract metadata
    $.metadata.setType("attr", "rel");
    var metadata = link.metadata();

    // save location
    var linkLocation = link.attr("href");
    // replace location
    link.attr("href", "#");

    // append dialog
    var dialogId = "confirmationDialog" + link.attr("id");
    var dialogText = metadata.dialogText || "Are you sure?";
    var dialogTitle = metadata.dialogTitle || "Confirmation";
    
    var dialogHtml = "<div id='" + dialogId + "' title='" + dialogTitle + "'><p>" + dialogText + "</p></div>";
    $(".page").append(dialogHtml);

    $("#" + dialogId).dialog({
        bgiframe: true,
        resizable: false,
        autoOpen: false,
        draggable: false,
        height: 140,
        modal: true,
        overlay: {
            backgroundColor: '#000',
            opacity: 0.5
        },
        buttons: {
            Yes: function () {
                $(this).dialog('close');
                window.location = linkLocation;
            },
            No: function () {
                $(this).dialog('close');
            }
        }
    });

    link.click(function () {
        $("#" + dialogId).dialog('open');
    });
}