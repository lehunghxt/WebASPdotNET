<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

 <div class="col-md-3">
  <b><%=Title %></b>
   <ul>  
  <%foreach(var item in this.Data) 
  {%>  	
	<li>
        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%> " title="<%=item.Title%>">
            <%=item.Title%>
        </a>
	</li>	       
  <%} %>
</ul>






 </div>


                

