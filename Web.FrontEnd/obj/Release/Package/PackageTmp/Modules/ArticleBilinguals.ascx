<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleBilinguals.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleBilinguals" %>

<%foreach(var item in this.Data) 
  {%>
        <%=item.ID%>
        <%=item.CategoryId%>
        <%=item.Title1%>
        <%=item.ImagePath%>
        <%=item.Description1%> 
  <%} %>

                

