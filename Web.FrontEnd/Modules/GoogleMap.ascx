<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="Web.FrontEnd.Modules.GoogleMap" %>
<div id="gmap" class="contact-map">
            <iframe style="Width:100%" src="//www.google.com/maps/embed/v1/search?q=<%=Address%>
          &zoom=<%= Zoom %>
          &key=<%= GoogleAPIKey%>">
      </iframe> 
</div>  