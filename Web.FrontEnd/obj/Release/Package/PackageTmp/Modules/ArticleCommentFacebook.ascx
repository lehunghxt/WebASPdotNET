<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<%= this.DisplayTitle%>
<%= this.DisplayDate%>
<%= this.DisplayImage%>

Id: <%=dto.Id%> <br/>
Title: <%=dto.Title%> <br/>
Tag: <%=dto.Tag%> <br/>
Brief: <%=dto.Brief%> <br/>
Contents: <%=dto.Contents%> <br/>
ImageName: <%=dto.ImageName%> <br/>
ImageThumdPath: <%=dto.ImageThumdPath%> <br/>
ImageDetailPath: <%=dto.ImageDetailPath%> <br/>
Time: <%=dto.Time%> <br/>

<VIT:Position runat="server" ID="psComment"></VIT:Position>