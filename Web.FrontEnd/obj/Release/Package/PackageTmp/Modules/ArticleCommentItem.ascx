<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleCommentItem.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentItem" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>

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