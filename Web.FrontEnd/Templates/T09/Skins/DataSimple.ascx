<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<ul class="<%=RederectComponent%>">
    <%foreach(var item in this.Data) 
  {%>
                        <li>
                            <a class="simpleimg" href="<%=(item.URL != null && item.URL.Length > 0) ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                                <img src='<%=item.ImagePath%>' alt='<%=item.Title%>' style='<%#(Eval("ImagePath") != null) && Eval("ImagePath").ToString().Length > 0 ? "" : "display:none"%><%=Width > 0 ? "Width:" + Width + "px;": ""%><%=Height > 0 ? "Height:" + Height + "px;": ""%>'/>
                            </a>
                            <a class="simpletitle" href="<%=(item.URL != null && item.URL.Length > 0) ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+item.Title.ConvertToUnSign())%>" title="<%=item.Title%>"><%=item.Title%></a>
                            <span class="simpledes"><%=item.Description%></span>






                            <div class="clear"></div>
                        </li>
                <%} %>
</ul>