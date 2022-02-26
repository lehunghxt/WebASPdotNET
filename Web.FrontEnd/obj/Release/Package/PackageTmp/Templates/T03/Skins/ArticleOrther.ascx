<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
			<h4 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h4>






		</div>
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">

<table class="compare-table">
    <%foreach (var article in Data)
        {%>
             <tr>
                 <td class="product-thumbnail">
                    <a href="<%=HREF.LinkComponent("Article","sArt/"+article.ID+"/"+article.TITLE.ConvertToUnSign())%>">
                        <img src='<%=!string.IsNullOrEmpty(article.PathImage) ? article.PathImage : "/templates/t02/img/no-image-available.png"%>' alt='<%=article.TITLE%>'/>
			        </a>
                </td>
                <td class="product-info">
                    <p><a href="<%=HREF.LinkComponent("Article","sArt/"+article.ID+"/"+article.TITLE.ConvertToUnSign())%>"><%=article.TITLE%></a></p>
                    <span class="date"><i class="icons icon-clock"></i> <%=string.Format("{0:dd/MM/yyyy}", article.DISPLAYDATE) %></span>
                </td>
            </tr>
        <%} %>
                                    
</table>
</div>
    </div>