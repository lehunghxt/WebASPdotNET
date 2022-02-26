<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookComment.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookComment" %>
<section class=" container py-lg-4 py-md-3 py-sm-3 py-3">
<div id="divBinhluan" class="share">






    <div id="fb-root"></div>
    <script src="http://connect.facebook.net/en_US/all.js#xfbml=1&appId=<%=this.FacebookAppId %>"></script>
    <fb:comments colorscheme="light" href="<%=YourUrl %>" num_posts="<%=NumberPost %>" width="<%=Width == 0 ? "100%" : Width + "px" %>"></fb:comments> 
</div>
    </section>