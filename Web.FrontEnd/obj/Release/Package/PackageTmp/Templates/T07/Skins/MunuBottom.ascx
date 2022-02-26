<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>







<div class="t_foot_2"><%=Title %></div>

<div class="m_foot_2">

                        <ol class="ol2_foot_2">
                            <li>
                                <a href="/">Trang chủ</a>
                            </li>
                            <%foreach (var parent in this.Model)
                             {%>
     <%if (parent != null)
         {%>
                            <li>
                                <a title="<%=parent.CategoryName%>" href="<%=CreateLink(parent.Type, false, parent.ID, parent.CategoryName)%>"><%=parent.CategoryName%></a>
                            </li>
    <%} %>
                         <%} %>

                            <li>
                                <a href="<%=HREF.LinkComponent("Contact")%>"><%=Language["Contact"] %></a>
                            </li>

                        </ol>

                        <br>

                        <a href="#">

                            <img src="imgs/layout/bct.png" alt="" height="50">

                        </a>

                    </div>
