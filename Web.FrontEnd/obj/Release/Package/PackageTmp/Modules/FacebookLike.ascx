<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookLike.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookLike" %>

<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
<div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" data-width="<%= Width%>" data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>