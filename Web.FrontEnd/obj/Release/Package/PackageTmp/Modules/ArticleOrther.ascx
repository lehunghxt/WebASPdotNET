<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>


 
   <%foreach (var article in Data)
        {%>
             <li class="form-group" title="<%=article.BRIEF.DeleteHTMLTag().Trim()%>">
                <a class="simpletitle" href="<%=HREF.LinkComponent("Article","sArt/"+article.ID+"/"+article.TITLE.ConvertToUnSign())%>">
                    <%=article.TITLE%>
                </a>
            </li>
        <%} %>