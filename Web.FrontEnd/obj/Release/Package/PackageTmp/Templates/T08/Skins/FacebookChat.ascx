<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookChat.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookChat" %>







<div id="fb-root"></div>
  <script>(function(d, s, id) {
  var js, fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) return;
  js = d.createElement(s); js.id = id;
  js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.5";
  fjs.parentNode.insertBefore(js, fjs);
  }(document, 'script', 'facebook-jssdk'));</script>
<style>
    
    #cfacebook {
        position: fixed;
        bottom: 0px;
        right: 0px;
        z-index: 999999999999999;
        width: <%= Width%>px;
        height: auto;
        box-shadow: 6px 6px 6px 10px rgba(0,0,0,0.2);
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
        overflow: hidden;
        box-shadow:none !important;padding-top: 0px !important;padding-left: 0px !important
    }

        #cfacebook .fchat {
            float: left;
            overflow: hidden;
            display: none;
            background-color: #fff;
            padding: 0px !important;
        }

            #cfacebook .fchat .fb-page {
                padding: 0px !important;
            }

        #cfacebook a.chat_fb {
            float: left;
            padding: 0 25px;
            width: <%= Width - 50%>px;
            color: #fff;
            text-decoration: none;
            height: 40px;
            line-height: 40px;
            text-shadow: 0 1px 0 rgba(0,0,0,0.1);
            background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAqCAMAAABFoMFOAAAAWlBMV…8/UxBxQDQuFwlpqgBZBq6+P+unVY1GnDgwqbD2zGz5e1lBdwvGGPE6OgAAAABJRU5ErkJggg==);
            background-repeat: repeat-x;
            background-size: auto;
            background-position: 0 0;
            background-color: #3a5795;
            border: 0;
            border-bottom: 1px solid #133783;
            z-index: 9999999;
            margin-right: 12px;
            font-size: 18px;
        }

            #cfacebook a.chat_fb:hover {
                color: yellow;
                text-decoration: none;
            }
</style>
<script>
  jQuery(document).ready(function () {
  jQuery(".chat_fb").click(function() {
jQuery('.fchat').toggle('slow');
  });
  });
  </script>
  <div id="cfacebook">
  <a href="javascript:;" class="chat_fb" onclick="return:false;"><i class="fa fa-facebook-square"></i> <%=Title %></a>
  <div class="fchat">
  <div class="fb-page" data-tabs="messages" data-href="<%=YourUrl%>" data-width="<%= Width - 50%>" data-height="300" 
      data-small-header="<%= Header%>" data-adapt-container-width="true" data-hide-cover="false" 
      data-show-facepile="<%=ShowFacepile %>" data-show-posts="<%=ShowPost %>"></div>
  </div>
  </div>