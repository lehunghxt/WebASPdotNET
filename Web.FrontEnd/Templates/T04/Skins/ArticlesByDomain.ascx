<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticlesByDomain.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticlesByDomain" %>

<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="cate_left_title" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
    <b style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></b>
</div>

    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
        <ul class="row module_colors">
        <%foreach(var item in this.Data) 
  {%>
             <li>
                        <a class="simpletitle" href="http://<%=Domain %><%=HREF.LinkComponent("Articles", "sart/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" target="_blank">
                        <i class="icons icon-right-dir"></i> <%=item.Title %></a>:
                 <div style="margin-top:10px">
                     <%=item.Description != null ? item.Description.Replace("\n","<br />") : item.Description%> 
                 </div>
                 <hr />
            </li>
        
  <%} %>
            </ul>
        </div>

                

