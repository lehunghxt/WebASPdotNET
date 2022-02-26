<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<ul class="menu mt-2">
                     <li class="active"><a href="/"><%=Language["Home"] %></a></li>
    <%foreach (var parent in this.Model)
                             {%>
     <%if (parent != null && parent.Childs != null && parent.Childs.Count > 0)
         {%>
    <li class="mx-lg-3 mx-md-2 my-md-0 my-1">
                        <!-- First Tier Drop Down -->
                        <label for="drop-2" class="toggle toogle-2"><%=parent.CategoryName %> <span class="fa fa-angle-down" aria-hidden="true"></span>
                        </label>
                        <a href="#"><%=parent.CategoryName %> <span class="fa fa-angle-down" aria-hidden="true"></span></a>
                        <input type="checkbox" id="drop-2">
                        <ul>
                            <%foreach (var child in parent.Childs)
                        {%>
                                <li class="drop-text"><a href="<%=CreateLink(parent.Type, false, child.ID, child.CategoryName)%>"><%=child.CategoryName%></a>
                        <%} %>
                        </ul>
                     </li>
    <%} else if (parent != null && parent.Items != null && parent.Items.Count > 0)
                    {%>
                    <li class="mx-lg-3 mx-md-2 my-md-0 my-1">
                        <!-- First Tier Drop Down -->
                        <label for="drop-2" class="toggle toogle-2"><%=parent.CategoryName %> <span class="fa fa-angle-down" aria-hidden="true"></span>
                        </label>
                        <a href="#"><%=parent.CategoryName %> <span class="fa fa-angle-down" aria-hidden="true"></span></a>
                        <input type="checkbox" id="drop-2">
                        <ul>
                            <%foreach (var item in parent.Items)
                        {%>
                                <li class="drop-text"><a href="<%=CreateLink(parent.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                        <%} %>
                        </ul>
                     </li>
                    <%}%>
                         <%} %>
                     
                     <li><a href="<%=HREF.LinkComponent("Contact")%>"><%=Language["Contact"] %></a></li>
                  </ul>

<style>
    nav ul li a, nav ul li ul li a{color:<%=this.GetValueParam<string>("FontColor") %>}
    nav ul, nav ul li ul{background:<%=this.GetValueParam<string>("BackgroundColor") %>}
</style>


