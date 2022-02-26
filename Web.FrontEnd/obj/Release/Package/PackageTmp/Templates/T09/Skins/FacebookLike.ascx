<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="VIT.Wedding.Modules.FacebookLike" %>


<div class="widget-4 widget-last widget span3">
    <h3><%=this.Language["Facebook"] %></h3>






    <div class="clear"></div>
    <div class="soc-icons">
        <div id="fb-root"></div>
        <script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
        <div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" data-width="<%= Width%>" data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>
    </div>
</div>



<script language="javascript" type="text/jscript">
    jQuery(document).ready(function ($) {
        $('iframe').load(function () {
        $('iframe').contents().find("head")
          .append($("<style type='text/css'>  .pluginSkinLight {border:0 !important}  </style>"));
        });
    });
</script>