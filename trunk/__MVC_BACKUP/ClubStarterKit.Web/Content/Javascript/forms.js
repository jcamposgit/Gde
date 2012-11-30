// 8

// the form functionality requires:
// * code in ClubStarterKit.Infrastructure.UI.AsyncForm
// * jquery
// * jquery form pluggin
// * jquery metadata pluggin
// * jquery validation pluggin
// * jquery wysiwyg 
//   (only for wysiwgy editor hook in first few lines)

/// <reference path="jquery-1.3.2.js" />

$(document).ready(function () {
    // all elements with the wysiwyg class will be
    // "wysiwyg"-ed
    $('.wysiwyg').each(function (i) {
        $(this).wysiwyg();
    });

    // trigger validation on all forms marked for validation
    $('form.validateForm').each(function (i) {
        $(this).validate();
    });

    /// add the X-Requested-With header to every ajax request 
    // to signify that the request is an AJAX request for MVC
    jQuery.ajaxSetup({
        beforeSend: function (xhr) {
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            return xhr;
        },
        error: function () {
            window.location = "/error";
        }
    });
    // setup async forms
    setupForms();
});


function setupForms() {
    $.metadata.setType("attr", "rel");
    
    $('form.async').each(function(i){        
        setupSingleForm($(this));
    });
}

function setupSingleForm(frm) {
    var metadata = frm.metadata();
    var _dataType = metadata.datatype;
    var _blockElem = $(metadata.elem);
    var _before = metadata.before;
    var _after = metadata.after;
    var _target = null;
    
    // determine the type of form
    if (metadata.action == 'redirection') {
        _after = function (data, status) {
            metadata.after(data, status);
            window.location = data.url;
        }
    }
    else if (metadata.action == 'targetupdate') 
        _target = $(metadata.target)

    frm.ajaxForm({
        dataType: _dataType,
        success: function (responseText, statusText) {
            _blockElem.unblock();
            _after(responseText, statusText);
            // setup forms if there is an added form
            setupForms();
        },
        beforeSubmit: function (formData, jqForm, options) {
            var submitForm = _before(formData, jqForm, options) && frm.valid();
            if (submitForm)
                _blockElem.block();
            return submitForm;
        },
        target: _target
    });
}