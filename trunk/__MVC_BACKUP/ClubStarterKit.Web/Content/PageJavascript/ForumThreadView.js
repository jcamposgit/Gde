$(document).ready(setupLinks);

function setupLinks() {
    $("a.markspam").each(function () {
        var par = $(this).parent().get(0);
        $(this).click(function () {
            $(par).block();
            $.post($(this).attr("title"),
            null,
            function (data) {
                if (data.success) {
                    $.growlUI("Mark Spam", "Message marked as spam.");
                }
                else {
                    $.growlUI("Mark Spam", "Message spam mark failed.");
                }
                $(par).unblock();
            },
            "json");
            $(this).hide();
        });
    });

    $("a.editmessage").each(function () {
        $(this).click(function () {
            var metadata = $(this).metadata();
            setEditMode(metadata.message);
            $('html, body').animate({ scrollTop: $("#message-add-block").offset().top }, 'fast');
        });
    });

    $("a#newmessage").click(function () {
        setNewMessageMode();
        $('html, body').animate({ scrollTop: $("#message-add-block").offset().top }, 'fast');
    });

    $("a.deletemessage").each(function () {
        $(this).attr("title", $(this).attr("href"));
        $(this).attr("href", "#" + $(this).attr("rev"));

        $(this).click(function () {
            $.metadata.setType("rel")
            var par = $("#" + $(this).attr("rev"));
            $(par).block();

            $.post($(this).attr("title"),
                null,
                function (data) {
                    if (data.success) {
                        par.unblock();
                        par.hide('fast');
                        $.growlUI("Delete Message", "Message deleted.");
                    }
                    else {
                        par.unblock();
                        $.growlUI("Delete Message", "Message deletion failed.");
                    }
                },
                "json");
        });
    });
}

function setEditMode(messageID) {
    $("#message-add-block h3").html("Edit Message");
    $("#message-add-block input[name='messageId']").attr("value", messageID);
    $("#message-add-block input[type='submit']").attr("value", "Save Message");
    $($("#messageIFrame").document()).find('body').html($("#" + messageID + " .msg").html());
    $("a#newmessage").show();
}

function setNewMessageMode() {
    $("#message-add-block h3").html("Add Message");
    $("#message-add-block input[name='messageId']").attr("value", "-1");
    $("#message-add-block input[type='submit']").attr("value", "Add Message");
    $($("#messageIFrame").document()).find('body').html("");
    $("a#newmessage").hide();
}

function afterMessageUpdate() {
    var id = $("#message-add-block input[name='messageId']").attr("value");
    setupLinks();
    setNewMessageMode();

    if (id && id != "-1" && id != -1) {
        $('html, body').animate({ scrollTop: $("#" + id).offset().top }, 'fast');
    }
}