<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Source == "PRO") { %>
<section class="dv_spec">
        <div class="min_wrap">
            <h2 class="t_h"><%=Title %></h2>
            <ul class="ul_dv_spec clearfix">
                 <%foreach(var item in this.Data) 
  {%>  
                                <li>
                                    <a href="<%=HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                        <figure class="img_dv_spec">
                            <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                        </figure>
                        <h3 class="n_dv_spec"><%=item.Title %></h3>
                    </a>
                </li>
                <%} %>
                            </ul><!-- End .ul_dv_spec -->






        </div><!-- End .min_wrap -->
    </section>
<%} %>

<%if(Source == "ART") { %>
<section class="news_h">
        <div class="min_wrap">
            <h2 class="t_h"><%=Title %></h2>

            <div class="m_news_h">
                <ul>
                    <%foreach(var item in this.Data) 
  {%>  
                	                    <li>
                        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                            <figure class="img_news_h">
                                <img class="img_object_fit" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                            </figure>
                            <div class="info_news_h">
                                <h3><%=item.Title %></h3>
                                <p><%=item.Description %></p>
                                <span>
                                    Xem thêm
                                    <svg class="svg-inline--fa fa-long-arrow-right fa-w-14" aria-hidden="true" focusable="false" data-prefix="fal" data-icon="long-arrow-right" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M311.03 131.515l-7.071 7.07c-4.686 4.686-4.686 12.284 0 16.971L387.887 239H12c-6.627 0-12 5.373-12 12v10c0 6.627 5.373 12 12 12h375.887l-83.928 83.444c-4.686 4.686-4.686 12.284 0 16.971l7.071 7.07c4.686 4.686 12.284 4.686 16.97 0l116.485-116c4.686-4.686 4.686-12.284 0-16.971L328 131.515c-4.686-4.687-12.284-4.687-16.97 0z"></path></svg><!-- <i class="fal fa-long-arrow-right"></i> -->
                                </span>
                            </div>
                        </a>
                    </li>
                <%} %>
            </div>
        </div><!-- End .min_wrap -->
    </section>
<%} %>

<%if(Source == "LIN") { %>
<section class="dt_h">

        <div class="min_wrap">

            <div class="swiper-container swiper3 swiper-container-initialized swiper-container-horizontal">

                <div class="swiper-wrapper" style="transition-duration: 0ms; transform: translate3d(-199px, 0px, 0px);">

                     
                    <%foreach(var item in this.Data) 
                    {%>  
                	    <div class="swiper-slide swiper-slide-prev" style="width: 175px;">
                        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                            <figure>
                                <img class="swiper-lazy img_object_fit swiper-lazy-loaded" src="<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>" alt="<%=item.Title %>"/>
                            </figure>
                        </a>
                    </div>
                <%} %>
					
                </div><!-- End .swiper-wrapper -->

            <span class="swiper-notification" aria-live="assertive" aria-atomic="true"></span>

            </div><!-- End .swiper3 -->

        </div><!-- End .min_wrap -->

    </section>

<link rel="stylesheet" type="text/css" href="/Templates/T07/style/swiper.css" />
    <script src="/Templates/T07/js/swiper.js"></script>
 <%} else if(Source == "MID"){ %>
    <div class="hanhhoatd">
            <div class="title_main">
              <h3><%=Title %></h3>
            </div>
            <script type="text/javascript" src="/Templates/T07/js/jssor.slider.min.js"></script>
             <script>
              jssor_slider1_init = function () {
                  var options = {
                      $AutoPlay: 1,                                    //[Optional] Auto play or not, to enable slideshow, this option must be set to greater than 0. Default value is 0. 0: no auto play, 1: continuously, 2: stop at last slide, 4: stop on click, 8: stop on user navigation (by arrow/bullet/thumbnail/drag/arrow key navigation)
                      $Idle: 4000,                            //[Optional] Interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
                      $SlideDuration: 500,                                //[Optional] Specifies default duration (swipe) for slide in milliseconds, default value is 500
                      $DragOrientation: 3,                                //[Optional] Orientation to drag slide, 0 no drag, 1 horizental, 2 vertical, 3 either, default value is 1 (Note that the $DragOrientation should be the same as $PlayOrientation when $Cols is greater than 1, or parking position is not 0)
                      $UISearchMode: 0,                                   //[Optional] The way (0 parellel, 1 recursive, default value is 1) to search UI components (slides container, loading screen, navigator container, arrow navigator container, thumbnail navigator container etc).
      
                      $ThumbnailNavigatorOptions: {
                          $Class: $JssorThumbnailNavigator$,              //[Required] Class to create thumbnail navigator instance
                          $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
      
                          $Loop: 1,                                       //[Optional] Enable loop(circular) of carousel or not, 0: stop, 1: loop, default value is 1
                          $SpacingX: 3,                                   //[Optional] Horizontal space between each thumbnail in pixel, default value is 0
                          $SpacingY: 3,                                   //[Optional] Vertical space between each thumbnail in pixel, default value is 0
                          
                          $ArrowNavigatorOptions: {
                              $Class: $JssorArrowNavigator$,              //[Requried] Class to create arrow navigator instance
                              $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
                              $Steps: 6                                       //[Optional] Steps to go for each navigation request, default value is 1
                          }
                      }
                  };
      
                  var jssor_slider1 = new $JssorSlider$('slider2_container', options);
      
                  /*#region responsive code begin*/
                  //you can remove responsive code if you don't want the slider scales while window resizing
                  function ScaleSlider() {
                      var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
                      if (parentWidth)
                          jssor_slider1.$ScaleWidth(Math.min(parentWidth, 550));
                      else
                          $Jssor$.$Delay(ScaleSlider, 30);
                  }
      
                  ScaleSlider();
                  $Jssor$.$AddEvent(window, "load", ScaleSlider);
      
                  $Jssor$.$AddEvent(window, "resize", ScaleSlider);
                  $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
                  /*#endregion responsive code end*/
              };
          </script>
        <div id="slider2_container" style="position: relative; width: 550px; height: 315px; overflow: hidden; visibility: hidden;">
    <div data-u="slides" style="cursor:default;position:relative;top:0px;left:0px;width:550px;height:380px;overflow:hidden;">
         <%for (int i = 0; i < this.Data.Count; i++)
                           {%>
        <div>
          <img data-u="image" alt="<%=Data[i].Title%>" src="<%=Data[i].URL != null ? Data[i].URL : Data[i].ImagePath%>" />
          <img data-u="thumb" alt="<%=Data[i].Title%>" src="<%=Data[i].URL != null ? Data[i].URL : Data[i].ImagePath%>" />
      </div>
                      <%} %>
        </div>
        <!-- Thumbnail Navigator -->
      <div data-u="thumbnavigator" class="jssort07" style="position:absolute;left:0px;bottom:0px;width:550px;height:100px;" data-autocenter="1" data-scale-bottom="0.75">
              <div data-u="slides">
                  <div data-u="prototype" class="p" style="width:99px;height:66px;">
                      <div data-u="thumbnailtemplate" class="i"></div>
                      <div class="o" style="z-index: 1;"></div>
                  </div>
              </div>
              <div data-u="arrowleft" class="jssora051" style="top: 30px; left: 8px; width: 40px; height: 40px;" data-scale="0.75">
                <svg viewBox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                  <polyline class="a" points="11040,1920 4960,8000 11040,14080 "></polyline>
              </svg>
            </div>
          
            <div data-u="arrowright" class="jssora051" style="top: 30px; right: 8px; width: 40px; height: 40px;" data-scale="0.75">
              <svg viewBox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                <polyline class="a" points="4960,1920 11040,8000 4960,14080 "></polyline>
              </svg>
            </div>
      </div>
          <!-- Arrow Navigator -->

    </div>
  <!-- Trigger -->
        
  <script>
      jssor_slider1_init();
  </script>
</div>
        <%} %>