<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<%if(!string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")))
{%>
<style> 
    #carts{background:<%=this.GetValueParam<string>("BackgroundColor")%>;}
    #main-navigation>ul>li{background:<%=this.GetValueParam<string>("BackgroundColor")%>;}
    #main-navigation>ul>li>a{background:<%=this.GetValueParam<string>("BackgroundColor")%>;}
    #main-navigation li ul.normal-dropdown li{background:<%=this.GetValueParam<string>("BackgroundColor")%>;}	
    #main-navigation .nav-search{background:<%=this.GetValueParam<string>("BackgroundColor")%>;}
    #main-navigation li ul.normal-dropdown li:hover>a{background:#34495e;}
</style>
<style>
    li.orange,li.orange>a, input.orange, .button.orange, #main-navigation li.orange li, span.product-action.orange, .banner-item.light-blue{background:<%=this.GetValueParam<string>("BackgroundColor")%> !important;}
</style>
<%} %>

<%if(!string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")))
{%>
<style>
    #main-navigation ul li a{color:<%=this.GetValueParam<string>("FontColor")%>;}
</style>
<%} %>

<section>
  <nav role="navigation" class="f_left f_xs_none d_xs_none">
    <div class="mobile-nav">      
      <button type="button" class="btn" onclick="ToggleSideBar();"><i class="fa fa-bars"></i></button>
      <div class="contenedor-menu hide" >
		<ul class="menu">
            <li><a href="/" title="Trang chủ">Trang chủ</a></li>
            <li><a href="<%=HREF.LinkComponent("AboutUs")%>">Giới thiệu</a></li>
            <%foreach (var cat in this.Model)
                {%>
			<li>
                <%--<a href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>"><i class="fa fa-chevron-down"></i>--%>
                <a href="#" class="dropbtn">
                <%=cat.CategoryName%>
            </a>
                 <%if (cat.Items != null && cat.Items.Count > 0)
            {%>
				<ul>
                    <%foreach (var item in cat.Items)
                    {%>
                        <li><a href="<%=!string.IsNullOrEmpty(item.URL)?item.URL : CreateLink(cat.Type, true, item.ID, item.Title)%>"><%=item.Title%></a></li>
                    <%} %>
				</ul>
                <%} else if (cat.Childs != null && cat.Childs.Count > 0)
            {%>
                <ul>
                    <%foreach (var dto in cat.Childs)
                    {%>
                    <li><a href="<%=CreateLink(cat.Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a></li>
                    <%} %>
                </ul>
                <%} %>
			</li>
            <%} %>
            <li><a href="<%=HREF.LinkComponent("Contact")%>">Liên hệ</a></li>
		</ul>
	</div>
    </div>
    <div class="container web-nav">
    <ul class="row" style="display:flex">
        <li class=""><div class="dropdown"><a href="/" title="Trang chủ" class="dropbtn">Trang chủ</a></div></li>
        <li class=""><div class="dropdown"><a href="<%=HREF.LinkComponent("AboutUs")%>" class="dropbtn">Giới thiệu</a></div></li>
      <%foreach (var cat in this.Model)
                {%>
			<li class=""><div class="dropdown">
                <%--<a href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>" class="dropbtn">--%>
                <a href="#" class="dropbtn">
                <%=cat.CategoryName%>
            </a>
                 <%if (cat.Items != null && cat.Items.Count > 0)
            {%>
				<div class="dropdown-content top_arrow">
                    <%foreach (var item in cat.Items)
                    {%>
                        <a href="<%=!string.IsNullOrEmpty(item.URL)?item.URL : CreateLink(cat.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                    <%} %>
				</div>
                <%} else if (cat.Childs != null && cat.Childs.Count > 0)
            {%>
                <div class="dropdown-content top_arrow">
                    <%foreach (var dto in cat.Childs)
                    {%>
                    <a href="<%=CreateLink(cat.Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a>
                    <%} %>
                </div>
                <%} %>
                </div>
			</li>
            <%} %>
			<li><div class="dropdown"><a href="<%=HREF.LinkComponent("Contact")%>" class="dropbtn">Liên hệ</a></div></li>
      </ul>
      
    </div>
</nav>
</section>



 <script>
    var availableTags = [];
    $("#lookup").autocomplete({
      source: availableTags,
    });
  
    $(document).ready(function() {
	


  $('.dropbtn').click(function(){
    var check = $(this).parent().find('.dropdown-content').hasClass("show-menu");
    if(check==false){
      $('.dropdown-content').removeClass("show-menu");
      $(this).parent().find('.dropdown-content').toggleClass("show-menu");
    }else
    {
      $(this).parent().find('.dropdown-content').removeClass("show-menu");
    }

   
   
  });
  $('.dropbtn').off("click",function(){
    $('.dropdown-content').removeClass("show-menu");
  });

});
function ToggleSideBar(){
$(".contenedor-menu").toggleClass("hide", "show-mobile-nav");
}
  </script>