<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleMap.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.GoogleMap" %>

    <!-- Google Script -->
    <asp:HiddenField ID="hf_address" runat="server" />
    <asp:HiddenField ID="hf_default_zoom" runat="server" />
    <asp:HiddenField ID="hf_latlong" runat="server" />
    
    <!-- Layout -->
    <iframe style="Width:100%" src="//www.google.com/maps/embed/v1/search?q=<%=HttpUtility.UrlEncode(hf_address.Value)%>
                  &zoom=<%= default_zoom %>
                  &key=<%= Google_API_key%>">
      </iframe> 


