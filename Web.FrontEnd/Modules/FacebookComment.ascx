<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookComment.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookComment" %>
<div id="divBinhluan" class="share">
    <div id="fb-root"></div>
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1&appId=<%=this.FacebookAppId %>"></script>
    <fb:comments colorscheme="light" href="<%=YourUrl %>" num_posts="<%=NumberPost %>" width="<%=Width %>"></fb:comments> 
</div>