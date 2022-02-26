<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttribute.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttribute" %>

<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<div class="sidebar-widget mt-40">
<h4 class="pro-sidebar-title" title="<%=Title %>"><%=Title %> </h4>
<div class="sidebar-widget-list mt-20">
    <ul>
        <%foreach (var item in this.Data)
            {%>
        <li>
            <div class="sidebar-widget-list-left">
                <a href="<%=HREF.LinkComponent(ComponentProducts, SettingsManager.Constants.SendAttributeId + "/" + item.CategoryId + "/" + SettingsManager.Constants.SendAttributeValue + "/" + item.Id + "/" + item.Value.ConvertToUnSign())%>"><input type="radio" name="<%=item.CategoryId%>" value="<%=item.Id%>" id="<%=item.Id%>" <%=item.Id == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>> <%=item.Value%> <span></span> </a>
                <span class="checkmark"></span>
            </div>
        </li>
        <%}%>
    </ul>
</div>
</div>



                


                

