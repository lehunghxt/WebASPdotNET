<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
			<h1 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h1>






		</div>
							
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent"><div class="article_content"><strong><%=dto.BRIEF%></strong></div></div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
    <ul style="margin: 0px;padding: 0px 75px;background: #fff;">
        <%foreach (var article in RelatiedArticles)
            { %>
        <li><a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+article.ID+"/"+ article.Title.ConvertToUnSign())%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
        </div>
    
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
<div class="article_content"><%=dto.CONTENT%></div>
        <%if (this.DisplayTag == true)
                {%>
<div class="post-header">
    <span class="post-timestamp"><i class="icons icon-clock"></i> <%=dto.DISPLAYDATE.ToString("dd/MM/yyyy") %> </span>
    <span class="post-comment-link">
        <i class="icons icon-eye"></i> <%=dto.Views%>
    </span>
    <span class="post-labels tag">
    <i class="icons icon-tag"></i> Tag: 
<%for (int i = 0; i < Tags.Count; i++)
                {%>
    <%=Tags[i]%>, 
<%} %>
        </span>
</div>
        <%} %>

<VIT:Position runat="server" ID="psComment"></VIT:Position>
        </div>
    </div>