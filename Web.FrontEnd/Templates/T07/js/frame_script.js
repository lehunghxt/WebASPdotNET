$(document).ready(function(){
	
	if($(".mn_child_02").length){
		$(".mn_child_01").mouseover(function() {
			$(".mn_child_01 .mn_child_02").attr("style","left:"+$(this).width()+"px");
		});	
	}
	
	/***********************************************************************************/
	
	if($(".click_s").length){
		$(".click_s").click(function() {
			$(".form_s").addClass("active");
			$('body').addClass('fixbody');
		});	
		$(".close_form_s").click(function() {
			$(".form_s").removeClass("active");
			$('body').removeClass('fixbody');
		});	
	}
	
	/***********************************************************************************/
	
	if($(".swiper1").length){
		var swiper1 = new Swiper('.swiper1', {
			pagination: {
				el: '.swiper-pagination-1',
				type: 'bullets',
				clickable: true,
			},
			watchOverflow: true,
			preloadImages: false,
			lazy: {
				loadPrevNext: true,					
			},
			autoplay: {
				delay: 5000,
			},
			effect: 'fade',
		});
	}
	
	if($(".swiper3").length){
		var swiper3 = new Swiper('.swiper3', {
			watchOverflow: true,
			preloadImages: false,
			lazy: {
				loadPrevNext: true,					
			},
			autoplay: {
				delay: 2000,
			},
			speed: 500,
			slidesPerView: 6,
			spaceBetween: 24,
			breakpoints: {
				1024: {
					lazy: {
						loadPrevNextAmount: 6,	
					},
					slidesPerView: "auto",
					spaceBetween: 10,
				}
			}
		});
	}
	
	/***********************************************************************************/	
	
	if($(".back_to_top").length){
		$(".back_to_top").click(function () {
			$('body,html').animate({
				scrollTop: 0
			});
		});
	}
	
	/***********************************************************************************/
	
	$(".icon_menu_mobile").click(function(e) {
		$val=$(".icon_menu_mobile").attr("val");
		if($val==0){
			$(".menu_mobile").attr("style","visibility: visible;");
			$(this).attr("val",1);
			$('body').addClass("ad_body");
		}
	});
	$(".close_menu_mobile").click(function() {
		$(".menu_mobile").removeAttr("style");
		$(".icon_menu_mobile").attr("val",0);
		$('body').removeClass("ad_body");
	});
	
});