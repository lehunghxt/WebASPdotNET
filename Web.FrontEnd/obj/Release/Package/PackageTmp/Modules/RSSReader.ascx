<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RSSReader.ascx.cs" Inherits="Web.FrontEnd.Modules.RSSReader" %>

<%if(Feed != null && Feed.Items != null && Feed.Items.Count() > 0) {%>
<%foreach (var item in Feed.Items) {%>
                <%=item.Title.Text %>
                <%=item.Summary.Text %>
                <%=item.Links[0].Uri.AbsoluteUri %>
<% }%>
<% }%>