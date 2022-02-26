<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<section id="user-notification" style="display:none">
    <h4><%=Title %></h4>






    <div class="mb-20"></div>
    <%foreach(var item in this.Data) 
  {%>      
                
    <div class="item mb-10 container-fluid">
        <div class="row">
            <div class="img-cont ml-10">
                <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                    <img src="<%=item.ImagePath%>" alt='<%=item.Title%>' height="96px">
                </a>
            </div>
            <div class="ml-10 noti-content">

                <p><%=item.Title%></p>
                <p class="small-grey"><%=item.Description.DeleteHTMLTag().Trim().Length > 120 ? item.Description.DeleteHTMLTag().Trim().Substring(0, 120) + "..." : item.Description.DeleteHTMLTag().Trim() %></p>
                <p class="small-grey"><%=string.Format("{0:dd/MM/yyyy}", item.CreateDate)%></p>

            </div>
        </div>
    </div>
    <%} %>

</section>