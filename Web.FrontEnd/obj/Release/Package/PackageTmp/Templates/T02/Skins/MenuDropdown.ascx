<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>


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
        <%foreach (var menu in item.Childs)
        {%>
                    <li><a href="<%=CreateLink(menu.Type, true, menu.ID, menu.CategoryName)%>" title="<%=menu.CategoryName %>"><i class="fa fa-caret-right"></i><%=menu.CategoryName %></a></li>
        <%} %>
                    </ul>
            </div>
        </div>
        <%} else if (item.Items != null && item.Items.Count > 0)
        {%>
        <div class="fly-menu">
            <div class="uk-clearfix container">
                <ul class="uk-list submenu">
                    <%foreach (var dto in item.Items)
                    {%>
                    <li><a href="<%=CreateLink(item.Type, true, item.ID, item.CategoryName)%>" title="<%=item.CategoryName %>"><i class="fa fa-caret-right"></i><%=item.CategoryName %></a></li>
                    <%} %>
                </ul>
                <%--<div class="banner mega-menu-banner">
                    <a href="<%=CreateLink(menu.RederectComponent, menu.RederectView, menu.RederectSendKey, dto.CategoryId, dto.CategoryName)%>" title="<%=dto.CategoryName %>">
                        <img src="/uploads/images/banner/tinh-dau.jpg" alt="<%=dto.CategoryName %>"></a>
                </div>--%>
            </div>
        </div>
        <!-- .fly-menu -->
        <%} %>
    </li>
    <%} %>
</ul>
