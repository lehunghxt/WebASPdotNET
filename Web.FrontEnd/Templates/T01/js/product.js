$(document).ready(function () {
    var sliderrange = $('#slider-range');
    var amountprice = $('#amount');
    $(function () {
        sliderrange.slider({
            range: true,
            min: 16,
            max: 400,
            values: [0, 300],
            slide: function (event, ui) {
                amountprice.val("$" + ui.values[0] + " - $" + ui.values[1]);
            }
        });
        amountprice.val("$" + sliderrange.slider("values", 0) +
            " - $" + sliderrange.slider("values", 1));
    });

    $('#related-product .owl-carousel1').owlCarousel({
        autoplay: false,
        autoplayTimeout: 10000,
        autoplayHoverPause: true,
        navSpeed: 10000,
        smartSpeed: 10,
        fluidSpeed: 10000,
        autoplaySpeed: 1000,
        items: 3,
        loop: true,
        center: false,
        rewind: false,

        mouseDrag: true,
        touchDrag: true,
        pullDrag: true,
        freeDrag: false,

        margin: 5,
        stagePadding: 0,

        merge: false,
        mergeFit: true,
        autoWidth: false,

        startPosition: 0,
        rtl: false,

        smartSpeed: 4000,
        fluidSpeed: false,
        dragEndSpeed: false,
        responsive: {
            0: {
                items: 1,
                nav: false,
                dots: false

            },
            375: {
                items: 2,
                nav: false,
                dots: false
            },
            768: {
                items: 3,
                loop: true,
                dots: false
            },
            992: {
                items: 5,
                loop: true,
            },
            1500: {
                items: 7,
                loop: true,

            }
        },
        responsiveRefreshRate: 200,
        responsiveBaseElement: window,

        fallbackEasing: 'swing',

        info: false,

        nestedItemSelector: false,
        itemElement: 'div',
        stageElement: 'div',

        refreshClass: 'owl-refresh',
        loadedClass: 'owl-loaded',
        loadingClass: 'owl-loading',
        rtlClass: 'owl-rtl',
        responsiveClass: 'owl-responsive',
        dragClass: 'owl-drag',
        itemClass: 'owl-item',
        stageClass: 'owl-stage',
        stageOuterClass: 'owl-stage-outer',
        grabClass: 'owl-grab',
        autoHeight: false,
        lazyLoad: false
    })
    /*-----------------------
        Product details slider product-slider-active
    --------------------------- */
    $('.product-dec-slider').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 1,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',
        responsive: [{
                breakpoint: 1199,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            }
        ]
    });
    $('#related-product .product-slider-active1').slick({
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 5,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',

        responsive: [{
                breakpoint: 1199,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 475,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 325,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },

        ]
    });
    var relatedProduct = new Swiper('#related-product .swiper-container', {
        slidesPerView: 5,
        spaceBetween: 5,
        navigation: {
            nextEl: '.arrow.right',
            prevEl: '.arrow.left',
        },
        // init: false,
        breakpoints: {
            1024: {
                slidesPerView: 5,
                spaceBetween: 5,
            },
            768: {
                slidesPerView: 3,
                spaceBetween: 5,
            },
            640: {
                slidesPerView: 3,
                spaceBetween: 5,
            },
            475: {
                slidesPerView: 2,
                spaceBetween: 5,
            },
            320: {
                slidesPerView: 2,
                spaceBetween: 5,
            }
        },
        loop: true
    })

    /*----------------------------
        Product details slider 2
    ------------------------------ */
    $('.product-dec-slider-2').slick({
        infinite: true,
        slidesToShow: 4,
        vertical: true,
        slidesToScroll: 1,
        centerPadding: '60px',
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-up"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-down"></i></span>',
        responsive: [{
                breakpoint: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 1199,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            }
        ]
    });

    /*--------------------------
        Product Zoom
	---------------------------- */
    $(".zoompro").elevateZoom({
        gallery: "gallery",
        galleryActiveClass: "active",
        zoomWindowWidth: 0,
        zoomWindowHeight: 0,
        scrollZoom: false,
        zoomLens: false,
        zoomType: "inner",
        cursor: "crosshair"
    });

    /*--------------------------
        Video popup
	---------------------------- */
    $('.video-popup').magnificPopup({
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        zoom: {
            enabled: true,
        }
    });



    /* Product details slider */
    $('.box-slider-active').owlCarousel({
        loop: true,
        nav: true,
        autoplay: false,
        autoplayTimeout: 5000,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        navText: ['<i class="lnr lnr-chevron-left"></i>', '<i class="lnr lnr-chevron-right"></i>'],
        item: 3,
        margin: 30,
        responsive: {
            0: {
                items: 1
            },
            576: {
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

    /*--
    Image Popup
    ------------------------*/
    //    $('.img-popup').magnificPopup({
    //        type: 'image',
    //        gallery: {
    //            enabled: true
    //        }
    //    });
    /*----------------------------
        	Cart Plus Minus Button
        ------------------------------ */
    var CartPlusMinus = $('.cart-plus-minus');
    CartPlusMinus.prepend('<div class="dec qtybutton">-</div>');
    CartPlusMinus.append('<div class="inc qtybutton">+</div>');
    $(".qtybutton").on("click", function () {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();
        if ($button.text() === "+") {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find("input").val(newVal);
    });
    $('.js-show-more').on('click', function (event) {
        event.preventDefault();
        if (this.classList.contains('explain')) {
            $('a.js-show-more').text('Hiển thị nội dung')
            $('#des-details1').css('max-height', '500px');
            $('.js-content').css('max-height', '400px');

            $('.js-show-more').removeClass('explain');
        } else {
            $('.js-show-more').addClass('explain');
            $('a.js-show-more').text('Ẩn bớt nội dung')
            $('#des-details1').css('max-height', 'none');
            $('.js-content').css('max-height', 'none');
        }


    });

    setTimeout(function () {
        $('img.zoompro').off("touchmove");
        $('img.zoompro').off("touchend");
        $('.zoomContainer').hide();
        //        if ($(document).width() > 767) {
        //            $('.zoomContainer').show();
        //        } else {
        //            $('.zoomContainer').hide();
        //
        //        }
    }, 500);

    //    $(window).resize(function () {
    //        if ($(document).width() > 767) {
    //            $('.zoomContainer').show();
    //        } else {
    //            $('.zoomContainer').hide();
    //        }
    //    });
});
