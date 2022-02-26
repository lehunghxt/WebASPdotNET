<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>

<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


<div class="sidebar-widget mt-40">
<h4 class="pro-sidebar-title" title="<%=Title %>"><%=Title %> </h4>
    <%if(this.Source == "COR"){ %>
<div class="sidebar-widget-list mt-20">
    <ul>
        <%foreach (var item in this.Data)
            { if (item.Title == "") continue;%>
        <li>
            <div class="sidebar-widget-list-left">
                <a href="<%=HREF.LinkComponent("Products", SettingsManager.Constants.SendColor + "/" + (item.Description.StartsWith("#") ? item.Description.Substring(1,item.Description.Length - 1) : item.Description) + "/" + item.Title.ConvertToUnSign())%>">
                    <input type="radio" name="Cor<%=item.CategoryId%>" value="<%=item.Description%>" id="<%=item.Description%>" <%=item.Description == this.GetValueRequest<string>(SettingsManager.Constants.SendColor) ? "checked='checked'" : "" %>> <%=item.Title%> 
                    <span style="background:<%=item.Description%>"></span> 
                </a>
                <span class="checkmark"></span>
            </div>
        </li>
        <%}%>
    </ul>
</div>
        <%} else if(this.Source == "PTG"){%>
    <div class="sidebar-widget-tag mt-25">
                                                            <ul>
                                                                <%foreach (var item in this.Data)
            { %>
        <li><a href="<%=HREF.LinkComponent("Products", SettingsManager.Constants.SendTag + "/" + item.Title.Trim().ConvertToUnSign(), false)%>"><%=item.Title %></a></li>
        <%}%>
                                                            </ul>
                                                        </div>

        
        <%}%>
</div>



                


                

