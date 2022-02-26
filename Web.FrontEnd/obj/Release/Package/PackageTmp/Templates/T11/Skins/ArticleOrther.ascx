<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div >
            <div class="header_menu_right">
              <h3 style="color: white;font-size: 1.4em;"><%=Title %></h3>
            </div>
    <ul class="sub_menu_right">

   <%foreach (var article in Data)
        {%>
             <li class="form-group" title="<%=article.BRIEF.DeleteHTMLTag().Trim()%>">
                <h4><a class="simpletitle" href="<%=HREF.LinkComponent("Article","sArt/"+article.ID+"/"+article.TITLE.ConvertToUnSign())%>">
                    <%=article.TITLE%>
                </a></h4>
            </li>
        <%} %>
</ul>
          </div>