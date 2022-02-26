<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Source == "PTG"){ %>
<div class="tags">
<h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>
	<ul class="article_links">
        <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
            {%>
        <li class="gentag"><a href="/products/vit/<%=SettingsManager.Constants.SendTag%>/<%=item.Title.Trim().ConvertToUnSign()%>"><%=item.Title %></a></li>
            <%} %>	
    </ul>






    <div class="clear"></div>
</div>
<%} %>

