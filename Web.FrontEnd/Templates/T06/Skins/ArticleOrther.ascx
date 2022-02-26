<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<section class=" container py-lg-4 py-md-3 py-sm-3 py-3">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h2 class="heading" title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") > 0 ? "" : ";font-size:" + this.GetValueParam<string>("HeaderFontSize") + "px"%>"><%=Title %></h2>
    </header>
    <section class="panel-body uk-clearfix">
        <div class="projectlist">
        <ul style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";color:" + this.GetValueParam<string>("HeaderBackground") %>">
        <%foreach (var article in Data)
        {%>
             <li class="form-group" title="<%=article.BRIEF.DeleteHTMLTag().Trim()%>">
                <a class="simpletitle" href="<%=HREF.LinkComponent("Article","sArt/"+article.ID+"/"+article.TITLE.ConvertToUnSign())%>">
                    <%=article.TITLE%>
                </a>
            </li>
        <%} %>
            </ul>






            </div>
	</section>
        
</section>