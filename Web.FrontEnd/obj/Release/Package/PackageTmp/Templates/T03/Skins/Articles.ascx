<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %>">
             <%if (!string.IsNullOrEmpty(Search))
            { %>
        <span style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>">Tìm thấy <%=this.Data.Count %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></span>
        <%}
    else
    { %>
            <h1 title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h1>
        <%} %>
			






		</div>
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
<div class="row module_colors">
    <%foreach(var item in this.Data) 
  {%>  
                    <div class="col-sm-6 col-xs-12 lstArticle">
                        <div class="row">
                            <a class="col-xs-3" href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>">
                                <img src='<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>' alt='<%=item.Title%>' width="100%"/>
                            </a>
                            <div class="col-xs-9">
                                <h2 title="<%=item.Title%>">
                                    <a class="article_title" href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" title="<%=item.Title%>"><%=item.Title%></a>
                                </h2>
                                    <p class="article_brief"><%=item.Description.DeleteHTMLTag().Trim().Length > 120 ? item.Description.DeleteHTMLTag().Trim().Substring(0, 120) + "..." : item.Description.DeleteHTMLTag().Trim() %></p>
                            </div>
                        </div>
                    </div>       
                <%} %>
</div>
    </div>
    </div>
