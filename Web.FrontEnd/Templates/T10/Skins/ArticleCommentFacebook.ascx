<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

    <div style="margin-top:30px">
<a href="/" title="Trang chủ">Trang chủ</a> / <a href="https://truyencuoi.top/san-hang-khuyen-mai-vit-contact" title="Săn hàng khuyến mãi">Săn khuyến mãi - voucher giảm giá</a> / <%=dto.TITLE%>
<div class="clearfix"></div>
</div>

<%if(this.GetValueParam<bool>("DisplayTitle")){ %>
<h1 class="main-heading" title="<%=dto.TITLE%>" style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px"><%=dto.TITLE%></h1>
<%} %>
<%if(this.GetValueParam<bool>("DisplayTitle") && this.GetValueParam<bool>("DisplayTag")){ %>
<div class="post-header">
    <span class="post-timestamp">
        <i class="glyphicon glyphicon-calendar"></i><%=String.Format("{0:dd/MM/yyyy}", dto.DISPLAYDATE)%>
    </span>
    <span class="post-comment-link">
        <i class="glyphicon glyphicon-eye-open"></i> <%=dto.Views%>
    </span>
    <span class="post-labels tag">
        <i class="glyphicon glyphicon-tags"></i>
        <%=string.Join(", ", Tags)%> 
    </span>






</div>
<%} %>
     <div class="row">
        <%if (!string.IsNullOrEmpty(dto.IMAGE) && this.GetValueParam<bool>("DisplayImage")){ %>
        <div class="col-sm-2 col-xs-4">
            <img class="article_image" src="<%=dto.PathImage%>" alt="<%=dto.TITLE%>" style="width: 100%"/>
        </div>
        <%} %>
        <div class="col-sm-10 col-xs-8">
<div><strong><%=dto.BRIEF%></strong></div>
    <ul class="article_content">
        <%foreach (var article in RelatiedArticles)
            { %>
        <li><a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+article.ID+"/"+ article.Title.ConvertToUnSign())%>" title="<%=article.Title %>"><%=article.Title %></a></li>
        <%} %>
    </ul>
            </div>
         </div>
<div style="text-align:left"><%=dto.CONTENT.PageArticleLink()%></div>
    
        
<VIT:Position runat="server" ID="psComment"></VIT:Position>
    </section>
