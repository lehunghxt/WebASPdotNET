<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<div style="line-height: 60px;" class="w3-right w3-hide-small">
  <a href="/" class="w3-bar-item w3-button"><%=Language["Home"] %></a>
    <%foreach (var cat in this.Model)
        {%>
            <%if (cat.Items != null && cat.Items.Count > 0)
            {%>
            <div class="w3-dropdown-click">
                <button class="w3-button w3-wide" onclick="myMenu('<%=cat.ID%>')" type="button"><%=cat.CategoryName%><i class="fa fa-caret-down"></i></button>
                <div id="childen<%=cat.ID%>" class="w3-dropdown-content w3-bar-block">
                   <%foreach (var item in cat.Items)
                            {%>
                                <a class="w3-bar-item w3-button" href="<%=!string.IsNullOrEmpty(item.URL)?item.URL : CreateLink(cat.Type, true, item.ID, item.Title)%>" target="<%=item.TargetTag %>"><%=item.Title%></a>
                            <%} %>






                </div>
              </div>
            <%} else if (cat.Childs != null && cat.Childs.Count > 0 && cat.Type != "ART")
            {%>
                <div class="w3-dropdown-hover">
                <button class="w3-button"><a class="w3-wide" href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>"><%=cat.CategoryName%></a><i class="fa fa-caret-down"></i></button>
                <div class="w3-dropdown-content w3-bar-block">
                   <%foreach (var dto in cat.Childs)
                            {%>
                    <a class="w3-bar-item w3-button" href="<%=CreateLink(cat.Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a>
                            <%} %>
                </div>
              </div>
            <%} else {%>
                        <a class="w3-bar-item w3-button w3-mobile" href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>"><%=cat.CategoryName%></a>
                    <%} %>
        <%} %>
    <a href="<%=HREF.LinkComponent("Contact")%>" class="w3-bar-item w3-button w3-mobile"><%=Language["Contact"] %></a>
</div>

<!-- Sidebar/menu -->

                <nav class="w3-sidebar w3-bar-block w3-white w3-collapse w3-top w3-hide-large w3-hide-medium" style="z-index:9999;width:300px" id="mySidebar">
                    <div class="w3-container w3-display-container w3-padding-16">
                        <i onclick="w3_close()" class="fa fa-remove w3-hide-large w3-button w3-display-topright"></i>
                        <h3 class="w3-wide"><b><%=Title %></b></h3>
                    </div>
                    <div>
                        <div class="cate_left_list">
                                <%foreach (var cat in this.Model)
        {%>
            <%if (cat.Items != null && cat.Items.Count > 0)
            {%>
                <button class="w3-button w3-block w3-left-align" onclick="myMenuOver('<%=cat.ID%>')" type="button"><%=cat.CategoryName%> <i class="fa fa-caret-down"></i></button>
                <div id="childenover<%=cat.ID%>" class="w3-bar-block w3-hide w3-white w3-card-4" style="margin-left:30px">
                   <%foreach (var item in cat.Items)
                            {%>
                                <a class="w3-bar-item w3-button" href="<%=!string.IsNullOrEmpty(item.URL)?item.URL : CreateLink(cat.Type, true, item.ID, item.Title)%>" target="<%=item.TargetTag %>"><%=item.Title%></a>
                            <%} %>
                </div>
            <%} else if (cat.Childs != null && cat.Childs.Count > 0)
            {%>
                <button class="w3-button w3-block w3-left-align" onclick="myMenuOver('<%=cat.ID%>')" type="button"><%=cat.CategoryName%> <i class="fa fa-caret-down"></i></button>
                <div id="childenover<%=cat.ID%>" class="w3-bar-block w3-hide w3-white w3-card-4" style="margin-left:10px">
                   <%foreach (var dto in cat.Childs)
                            {%>
                    <a class="w3-bar-item w3-button" href="<%=CreateLink(cat.Type, false, dto.ID, dto.CategoryName)%>"><%=dto.CategoryName%></a>
                            <%} %>
                </div>
            <%} else {%>
                                    <a class="w3-bar-item w3-button" href="<%=CreateLink(cat.Type, false, cat.ID, cat.CategoryName)%>">

                                        <%=cat.CategoryName%>
                                    </a>
                    <%} %>
        <%} %>
                        </div>
                    </div>
                </nav>
                <!-- Overlay effect when opening sidebar on small screens -->
                <div style="z-index: 999;" class="w3-overlay w3-hide-large" onclick="w3_close()" style="cursor:pointer" title="close side menu" id="myOverlay"></div>

                <!--END Sidebar/menu -->

<script>
    function myMenu(id) {
    var x = document.getElementById("childen" + id);
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else { 
        x.className = x.className.replace(" w3-show", "");
    }
    }

    function myMenuOver(id) {
        var x = document.getElementById("childenover" + id);
        if (x.className.indexOf("w3-show") == -1) {
            x.className += " w3-show";
        } else { 
            x.className = x.className.replace(" w3-show", "");
        }
}
</script>