<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web" %>



<nav class="main-nav">
    <ul class="uk-navbar-nav main-menu">
        <%foreach (var item in this.Model)
        {%>
            <%if (item.Childs != null && item.Childs.Count > 0)
            {%>
        <%foreach (var dto in item.Childs)
                     {%>
        <li>
            <a href="<%=CreateLink(dto.Type, false, dto.ID, item.CategoryName)%>" title="<%=dto.CategoryName %>"><%=dto.CategoryName %></a>
                <%if (dto.Items != null && dto.Items.Count > 0)
                {%>
            <div class="drop-menu">
                <ul class="uk-list uk-clearfix sub-menu">
                     <%foreach (var di in dto.Items)
                     {%>
                    <li>
                        <a href="<%=CreateLink(item.Type, true, di.ID, di.Title)%>" title="<%=di.Title %>"><%=di.Title %></a>
                    </li>
                     <%} %>
                </ul>






            </div>
                <%} %>
            
        </li>
        <%} %>
        
            <%} %>
            <%if (item.Items != null && item.Items.Count > 0)
                    {%>
                    <%foreach (var lịnk in item.Items)
                     {%>
                    <li><a href="<%=lịnk.URL%>" title="<%=lịnk.Title%>" target="<%=lịnk.TargetTag%>"><%=lịnk.Title%></a></li>
                    <%} %>
            <%} %>
        <%} %>

        <li  >
            <a href="<%=HREF.LinkComponent("Contact")%>">
                Liên hệ
            </a>
        </li>
    </ul>
</nav>
