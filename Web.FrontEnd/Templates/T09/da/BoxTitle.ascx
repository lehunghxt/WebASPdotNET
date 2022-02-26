<%@ Control Language="C#" AutoEventWireup="true" Inherits="VIT.Web.UI.VITSkin" %>

<div class="widget-1 widget woocommerce widget_best_sellers">
	<h3 style="<%=string.IsNullOrEmpty(this.FontColor) ? "" : ";color:" + this.FontColor %>"><%=this.Title %></h3>






    <asp:PlaceHolder ID="PlaceHolderContent" runat="server"></asp:PlaceHolder>
    <div class="clear"></div>
</div>