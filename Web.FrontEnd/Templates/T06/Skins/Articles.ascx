<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Articles.ascx.cs" Inherits="Web.FrontEnd.Modules.Articles" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Library.Web" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<section class="blog_w3ls py-5">
        <div class="container py-lg-5">
            <div class="text-center wthree-title pb-5">
                 <%if (!string.IsNullOrEmpty(Search))
            { %>
        <h4>Tìm thấy <%=this.Data.Count %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></h4>
        <%}
    else
    { %>
            <h1 class="w3l-sub pb-5" title="<%=Title %>"><%=Title %></h1>
                <p class="mx-auto"><%=Category.Description %></p>
        <%} %>






            </div>
            <div class="row py-sm-5">
                <%foreach(var item in this.Data) 
  {%>  
                <div class="col-lg-4 col-md-6 mb-5">
                    <div class="card border-0">
                        <div class="card-header p-0">
                            <a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>">
                                <img class="card-img-bottom" src='<%=!string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath : "/templates/t02/img/no-image-available.png"%>' alt='<%=item.Title%>'/>
                            </a>
                        </div>
                        <div class="card-body p-0 border-0">
                            <div class="blog_w3icon d-flex justify-content-between">
                                <span>
                                    tag: <%=item.TargetTag.Split(',')[0] %></span>
                                <span>
                                    <%=string.Format("{0:dd/MM/yyyy}", item.CreateDate) %></span>
                            </div>
                            <div class="pt-2">
                                <h5 class="blog-title card-title font-weight-bold">
                                    <a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>"><%=item.Title%></a>
                                </h5>
                                <p><%=item.Description.DeleteHTMLTag().Trim().Length > 120 ? item.Description.DeleteHTMLTag().Trim().Substring(0, 120) + "..." : item.Description.DeleteHTMLTag().Trim() %></p>
                            </div>
                            <a href="<%=HREF.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%>" class="blog-btn mt-2 d-inline-block">Đọc tiếp</a>
                        </div>
                    </div>
                </div>     
                <%} %>
            </div>
        </div>
    </section>
