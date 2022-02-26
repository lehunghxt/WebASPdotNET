<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Document.ascx.cs" Inherits="Web.FrontEnd.Modules.Document" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>


<%= this.DisplayTitle%>
<%= this.DisplayImage%>

Id: <%=dto.ID%> <br/>
Title: <%=dto.TITLE%> <br/>
Brief: <%=dto.BRIEF%> <br/>
Contents: <%=dto.CONTENT%> <br/>
ImageName: <%=dto.IMAGE%> <br/>
ImagePath: <%=dto.PathImage%> <br/>
FileUrl: <%=dto.FileUrl%> <br/>
Size: <%=dto.Size%> <br/>