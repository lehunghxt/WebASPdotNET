<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<div class="homepage-category">
    <header class="panel-head">
	    <h2 class="heading"><a href="tinh-dau.html" title="<%=Title %>"><%=Title %></a></h2>
    
<div class="projectlist">
    <ul>
         <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
        <li style="opacity: 1;"><a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + Data[i].ID + "/" + Data[i].Title.ConvertToUnSign())%>">
            <img src="<%=this.Data[i].ImagePath%>" style="opacity: 1;" alt="<%=this.Data[i].Title%>"/>
            <span class="name"><%=Data[i].Title%></span></a></li>
            <% }%>
        </ul>






</div> 
</header>
</div>
