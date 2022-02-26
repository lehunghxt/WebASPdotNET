<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %>">
    <b style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></b>
</div>
 <div class="cate_left_list">
<ul class="module_colors <%=RederectComponent%>">
    <%foreach(var item in this.Data) 
    {%>
        <%if (this.GetValueParam<string>("Source") == "LIN")
        {%>
            <li>
                <a class="w3-row" href="<%=item.URL%>" title="<%=item.Title %>">
                    <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='Width:100%;<%=!string.IsNullOrEmpty(item.ImagePath) ? "" : "display:none;"%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'/>
                </a>
            </li>
        <%} else if (this.GetValueParam<string>("Source") == "MID")
        {%>
            <li>
                <a class="w3-row" href="<%=item.ImagePath%>" data-lightbox="example-set" title="<%=item.Title %>">
                    <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='<%=!string.IsNullOrEmpty(item.ImagePath) ? "" : "display:none;"%><%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'/>
                </a>
            </li>
        <%} else if (this.GetValueParam<string>("Source") == "ATR")
            {%>
        <li>
            <% var param = Source != "ATR" ? RederectSendKey + "/" + item.ID
                                          : RederectSendKey + "Id/" + item.CategoryId + "/" + RederectSendKey + "Vl/" + item.ID; %>
                                    <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, param + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title%>" class="w3-row">
                                        <div class="w3-col w3-center" style="width:20%">
                                            <input type="radio" id="<%=item.ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" style="margin-top:22px" <%=item.ID == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                                        </div>
                                        <div class="w3-col" style="width:80%">
                                            <%=item.Title %>
                                        </div>
                                    </a>
                                </li>
     <%} else if (this.GetValueParam<string>("Source") == "ART")
            {%>
    <li>
                                    <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey+ "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title=" <%=item.Title %>" class="w3-row">
                                        <div class="w3-col w3-center" style="width:20%">
                                            <input type="radio" id="<%=item.ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter"  style="margin-top:22px" <%=item.ID == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                                        </div>
                                        <div class="w3-col" style="width:80%">
                                            <%=item.Title %>
                                        </div>
                                    </a>
                                </li>
     <%}else if (this.GetValueParam<string>("Source") == "CAT"){ %>
    <li> 
                                    <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title%>" class="w3-row">
                                        <div class="w3-col w3-center" style="width:20%">
                                            <input type="radio" id="<%=item.ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" style="margin-top:22px" <%=item.ID == this.GetValueRequest<int>(SettingsManager.Constants.SendCategory) ? "checked='checked'" : "" %>/>
                                        </div>
                                        <div class="w3-col" style="width:80%">
                                            <%=item.Title %>
                                        </div>
                                    </a>
                                </li>
       <%}else {%>
	<li>
                                    <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey+ "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title=" <%=item.Title %>" class="w3-row">
                                        <div class="w3-col w3-center" style="width:20%">
                                            <input type="radio" id="<%=item.ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" style="margin-top:22px" <%=item.ID == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                                        </div>
                                        <div class="w3-col" style="width:80%">
                                            <%=item.Title %>
                                        </div>
                                    </a>
                                </li>
    <%} %>
	 <%} %>
</ul>
     </div>

