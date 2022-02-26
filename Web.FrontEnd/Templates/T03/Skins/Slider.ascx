<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<%if (this.GetValueParam<string>("Source") == "CAT") { %>
<div class="row" style="margin-top:10px">
<%foreach(var item in this.Data) 
    {%>
<div class="product col-lg-3 col-md-3 col-sm-3 col-xs-6" >
                <div id="product<%=item.ID%>" class="product-image" ;text-align:center'>
			        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/" + item.ID + "/" + item.Title.ConvertToUnSign())%>"><img src="<%=item.ImagePath%>" alt="<%=item.Title%>"'></a>
		        </div>
            </div>
<%} %>
  </div>
<%} else { %>
<div class="iosSlider hidden-xs hidden-sm">
	<div class="slider">
        <%foreach(var item in this.Data) 
        {%>
		    <div class="item">		
			    <div class="image">
                    <a href="<%=item.URL%>">
				    <img src="<%=item.ImagePath%>" alt="<%=item.Title%>"/></a>
			    </div>
								
			    <%--<div class="text">	
				    <div class="bg"></div>	
				    <div class = "title">
					    <h2><strong><%=item.Title%></strong></h2>
				    </div>		
				    <div class = "desc">
					    <%=item.Description%>
				    </div>
			    </div>--%>			
		    </div>	
        <%} %>		
	</div>	
	<div class='prevButton'></div>
	<div class='nextButton'></div>
</div>
<%} %>