/// <reference path="../Javascript/jquery-1.3.2.js" />
/// <reference path="../Javascript/jquery-blockui.js" />


function UpdateList(data) {

    if (!data.success) {
        $.growlUI('Add Answer', 'Adding answer failed.'); 
        return false;
    }

    var elem = $("input[name='answer']");
    $("#answers").append("<li>" + elem.val() + "</li>");
    elem.val("");
}