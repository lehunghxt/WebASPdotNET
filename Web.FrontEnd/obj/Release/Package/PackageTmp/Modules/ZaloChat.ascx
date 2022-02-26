<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZaloChat.ascx.cs" Inherits="Web.FrontEnd.Modules.ZaloChat" %>

<script src="https://sp.zalo.me/plugins/sdk.js"></script>

<div class="zalo-chat-widget" data-oaid="<%=OfficialAccountId %>" data-welcome-message="<%=Title %>" data-autopopup="<%= AutoPopup ? 1 : 0 %> " data-width="<%= Width%>" data-height="<%= Height%>"></div>
