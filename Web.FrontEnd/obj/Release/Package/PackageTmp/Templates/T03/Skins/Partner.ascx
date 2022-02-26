﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimpleNoPage.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimpleNoPage" %>
<%@ Import Namespace="VIT.Library" %>
<%@ Import Namespace="VIT.Library.Web" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>

<li>
    Các đối tác lớn:
</li>
<li class="bigpartner">             
    <%foreach (var item in this.Data)
    {%>
        <a class="partner-" rel="nofollow" target="_blank" href="<%=item.URL%>">
            <img src="<%=item.ImagePath%>" style="opacity: 1; display: inline-block;" alt="<%=item.Title %>"/>
        </a>
    <%} %>
 </li>


