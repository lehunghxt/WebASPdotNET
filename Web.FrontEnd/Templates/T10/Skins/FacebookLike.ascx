<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/FacebookLike.ascx.cs" Inherits="Web.FrontEnd.Modules.FacebookLike" %>
<style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>

<script id="facebook-jssdk" src="//connect.facebook.net/vi_VN/all.js#xfbml=1"></script>
<div class="fb-like<%=Box?"-box" : "" %>" data-href="<%= YourUrl%>" <%= Width == 0 ? "" : "data-width='"+Width+"'"%> data-show-faces="<%= ShowFaces%>" data-border-color="<%= BorderColor%>" data-stream="<%= Stream%>" data-header="<%= Header%>"></div>


<%--<script language="javascript" type="text/jscript">
    $(document).ready(function () {
        $('iframe').load(function () {
        $('iframe').contents().find("head")
      .append($("<style type='text/css'>  .pluginSkinLight {border:0 !important}; </style>"));
    });
});
</script>--%>


<link href="https://adpia.vn/css/adpia_banner_style.css" rel="stylesheet" type="text/css"/>

<div class="adpia_banner" style="width:100%; margin-top:10px">
<a href="https://adpia.vn/" class="adpia_link"><span class="adpia_bg_hide">Ads by Adpia</span></a>
<a href="https://click.adpia.vn/tracking.php?m=earlystart&a=A100047119&l=0000"><img src="https://ac.adpia.vn/upload/images/monkey_300x300.jpg" border="0" width="100%"></a>
<img src="https://img.adpia.vn/apshow.php?m_id=earlystart&a_id=A100047119&p_id=0000&l_id=0001&l_cd1=G&l_cd2=2" width="1" height="1" border="0" nosave style="display:none">
</div>

<div class="adpia_banner" style="width:100%; margin-top:10px">
<a href="https://adpia.vn/" class="adpia_link"><span class="adpia_bg_hide">Ads by Adpia</span></a>
<a href="https://tinyurl.com/yhxx54sc"><img src="/Templates/T10/Images/unica.jpg" border="0" width="100%"></a>
<img src="https://tinyurl.com/yhxx54sc" width="1" height="1" border="0" nosave style="display:none">
</div>
		
<div class="adpia_banner" style="width:100%; margin-top:10px">
<a href="https://adpia.vn/" class="adpia_link"><span class="adpia_bg_hide">Ads by Adpia</span></a>
<a href="https://click.adpia.vn/click.php?m=smis&a=A100047119&l=0002&l_cd2=2"><img src="https://s3-ap-southeast-1.amazonaws.com/storage.adpia.vn/affiliate_document/multi/300x300-smis.jpg" border="0" width="100%"></a>
<img src="https://img.adpia.vn/apshow.php?m_id=smis&a_id=A100047119&p_id=0000&l_id=0002&l_cd1=G&l_cd2=2" width="1" height="1" border="0" nosave style="display:none">
</div>	