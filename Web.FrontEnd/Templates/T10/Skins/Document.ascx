<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Document.ascx.cs" Inherits="Web.FrontEnd.Modules.Document" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<%if(this.DisplayTitle) {%>
<h1 title="<%=dto.TITLE%>"><%=dto.TITLE%></h1>
<%} %>

<div class="row">
    <div class="col-md-3 hidden-4 hidden-xs-down">
        <img src="<%=dto.PathImage%>" alt="<%=dto.TITLE%>" style="width:100%"/>
    </div>
    <div class="col-md-9 hidden-8 hidden-xs-12">
        <table class="table table-success table-striped" style="width:100%;text-align:left">
            <tr>
                <td style="width:150px">Lượt xem</td>
                <td><%=dto.Views %></td>
            </tr>
            <tr>
                <td>Định dạng</td>
                <td><%=dto.Type %></td>
            </tr>
            <tr>
                <td>Dung lượng</td>
                <td><%=(dto.Size/1024).ToString("0,##") %> kb</td>
            </tr>
        </table>
        <strong><%=dto.BRIEF%></strong>
    </div>
    
</div>


<%if(this.Config.Language.ToLower() == "en-us") { %>
<a href="https://link1s.com/ref/104476169309621930641" target="_blank"><img src="//link1s.com/img/refbanner/728x90-en.png" title="Shorten URLs and EARN money" style="width:100%; margin-top:20px"/></a>
<%} else { %>
<a href="https://link1s.com/ref/104476169309621930641" target="_blank"><img src="//link1s.com/img/refbanner/728x90.png" title="Rút gọn link kiếm tiền uy tín số 1 Việt Nam" style="width:100%; margin-top:20px"/></a>
<%} %>

<div style="margin-top:20px"><%=dto.CONTENT%></div>