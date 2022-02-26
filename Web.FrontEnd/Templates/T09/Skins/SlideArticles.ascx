<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimpleNoPage.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.DataSimpleNoPage" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library" %>
<%@ Import Namespace="VIT.Library.Web" %>

<div class="widget span3 testimonial-widget">
    <h3><%=this.Language["LastedNews"] %></h3>






    <div class="testimonial-text"></div>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('.testimonial-widget ul').css('max-height', 'none');


            var animation = $.browser.msie || $.browser.opera ? 'fade' : 'slide';
            $('.testimonial-widget').flexslider({
                animation: animation,
                slideshowSpeed: 8000,
                animationSpeed: 300,
                selectors: '.slides > li',
                directionNav: true,
                slideshow: true,

                pauseOnAction: false,
                controlNav: false,
                touch: true
            });
        });
    </script>

    <div class="flex-viewport" style="overflow: hidden; position: relative;"></div>
    <div class="flex-viewport" style="overflow: hidden; position: relative;">
    <ul class="slides" style="max-height: none; width: 1200%; transition-duration: 0.3s; transform: translate3d(-810px, 0px, 0px);">
        <%foreach(var item in this.Data) 
        {%>
            <li class="clone" style="width: 270px; float: left; display: block;">
                <div>
                    <div>
                        <p><%=item.Description.DeleteHTMLTag()%></p>
                    </div>

                    <div class="name-testimonial">
                        <a class="name-testimonial" href="#"><%=this.Language["Detail"] %></a>
                    </div>
                    <div class="clear"></div>
                </div>
            </li>
            <%} %>
        </ul>
    </div>
    <ul class="flex-direction-nav" style="max-height: none;">
        <li>
            <a class="flex-prev" href="#">Previous</a>
        </li>
        <li>
            <a class="flex-next" href="#">Next</a>
        </li>
    </ul>

</div>