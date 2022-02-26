(function ($) {
    "use Strict";
    /*--------------------------
    1. Newsletter Popup
    ---------------------------*/
    setTimeout(function () {
        $('.popup_wrapper').css({
            "opacity": "1",
            "visibility": "visible"
        });
        $('.popup_off').on('click', function () {
            $(".popup_wrapper").fadeOut(500);
        })
    }, 700000);

    /*----------------------------
    2. Mobile Menu Activation
    -----------------------------*/
    jQuery('.mobile-menu nav').meanmenu({
        meanScreenWidth: "991",
    });

    /*----------------------------
    3. Tooltip Activation
    ------------------------------ */
    $('.pro-actions a,.quick_view').tooltip({
        animated: 'fade',
        placement: 'top',
        container: 'body'
    });

    /*----------------------------
    4.1 Vertical-Menu Activation
    -----------------------------*/
    $('.categorie-title,.mobile-categorei-menu').on('click', function () {
        $('.vertical-menu-list,.mobile-categorei-menu-list').slideToggle();
    });

    /*------------------------------
     4.2 Category menu Activation
    ------------------------------*/
    $('#cate-toggle li.has-sub>a,#cate-mobile-toggle li.has-sub>a,#shop-cate-toggle li.has-sub>a').on('click', function () {
        $(this).removeAttr('href');
        var element = $(this).parent('li');
        if (element.hasClass('open')) {
            element.removeClass('open');
            element.find('li').removeClass('open');
            element.find('ul').slideUp();
        } else {
            element.addClass('open');
            element.children('ul').slideDown();
            element.siblings('li').children('ul').slideUp();
            element.siblings('li').removeClass('open');
            element.siblings('li').find('li').removeClass('open');
            element.siblings('li').find('ul').slideUp();
        }
    });
    $('#cate-toggle>ul>li.has-sub>a').append('<span class="holder"></span>');

    /*----------------------------
    4.3 Checkout Page Activation
    -----------------------------*/
    $('#showlogin').on('click', function () {
        $('#checkout-login').slideToggle();
    });
    $('#showcoupon').on('click', function () {
        $('#checkout_coupon').slideToggle();
    });
    $('#cbox').on('click', function () {
        $('#cbox_info').slideToggle();
    });
    $('#ship-box').on('click', function () {
        $('#ship-box-info').slideToggle();
    });

    /*----------------------------
    5. NivoSlider Activation
    -----------------------------*/
    $('#slider').nivoSlider({
        effect: 'random',
        animSpeed: 300,
        pauseTime: 5000,
        directionNav: true,
        manualAdvance: true,
        controlNavThumbs: false,
        pauseOnHover: true,
        controlNav: false,
        prevText: "<i class='lnr lnr-arrow-left'></i>",
        nextText: "<i class='lnr lnr-arrow-right'></i>"
    });

    /*----------------------------------------------------
    6. Hot Deal Product Activation
    -----------------------------------------------------*/
    $('.hot-deal-active').owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 1,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            480: {
                items: 2
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            },
            1200: {
                items: 5
            }
        }
    })
    $('.flash-sale-active').owlCarousel({
        loop: true,
        nav: false  ,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 1,
        responsive: {
            0: {
                items: 3,
                autoplay: false,
                smartSpeed: 500
            },
            480: {
                items: 3,
                autoplay: false,
                smartSpeed: 500
            },
            768: {
                items: 4,
                autoplay: true,
                smartSpeed: 500
            },
            992: {
                items: 5,
                autoplay: true,
                smartSpeed: 500
            },
            1200: {
                items: 5,
                autoplay: true,
                smartSpeed: 500
            }
        }
    })
    $('.catalog-active ').owlCarousel({
        loop: true,
        nav: false  ,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 1,
        responsive: {
            0: {
                items: 4,
                autoplay: true,
                smartSpeed: 500
            },
            480: {
                items: 4,
                autoplay: true,
                smartSpeed: 500
            },
            768: {
                items: 6,
                autoplay: true,
                smartSpeed: 500
            },
            992: {
                items: 8,
                autoplay: true,
                smartSpeed: 500
            },
            1200: {
                items: 10,
                autoplay: true,
                smartSpeed: 500
            }
        }
    })
    $('.support-active ').owlCarousel({
        loop: true,
        nav: false  ,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 1,
        responsive: {
            0: {
                items: 2,
                autoplay: true,
                smartSpeed: 500
            },
            480: {
                items: 2,
                autoplay: true,
                smartSpeed: 500
            },
            768: {
                items: 4,
            },
            992: {
                items: 4,
            },
            1200: {
                items: 4,
            }
        }
    })
    $('.popular-active ').owlCarousel({
        loop: true,
        nav: false  ,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 1,
        responsive: {
            0: {
                items: 3,
                autoplay: true,
                smartSpeed: 500
            },
            480: {
                items: 3,
                autoplay: true,
                smartSpeed: 500
            },
            768: {
                items: 4,
            },
            992: {
                items: 4,
            },
            1200: {
                items: 4,
            }
        }
    })
    $('.hot-deal-active3').owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 10,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            480: {
                items: 1
            },
            768: {
                items: 1
            },
            992: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    })

    /*----------------------------------------------------
    7. Brand Banner Activation
    -----------------------------------------------------*/
    $('.brand-banner').owlCarousel({
        loop: true,
        nav: true,
        autoplay: true,
        dots: false,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        smartSpeed: 1200,
        margin: 0,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            380: {
                items: 2
            },
            768: {
                items: 3
            },
            1000: {
                items: 3
            }
        }
    })
    $('.brand-banner-sidebar').owlCarousel({
        loop: true,
        nav: false,
        autoplay: true,
        dots: false,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        smartSpeed: 1200,
        margin: 0,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            380: {
                items: 2
            },
            768: {
                items: 2
            },
            1000: {
                items: 2
            }
        }
    })

    /*----------------------------------------------------
    8. Electronics Product Activation
    -----------------------------------------------------*/
    $('.electronics-pro-active')
        .on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target)
                .find('.owl-item').removeClass('last')
                .eq(event.item.index + event.page.size - 1).addClass('last');
        }).owlCarousel({
            loop: false,
            nav: true,
            dots: false,
            smartSpeed: 1000,
            navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
            margin: 10,
            responsive: {
                0: {
                    items: 1,
                    autoplay: true,
                    smartSpeed: 500
                },
                768: {
                    items: 3
                },
                992: {
                    items: 4
                },
                1200: {
                    items: 4
                }
            }
        })

    $('.electronics-pro-active2')
        .on('changed.owl.carousel initialized.owl.carousel', function (event) {
            $(event.target)
                .find('.owl-item').removeClass('last')
                .eq(event.item.index + event.page.size - 1).addClass('last');
        }).owlCarousel({
            loop: false,
            nav: true,
            dots: false,
            smartSpeed: 1000,
            navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
            margin: 10,
            responsive: {
                0: {
                    items: 1,
                    autoplay: true,
                    smartSpeed: 500
                },
                768: {
                    items: 3
                },
                992: {
                    items: 4
                },
                1200: {
                    items: 4
                }
            }
        })

    /*----------------------------------------------------
    9. Best Seller Product Activation
    -----------------------------------------------------*/
    $('.best-seller-pro-active').owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 10,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            450: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    })
    $('.trending-pro-active').owlCarousel({
        loop: false,
        nav: false,
        dots: true,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 10,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            450: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    })

    /*----------------------------------------------------
    10. Like Product Activation
    -----------------------------------------------------*/
    $('.like-pro-active').owlCarousel({
        loop: false,
        nav: false,
        dots: true,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 10,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            450: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    })

    /*----------------------------------------------------
    11. Second Hot Deal Product Activation
    -----------------------------------------------------*/
    $('.second-hot-deal-active').on('changed.owl.carousel initialized.owl.carousel', function (event) {
        $(event.target)
            .find('.owl-item').removeClass('last')
            .eq(event.item.index + event.page.size - 1).addClass('last');
    }).owlCarousel({
        loop: false,
        nav: true,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 0,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            768: {
                items: 1
            },
            992: {
                items: 2
            },
            1200: {
                items: 2
            }
        }
    })

    /*----------------------------------------------------
    12. Side Product Activation
    -----------------------------------------------------*/
    $('.side-product-active').owlCarousel({
        loop: false,
        nav: false,
        dots: false,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 0,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            450: {
                items: 1
            },
            768: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    })

    /*----------------------------------------------------
    12. New Product Tow For Home-2 Activation
    -----------------------------------------------------*/
    $('.latest-blog-active').owlCarousel({
        loop: false,
        nav: false,
        dots: true,
        smartSpeed: 1500,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 20,
        responsive: {
            0: {
                items: 1,
                autoplay: true,
                smartSpeed: 500
            },
            450: {
                items: 1
            },
            768: {
                items: 1
            },
            992: {
                items: 2
            },
            1200: {
                items: 2
            }
        }
    })

    /*-------------------------------------
    13. Thumbnail Product activation
    --------------------------------------*/
    $('.thumb-menu').owlCarousel({
        loop: false,
        navText: ["<i class='lnr lnr-arrow-left'></i>", "<i class='lnr lnr-arrow-right'></i>"],
        margin: 15,
        smartSpeed: 1000,
        nav: true,
        dots: false,
        responsive: {
            0: {
                items: 3,
                autoplay: true,
                smartSpeed: 500
            },
            768: {
                items: 3
            },
            1000: {
                items: 3
            }
        }
    })
    $('.thumb-menu a').on('click', function () {
        $('.thumb-menu a').removeClass('active');
    })

    /*----------------------------
    14. Countdown Js Activation
    -----------------------------*/
    $('[data-countdown]').each(function () {
        var $this = $(this),
            finalDate = $(this).data('countdown');
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime('<span">%D Ngày %H:%M:%S</span>'));
        });
    });
    $('[data-sale-countdown]').each(function () {
        var $this = $(this),
            finalDate = $(this).data('sale-countdown');
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime('<span class="red-block">%H</span>:<span class="red-block">%M</span>:<span class="red-block">%S</span>'));
        });
    });

    /*----------------------------
    15. ScrollUp Activation
    -----------------------------*/
    $.scrollUp({
        scrollName: 'scrollUp', // Element ID
        topDistance: '550', // Distance from top before showing element (px)
        topSpeed: 1000, // Speed back to top (ms)
        animation: 'fade', // Fade, slide, none
        scrollSpeed: 900,
        animationInSpeed: 1000, // Animation in speed (ms)
        animationOutSpeed: 1000, // Animation out speed (ms)
        scrollText: '<i class="fa fa-angle-double-up" aria-hidden="true"></i>', // Text for element
        activeOverlay: false // Set CSS color to display scrollUp active point, e.g '#00FFFF'
    });

    /*----------------------------
    16. Sticky-Menu Activation
    ------------------------------ */
    // $(window).scroll(function () {
    //     if ($(this).scrollTop() > 300) {
    //         $('.header-sticky').addClass("sticky");
    //     } else {
    //         $('.header-sticky').removeClass("sticky");
    //     }
    // });

    /*----------------------------
    17. Nice Select Activation
    ------------------------------ */
    $('select.nice-select').niceSelect();

    /*----------------------------
    18. Price Slider Activation
    -----------------------------*/
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 100,
        values: [0, 85],
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
        " - $" + $("#slider-range").slider("values", 1));



    /*--------------------------
         banner colse Popup
    ---------------------------*/
    $('.popup_off_banner').on('click', function () {
        $(".popup_banner").fadeOut(500);
    })
})(jQuery);

$(document).ready(function () {
    $('.btn.navbar-toggler,.tmhm-close').on('click', function() {
        $('.handheld-navigation').toggleClass('toggled');
    });
    $('.js-show-more').on('click', function(event) {
        event.preventDefault();
        if (this.classList.contains('explain')) {
            $('a.js-show-more').text('Hiển thị nội dung')
            $('#gioi-thieu').css('max-height', '500px');
            $('.js-show-more').removeClass('explain');
        } else {
            $('.js-show-more').addClass('explain');
            $('a.js-show-more').text('Ẩn bớt nội dung')
            $('#gioi-thieu').css('max-height', 'none');
        }
        

    });
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
        })
    });
    if ($('ul.nav-register li')) {
        $('ul.nav-register li').removeClass('active');
        $($('ul.nav-register li')[0]).addClass('active');
        $('#login-form').show();
            $('#register-form').hide();
    }
    $('ul.nav-register li').on('click', function() {
        $('ul.nav-register li').removeClass('active')
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
    $('#login_popup_submit').on('click', function() {
        $('#step-1').hide();
        $('#step-2').show();
    });
    
    $('#btn-address').on('click', function() {
        $('#step-2').hide();
        $('#step-3').show();
    });
})