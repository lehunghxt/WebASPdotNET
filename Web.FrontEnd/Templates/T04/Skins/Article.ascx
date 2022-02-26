<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleCommentFacebook.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleCommentFacebook" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library.Web"%>

<div class="products">
    <div class="news_title">
         <%if (this.DisplayTitle)
                            {%>
					<h1 class="info_title"><%=Title %></h1>
					<div class="info_txt">
						<span class="info_txt_item">Browse: <%=dto.Views%></span>
                        <%if (this.DisplayDate)
                            {%>
						<span class="separator">|</span><span class="info_txt_item">Update: <%=String.Format("{0:dd/MM/yyyy}", dto.DISPLAYDATE)%></span>
						<%} %>
                        <%if (this.DisplayTag)
                            {%>
                        <span class="separator">|</span><span class="info_txt_item">Tags: <%=string.Join(", ", Tags)%></span>
                        <%} %>






					</div>
        <div class="w3-row" style="margin:10px 0px">
					<div class="w3-col l4 s5" style="padding-left:10px">
						<img src="<%=dto.PathImage %>" alt="<%=Title %>">
					</div>        
        <div class="w3-col l8 s12">
            <b>
						<%=dto.BRIEF %></b>
					</div>
				</div>
        <%} %>
        </div>
                                <div class="news_des" style="width:100%">
                                    <%=dto.CONTENT %>
                                </div>

    <VIT:Position runat="server" ID="psComment"></VIT:Position>
</div>


