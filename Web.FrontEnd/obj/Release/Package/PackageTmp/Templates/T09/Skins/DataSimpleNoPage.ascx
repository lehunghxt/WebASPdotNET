<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimpleNoPage.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.DataSimpleNoPage" %>
<%@ Import Namespace="VIT.Library" %>
<%@ Import Namespace="VIT.Library.Web" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>

<%--<link rel="stylesheet" href="/Templates/T0150/Plugins/LightBoxV2/css/lightbox.css">






<script src="/Templates/T0150/Plugins/LightBoxV2/js/lightbox-plus-jquery.min.js"></script>--%>

<!-- Add jQuery library -->
	<%--<script type="text/javascript" src="/includes/fancybox/jquery-1.10.1.min.js"></script>--%>

	<!-- Add mousewheel plugin (this is optional) -->
	<script type="text/javascript" src="/includes/fancybox/jquery.mousewheel-3.0.6.pack.js"></script>

	<!-- Add fancyBox main JS and CSS files -->
	<script type="text/javascript" src="/includes/fancybox/jquery.fancybox.js?v=2.1.5"></script>
	<link rel="stylesheet" type="text/css" href="/includes/fancybox/jquery.fancybox.css?v=2.1.5" media="screen" />

	<!-- Add Button helper (this is optional) -->
	<link rel="stylesheet" type="text/css" href="/includes/fancybox/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
	<script type="text/javascript" src="/includes/fancybox/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>

	<!-- Add Thumbnail helper (this is optional) -->
	<link rel="stylesheet" type="text/css" href="/includes/fancybox/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" />
	<script type="text/javascript" src="/includes/fancybox/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>

	<!-- Add Media helper (this is optional) -->
	<script type="text/javascript" src="/includes/fancybox/helpers/jquery.fancybox-media.js?v=1.0.6"></script>

<div class="row <%=RederectComponent + "_" + RederectView%>">
    <%foreach(var item in this.Data) 
    {%>
        <%if (this.GetValueParam<string>("Source") == "Picture")
        {%>
            <div class='grid span2' style='<%=Width > 0 ? "Width:" + Width + "px;": ""%>'>
                <a class="simpleimg fancybox-buttons" data-fancybox-group="button" href="<%=item.ImagePath%>" data-lightbox="example-set" title="<%=item.Title %>">
                    <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='<%=!string.IsNullOrEmpty(item.ImagePath) ? "" : "display:none;"%><%=Width > 0 ? "Width:" + Width + "px;": "max-Width:100%;"%><%=Height > 0 ? "Height:" + Height + "px;": "max-Height:100%;"%>'/>
                </a>
            </div>
        <%} else if (this.GetValueParam<string>("Source") == "Video")
        {%>
            <div class='item item_0' style='<%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'>
                <%if (item.URL.StartsWith("<"))
                {%>
                    <%=item.URL%>
                <%}else{ %>
                    <video <%=Width > 0 ? "Width='" + Width + "'": ""%> <%=Height > 0 ? "Height='" + Height + "'": ""%> controls>
                      <source src="<%=item.URL%>" type="video/mp4">
                    </video>
                <%} %>
                <h1 class="hsimpletitle" title="<%=item.Description.DeleteHTMLTag().Trim() %>">
                    <a class="simpletitle" href="<%=!string.IsNullOrEmpty(item.URL) ? item.URL : HREF.LinkComponent(RederectComponent, RederectView, RederectSendKey + "/"+item.Id+"/"+item.Title.ConvertToUnSign())%>" title="<%=item.Description.DeleteHTMLTag().Trim() %>"><%=item.Title %></a>
                </h1> 
            </div>
        <%} else if (this.GetValueParam<string>("Source") == "Audio")
        {%>
            <div class='item item_0' style='<%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'>
                <%if (item.URL.StartsWith("<"))
                {%>
                    <%=item.URL%>
                <%}else{ %>
                    <audio controls>
                      <source src="<%=item.URL%>" type="audio/mpeg">
                    </audio>
                <%} %>
                <h1 class="hsimpletitle" title="<%=item.Description.DeleteHTMLTag().Trim() %>">
                    <a class="simpletitle" href="<%=!string.IsNullOrEmpty(item.URL) ? item.URL : HREF.LinkComponent(RederectComponent, RederectView, RederectSendKey + "/"+item.Id+"/"+item.Title.ConvertToUnSign())%>" title="<%=item.Description.DeleteHTMLTag().Trim() %>"><%=item.Title %></a>
                </h1> 
            </div>
        <%} %>
    <%} %>
	<div class="clear"></div>
</div>


                

<script type="text/javascript">
    jQuery(document).ready(function () {
        
        /*
         *  Button helper. Disable animations, hide close button, change title type and content
         */

        jQuery('.fancybox-buttons').fancybox({
            openEffect: 'none',
            closeEffect: 'none',

            prevEffect: 'none',
            nextEffect: 'none',

            closeBtn: false,

            helpers: {
                title: {
                    type: 'inside'
                },
                buttons: {}
            },

            afterLoad: function () {
                this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
            }
        });




    });
	</script>