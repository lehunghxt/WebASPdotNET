<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>
<div class="menu_box">
				   	  	<h3 class="menu_head"><%=this.Title %></h3>
				   	     <ul class="nav">
               <li><a href="/">Trang chủ</a></li>                 
                                <%foreach (var parent in this.Model)
                {%>
                <li>
                    <a href="<%=CreateLink(parent.Type, false, parent.ID, parent.CategoryName)%>">
                        <%=parent.CategoryName%>
                    </a>
                    
                    
                    <%if (parent.Childs != null && parent.Childs.Count > 0)
                    {%>
                    <span class="badge" style="float:right;margin: -30px 5px 0px;"><i class="glyphicon glyphicon-chevron-down "></i></span>
                    <ul class="child">
                         <%foreach (var child in parent.Childs)
                        {%>
                            <%if (child.Items != null && child.Items.Count > 0)
                            {%>
                            <li><a class="dropdown-toggle" data-toggle="dropdown" href="<%=CreateLink(parent.Type, true, child.ID, child.CategoryName)%>"><%=child.CategoryName%> <i class="icons icon-right-dir"></i></a>
                            <ul>
                                <%var count = child.Items.Count(); int stt = 0; %>
                                <%foreach (var item in child.Items)
                                                                {%>
                                <li><a target="<%=item.TargetTag %>" href="<%=!string.IsNullOrEmpty(item.URL) ? item.URL : CreateLink(child.Type, true, item.ID, item.Title)%>"><%=item.Title%></a>
                                    <% stt++; if(count/2 == stt) { %>
                                </li>
                                <%} %>
                                <%} %>
                            </ul>
                            <%} else {%>
                                <li><a href="<%=CreateLink(parent.Type, false, child.ID, child.CategoryName)%>"><%=child.CategoryName%></a>
                            <%} %>
                        </li>
                        <%} %>
                    </ul>
                    <%} %>

                    <%if (parent != null && parent.Items != null && parent.Items.Count > 0)
                    {%>
                    <span class="badge" style="float:right;margin: -30px 5px 0px;"><i class="glyphicon glyphicon-chevron-down "></i></span>
                    <ul class="child">
                        <%foreach (var item in parent.Items)
                        {%>
                            <li ><a target="<%=item.TargetTag %>" href="<%=!string.IsNullOrEmpty(item.URL) ? item.URL : CreateLink(parent.Type, true, item.ID, item.Title)%>"><%=item.Title%></a></li>
                        <%} %>
                        </ul>
                    <%} %>
                </li>
                <%} %>

                <li>
                    <a href="<%=HREF.LinkComponent("Contact")%>">
                        Liên hệ
                    </a>
                </li>

					   	 </ul>






			   	    </div>
<script>
$(document).ready(function() {
    $("ul.nav > li > span.badge").click(function () {
        var ul = $(this).parent().children("ul");
		var display = ul.css("display");
		if (display == "none") {
		    $(this).children("i").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
		    ul.show("slow");
		}
		else {
		    $(this).children("i").removeClass("glyphicon-chevron-down").addClass("glyphicon-chevron-right");
		    ul.hide("slow");
		}
	});

});
</script>