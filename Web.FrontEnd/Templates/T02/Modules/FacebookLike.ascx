<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="VIT.Wedding.Modules.FacebookLike" %>







<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
<div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" data-width="<%= Width%>" data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>


<%--<script language="javascript" type="text/jscript">
    $(document).ready(function () {
        $('iframe').load(function () {
        $('iframe').contents().find("head")
      .append($("<style type='text/css'>  .pluginSkinLight {border:0 !important}; </style>"));
    });
});
</script>--%>