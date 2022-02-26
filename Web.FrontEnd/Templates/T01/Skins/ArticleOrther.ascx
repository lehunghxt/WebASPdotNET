<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="col-lg-12">
<h2 class="top-1"><%=this.Title %></h2>

<ul class="row Article_Articles">
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
