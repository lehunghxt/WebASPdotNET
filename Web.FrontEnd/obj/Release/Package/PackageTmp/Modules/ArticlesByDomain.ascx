<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticlesByDomain.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticlesByDomain" %>


<%foreach(var item in this.Data) 
  {%>
        <%=item.ID%>
        <%=item.CategoryId%>
        <%=item.Title%>
        <%=item.ImagePath%>
        <%=item.Description%> 
  <%} %>

                

