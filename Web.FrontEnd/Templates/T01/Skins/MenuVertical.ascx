<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

 <ul class="ulmenu" style="background:#fff;margin:0;padding:0;">
    <%foreach (var model in this.Model)
    {%>
     <%foreach (var item in model.Childs)
    {%>
    <li class="limenu">
        <a class="a-menu" href="<%=CreateLink(item.Type, false, item.ID, item.CategoryName)%>" title="<%=item.CategoryName %>">
            <%if (!string.IsNullOrEmpty(item.CategoryImage)) {  %>
            <img src="<%=item.CategoryImage %>" alt="<%=item.CategoryName %>"/>
            <%} %>
            <span class="title"><%=item.CategoryName %></span>
        </a>
    <%if (item.Childs != null && item.Childs.Count > 0)
        {%>
        <div class="ddmn">
            <div class="w3-row ddmnd">
                <div class="w3-col" style="width:25%">






                <div><b>Su hướng hiện tại</b></div>
                <ul>
                    <%foreach (var dto in item.Childs)
                    {%>
                    <li><a href="<%=CreateLink(item.Type, true, dto.ID, dto.CategoryName)%>" title="<%=dto.CategoryName %>"><%=dto.CategoryName %></a></li>
                    <%} %>
                </ul>
              </div>
                <%for (int i = 0; i < item.Childs.Count && i < 3; i++)
                    {%>
                    <div class="w3-col" style="width:25%">
                        <a href="<%=CreateLink(item.Type, true, item.Childs[i].ID, item.Childs[i].CategoryName)%>" title="<%=item.Childs[i].CategoryName %>"><img src="<%=item.Childs[i].CategoryImage %>" alt="<%=item.Childs[i].CategoryName %>"/></a>
                    </div>
                    <%} %>
            </div>
        </div>
        <!-- .fly-menu -->
        <%} %> 
         </li>
    <%} %>
      <%} %>
</ul>
