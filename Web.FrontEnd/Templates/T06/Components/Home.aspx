<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    #psTop,.breadcrumb{display:none}
</style>
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <section class="about-w3ls py-md-5 py-3" id="about">
        <div class="container">
            <div class="row py-sm-5 py-3">
                <div class="col-lg-4 mt-lg-0 mt-5">
                    <div class="agileits-abt-grids">
                            <img src="<%=HREF.DomainStore %><%=this.Company.PathImage %>" style="width:100%; margin-bottom:20px" alt="<%=this.Company.FULLNAME %>"/>
                        <div class="lead about-text-wthree">
                            <div><i class="fa fa-phone"></i> <a href="tel:<%=this.Company.PHONE %>"><%=this.Company.PHONE %></a></div>
									<div><i class="fa fa-envelope"></i><a href="mailto:<%=this.Company.EMAIL %>"> <%=this.Company.EMAIL %></a></div>
                           <div><i class="fa fa-map"></i> <%=this.Company.ADDRESS %></div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-8">
                    <div class="order-lg-0 order-1">
                        <h4 class="w3l-sub pb-lg-5 pb-3"><%=Company.SLOGAN %></h4>
                        <p class="lead pr-lg-5 mr-lg-5 about-text-wthree">
                            <%=Company.ABOUTUS %></p>
                    </div>
                </div>
                
            </div>
        </div>
    </section>
    
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
</asp:Content>