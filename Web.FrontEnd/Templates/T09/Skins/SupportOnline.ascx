<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SupportOnline.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.SupportOnline" %>
<%@ Import Namespace="VIT.DataTransferObjects.Presentation" %>

<style>
    .SupportOnline img{margin:0px 5px 0px 0px !important; vertical-align:0 !important; height:20px; width:20px !important; padding-top:-2px 0 0 0  !important;}
    .SupportOnline ul li {clear:both;}
    .spnick img{width: 20px;height:20px;margin-right:5px}
</style>
<div class="widget span3 widget_nav_menu">
<%foreach (var cat in base.Categories)
{
    var supports = base.SupportOnlines.Where(e => e.GroupId == cat.ID).ToList();
    %>
    <p><strong><%=cat.Title %></strong></p>
        <%foreach (var sup in supports)
        {%>
            <p class="spnick"><%=this.GetLink(sup) %><%=sup.FullName %> - <%=sup.Phone %></p>
        <%}%>
<%}%>






</div>
