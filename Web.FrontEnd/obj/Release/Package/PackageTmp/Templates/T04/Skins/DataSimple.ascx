<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<ul class="Data_<%=this.GetValueParam<string>("Source")%>">
    <%foreach(var item in this.Data) 
    {%>
        <%if (this.GetValueParam<string>("Source") == "LIN")
        {%>
            <li class='item item_0 col-lg-<%=12/(Width == 0 ? 1: Width) %> col-md-<%=12/(Width == 0 ? 1: Width) %> col-sm-<%=12/(Width == 0 ? 1: Width) %>'>
                <a class="simpleimg" href="<%=item.URL%>" title="<%=item.Title %>">
                    <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='Width:100%;<%=!string.IsNullOrEmpty(item.ImagePath) ? "" : "display:none;"%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'/>
                </a>
            </li>
        <%} else if (this.GetValueParam<string>("Source") == "MID")
        {%>
            <li class='item item_0' style='<%=Width > 0 ? "Width:" + Width + "px;": ""%>'>
                <a class="simpleimg" href="<%=item.ImagePath%>" data-lightbox="example-set" title="<%=item.Title %>">
                    <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='<%=!string.IsNullOrEmpty(item.ImagePath) ? "" : "display:none;"%><%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'/>
                </a>
            </li>
        <%} else if (this.GetValueParam<string>("Source") == "ATR")
            {%>
    <li>
           <% var param = Source != "ATR" ? RederectSendKey + "/" + item.ID
                                          : RederectSendKey + "Id/" + item.CategoryId + "/" + RederectSendKey + "Vl/" + item.ID; %>
        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, param + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                <input type="radio" id="<%=item.ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%=item.ID == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                <label class="form-label"><%=item.Title%></label>
            </a>
        </li>
     <%} else if (this.GetValueParam<string>("Source") == "ART")
            {%>
   <li>
                        <a class="simpletitle" href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey+ "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                        <i class="icons icon-right-dir"></i> <%=item.Title %></a>
                    </li>
     <%} %>
    <%} %>
	
</ul>



                



