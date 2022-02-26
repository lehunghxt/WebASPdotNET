<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="rev_slider_wrapper">
    <div id="slider-elastic" class="slider slider-elastic" style="max-height: 400px; height: 331.726px;">
        <ul class="ei-slider-large">
            <% int i = 0;%>
            <%foreach(var item in this.Data) 
            { i++;%>
                <li data-transition="random" data-slotamount="7" data-masterspeed="300" data-saveperformance="off">
                    <img src="<%=item.ImagePath%>" class="attachment-full" style="width: 100%;">
                </li>   
            <%} %>              
        </ul>






        <div class="shadow"></div>
    </div>
</div>


    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            var setREVStartSize = function () {
                var tpopt = new Object();
                tpopt.startwidth = 960;
                tpopt.startheight = 350;
                tpopt.container = jQuery('#slider-elastic');
                tpopt.fullScreen = "off";
                tpopt.forceFullWidth = "off";
                tpopt.container.closest(".rev_slider_wrapper").css({ height: tpopt.container.height() });
                tpopt.width = parseInt(tpopt.container.width(), 0);
                tpopt.height = parseInt(tpopt.container.height(), 0);
                tpopt.bw = tpopt.width / tpopt.startwidth;
                tpopt.bh = tpopt.height / tpopt.startheight;
                if (tpopt.bh > tpopt.bw) tpopt.bh = tpopt.bw;
                if (tpopt.bh < tpopt.bw) tpopt.bw = tpopt.bh;
                if (tpopt.bw < tpopt.bh) tpopt.bh = tpopt.bw;
                if (tpopt.bh > 1) { tpopt.bw = 1; tpopt.bh = 1 }
                if (tpopt.bw > 1) { tpopt.bw = 1; tpopt.bh = 1 }
                tpopt.height = Math.round(tpopt.startheight * (tpopt.width / tpopt.startwidth));
                if (tpopt.height > tpopt.startheight && tpopt.autoHeight != "on") tpopt.height = tpopt.startheight;
                if (tpopt.fullScreen == "on") {
                    tpopt.height = tpopt.bw * tpopt.startheight;
                    var cow = tpopt.container.parent().width();
                    var coh = jQuery(window).height();
                    if (tpopt.fullScreenOffsetContainer != undefined) {
                        try {
                            var offcontainers = tpopt.fullScreenOffsetContainer.split(",");
                            jQuery.each(offcontainers, function (e, t) {
                                coh = coh - jQuery(t).outerHeight(true);
                                if (coh < tpopt.minFullScreenHeight) coh = tpopt.minFullScreenHeight
                            })
                        }
                        catch (e) { }
                    }
                    tpopt.container.parent().height(coh);
                    tpopt.container.height(coh);
                    tpopt.container.closest(".rev_slider_wrapper").height(coh);
                    tpopt.container.closest(".forcefullwidth_wrapper_tp_banner").find(".tp-fullwidth-forcer").height(coh);
                    tpopt.container.css({ height: "100%" });
                    tpopt.height = coh;
                } else {
                    tpopt.container.height(tpopt.height);
                    tpopt.container.closest(".rev_slider_wrapper").height(tpopt.height);
                    tpopt.container.closest(".forcefullwidth_wrapper_tp_banner").find(".tp-fullwidth-forcer").height(tpopt.height);
                }
            };

            setREVStartSize();
            var tpj = jQuery;
            tpj.noConflict();
            var revapi1;
            tpj(document).ready(function () {
                if (tpj('#slider-elastic').revolution == undefined)
                    revslider_showDoubleJqueryError('#slider-elastic');
                else
                    revapi1 = tpj('#slider-elastic').show().revolution(
                    {
                        dottedOverlay: "none",
                        delay: 5500,
                        startwidth: 960,
                        startheight: 350,
                        hideThumbs: 200,
                        thumbWidth: 100,
                        thumbHeight: 50,
                        thumbAmount: 4,
                        simplifyAll: "off",
                        navigationType: "bullet",
                        navigationArrows: "solo",
                        navigationStyle: "round",
                        touchenabled: "on",
                        onHoverStop: "on",
                        nextSlideOnWindowFocus: "off",
                        swipe_threshold: 75,
                        swipe_min_touches: 1,
                        drag_block_vertical: false,
                        keyboardNavigation: "off",
                        navigationHAlign: "center",
                        navigationVAlign: "bottom",
                        navigationHOffset: 0,
                        navigationVOffset: 20,
                        soloArrowLeftHalign: "left",
                        soloArrowLeftValign: "center",
                        soloArrowLeftHOffset: 20,
                        soloArrowLeftVOffset: 0,
                        soloArrowRightHalign: "right",
                        soloArrowRightValign: "center",
                        soloArrowRightHOffset: 20,
                        soloArrowRightVOffset: 0,
                        shadow: 0,
                        fullWidth: "on",
                        fullScreen: "off",
                        spinner: "spinner0",
                        stopLoop: "off",
                        stopAfterLoops: -1,
                        stopAtSlide: -1,
                        shuffle: "off",
                        autoHeight: "off",
                        forceFullWidth: "off",
                        hideThumbsOnMobile: "off",
                        hideNavDelayOnMobile: 1500,
                        hideBulletsOnMobile: "off",
                        hideArrowsOnMobile: "off",
                        hideThumbsUnderResolution: 0,
                        hideSliderAtLimit: 0,
                        hideCaptionAtLimit: 0,
                        hideAllCaptionAtLilmit: 0,
                        startWithSlide: 0
                    });
            });
        });
    </script>
