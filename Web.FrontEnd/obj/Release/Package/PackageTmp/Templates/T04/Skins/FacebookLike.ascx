﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookLike" %>
<style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>
<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BorderColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BorderColor") %>">
    <b style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></b>






</div>
 <div class="cate_left_list">

<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
<div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" <%= Width == 0 ? "" : "data-width='"+Width+"'"%> data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>

    </div>
<%--<script language="javascript" type="text/jscript">
    $(document).ready(function () {
        $('iframe').load(function () {
        $('iframe').contents().find("head")
      .append($("<style type='text/css'>  .pluginSkinLight {border:0 !important}; </style>"));
    });
});
</script>--%>