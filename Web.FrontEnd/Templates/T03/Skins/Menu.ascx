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

<nav id="main-navigation" class="style1">
    <ul class="l_tinynav1">
        <li>
            <a href="/" style="height: 56px;">
                <span class="nav-caption">Trang chủ</span>
            </a>
        </li>
        <%foreach (var cat in this.Model)
        {%>
        <li class="dropdown" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : "color:" + this.GetValueParam<string>("FontColor") %>">
            <a href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>" style="height: 56px;">
                <span class="nav-caption"><%=cat.CategoryName%></span>
            </a>
            <%if (cat.Items != null && cat.Items.Count > 0)
            {%>
            <ul class="normal-dropdown" style="display: none; opacity: 0; top: 150%;">
                <%foreach (var item in cat.Items)
                {%>
                    <li><a href="<%=!string.IsNullOrEmpty(item.URL)?item.URL : CreateLink(cat.Type, true, item.ID, item.Title)%>"><%=item.Title%></a></li>
                <%} %>
                </ul>
            <%} else if (cat.Childs != null && cat.Childs.Count > 0)
            {%>
            <ul class="normal-dropdown" style="display: none; opacity: 0; top: 150%;">
                <%foreach (var dto in cat.Childs)
                {%>
                    <%if (dto.Items != null && dto.Items.Count > 0)
                    {%>
                    <li><a href="<%=CreateLink(cat.Type, true, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%><i class="icons icon-right-dir"></i></a>
                    <ul class="" style="display: none;">
                        <%var count = dto.Items.Count(); int stt = 0; %>
                        <%foreach (var item in dto.Items)
                                                        {%>
                        <li><a href="<%=item.URL%>"><%=item.Title%></a>
                            <% stt++; if(count/2 == stt) { %>
                        </li>
                        <%} %>
                        <%} %>
                    </ul>
                    <%} else {%>
                        <li><a href="<%=CreateLink(cat.Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a>
                    <%} %>
                </li>
                <%} %>
            </ul>
            <%}%>

            
        </li>
        <%} %>

        <li>
            <a href="<%=HREF.LinkComponent("Contact")%>" style='height: 56px;'>
                <span class="nav-caption">Liên hệ</span>
            </a>
        </li>

        <li class="nav-search">
            <i class="icons icon-search-1"></i>
        </li>

    </ul>

    <div id="search-bar">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <table id="search-bar-table">
                <tbody>
                    <tr>
                        <td class="search-column-1">
                            <input id="search" type="text" onkeypress="return runScript(event) name="" class="topinput" placeholder="Nhập từ khóa cần tìm...">
                        </td>
                        <%--<td class="search-column-2">
                            <select class="chosen-select-search" style="display: none;" name="find">
                                <option value="PRO" selected>Sản phẩm</option>
                                <option value="ART">Bài viết</option>
                            </select>
                        </td>--%>
                    </tr>
                </tbody>
            </table>






        </div>
        <div id="search-button">
            <a id="btnSearch" name="" class="icon-btnsearch" onclick="searchTitle();"><i class="icons icon-search-1"></i></a>
        </div>
    </div>

</nav>

<script type="text/javascript">
    $("#search").keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            searchTitle();
        }
    });
    function searchTitle() {
        var key = $("#search").val();
        if (key) {
            //var find = $("#find").val();
            window.location = 'http://<%=HREF.Domain%>/Products/vit/key/' + key;
                                }
    }
                        </script>
