$(document).ready(function () {
    $('#cssmenu ul li').click(function() {
        $(this).addClass('subactive').siblings('li').removeClass('subactive');
        $('#' + $(this).data('rel')).stop().fadeIn(400, 'linear').siblings().stop().fadeOut(400, 'linear'); 
      });
      $('#coupon ul li').click(function() {
        $(this).addClass('active').siblings('li').removeClass('active');
        
      });
    $('#checkout-mgd ul li').click(function() {
        $(this).addClass('active').siblings('li').removeClass('active');
        $('#' + $(this).data('rel')).stop().fadeIn(0, 'linear').siblings('div.state').stop().fadeOut(0 , 'linear'); 
      });
});