<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>

<div id="slideshow" style="margin-bottom:15px">
    <div id="carousel-slideshow" class="carousel slide" data-ride="carousel">
        <ol class="carousel-indicators">
            <%for(int i = 0; i< this.Data.Count;i++)
                {%>
            <li data-target="#carousel-slideshow" data-slide-to="<%=i %>" class="<%=i == 0 ? "active" : "" %>"></li>
             <%} %>	
        </ol>
        <div class="carousel-inner" role="listbox">
            <%for(int i = 0; i< this.Data.Count;i++)
                {%>
            <div class="item <%=i == 0 ? "active" : "" %>">
                <img src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>" style="width:100%;height:373px">






            </div>
            <%} %>	
        </div>
        <a class="left carousel-control" href="#carousel-slideshow" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#carousel-slideshow" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
</div>