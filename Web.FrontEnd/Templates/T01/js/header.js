$(document).ready(function () {
    $('#menu-departments-menu li.menu-item-has-children').hover(
        function () {
            $($(this).children()[1]).css({
                'visibility': 'visible',
                'display': 'block',
                'width': '525px',
                'opacity': 1
            });
        },
        function () {
            $($(this).children()[1]).css({
                'visibility': 'hidden',
                'display': 'none',
                'width': '0px',
                'opacity': 0
            });
        }
    );
    $('.btn.navbar-toggler,.tmhm-close').on('click', function () {
        $('.handheld-navigation').toggleClass('toggled');
    });
    $('.btn.dropdown-toggle.btn-block').on('click', function (e) {
        e.preventDefault();
        $('#departments-menu').toggleClass('show');
        $('#menu-departments-menu').toggleClass('show');
        $('#menu-departments-menu').removeAttr();
    });
    $('body').click(function (e) {
        if (!$(e.target).closest('.handheld-navigation').length && $('.handheld-navigation').hasClass('toggled')) {
            $('.handheld-navigation').toggleClass('toggled');
        }
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 35) {
            $('.handheld-header > div.row').addClass('fixed-header');
            //$('nav div').addClass('visible-title');
        } else {
            $('.handheld-header > div.row').removeClass('fixed-header');
            //$('nav div').removeClass('visible-title');
        }
    });
    $('ul#menu-departments-menu a.dropdown-toggle').addClass('disabled');
    $('ul#menu-departments-menu a.dropdown-toggle').off("click.bs.dropdown");


});
