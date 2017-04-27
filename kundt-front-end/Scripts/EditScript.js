function PasswordCheck(method) {
    var password = $('#password' + method).val();
    var passwordConfirmation = $('#passwordConfirmation').val();
    if (password.length == 0 && passwordConfirmation.length == 0) {
        $('#submit').removeClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit').addClass('buttonOrangeForwardStep3');
        $('#submit').prop('disabled', false);
        $('#password' + method).css('border', 'solid 1px #ccc');
        return true;
    } else if (password.length < 8) {
        $('#submit').removeClass('buttonOrangeForwardStep3');
        $('#submit').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit').prop('disabled', true);
        $('#password' + method).css('border', 'solid 2px red');
        return false;
    } else {
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasNumbers = /\d/.test(password);
        var hasNonalphas = /\W/.test(password);
        if (hasUpperCase + hasLowerCase + hasNumbers + hasNonalphas < 3) {
            $('#submit').removeClass('buttonOrangeForwardStep3');
            $('#submit').addClass('buttonOrangeForwardStep3OhneAnimation');
            $('#submit').prop('disabled', true);
            $('#password' + method).css('border', 'solid 2px red');
            return false;
        } else {
            $('#password' + method).css('border', 'solid 2px green');
            return true;
        }
    }
}

function PasswordConfirmationCheck() {
    var password = $('#passwordEdit').val();
    var passwordConfirmation = $('#passwordConfirmation').val();
    if (password == "" && passwordConfirmation == "") {
        $('#submit').removeClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit').addClass('buttonOrangeForwardStep3');
        $('#submit').prop('disabled', false);
        $('#passwordEdit').css('border', 'solid 1px #ccc');
        $('#passwordConfirmation').css('border', 'solid 1px #ccc');
        return true;
    } else if (password != passwordConfirmation) {
        $('#submit').removeClass('buttonOrangeForwardStep3');
        $('#submit').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit').prop('disabled', true);
        $('#passwordConfirmation').css('border', 'solid 2px red');
        return false;
    } else {
        $('#passwordConfirmation').css('border', 'solid 2px green');
        return true;
    }
}

function ValidateInput() {
    if (PasswordCheck('Edit') && PasswordCheck('') && PasswordConfirmationCheck()) {
        $('#submit').removeClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit').addClass('buttonOrangeForwardStep3');
        $('#submit').prop('disabled', false);
    }
}