<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="homepage-artcategory home-news">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h2 class="heading" title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h2>
    </header>
    <section class="panel-body uk-clearfix">
<%if (this.GetValueParam<string>("Source") == "ART") { %>
     <%for(int i = 0; i< this.Data.Count; i++)
     {%>
        <%if(i == 0) { %>
            <article class="featured" style="float: left; width: 50%;padding-right: 10px;">
				<div class="thumb">
					<a class="image img-rsp" href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+Data[i].ID+"/"+ Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>"><img data-original="<%=Data[i].ImagePath%>" class="lazyload" src="<%=Data[i].ImagePath%>" alt="<%=Data[i].Title%>" style="display: block;"></a>






				</div>
				<div class="info">
					<h4 class="title"><a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+Data[i].ID+"/"+ Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>"><%=Data[i].Title%></a></h4>
					<div class="description">
						<%=Data[i].Description.DeleteHTMLTag()%></div>
				</div>
			</article>
			
        <%} %>
        <ul class="uk-list list-post">
         <%if(i > 0) { %>
            <li><a href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+Data[i].ID+"/"+ Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Title%>"><%=Data[i].Title%></a></li>
        <%} %>
            </ul>
    <%} %>
<%} %>


                
        	</section>
</section>
