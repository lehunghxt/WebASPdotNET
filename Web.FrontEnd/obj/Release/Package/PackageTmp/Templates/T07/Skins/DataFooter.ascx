<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>

<%if(Source == "ART") { %>
<div class="news_index">
    <div class="title_main">
        <h3><%=Title %></h3>






    </div>
    <div class="tintucsk">
        <div class="righttin">
            <div class="vert simply-scroll-container">
                <div class="simply-scroll-clip">
                    <div class="simply-scroll-list" style="height: 3336px;">
                        <div class="scroll_tin simply-scroll-list" style="height: 1668px;">
                             <%foreach(var item in this.Data) 
  {%>  
                            <div class="item_tin">
                                <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>">
                                    <div class="img_tin yktin transition">
                                        <img
                                            class="hover_opacity"
                                            src="<%=item.ImagePath %>"
                                            alt="<%=item.Title %>" />
                                    </div>
                                    <h4><%=item.Title %></h4>
                                </a>
                                <p class="destin">
                                    <%= string.IsNullOrEmpty(item.Description) ? string.Empty : item.Description.Replace("\n", "<br />") %>
                                </p>
                            </div>
                <%} %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%} %>