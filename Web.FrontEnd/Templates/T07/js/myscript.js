$(window).bind("scroll", function() {
            var cach_top = $(window).scrollTop();
            if(cach_top > 42){
                    $('#header').addClass('fix_head fadeInDown animated');
                    $('.openMenu').addClass('hidemenu');
                    $('.btnMenu').addClass('showmenu');
                    $('.btnMenuRight').addClass('hide_scroll');
            }else{
                    $('#header').removeClass('fix_head fadeInDown animated');
                    $('.openMenu').removeClass('hidemenu');
                    $('.btnMenu').removeClass('showmenu');
                    $('.btnMenuRight').removeClass('hide_scroll');
            }
});

$('[data-fancybox]').fancybox();
$('#top').fadeOut();
		$(window).scroll(function() {
			$top = $(window).scrollTop();
			if($top > 100) {
				$('#top').fadeIn();
			} else {
				$('#top').fadeOut();
			}
	   	});
	   	$('#top').click(function() {
			$('html, body').animate({scrollTop:0},500);
			return false;
});
$("#pre-loader").fadeOut("slow");

 $(".scroll_tin").simplyScroll({
        orientation: "vertical",
        customClass: "vert"
    });

$("#sl_doitac").owlCarousel({
        items:9,
        loop:true,
        autoplay:true,
        nav:true,
        responsive:{
        0:{
            items:3,
            nav:false
        },
        600:{
            items:4,
            nav:false
        },
        1000:{
            items:7,
            nav:false,
            loop:false
        },
         1200:{
            items:9,
            nav:false

        }
    }
    });
$(".scroll_dc").owlCarousel({
        items:4,
        loop:true,
        autoplay:true,
        nav:false,
        responsive:{
        0:{
            items:1,
            nav:false
        },
        600:{
            items:2,
            nav:false
        },
        1000:{
            items:3,
            nav:false,
            loop:false
        },
         1200:{
            items:4,
            nav:false

        }
    }
    });
$('.sl_qc').owlCarousel({
    loop:true,
    margin:10,
    lazyLoad:true,
    autoplay:true,
    autoplayTimeout:2000,
    autoplayHoverPause:true,
    responsiveClass:true,
    responsive:{
        0:{
            items:1,
            nav:false
        },
        600:{
            items:1,
            nav:false
        },
        1000:{
            items:1,
            nav:true,
            loop:false
        },
         1200:{
            items:1,
            nav:false

        }
    }
});

$('#thumb').owlCarousel({
   items:4,
        loop:false,
        margin:10,
        autoplay:true,
        nav:false,
           responsive:{
        0:{
            items:3,
            nav:false
        },
        600:{
            items:3,
            nav:false
        },
        1000:{
            items:3,
            nav:true,
            loop:false
        },
         1200:{
            items:4,
            nav:false

        },
        1600:{
            items:4,
            nav:false

        }
    }
});


 $('.sharePop').on('click', function(e){
    
        e.preventDefault();
        $('.sharePop').removeClass('active');
        $(this).parents('.sharePop').addClass('active');
    });
$(document).click(function (e) {
        if (!$(e.target).parents().andSelf().is('.sharePop')) {
            $(".sharePop").removeClass("active");
        }
});
var mainSlider;
            $(document).ready(function() {
                mainSlider = $('.homeNews .owl-carousel');
                mainSlider.owlCarousel({
                    autoHeight: true,
                    autoplay: false,
                    lazyLoad: true,
                    loop: true,
                    items: 1,
                    autoplay:false,
                    smartSpeed: 500,
                });
                /*$('.homeNews .owl-item').on('mouseover',function(){
                    mainSlider.trigger('stop.owl.autoplay');
                });
                $('.homeNews .owl-item').on('mouseout',function(){
                    mainSlider.trigger('play.owl.autoplay',[5000]);
                });*/
                mainSlider.on('loaded.owl.lazy', function(property) {
                    var current = property.item.index;
                    var prev = $(property.target).find(".owl-item").eq(current).prev().find(".itemNews").children('.thumb').attr('data-image');
                    var next = $(property.target).find(".owl-item").eq(current).next().find(".itemNews").children('.thumb').attr('data-image');

                    $('.navPrev').css({
                        'background-image':'url('+prev+')'
                    });
                    $('.navNext').css({
                        'background-image':'url('+next+')'
                    });
                });
                mainSlider.on('changed.owl.carousel', function(property) {
                    var current = property.item.index;
                    var prev = $(property.target).find(".owl-item").eq(current).prev().find(".itemNews").children('.thumb').attr('data-image');
                    var next = $(property.target).find(".owl-item").eq(current).next().find(".itemNews").children('.thumb').attr('data-image');

                    $('.navPrev').css({
                        'background-image':'url('+prev+')'
                    });
                    $('.navNext').css({
                        'background-image':'url('+next+')'
                    });
                });
            });
            $('.navNext').on('click', function() {
                mainSlider.trigger('next.owl.carousel', [300]);
                return false;
            });
            $('.navPrev').on('click', function() {
                mainSlider.trigger('prev.owl.carousel', [300]);
                return false;
            });
 $('.hiden:first').show();
    $('.tab ul li a').click(function(){
      $('.tab ul li a').removeClass("active");
      $(this).addClass("active");
      $('.hiden').hide();
      var show = $(this).attr('rel');
      $(show).fadeIn();
    });