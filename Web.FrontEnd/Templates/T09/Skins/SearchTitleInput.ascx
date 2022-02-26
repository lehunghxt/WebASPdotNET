<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SearchTitleInput.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.SearchTitleInput" %>
<div id="search_mini-2" class="widget-2 widget-last widget widget_search_mini">






    <asp:TextBox runat="server" ID="txtKey" ClientIDMode="Static" CssClass="field"></asp:TextBox>      
    <asp:LinkButton ID="btnSearch" runat="server" Onclick="btnSearch_Click" CssClass="btnsearch">
        <input type="submit" value="Search" id="mini-search-submit">
    </asp:LinkButton>
</div>

<script>
    jQuery(document).ready(function ($) {
        $("#txtKey").attr("placeholder", "<%=this.Language["Search"] %>");
    });
</script>