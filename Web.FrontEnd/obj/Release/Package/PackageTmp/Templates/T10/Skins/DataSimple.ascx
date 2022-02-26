<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if(Source == "ATG"){ %>
<div class="tags">
<h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>
	<ul class="tags_links">
        <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
            {%>
        <li class="gentag"><a href="/articles/vit/<%=SettingsManager.Constants.SendTag%>/<%=item.Title.Trim().ConvertToUnSign()%>"><%=item.Title %></a></li>
            <%} %>	
    </ul>
    <div class="clear"></div>
</div>
<%} else if(Source == "ART"){%>
<div class="articles">
    <%if (this.ColumnCount == 1)
        {%>
<h3 class="tag_head" style="margin:15px 0px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>">
    <a href="/san-hang-khuyen-mai-vit-contact" title="<%=Title %>"><%=Title %></a>
</h3>
	<div class="news_list">
        <%foreach (var item in this.Data)
    {%>
        <div style="margin-bottom:10px">
            <a href="/<%=item.Title.Trim().ConvertToUnSign()%>-vit-<%=SettingsManager.Constants.SendArticle%>-<%=item.ID%>-news">
                <img src="<%=item.ImagePath %>" alt="<%=item.Title %>" style="width:100%;float:left; margin-bottom:5px"/>
            </a>
            <a href="/<%=item.Title.Trim().ConvertToUnSign()%>-vit-<%=SettingsManager.Constants.SendArticle%>-<%=item.ID%>-news"><%=item.Title %></a>
        </div>
            <%} %>	
    </div>
    <%} else {%>
    <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
    <p class="jumbotron" style="padding:20px">
        <%=Category.Description%>
    </p>

    <div class="row">
    <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
        {%>
    <div style="margin-bottom: 30px;" class="document col-lg-<%=12 / ColumnCount %> col-md-<%=12 / ColumnCount %> col-sm-<%=12 / ColumnCount %> col-xs-12" >
        <div style="margin-bottom:10px">
            <a href="/<%=item.Title.Trim().ConvertToUnSign()%>-vit-<%=SettingsManager.Constants.SendArticle%>-<%=item.ID%>-news">
                <img src="<%=item.ImagePath %>" alt="<%=item.Title %>" style="width:100%;float:left; margin-bottom:5px;max-height: <%=Height%>px"/>
            </a>
            <a href="/<%=item.Title.Trim().ConvertToUnSign()%>-vit-<%=SettingsManager.Constants.SendArticle%>-<%=item.ID%>-news"><%=item.Title %></a>
        </div>
    </div>
    <%} %>
	
	<div style="margin-bottom: 30px;" class="document col-lg-<%=12 / ColumnCount %> col-md-<%=12 / ColumnCount %> col-sm-<%=12 / ColumnCount %> col-xs-12" >
            <style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>
        <script src="https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&amp;version=v2.5"></script>
<div class="fb-page" data-href="https://www.facebook.com/SanHangKhuyenMai.Top" data-width="" data-show-facepile="true" data-show-faces="true" data-stream="" data-header="true"></div>
    </div>
	
        </div>
		<div class="adpia_minishop" aff-id=A100047119 top-slide="true" under-slide="true" flash-sale="true" discount-code="true" hot-deal="true" cpo-offer="true"></div>
<div class="adpia_coupon_list" shopee="true" lazada="true" klook="true" fahasa="true"></div>
<div class="adpia_categories_list" data-list="[DT,TBDT,MT,CMR,HB,FA,HL,GD,MB,TG,SK,BS,SP,DL]"></div>
    <%} %>	
</div>
<%} else if(Source == "DOC"){%>
<div class="documents">
    <%if (this.ColumnCount > 1)
        {%>
    <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
    <p class="jumbotron" style="padding:20px">
        <%=Category.Description%>
        <br />
        <strong>Xem thêm hướng dẫn tải file tại:</strong> <a href="https://truyencuoi.top/huong-dan-tai-file-tai-link-rut-gon-link1scom-vit-news" title="Hướng dẫn tải file" target="_blank">https://truyencuoi.top/huong-dan-tai-file-tai-link-rut-gon-link1scom-vit-news</a>
    </p>

    <div class="row">
    <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
        {%>
    <div style="max-height:<%=Height%>px;overflow: hidden;margin-bottom: 30px;" class="document col-lg-<%=12 / ColumnCount %> col-md-<%=12 / ColumnCount %> col-sm-<%=12 / ColumnCount %> col-xs-12" >
        <div class="row">
            <div class="col-lg-5 col-md-5 col-sm-5 hidden-xs-down">
                <a href="<%=HREF.LinkComponent("Document", SettingsManager.Constants.SendDocument + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title %>">
                    <img src="<%=item.ImagePath %>" alt="<%=item.Title %>" style="width:100%;max-height:100%"/>
                </a>
            </div>
            <div class="col-md-7 col-sm-12 col-xs-12">
                <h5><a href="<%=HREF.LinkComponent("Document", SettingsManager.Constants.SendDocument + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title %>"><%=item.Title%></a></h5>
                <%=item.Description.Length > 150 ? item.Description.Substring(0, 150) + "..." : item.Description%>
            </div>
        </div>
    </div>
    <%} %>
	<div style="max-height:<%=Height%>px;overflow: hidden;margin-bottom: 30px;" class="document col-lg-<%=12 / ColumnCount %> col-md-<%=12 / ColumnCount %> col-sm-<%=12 / ColumnCount %> col-xs-12" >
            <style>.fb_iframe_widget, .fb_iframe_widget span, .fb_iframe_widget span iframe[style] 
{
    width: 100% !important;
}</style>
        <script src="https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&amp;version=v2.5"></script>
<div class="fb-like-box" data-href="https://www.facebook.com/Sach.TaiLieu.TengAnh" data-width="" data-show-facepile="true" data-show-faces="true" data-stream="" data-header="true"></div>
    </div>
        </div>
            <%}
                else
                {%>
    <h3 class="tag_head" style="margin-bottom:15px; <%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>">
        <a href="/Tai-lieu-tieng-Anh" title="<%=Title %>"><%=Title %></a>
    </h3>
         <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
        {%>
    <div style="margin-bottom: 10px;" >
                <a href="<%=HREF.LinkComponent("Document", SettingsManager.Constants.SendDocument + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title %>">
                    <img src="<%=item.ImagePath %>" alt="<%=item.Title %>" style="width:30%;float:left; margin:0px 10px 10px 0px"/>
                </a>

                <h5><a style="font-size:14px" href="<%=HREF.LinkComponent("Document", SettingsManager.Constants.SendDocument + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>" title="<%=item.Title %>"><%=item.Title%></a></h5>
    <div class="clear" style="clear:both"></div>    
    </div>
    
    <%} %>
        <%} %>	
    </div>
<%} else {%>
<%if (this.ColumnCount > 1)
        {%>
    <h1 title="<%=Title%>"><strong> <%=Title%></strong></h1>
    <p class="jumbotron" style="padding:20px"><%=Category.Description%></p>
    <%} %>
    <%foreach (var item in this.Data.OrderBy(e => e.Title.Length))
            {%>
    <div class="videoitem">
        <%=item.Description %>
        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>"><%=item.Title %></a>
    </div>
            <%} %>	
    <div style="clear:both"></div>
<%} %>
