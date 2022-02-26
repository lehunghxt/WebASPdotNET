<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleBilingual.ascx.cs" Inherits="Web.FrontEnd.Modules.ArticleBilingual" %>
<%@ Import Namespace="Library" %>

<div id="article<%=this.Data.ID %>">
<h1 title="<%=Data.Title1 %>" class="text-center"><%=Data.Title1 %></h1>
     <%if (!string.IsNullOrEmpty(Data.ImagePath))
        { %>
    <div class="text-center">
        <img alt="<%=this.Data.Title1 %>" src="<%=Data.ImagePath %>" style="margin: 0px 20px 20px 20px; max-width:50%"/>
        </div>
    <%} %>
<div class="articlecontent1">
    <%=Data.Description1 %>
</div>
<button type="button" class="btn btn-secondary" onclick="$('#Translate<%=this.Data.ID %>').toggle(500);"><%=TextTranslate %></button>
<div class="jumbotron mt-3" id="Translate<%=this.Data.ID %>" style='display:none'>
      <%if (!string.IsNullOrEmpty(this.Data.Title2)){ %>
    <h3 title="<%=this.Data.Title2 %>"><%=this.Data.Title2 %></h3>
    <div class="articlecontent2"><%=this.Data.Description2 %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data.ID %>', 1)"><%=TextFixTranslationYet %></button>
	    </div>
      <%} else { %>
      <div><%=TextNoTranslationYet %></div>
        <div style="text-align:right; margin-top:20px">
	        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#sendTranslate" onclick="SetData('<%=this.Data.ID %>', 0)"><%=TextSendTranslationYet %></button>
	    </div>
	
      <%} %>
  </div>
<ol class="sty_date">
                    <li>
                        <svg class="svg-inline--fa fa-calendar-alt fa-w-14" aria-hidden="true" focusable="false" data-prefix="far" data-icon="calendar-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M148 288h-40c-6.6 0-12-5.4-12-12v-40c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v40c0 6.6-5.4 12-12 12zm108-12v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 96v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm-96 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm192 0v-40c0-6.6-5.4-12-12-12h-40c-6.6 0-12 5.4-12 12v40c0 6.6 5.4 12 12 12h40c6.6 0 12-5.4 12-12zm96-260v352c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h48V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h128V12c0-6.6 5.4-12 12-12h40c6.6 0 12 5.4 12 12v52h48c26.5 0 48 21.5 48 48zm-48 346V160H48v298c0 3.3 2.7 6 6 6h340c3.3 0 6-2.7 6-6z"></path></svg><!-- <i class="far fa-calendar-alt"></i> -->

                        Ngày đăng: <%=String.Format("{0:dd/MM/yyyy}", Data.CreateDate)%>
                    </li>
                    <li>
                        <svg class="svg-inline--fa fa-eye fa-w-18" aria-hidden="true" focusable="false" data-prefix="far" data-icon="eye" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg=""><path fill="currentColor" d="M288 144a110.94 110.94 0 0 0-31.24 5 55.4 55.4 0 0 1 7.24 27 56 56 0 0 1-56 56 55.4 55.4 0 0 1-27-7.24A111.71 111.71 0 1 0 288 144zm284.52 97.4C518.29 135.59 410.93 64 288 64S57.68 135.64 3.48 241.41a32.35 32.35 0 0 0 0 29.19C57.71 376.41 165.07 448 288 448s230.32-71.64 284.52-177.41a32.35 32.35 0 0 0 0-29.19zM288 400c-98.65 0-189.09-55-237.93-144C98.91 167 189.34 112 288 112s189.09 55 237.93 144C477.1 345 386.66 400 288 400z"></path></svg><!-- <i class="far fa-eye"></i> -->
                        Lượt xem: <%=Data.Views%>
                    </li>
    <li>
        <i class="glyphicon glyphicon-tags"></i> Tags: 
        <%=string.Join(", ", Tags)%> 
    </li>

    <li class="addthis_sharing_toolbox float-right" data-url="<%=HREF.BaseUrl + Data.Title1.ConvertToUnSign() + "-vit-sart-" + Data.ID%>-article" data-title="Dịch vụ" data-description="Dịch vụ" style="clear: both;"><div id="atstbx2" class="at-share-tbx-element addthis-smartlayers addthis-animated at4-show" aria-labelledby="at-7b81f5c5-87b8-481e-ba0c-8478d2133c28" role="region"><span id="at-7b81f5c5-87b8-481e-ba0c-8478d2133c28" class="at4-visually-hidden">AddThis Sharing Buttons</span><div class="at-share-btn-elements"><a role="button" tabindex="0" class="at-icon-wrapper at-share-btn at-svc-facebook" style="background-color: rgb(59, 89, 152); border-radius: 0%;"><span class="at4-visually-hidden">Share to Facebook</span><span class="at-icon-wrapper" style="line-height: 16px; height: 16px; width: 16px;"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 32 32" version="1.1" role="img" aria-labelledby="at-svg-facebook-1" style="width: 16px; height: 16px;" class="at-icon at-icon-facebook"><title id="at-svg-facebook-1">Facebook</title><g><path d="M22 5.16c-.406-.054-1.806-.16-3.43-.16-3.4 0-5.733 1.825-5.733 5.17v2.882H9v3.913h3.837V27h4.604V16.965h3.823l.587-3.913h-4.41v-2.5c0-1.123.347-1.903 2.198-1.903H22V5.16z" fill-rule="evenodd"></path></g></svg></span></a><a role="button" tabindex="0" class="at-icon-wrapper at-share-btn at-svc-twitter" style="background-color: rgb(29, 161, 242); border-radius: 0%;"><span class="at4-visually-hidden">Share to Twitter</span><span class="at-icon-wrapper" style="line-height: 16px; height: 16px; width: 16px;"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 32 32" version="1.1" role="img" aria-labelledby="at-svg-twitter-2" style="width: 16px; height: 16px;" class="at-icon at-icon-twitter"><title id="at-svg-twitter-2">Twitter</title><g><path d="M27.996 10.116c-.81.36-1.68.602-2.592.71a4.526 4.526 0 0 0 1.984-2.496 9.037 9.037 0 0 1-2.866 1.095 4.513 4.513 0 0 0-7.69 4.116 12.81 12.81 0 0 1-9.3-4.715 4.49 4.49 0 0 0-.612 2.27 4.51 4.51 0 0 0 2.008 3.755 4.495 4.495 0 0 1-2.044-.564v.057a4.515 4.515 0 0 0 3.62 4.425 4.52 4.52 0 0 1-2.04.077 4.517 4.517 0 0 0 4.217 3.134 9.055 9.055 0 0 1-5.604 1.93A9.18 9.18 0 0 1 6 23.85a12.773 12.773 0 0 0 6.918 2.027c8.3 0 12.84-6.876 12.84-12.84 0-.195-.005-.39-.014-.583a9.172 9.172 0 0 0 2.252-2.336" fill-rule="evenodd"></path></g></svg></span></a><a role="button" tabindex="0" class="at-icon-wrapper at-share-btn at-svc-print" style="background-color: rgb(115, 138, 141); border-radius: 0%;"><span class="at4-visually-hidden">Share to In</span><span class="at-icon-wrapper" style="line-height: 16px; height: 16px; width: 16px;"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 32 32" version="1.1" role="img" aria-labelledby="at-svg-print-3" style="width: 16px; height: 16px;" class="at-icon at-icon-print"><title id="at-svg-print-3">Print</title><g><path d="M24.67 10.62h-2.86V7.49H10.82v3.12H7.95c-.5 0-.9.4-.9.9v7.66h3.77v1.31L15 24.66h6.81v-5.44h3.77v-7.7c-.01-.5-.41-.9-.91-.9zM11.88 8.56h8.86v2.06h-8.86V8.56zm10.98 9.18h-1.05v-2.1h-1.06v7.96H16.4c-1.58 0-.82-3.74-.82-3.74s-3.65.89-3.69-.78v-3.43h-1.06v2.06H9.77v-3.58h13.09v3.61zm.75-4.91c-.4 0-.72-.32-.72-.72s.32-.72.72-.72c.4 0 .72.32.72.72s-.32.72-.72.72zm-4.12 2.96h-6.1v1.06h6.1v-1.06zm-6.11 3.15h6.1v-1.06h-6.1v1.06z"></path></g></svg></span></a><a role="button" tabindex="0" class="at-icon-wrapper at-share-btn at-svc-compact" style="background-color: rgb(255, 101, 80); border-radius: 0%;"><span class="at4-visually-hidden">Share to Thêm...</span><span class="at-icon-wrapper" style="line-height: 16px; height: 16px; width: 16px;"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 32 32" version="1.1" role="img" aria-labelledby="at-svg-addthis-4" style="width: 16px; height: 16px;" class="at-icon at-icon-addthis"><title id="at-svg-addthis-4">AddThis</title><g><path d="M18 14V8h-4v6H8v4h6v6h4v-6h6v-4h-6z" fill-rule="evenodd"></path></g></svg></span></a></div></div></li>
            
                </ol>
</div>
<div style="clear:both"></div>
<script src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-52493e8d684da4cc" async="" defer=""></script>