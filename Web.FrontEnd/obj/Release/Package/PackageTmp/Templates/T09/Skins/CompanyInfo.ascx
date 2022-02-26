<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CompanyInfo.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.CompanyInfo" %>
<%@ Import Namespace="VIT.Library" %>

<div class="widget span3 widget_nav_menu">
    <h3><%=this.Language["ContactUs"] %></h3>
    <div class="recent-post group">
        <div class="hentry-post group">
            <p style='display:<%=DisplayShortName ? "" : "none"%>' class="text without-thumbnail"><a class="title"><%=_dto.FullName%></a></p>
            <p style='display:<%=DisplayAddress ? "" : "none"%>'><%=Language["Address"]%>: <%=_dto.Address%></p>
            <p style='display:<%=DisplayPhone ? "" : "none"%>'><%=Language["Phone"]%>: <a href="tel:<%=this._dto.Phone %>"><%=_dto.Phone%></a></p>
            <p style='display:<%=DisplayFax ? "" : "none"%>'>Fax: <%=_dto.Fax%></p>
            <p style='display:<%=DisplayEmail ? "" : "none"%>'>Email: <a href="mailto:<%=this._dto.Email %>"><%=_dto.Email%></a></p>






        </div>
    </div>
</div> 