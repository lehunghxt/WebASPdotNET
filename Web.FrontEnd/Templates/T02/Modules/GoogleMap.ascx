<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GoogleMap.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.GoogleMap" %>

<!-- Google Script -->






<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=<%= Google_API_key%>" type="text/javascript"></script>
<asp:HiddenField ID="hf_address" runat="server" />
<asp:HiddenField ID="hf_default_zoom" runat="server" />
<asp:HiddenField ID="hf_latlong" runat="server" />

    
    <div class="page-content contact-info">
        
        <div id="gmap" class="contact-map">
            <iframe style="Width:100%" src="//www.google.com/maps/embed/v1/search?q=<%=HttpUtility.UrlEncode(hf_address.Value)%>
          &zoom=<%= default_zoom %>
          &key=<%= Google_API_key%>">
      </iframe> 
        </div>             
               	
        
		<div class="row comInfo">
                                    
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="contact-item purple">
                    <i class="icons icon-clock"></i>
                    <p><%=_company.AboutUs %></p>
				</div>
            </div>
                                	
            <div class="col-lg-6 col-md-6 col-sm-6">
                <div class="contact-item green">
                    <i class="icons icon-location-3"></i>
                    <p><%=_company.Address%></p>
				</div>
                <div class="contact-item blue">
                    <i class="icons icon-mail"></i>
                    <p><%=HREF.Domain%><br /><%=_company.Email %></p>
				</div>
                <div class="contact-item orange">
                    <i class="icons icon-phone"></i>
                    <p><%=this.Language["Tel"] %>: <%=_company.Phone %><br>
                        <%=this.Language["Fax"] %>: <%=_company.Fax %></p>
				</div>
            </div>
                                    
        </div>
                                
    </div>