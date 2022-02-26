$(document).ready(function () {
    $('#related-product .owl-carousel').owlCarousel({
        autoplay: true,
        autoplayTimeout: 10000,
        autoplayHoverPause: true,
        navSpeed: 10,
        smartSpeed: 10,
        fluidSpeed: 10,
        autoplaySpeed: 10,
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

        smartSpeed: 40,
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
    });
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
                newVal = 0;
            }
        }
        $button.parent().find("input").val(newVal);
    });
    $('.coupon a').on('click', function (e) {
        e.preventDefault();
        $('.item-content.coupon-code').css('max-height', 500);
    })
});
