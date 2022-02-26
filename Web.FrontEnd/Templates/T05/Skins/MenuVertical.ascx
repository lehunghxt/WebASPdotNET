<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>
<div class="mega-menu">
<ul class="uk-list main">
    <%foreach (var item in this.Model)
    {%>
    <li class="item">
        <a href="<%=CreateLink(item.Type, false, item.ID, item.CategoryName)%>" title="<%=item.CategoryName %>">






            <div class="title"><%=item.CategoryName %></div>
        </a>
    <%if (item.Childs != null && item.Childs.Count > 0)
        {%>
        <div class="fly-menu">
            <div class="uk-clearfix container">
                <ul class="uk-list submenu">
                    <%foreach (var dto in item.Childs)
                    {%>
                    <li><a href="<%=CreateLink(item.Type, true, dto.ID, dto.CategoryName)%>" title="<%=dto.CategoryName %>"><i class="fa fa-caret-right"></i><%=dto.CategoryName %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <!-- .fly-menu -->
        <%} %> 
        <%if (item.Items != null && item.Items.Count > 0)
        {%>
        <div class="fly-menu">
            <div class="uk-clearfix container">
                <ul class="uk-list submenu">
                    <%foreach (var dto in item.Items)
                    {%>
                    <li><a href="<%=CreateLink(item.Type, true, dto.CategoryId, dto.Title)%>" title="<%=dto.Title %>"><i class="fa fa-caret-right"></i><%=dto.Title %></a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <!-- .fly-menu -->
        <%} %> 
         </li>
    <%} %>
</ul>
    </div>