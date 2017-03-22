function ValidateEmail(method) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test($('#email' + method).val())) {
        $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeForwardStep3');
        $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
        $('#email' + method).css('border', 'solid 2px red');
        return false;
    } else {
        $('#email' + method).css('border', 'solid 2px green');
        return true;
    }
}

function EmailConfirmation() {
    var email = $('#emailRegister').val();
    var emailConfirmation = $('#emailConfirmation').val();
    if (email != emailConfirmation || email == "") {
        $('#submitRegister,#submitRegisterMobile').removeClass('buttonOrangeForwardStep3');
        $('#submitRegister,#submitRegisterMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submitRegister,#submitRegisterMobile').prop('disabled', true);
        $('#emailConfirmation').css('border', 'solid 2px red')
        return false;
    } else {
        $('#emailConfirmation').css('border', 'solid 2px green')
        return true;
    }
}

function PasswordCheck(method) {
    var password = $('#password' + method).val();
    if (password.length < 8) {
        $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeForwardStep3');
        $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
        $('#password' + method).css('border', 'solid 2px red')
        return false;
    } else {
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasNumbers = /\d/.test(password);
        var hasNonalphas = /\W/.test(password);
        if (hasUpperCase + hasLowerCase + hasNumbers + hasNonalphas < 3) {
            $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeForwardStep3');
            $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeForwardStep3OhneAnimation');
            $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', true);
            $('#password' + method).css('border', 'solid 2px red')
            return false;
        } else {
            $('#password' + method).css('border', 'solid 2px green');
            return true;
        }
    }
}

function PasswordConfirmationCheck() {
    var password = $('#passwordRegister').val();
    var passwordConfirmation = $('#passwordConfirmation').val();
    if (password != passwordConfirmation || password == "") {
        $('#submitRegister,#submitRegisterMobile').removeClass('buttonOrangeForwardStep3');
        $('#submitRegister,#submitRegisterMobile').addClass('buttonOrangeForwardStep3OhneAnimation');
        $('#submitRegister,#submitRegisterMobile').prop('disabled', true);
        $('#passwordConfirmation').css('border', 'solid 2px red')
        return false;
    } else {
        $('#passwordConfirmation').css('border', 'solid 2px green')
        return true;
    }
}

function ValidateInput(method) {
    if (method == 'Register') {
        if (ValidateEmail(method)&& EmailConfirmation() && PasswordCheck(method) && PasswordConfirmationCheck()) {
            $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeForwardStep3OhneAnimation');
            $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeForwardStep3');
            $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', false);
        }
    } else if (method == 'Login') {
        if (ValidateEmail(method) && PasswordCheck(method)) {
            $('#submit' + method + ',#submit' + method + 'Mobile').removeClass('buttonOrangeForwardStep3OhneAnimation');
            $('#submit' + method + ',#submit' + method + 'Mobile').addClass('buttonOrangeForwardStep3');
            $('#submit' + method + ',#submit' + method + 'Mobile').prop('disabled', false);
        }
    }
}

function Bottom() {
    window.setTimeout(function () {
        if ($('#registrieren').hasClass('collapse in')) {
            $('html, body').animate({
                scrollTop: $("#registrieren").offset().top
            }, 500);
            //document.getElementById('registrieren').scrollIntoView();
        }
    }, 500);
};

$(document).ready(function () {
    if (document.getElementById('errorMessage')) {
        $('#registrieren').removeClass('collapse');
        $('#registrieren').addClass('collapse in');
        document.getElementById('errorMessage').scrollIntoView();
    }
});