<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<div class="col-lg-3 col-md-3 col-sm-6">
	<h3 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h3>
      <ul class="ds<%=this.Source %>">
         <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
        <li style="opacity: 1;"><a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + Data[i].ID + "/" + Data[i].Title.ConvertToUnSign())%>">
            <%=Data[i].Title%></a></li>
            <% }%>
        </ul>
   






</div>
