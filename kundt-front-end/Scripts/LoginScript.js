function ValidateEmail() {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test($('#email').val())) {
        $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3');
        $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#loginSubmit,#loginSubmitMobile').prop('disabled', true);
        $('#email').css('border', 'solid 2px red');
        return false;
    } else {
        $('#email').css('border', 'solid 2px green');
        return true;
    }
}

function PasswordCheck() {
    var password = $('#password').val();
    if (password.length < 8) {
        $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3');
        $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#loginSubmit,#loginSubmitMobile').prop('disabled', true);
        $('#password').css('border', 'solid 2px red')
        return false;
    } else {
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasNumbers = /\d/.test(password);
        var hasNonalphas = /\W/.test(password);
        if (hasUpperCase + hasLowerCase + hasNumbers + hasNonalphas < 3) {
            $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3');
            $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
            $('#loginSubmit,#loginSubmitMobile').prop('disabled', true);
            $('#password').css('border', 'solid 2px red')
            return false;
        } else {
            $('#password').css('border', 'solid 2px green');
            return true;
        }
    }
}

function PasswordConfirmationCheck() {
    var password = $('#password').val();
    var passwordConfirmation = $('#passwordConfirmation').val();
    if (password != passwordConfirmation || password == "") {
        $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3');
        $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#loginSubmit,#loginSubmitMobile').prop('disabled', true);
        $('#passwordConfirmation').css('border', 'solid 2px red')
        return false;
    } else {
        $('#passwordConfirmation').css('border', 'solid 2px green')
        return true;
    }
}

function ValidateInput(method) {
    if (method == 'register') {
        if (ValidateEmail() && PasswordCheck() && PasswordConfirmationCheck()) {
            $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3OhneAnimation');
            $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3');
            $('#loginSubmit,#loginSubmitMobile').prop('disabled', false);
        }
    } else if (method == 'login') {
        if (ValidateEmail() && PasswordCheck()) {
            $('#loginSubmit,#loginSubmitMobile').removeClass('buttonOrangeForwardStep3OhneAnimation');
            $('#loginSubmit,#loginSubmitMobile').addClass('buttonOrangeForwardStep3');
            $('#loginSubmit,#loginSubmitMobile').prop('disabled', false);
        }
    }
}