<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<div class="col-lg-3 col-md-3 col-sm-6">
    <div style="padding:0px 20px">
<h3 title="<%=Title %>"><%=Title %></h3>
								<ul class="menu" style="color:#000">
                                    <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                                    <li><% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>
        <a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>">
                <%=Data[i].Title%>
            </a></li>
								<%}%>

								</ul>






    </div>
    </div>