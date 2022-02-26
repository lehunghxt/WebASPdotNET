<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section class="prdcategory-child list">
    <header class="panel-head">
		<h1 class="heading" title="<%=Title %>"><%=Title %></h1>
    </header>
    <section class="panel-body">
<div class="row">
    <%foreach(var item in this.Data) 
  {%>  
                    <div class="col-sm-6 col-xs-12 lstArticle">
                        <div class="row">
                            <a class="col-xs-3" href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                                <img src='<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>' alt='<%=item.Title%>' width="100%"/>
                            </a>
                            <div class="col-xs-9">
                                <h2 title="<%=item.Title%>">
                                    <a class="article_title" href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>"><%=item.Title%></a>
                                </h2>
                                    <p class="article_brief"><%=item.Description.DeleteHTMLTag().Trim().Length > 120 ? item.Description.DeleteHTMLTag().Trim().Substring(0, 120) + "..." : item.Description.DeleteHTMLTag().Trim() %></p>






                            </div>
                        </div>
                    </div>       
                <%} %>
</div>
</section>
    </section>
