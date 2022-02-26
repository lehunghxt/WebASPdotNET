<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

  <%foreach(var item in this.Data) 
  {%>  	
<li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" id="primary-menu-item-2945" class="yamm-fw menu-item menu-item-type-post_type menu-item-object-page menu-item-2945 animate-dropdown">
                                        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%> " title="<%=item.Title%>">
            <%=item.Title%>
        </a>
                                    </li>    
  <%} %>

                



