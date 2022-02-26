<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<ul class="navbar-nav ml-lg-auto text-center">
                            
    <li class="nav-item active  mr-3">
                                <a class="nav-link" href="/"><%=Language["Home"] %>
                                    <span class="sr-only">(current)</span>
                                </a>
                            </li>
    <%foreach (var parent in this.Model)
                             {%>
     <%if (parent != null && parent.Childs != null && parent.Childs.Count > 0)
         {%>

    <li class="nav-item dropdown mr-3">
                                <a class="nav-link dropdown-toggle" href="#" id="<%=parent.ID%>navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <%=parent.CategoryName %>
                                </a>
                                <div class="dropdown-menu" aria-labelledby="<%=parent.ID%>navbarDropdown">
                                    <%foreach (var child in parent.Childs)
                        {%>
                                <a class="dropdown-item" title="<%=child.CategoryName%>" href="<%=CreateLink(parent.Type, false, child.ID, child.CategoryName)%>"><%=child.CategoryName%></a>
                        <%} %>






                                </div>
                            </li>
    <%} else if (parent != null && parent.Items != null && parent.Items.Count > 0)
                    {%>
     <li class="nav-item dropdown mr-3">
                                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown<%=parent.ID%>" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <%=parent.CategoryName %>
                                </a>
                                <div class="dropdown-menu" aria-labelledby="navbarDropdown<%=parent.ID%>">
                                    <%foreach (var item in parent.Items)
                        {%>
                                <a class="dropdown-item" title="<%=item.Title%>" href="<%=item.URL != null ? item.URL : CreateLink(parent.Type, true, item.ID, item.Title)%>" target="<%=item.TargetTag%>"><%=item.Title%></a>
                        <%} %>
                                </div>
                            </li>
                    <%} else {%>
    <li class="nav-item  mr-3">
                                <a class="nav-link" href="<%=CreateLink(parent.Type, false, parent.ID, parent.CategoryName)%>"><%=parent.CategoryName%></a>
                            </li>
    <%} %>
                         <%} %>
                     
    <li class="nav-item">
                                <a class="nav-link" href="<%=HREF.LinkComponent("Contact")%>"><%=Language["Contact"] %></a>
                            </li>
                  </ul>

<style>
    nav ul li a, nav ul li ul li a {color:<%=this.GetValueParam<string>("FontColor") %> !important}
    nav ul, nav ul li ul{background:<%=this.GetValueParam<string>("BackgroundColor") %>}
    .header-left{background:linear-gradient(to right, <%=this.GetValueParam<string>("BackgroundColor") %>, <%=this.GetValueParam<string>("FontColor") %>)}
    .banner-main:before{background:<%=this.GetValueParam<string>("FontColor") %>}
</style>