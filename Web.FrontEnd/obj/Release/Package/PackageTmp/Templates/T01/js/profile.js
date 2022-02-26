$(document).ready(function () {

    if ($(document).width() > 1023) {
        document.getElementById('menu').checked = false;
        $('#cssmenu').css('max-height', '100%');
    } else {
        document.getElementById('menu').checked = true;
        $('#cssmenu').css('max-height', '0');
    }
    $('#cssmenu ul li').click(function () {
        $(this).addClass('subactive').siblings('li').removeClass('subactive');
        $('#' + $(this).data('rel')).stop().fadeIn(400, 'linear').siblings().stop().fadeOut(400, 'linear');
        if ($(document).width() < 1024) {
            document.getElementById('menu').checked = true;
            $('#cssmenu').css('max-height', 0);
        }
    });
    $('.menu-check input[type="checkbox"]').click(function () {
        if ($(this).prop("checked") == true) {
            $('#cssmenu').css('max-height', '0');
        } else if ($(this).prop("checked") == false) {
            $('#cssmenu').css('max-height', '100%');
        }
    });
    $('#coupon ul li').click(function () {
        $(this).addClass('active').siblings('li').removeClass('active');

    });
    $('#checkout-mgd ul li').click(function () {
        $(this).addClass('active').siblings('li').removeClass('active');
        $('#' + $(this).data('rel')).stop().fadeIn(0, 'linear').siblings('div.state').stop().fadeOut(0, 'linear');
    });
    let url = window.location.href;
    if (url.includes('#')) {
        let submenu = url.split('#')[1];
        $('[data-rel="' + submenu + '"]').addClass('subactive').siblings('li').removeClass('subactive');
        $('#' + submenu).stop().fadeIn(400, 'linear').siblings().stop().fadeOut(400, 'linear');
    }
    $(window).resize(function () {
        if ($(document).width() > 1023) {
            document.getElementById('menu').checked = false;
            $('#cssmenu').css('max-height', '100%');
        } else {
            document.getElementById('menu').checked = true;
            $('#cssmenu').css('max-height', '0');
        }
    });
});
