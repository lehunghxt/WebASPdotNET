<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<div class="w3-row w3-hide-small">

    <%for(int i = 0; i < this.Data.Count; i++)
        {
            int height = 190;
            if (this.Data.Count == 1) height = 540;
            else
            {
                if (this.Data.Count == 2)
                {
                    if (i == 0) height = 350;
                    else height = 190;
                }
            }
            %>
    
    <div class="w3-col" style="height:<%=height%>px;width:<%=((double)100/this.Data.Count).ToString().Replace(",", ".")%>%">
    <a title="<%=Data[i].Title%>" href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + Data[i].ID + "/" + Data[i].Title.ConvertToUnSign())%>">






        <img src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>" class="slider-note" style="width:100%;height:100%"/>   </a></div>
<%} %>
    </div>
                

