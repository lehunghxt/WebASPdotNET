jQuery(document).ready(function(a){a("#map-handler a").click(function(){a("#map iframe").slideToggle(400,function(){a("#map iframe").is(":visible")?a("#map-handler a").text(l10n_handler.map_close):a("#map-handler a").text(l10n_handler.map_open)})});a("a.socials-square, a.socials-square-small").each(function(){var b=a(this).text().charAt(0).toUpperCase()+a(this).text().slice(1),c=0!=a("#wpadminbar").length?28:0;a(this).tipTip({defaultPosition:"top",maxWidth:"auto",edgeOffset:c,content:b})});jQuery.isFunction(jQuery.fn.BlackAndWhite)&& (a(".bwWrapper").BlackAndWhite({hoverEffect:!0,webworkerPath:!1,responsive:!0,speed:{fadeIn:200,fadeOut:300}}),a("a.bwWrapper[href='#']").click(function(){return!1}),a(".service-wrapper").each(function(){a(this).mouseenter(function(){a(this).find("canvas").stop(!0,!0).fadeOut(300)});a(this).mouseleave(function(){a(this).find("canvas").stop(!0,!0).fadeIn(200)})}));a(".teaser .image").hover(function(){a(this).children("img").animate({opacity:0.3},300)},function(){a(this).children("img").animate({opacity:1}, 300)})});