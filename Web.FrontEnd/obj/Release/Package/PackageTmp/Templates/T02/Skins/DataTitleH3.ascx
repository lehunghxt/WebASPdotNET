<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="aside-panel aside-categories">
<header class="panel-head">
	<h3 class="heading" title ="<%=Title %>"><span><%=Title %></span></h3>
</header>
<section class="panel-body">
    
<div class="field-groups">
    
         <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
    <div class="form-row">
        <% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>
        <a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>">
                <input type="radio" id="<%=Data[i].ID%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%=Data[i].ID == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                <label class="form-label"><%=Data[i].Title%></label>
            </a>






        </div> 
            <%}%>
        </div>

</section>
    </div>

