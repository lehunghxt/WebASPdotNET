<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library.Web"%>

<section class=" container py-lg-4 py-md-3 py-sm-3 py-3">
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
<div class="article_content"><%=dto.CONTENT != null ? dto.CONTENT.PageArticleLink() : dto.CONTENT%></div>
    
        
<VIT:Position runat="server" ID="psComment"></VIT:Position>
    </section>