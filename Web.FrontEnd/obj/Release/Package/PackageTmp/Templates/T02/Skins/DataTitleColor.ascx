<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="homepage-artcategory home-news">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h2 class="heading" title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<string>("HeaderFontSize") + "px"%>"><%=Title %></h2>
    </header>
    <section class="panel-body uk-clearfix">
        <div class="projectlist">
        <ul style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";color:" + this.GetValueParam<string>("HeaderBackground") %>">
         <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
        <% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>
        <li style="opacity: 1;">
            <a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>">
                <%if (!string.IsNullOrEmpty(Data[i].ImagePath)){ %><img src="<%=Data[i].ImagePath%>" style="opacity: 1;"><%} %>
                <span class="name"><%=Data[i].Title%></span>
            </a>
        </li>
            <%}%>
            </ul>






            </div>
	</section>
        
</section>

