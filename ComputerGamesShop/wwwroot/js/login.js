
//Fade in dashboard box
$(document).ready(function(){
    $('.box').hide().fadeIn(1000);
    });

//Stop click event
$('a').click(function(event){
    event.preventDefault(); 
    });


$('#login-btn').click(function (event) {
    $('#login-err').text('')
    var isValid = validateForm()

    if (isValid) {
        $.ajax({
            type: 'POST',
            url: "/Home/login",
            datatype: 'Json',
            data: {
                email: $('#email').val(),
                password: $('#password').val()
            },
            success: function () {
                location.href = '/'
            },
            error: function (err, status, reason) {
                $('#login-err').text('Unauthrized')
            }
        });
    }
});

function validateForm() {
    var isValid = true
    var email = $('#email').val()
    var password = $('#password').val()

    if (!password) {
        $('#password-error').text('empty')
        isValid = false
    } else {
        $('#password-error').text('')
    }

    if (!email) {
        $('#email-error').text('empty')
        isValid = false
    }  else {
        $('#email-error').text('')
    }

    return isValid
}