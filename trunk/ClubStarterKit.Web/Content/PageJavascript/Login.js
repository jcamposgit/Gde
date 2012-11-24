function afterLogin(data) { 
    if (data.url) {
        window.location = data.url;
        return true;
    }
    $("#loginResult").show();
}

function showForgotMessage() {
    $("#forgotEmailMsg").show();
}

function showRegistrationMessage() {
    $("#regMsg").show();
}