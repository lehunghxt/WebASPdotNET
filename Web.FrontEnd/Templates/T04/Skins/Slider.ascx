<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>


        <%for(int i = 0; i< this.Data.Count; i++) 
        {%>
         <a href="<%=this.Data[i].URL != null ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+ this.Data[i].Title.ConvertToUnSign())%> " title="<%=this.Data[i].Title%>">
                        <!-- hình 1920x400 5s đổi 1 lần -->
                        <img class="nature" <%= i> 0? "style='display: none' " : ""%> src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>">
                    </a>	
        <%} %>		






<script>
        w3.slideshow(".nature", 10000);
   </script>