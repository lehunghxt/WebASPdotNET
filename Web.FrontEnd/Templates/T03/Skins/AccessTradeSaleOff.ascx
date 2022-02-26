<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/AccessTradeSaleOff.ascx.cs" Inherits="Web.FrontEnd.Modules.AccessTradeSaleOff" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>


<div style="display: inline-block<%=string.IsNullOrEmpty(this.BackgroundColor) ? "" : ";background-color:" + this.BackgroundColor %><%=string.IsNullOrEmpty(this.FontColor) ? "" : ";color:" + this.FontColor %><%=this.FontSize == 0 ? "" : ";font-size:" + this.FontSize + "px"%>">
<ul class="_">
    <%foreach(var item in this.Data) 
    {%>
            <li class='item item_0 col-lg-12 col-md-12 col-sm-12'>
                <a class="simpleimg" href="<%=this.DeepLink%>?url=<%=HttpUtility.UrlEncode(item.link)%>" title="<%=item.name %>">
                    <img src='<%=item.image%>' alt='<%=item.name%>' style='Width:100%;'/>
                </a>
            </li>
    <%} %>
	
</ul>






    <div class="clear"></div>
    </div>