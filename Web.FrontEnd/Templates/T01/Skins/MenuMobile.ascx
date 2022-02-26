<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<div id="departments-menu" class="dropdown departments-menu">
    <button class="btn dropdown-toggle btn-block" type="button" aria-haspopup="true" aria-expanded="false">
        <i class="fa fa-bars"></i>
        <span><%=Title %>
        </span>
    </button>
    <ul id="menu-departments-menu" class="dropdown-menu yamm departments-menu-dropdown animated-dropdown">
        <%--<li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" id="departments-menu-menu-item-2925" class="highlight menu-item menu-item-type-post_type menu-item-object-page menu-item-2925 animate-dropdown">
            <a title="Value of the Day" href="https://banmuagi.online/home-v2/">Value of the Day
            </a>
        </li>--%>
        <%foreach (var model in this.Model)
    {%>
     <%foreach (var item in model.Childs)
    {%>
    <%if (item.Childs != null && item.Childs.Count > 0)
        {%>
        <li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" id="departments-menu-menu-item-471" class="yamm-tfw menu-item menu-item-type-custom menu-item-object-custom menu-item-has-children menu-item-471 animate-dropdown dropdown-submenu">
            <a title="<%=item.CategoryName %>" data-toggle="dropdown" class="dropdown-toggle disabled" aria-haspopup="true" href="#"><%=item.CategoryName %>
                <span class="caret">
                </span>
            </a>
            <ul role="menu" class=" dropdown-menu" style="min-height: 525px; visibility: hidden; display: none; width: 0px; opacity: 0;">
                <li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" id="departments-menu-menu-item-2590" class="menu-item menu-item-type-post_type menu-item-object-static_block menu-item-2590 animate-dropdown" style="min-height: 525px;">
                    <div class="yamm-content">
                        <section class="kc-elm kc-css-783108 kc_row bg-yamm-content bg-yamm-content-bottom bg-yamm-content-right">
                            <div class="kc-row-container  kc-container">
                                <div class="kc-wrap-columns">
                                    <div class="kc-elm kc-css-788597 kc_column kc_col-sm-12">
                                        <div class="kc-col-container">
                                            <div class="kc-elm kc-css-302387 kc_shortcode kc_single_image">
                                                <img src="<%=item.CategoryImage %>" class="" alt="">






                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section class="kc-elm kc-css-702854 kc_row">
                            <div class="kc-row-container  kc-container">
                                <div class="kc-wrap-columns">
                                    <div class="kc-elm kc-css-651668 kc_col-sm-6 kc_column kc_col-sm-6">
                                        <div class="kc-col-container">
                                            <div class="kc-elm kc-css-773924 kc_text_block">
                                                <h5><%=item.CategoryName %></h5>
                                                <ul>
                                                    <%foreach (var dto in item.Childs)
                        {%>
                    <li><a href="<%=CreateLink(item.Type, false, dto.ID, dto.CategoryName)%>" title="<%=dto.CategoryName %>"><%=dto.CategoryName %></a></li>
                    <%} %>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                        </section>
                    </div>
                </li>
            </ul>
        </li>

        <!-- .fly-menu -->
        <%}
            else
            { %> 
        <li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" id="departments-menu-menu-item-<%=item.ID %>" class="menu-item menu-item-type-taxonomy menu-item-object-product_cat menu-item-<%=item.ID %> animate-dropdown">
            <a title="<%=item.CategoryName %>" href="<%=CreateLink(item.Type, false, item.ID, item.CategoryName)%>"><%=item.CategoryName %>
            </a>
        </li>
        <%} %>
    <%} %>
      <%} %>
        
        
        
    </ul>
</div>
