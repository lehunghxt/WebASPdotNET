<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAttribute.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttribute" %>

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
        <%=item.URL%> 
  <%} %>

                

