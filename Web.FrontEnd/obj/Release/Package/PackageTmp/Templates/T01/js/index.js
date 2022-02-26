$(document).ready(function () {
    //Carousel
    $('#flash-sale .x-carousel1').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 6,
        arrows: false,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',
        responsive: [{
                breakpoint: 1199,
                settings: {
                    slidesToShow: 6,
                    slidesToScroll: 6
                }
            },
            {
                breakpoint: 995,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5
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
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 375,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 0,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            },



        ]
    });
    var flashSale = new Swiper('#flash-sale .swiper-container', {
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
    $('#product-categories .x-carousel1').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 6,
        arrows: false,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',
        responsive: [{
                breakpoint: 1199,
                settings: {
                    slidesToShow: 6,
                    slidesToScroll: 6
                }
            },
            {
                breakpoint: 995,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5
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
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 375,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 0,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            },



        ]
    });
    var productCategories = new Swiper('#product-categories .swiper-container', {
        slidesPerView: 9,
        spaceBetween: 5,
        navigation: {
            nextEl: '.arrow.right',
            prevEl: '.arrow.left',
        },
        // init: false,
        breakpoints: {
            1368: {
                slidesPerView: 9,
                spaceBetween: 5,
            },
            1024: {
                slidesPerView: 7,
                spaceBetween: 5,
            },
            768: {
                slidesPerView: 5,
                spaceBetween: 5,
            },
            640: {
                slidesPerView: 4,
                spaceBetween: 5,
            },
            475: {
                slidesPerView: 3,
                spaceBetween: 5,
            },
            320: {
                slidesPerView: 3,
                spaceBetween: 5,
            }
        },
        loop: true
    })
    $('#recommended-section .x-carousel1').slick({
        infinite: true,
        slidesToShow: 6,
        slidesToScroll: 6,
        arrows: false,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',
        responsive: [{
                breakpoint: 1199,
                settings: {
                    slidesToShow: 6,
                    slidesToScroll: 6
                }
            },
            {
                breakpoint: 995,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 5
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
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 375,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 0,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            },



        ]
    });
    var recommendedSection = new Swiper('#recommended-section .swiper-container', {
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
    $('#recent-view .x-carousel').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 3,
        arrows: false,
        prevArrow: '<span class="product-dec-icon product-dec-prev"><i class="lnr lnr-chevron-left"></i></span>',
        nextArrow: '<span class="product-dec-icon product-dec-next"><i class="lnr lnr-chevron-right"></i></span>',
        responsive: [{
                breakpoint: 1500,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4
                }
            }, {
                breakpoint: 1199,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 995,
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
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 375,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 0,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },



        ]
    });
    //    var recentView = new Swiper('#recent-view .swiper-container', {
    //        slidesPerView: 3,
    //        spaceBetween: 3,
    //        // init: false,
    //        breakpoints: {
    //            1024: {
    //                slidesPerView: 3,
    //                spaceBetween: 5,
    //            },
    //            768: {
    //                slidesPerView: 3,
    //                spaceBetween: 5,
    //            },
    //            640: {
    //                slidesPerView: 2,
    //                spaceBetween: 5,
    //            },
    //            475: {
    //                slidesPerView: 2,
    //                spaceBetween: 5,
    //            },
    //            320: {
    //                slidesPerView: 2,
    //                spaceBetween: 5,
    //            }
    //        },
    //        loop: true
    //    })
});
