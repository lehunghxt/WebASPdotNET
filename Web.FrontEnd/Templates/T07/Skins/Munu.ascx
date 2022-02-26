<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<nav class="nav_mn">
            <ul class="ul_mn clearfix">
                <li>
                <a href="/" title="Trang chủ">
                  Trang chủ
                </a>
              </li>

                <%foreach (var parent in this.Model)
                             {%>
                <%if (parent.Type == "LIN")
                    {%>
                                    <%foreach (var item in parent.Items)
                        {%>
                                    <li>
                                <a title="<%=item.Title%>" href="<%=item.URL != null ? item.URL : CreateLink(parent.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                                        </li>
                        <%} %>
                <%} else { %>
     <%if (parent != null && parent.Childs != null && parent.Childs.Count > 0)
         {%>

    <li>
                                <a href="#">
                                    <%=parent.CategoryName %>
                                    <svg class="svg-inline--fa fa-angle-down fa-w-8" aria-hidden="true" focusable="false" data-prefix="fal" data-icon="angle-down" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512" data-fa-i2svg=""><path fill="currentColor" d="M119.5 326.9L3.5 209.1c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0L128 287.3l100.4-102.2c4.7-4.7 12.3-4.7 17 0l7.1 7.1c4.7 4.7 4.7 12.3 0 17L136.5 327c-4.7 4.6-12.3 4.6-17-.1z"></path></svg>
                                </a>
                                <ul class="mn_child_01">
                                    <%foreach (var child in parent.Childs)
                        {%>
                                    <%if (child.Childs != null && child.Childs.Count > 0)
                                        { %>
                                    <%}
    else
    { %>
                                    <li>
                                <a title="<%=child.CategoryName%>" href="<%=CreateLink(parent.Type, false, child.ID, child.CategoryName)%>"><%=child.CategoryName%></a>
                                        </li>
                                    <%} %>
                        <%} %>
                                </ul>
                            </li>
    <%} else if (parent != null && parent.Items != null && parent.Items.Count > 0)
                    {%>
     <li>
                                <a href="#">
                                    <%=parent.CategoryName %>
                                    <svg class="svg-inline--fa fa-angle-down fa-w-8" aria-hidden="true" focusable="false" data-prefix="fal" data-icon="angle-down" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 256 512" data-fa-i2svg=""><path fill="currentColor" d="M119.5 326.9L3.5 209.1c-4.7-4.7-4.7-12.3 0-17l7.1-7.1c4.7-4.7 12.3-4.7 17 0L128 287.3l100.4-102.2c4.7-4.7 12.3-4.7 17 0l7.1 7.1c4.7 4.7 4.7 12.3 0 17L136.5 327c-4.7 4.6-12.3 4.6-17-.1z"></path></svg>
                                </a>
                                <ul class="mn_child_01">
                                    <%foreach (var item in parent.Items)
                        {%>
                                    <li>
                                <a title="<%=item.Title%>" href="<%=item.URL != null ? item.URL : CreateLink(parent.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                                        </li>
                        <%} %>
                                </ul>
                            </li>
                    <%} else {%>
    <li>
                                <a href="<%=CreateLink(parent.Type, false, parent.ID, parent.CategoryName)%>"><%=parent.CategoryName%></a>
                            </li>
    <%} %>
                 <%} %>  
                         <%} %>              
              <li>
                    <a href="<%=HREF.LinkComponent("Contact")%>"><%=Language["Contact"] %></a>
              </li>
            </ul>
            <!-- End .ul_mn -->
          </nav>
<ul class="navbar-nav ml-lg-auto text-center">
                            

<style>
    nav ul li a, nav ul li ul li a {color:<%=this.GetValueParam<string>("FontColor") %> !important}
</style>


    <!-- End .form_s -->
    <div class="menu_mobile" style="visibility: hidden;">
      <span class="close_menu_mobile"></span>
      <div class="menu_accordion">
        <ul class="ul_ma_1">
            <li class="active">
            <a href="/" title="Trang chủ">Trang chủ</a>
          </li>
            <%foreach (var parent in this.Model)
                             {%>
     <%if (parent != null && parent.Childs != null && parent.Childs.Count > 0)
         {%>

    <li>
                                <a href="#"><%=parent.CategoryName %></a>
        <i class="arrown_menu_accordion" val="sub_ac_<%=parent.ID %>"></i>
                                <ul class="ul_ma_2" id="sub_ac_<%=parent.ID %>">
                                    <%foreach (var child in parent.Childs)
                        {%>
                                    <%if (child.Childs != null && child.Childs.Count > 0)
                                        { %>
                                    <%}
    else
    { %>
                                    <li>
                                <a title="<%=child.CategoryName%>" href="<%=CreateLink(parent.Type, false, child.ID, child.CategoryName)%>"><%=child.CategoryName%></a>
                                        </li>
                                    <%} %>
                        <%} %>
                                </ul>
                            </li>
    <%} else if (parent != null && parent.Items != null && parent.Items.Count > 0)
                    {%>
     <li>
                                <a href="#"><%=parent.CategoryName %></a>
         <i class="arrown_menu_accordion" val="sub_ac_<%=parent.ID %>"></i>
                                <ul class="ul_ma_2" id="sub_ac_<%=parent.ID %>">
                                    <%foreach (var item in parent.Items)
                        {%>
                                    <li>
                                <a title="<%=item.Title%>" href="<%=item.URL != null ? item.URL : CreateLink(parent.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                                        </li>
                        <%} %>
                                </ul>
                            </li>
                    <%} else {%>
    <li>
                                <a href="<%=CreateLink(parent.Type, false, parent.ID, parent.CategoryName)%>"><%=parent.CategoryName%></a>
                            </li>
    <%} %>
                         <%} %> 
         
          <li>
           <a href="<%=HREF.LinkComponent("Contact")%>"><%=Language["Contact"] %></a>
          </li>
        </ul>
        <!-- End .ul_ma_1 -->






      </div>
      <!-- End .menu_accordion -->
    </div>
    <!-- End .menu_mobile -->