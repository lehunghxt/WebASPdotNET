<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<div id="carouselExampleIndicators" class="carousel slide pointer-event" data-ride="carousel">
            <ol class="carousel-indicators">
                <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                <li data-target="#carouselExampleIndicators" data-slide-to="<%=i %>" class="<%=i==0?"active":"" %>"></li>
            <%} %>	
            </ol>
            <div class="carousel-inner">
                <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                <div class="carousel-item <%=i==0?"active":"carousel-item-next" %> carousel-item-left">
                    <img class="d-block w-100" src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>">






                </div>
            <%} %>	
            </div>
            <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>

                

