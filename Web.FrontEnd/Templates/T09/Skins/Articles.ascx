<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticlesWithImageViews.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.ArticlesWithImageViews" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library" %>

<div id="content-blog" class="span9 content group">
<asp:UpdatePanel runat="server" ID="udpDataView">
    <ContentTemplate>
        <asp:ListView ID="ListView" runat="server">
            <ItemTemplate>
                <div class="group blog-small-image row">
                    <div class="span9">
                        <div class="row">

                            <div class="thumbnail span4">
                                <a title="<%#Eval("Title") %>" href="<%#HREF.LinkComponent("Article","Article","sArt/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                                    <img src="<%#Eval("ImagePathDetail")%>" alt="<%#Eval("Title") %>" title="<%#Eval("Title") %>" class="attachment-blog_small_image"></a>
                                <div class="readmore-wrapper">
                                    <a class="read-more alt-button" title="<%#Eval("Title") %>" href="<%#HREF.LinkComponent("Article","Article","sArt/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%=Language["Detail"] %></a>






                                </div>

                            </div> 
                            <!-- post content -->
                            <div class="the-content-list the-content  span5 group">
                                <div class="blog-small-image-content">
                                    <!-- post title -->
                                    <h2 class="post-title upper">
                                        <a title="<%#Eval("Title") %>" href="<%#HREF.LinkComponent("Article","Article","sArt/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%#Eval("Title") %></a>
                                    </h2>
                                    <div><%#Eval("Description")%></div>

                                    <div class="clear"></div>
                                    <div class="post-footer meta">
                                        <p>
                                            <span class="author"><%=this.Language["Posted"] %>:</span> <%#Eval("Time", "{0:dd/MM/yyyy}") %>
                                        </p>

                                        <p>
                                            <span class="author"><%=this.Language["Views"] %>:</span> <%#Eval("Views", "{0,0:d}") %>
                                        </p>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <VIT:Pager ID="pager" runat="server"/>
    </ContentTemplate>
</asp:UpdatePanel>
</div>


    
