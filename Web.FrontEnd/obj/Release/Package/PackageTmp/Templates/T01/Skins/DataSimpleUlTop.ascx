<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<ul id="menu-top-bar-left" class="nav justify-content-center">
    <%foreach(var item in this.Data) 
  {%>  	
	<li itemscope="itemscope" itemtype="https://www.schema.org/SiteNavigationElement" class="menu-item menu-item-type-custom menu-item-object-custom menu-item-814 animate-dropdown">
        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%> " title="<%=item.Title%>">
            <%=item.Title%>
        </a>
	</li>	       
  <%} %>
                </ul>
  


                



