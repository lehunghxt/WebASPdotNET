<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Source == "LIN"){ %>
<section>
  <div id="myCarousel" class="carousel slide hidden-xs hidden-sm" data-ride="carousel">
    <div class="carousel-inner ">
        <%for (int i = 0; i < this.Data.Count; i++)
            {%>
        <div class="item <%= i == 0 ? "active" : "" %>">
            <a href="<%=(this.Data[i].URL != null && this.Data[i].URL.Length > 0) ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+this.Data[i].Title.ConvertToUnSign())%>">
        <img class="mx-auto" src="<%=this.Data[i].ImagePath %>" alt="<%=this.Data[i].Title %>" />
                </a>
      </div>
            <%} %>	
    </div>
    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
      <span class="glyphicon glyphicon-chevron-left"></span>
      <span class="sr-only">Previous</span>
    </a>
    <a class="right carousel-control" href="#myCarousel" data-slide="next">
      <span class="glyphicon glyphicon-chevron-right"></span>
      <span class="sr-only">Next</span>
    </a>
  </div>
</section>
<%} else { %>
<div >
            <div class="header_menu_right">
              <h3 style="color: white;font-size: 1.4em;"><%=Title %></h3>
            </div>
            <ul class="sub_menu_right">
                <%for (int i = 0; i < this.Data.Count; i++)
            {%>
              <li>
                  <h4 title="<%=this.Data[i].Title %>"><a title="<%=this.Data[i].Title %>" href="<%=(this.Data[i].URL != null && this.Data[i].URL.Length > 0) ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+this.Data[i].Title.ConvertToUnSign())%>">
       <%=this.Data[i].Title %>
                </a></h4>
              </li>
                <%} %>
            </ul>
          </div>
<%} %>