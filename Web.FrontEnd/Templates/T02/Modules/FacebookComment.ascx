<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookComment.ascx.cs" Inherits="VIT.Web.Modules.FacebookComment" %>
<div id="divBinhluan" class="share">






    <div id="fb-root"></div>
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1&appId=359358737465531"></script>
    <fb:comments colorscheme="light" href="<%=YourUrl %>" num_posts="<%=NumberPost %>" width="<%=Width> 0 ? Width + "px": "100%" %>"></fb:comments> 
</div>