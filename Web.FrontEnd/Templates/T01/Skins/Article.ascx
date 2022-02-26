<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library.Web"%>

<h1 class="link-2" title="<%=dto.TITLE%>" style='display:<%=DisplayTitle ? "" : "none"%>'><%=dto.TITLE%></h1>
<div class="post-header">
    <span class="post-timestamp">
        <i class="fa fa-clock-o"></i><%=String.Format("{0:dd/MM/yyyy}", dto.DISPLAYDATE)%>
    </span>
    <span class="post-comment-link">
        <i class="fa fa-eye"></i> <%=dto.Views%>
    </span>
    <span class="post-labels tag" style='display:<%=DisplayTag ? "" : "none"%>'>
        <i class="fa fa-tags"></i>
        <%=string.Join(", ", Tags)%>
    </span>






</div>

<div class="article_content"><%=dto.CONTENT.PageArticleLink()%></div>

<VIT:Position runat="server" ID="psComment"></VIT:Position>