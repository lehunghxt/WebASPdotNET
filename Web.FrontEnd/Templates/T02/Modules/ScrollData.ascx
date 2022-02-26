<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.DataSimple" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>







<script src="/Templates/T0155/js/jquery.imageslider.js"></script>
<style>
.my-slider {margin: 0 auto 10px;width: 100%;    padding: 10px; border: 1px solid #d2d2d2;background: #f7f5f5;}
.my-slider ul {overflow: hidden;list-style-type: none; margin: 0px;padding: 0px;width:3000px !important}
.my-slider li {float: left;padding-right:10px}
</style>      	


<div class="my-slider demo">
	<ul class="my-slider-list">
	<%for(int i = 0; i< this.Data.Count;i++)
    {%>
            <li class="my-slider-item">
                <a title="<%=this.Data[i].Title%>" href="<%=Data[i].URL%>">
			<img src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>"  Style="<%=Width > 0 ? "width:" + Width + "px" : ""%>;<%=Height > 0 ? "height:" + Height + "px" : ""%>"/>
		</a></li>
    <%} %>
		</ul>
</div>

<script>
    $(function () {
        $('.demo').imageslider({
            slideItems: '.my-slider-item',
            slideContainer: '.my-slider-list',
            slideDistance: 2,
            slideDuratin: 999,
            resizable: false,
            reverse: false,
            pause: true
        });
    });
</script>
