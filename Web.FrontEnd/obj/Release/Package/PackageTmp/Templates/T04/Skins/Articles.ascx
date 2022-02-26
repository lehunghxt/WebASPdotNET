<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="products">
                                    <div class="pro_list_title">
                                        <%if (!string.IsNullOrEmpty(Search))
            { %>
        <span>Tìm thấy <%=this.Data.Count %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></span>
        <%}
    else
    { %>
		<h1 class="cate_title" title="<%=Title %>"><%=Title %></h1>
        <%} %>






                                    </div>
    <div style="margin-top:20px">
<div class="rw3-row-padding spham">
    <%foreach(var item in this.Data) 
  {%>  
                    <div class="w3-col m6 s6 i6">
                        <div class="w3-row" style="height:110px; overflow:hidden">
                            <a class="w3-col m3 s12 i12" href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                                <img src='<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>' alt='<%=item.Title%>' width="100%"/>
                            </a>
                            <div class="w3-col m9 s12 i12" style="padding:0px 20px 0px 10px">
                                <h2 title="<%=item.Title%>" class="article_title">
                                    <a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>"><%=item.Title%></a>
                                </h2>
                                    <p class="article_brief"><%=item.Description.DeleteHTMLTag().Trim().Length > 120 ? item.Description.DeleteHTMLTag().Trim().Substring(0, 120) + "..." : item.Description.DeleteHTMLTag().Trim() %></p>
                            </div>
                        </div>
                    </div>       
                <%} %>
</div>
    </div>
    </div>
