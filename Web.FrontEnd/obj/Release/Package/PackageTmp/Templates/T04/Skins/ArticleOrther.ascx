<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %>">
    <b style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></b>






</div>
<div class="cate_left_list">
<div class="module_colors">

<table class="compare-table">
    <%foreach (var article in Data)
        {%>
             <tr>
                 <td style="width: 30%">
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