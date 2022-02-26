$(document).ready(function () {
    $('.method').on('click', function () {
        $('.method').removeClass('blue-border');
        $(this).addClass('blue-border');
    });
    var $cardInput = $('.input-fields input');

    $('.next-btn').on('click', function (e) {

        $cardInput.removeClass('warning');

        $cardInput.each(function () {
            var $this = $(this);
            if (!$this.val()) {
                $this.addClass('warning');
            }
        });
    });
    if ($('ul.nav-register li')) {
        $('ul.nav-register li').removeClass('active');
        $($('ul.nav-register li')[0]).addClass('active');
        $('#login-form').show();
        $('#register-form').hide();
    }
    $('ul.nav-register li').on('click', function () {
        $('ul.nav-register li').removeClass('active');
        $(this).addClass('active');
        let index = $(this).index();
        if (index == 0) {
            $('#login-form').show();
            $('#register-form').hide();
        } else {
            $('#login-form').hide();
            $('#register-form').show();
        }
        console.log($(this).index());
    });
    $('#step-1').show();
    $('#step-2').hide();
    $('#step-3').hide();
    $('#step-4').hide();
    $('#login_popup_submit').on('click', function () {
        $('#step-1').hide();
        $('#step-2').show();
    });

    $('#btn-address').on('click', function () {
        $('#step-2').hide();
        $('#step-3').show();
    });
    let screenWidth = $('.row-style-1').width();
    $('#checkout-step ul li').css('width', (screenWidth / 30 - 2) * 10);
    $(window).resize(function () {
        let screenWidth = $(window).width();
        $('#checkout-step ul li').css('width', (screenWidth / 30 - 2) * 10);
    });
    $('#btn-placeorder').on('click', function (e) {
        e.preventDefault();
        window.location.href = "confirm.html";
    })
});
