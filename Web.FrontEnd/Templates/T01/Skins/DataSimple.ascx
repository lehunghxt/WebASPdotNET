<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web"%>


<div class="links-area pb-30">
    <div class="link-header">
        <p class="header-title"><span class="highline"><%=this.Title.Split(' ')[0] %></span> <%=this.Title.Substring(Title.Split(' ')[0].Length)%>
    </p></div>
    <ul class="list-unstyled list">
        <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
        <li title="<%=this.Data[i].Description.DeleteHTMLTag().Trim()%>">

                <a class="anchorLink" href="<%=this.Data[i].URL != null ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+ this.Data[i].Title.ConvertToUnSign(), false)%>">
                    <i class="fas fa-chevron-right"></i> <%=this.Data[i].Title%>
                </a>
            </li>
            <%} %>	
    </ul>

</div>

