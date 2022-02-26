<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>

<%=this.Width%>
<%=this.Height%>

<%=this.RederectComponent%>
<%=this.RederectSendKey%>

<%foreach(var item in this.Data) 
  {%>
        <%=item.ID%>
        <%=item.CategoryId%>
        <%=item.Title%>
        <%=item.ImagePath%>
        <%=item.Description%> 
  <%} %>

                

