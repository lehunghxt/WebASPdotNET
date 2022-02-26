<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<ul id="menu-main-navigation" class="level-1 group">
    <li class="menu-item">
        <a href="/" style="padding-right: 31px;"><%=this.Language["Home"] %></a>
    </li>
    <%for (int i = 0; i < this.Model.Count; i++)
    {%>
        <%if (this.Model[i] != null && this.Model[i].Childs.Count > 0)
        {%>
            <li class="menu-item">
            <a href="<%=CreateLink(this.Model[i].Type, false, this.Model[i].ID, this.Model[i].CategoryName)%>" style="padding-right: 31px;"><%=this.Model[i].CategoryName%>
                <span class="sf-sub-indicator"> »</span>
                <span class="sf-sub-indicator"> »</span>
            </a>
            <div class="submenu group" style="display: none;">
                <ul class="sub-menu group">
            <%foreach (var dto in this.Model[i].Childs)
            {%>
                <%if (dto.Items != null && dto.Items.Count > 0)
                {%>
         <li id="menu-item-<%=dto.ID%>" class="menu-item">
            <a href="<%=CreateLink(this.Model[i].Type, true, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a>
            <div class="submenu group" style="display: none;">
                <ul class="sub-menu group">
                    <%foreach (var item in dto.Items)
                    {%>
                        <li><a href="<%=item.URL%>">
                            <%=item.Title%></a></li>
                    <%} %>
                </ul>






            </div>  
        </li>
                <%} 
                 else {%>
        <li id="menu-item-<%=dto.ID%>" class="menu-item">
            <a href="<%=CreateLink(this.Model[i].Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a></li>
                <%} %>
            <%} %>
                    </ul>
            </div>  
        </li>
        <%} %>
        <%else {%>
            <%if (this.Model[i].Items != null && this.Model[i].Items.Count > 0)
                {%>
    <%foreach (var item in this.Model[i].Items)
                                {%>
                     <li id="menu-item-<%=item.CategoryId%>" class="menu-item">
                                
                                    <a href="<%=item.URL%>">
                                        <%=item.Title%></a>
                    </li>
     <%} %>
                <%} 
                 else {%>
        <li id="menu-item-<%=this.Model[i].ID%>">
            <a href="<%=CreateLink(this.Model[i].Type, false, this.Model[i].ID, this.Model[i].CategoryName)%>"><%=this.Model[i].CategoryName%></a></li>
            <%} %>
        <%} %>
     <%} %>
    <li><a href="<%=HREF.LinkComponent("Contact")%>"><%=this.Language["Contact"] %></a></li>
</ul>

<%if(!string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor"))) {%>
<style>
    #menu-main-navigation li:hover{color:<%=this.GetValueParam<string>("BackgroundColor ")%> !important}
    #menu-main-navigation li a:hover{color:<%=this.GetValueParam<string>("BackgroundColor ")%> !important}
</style>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        $('#topbar').css('background-color', '<%=this.GetValueParam<string>("BackgroundColor")%>');
        $('#sidebar-home h3').css('color', '<%=this.GetValueParam<string>("BackgroundColor ")%>');
        $('#footer h3').css('color', '<%=this.GetValueParam<string>("BackgroundColor ")%>');
        $('#footer .title').css('color', '<%=this.GetValueParam<string>("BackgroundColor ")%>');
        $('#footer a').css('color', '<%=this.GetValueParam<string>("BackgroundColor ")%>');
        $('#copyright a').css('color', '<%=this.GetValueParam<string>("BackgroundColor ")%>');
    });
    </script>
<%} %>