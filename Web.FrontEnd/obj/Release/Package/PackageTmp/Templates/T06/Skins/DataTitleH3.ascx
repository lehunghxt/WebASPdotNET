<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="tags">
<h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>
	<ul class="article_links">
                                    <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                                    <li><% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>
        <a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>">
                <%=Data[i].Title%>
            </a></li>
								<%}%>

								</ul>






	<div class="clearfix"></div>

    </div>

