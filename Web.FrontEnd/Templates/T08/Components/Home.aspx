<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    #psTop,.breadcrumb{display:none}
</style>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <section class="about py-lg-4 py-md-3 py-sm-3 py-3">
         <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
            <div class="row">
               <div class="col-lg-12 col-md-12">
                  <div class="wthree-about-txt mb-lg-5 mb-md-4 mb-3">
                     <h5><%=Language["AboutUs"] %></h5>
                  </div>
                  <div class="about-para-txt">
                       <div class="row">
                           <div class="col-lg-6 col-md-6" style="border-right: dotted 1px #333">
                     <%=Company.ABOUTUS %>
                               </div>
                           <div class="col-lg-6 col-md-6">
                               <h2><%=Language["Contact"] %></h2>
                               <br />
                                    <div><i class="fa fa-phone"></i> <a href="tel:<%=this.Company.PHONE %>"><%=this.Company.PHONE %></a></div>
									<div><i class="fa fa-envelope"></i><a href="mailto:<%=this.Company.EMAIL %>"> <%=this.Company.EMAIL %></a></div>
                           <div><i class="fa fa-map"></i> <%=this.Company.ADDRESS %></div>    
                           </div>
                           </div>
                  </div>
               </div>
            </div>
         </div>
      </section>

    <section class="about py-lg-4 py-md-3 py-sm-3 py-3"">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </section>
</asp:Content>