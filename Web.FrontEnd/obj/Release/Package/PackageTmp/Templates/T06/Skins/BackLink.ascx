<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>

<nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
            <%for (int i = 0 ; i< Categories.Count; i++)
                                        { %>
            <%if(i != Categories.Count - 1){ %>
                                    <li class="breadcrumb-item">
                                        <a href="<%=HREF.LinkComponent(GetComponentByType(Categories[i].TargetTag), "sCat/" + Categories[i].ID + "/" + Categories[i].Title.ConvertToUnSign()) %>"><%=Categories[i].Title %></a></li>
     <%} else if(string.IsNullOrEmpty(ItemTitle)) {%>
            <li class="breadcrumb-item active" aria-current="page"><%=Categories[i].Title %></li>
            <%} else { %>
            <li class="breadcrumb-item">
                                        <a href="<%=HREF.LinkComponent(GetComponentByType(Categories[i].TargetTag), "sCat/" + Categories[i].ID + "/" + Categories[i].Title.ConvertToUnSign()) %>"><%=Categories[i].Title %></a></li>
            <li class="breadcrumb-item active" aria-current="page"><%=this.ItemTitle %></li>
            <%} %>
            
            <%} %>
        </ol>
    </nav>


