<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<%if(MainCore.isMobileBrowser()) {%>
    <style>
        .product-image{height:auto !important}
    </style>
<%} %>

<%if (ColumnCount == 0) ColumnCount = 3;%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
            <%if (!string.IsNullOrEmpty(Search))
            { %>
        <span style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontSize") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>">Tìm thấy <%=this.pager.TotalRowCount %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></span>
        <%}
    else
    { %>
            <h1 title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h1>
        <%} %>
		</div>
							
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">

<div class="row">
	<asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
            <div class="product col-lg-<%=12/ColumnCount %> col-md-<%=12/ColumnCount %> col-sm-<%=12/ColumnCount %> col-xs-6" >
                <div id="product<%#Eval("Id")%>" class="product-image" style='<%=WidthProduct > 0 ? "Width:" + WidthProduct + "px;": ""%><%=HeightProduct > 0 ? "Height:" + HeightProduct + "px;": ""%>;text-align:center'>
			        <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><img id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>"'></a>
		        </div>
											
		        <div class="product-info">
			        <h5><a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%#Eval("Title")%></a></h5>
			        <%#Convert.ToDecimal(Eval("Price")) > 0 ? Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='reduce'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Sale")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Price")) == 0 && Convert.ToDecimal(Eval("Sale")) == 0 ? "<span class='price'>Liên hệ</span>" : ""%> 
			        <div class="rating readonly-rating" data-score="4"></div>
		        </div>
											
		        <div class="product-actions" style='cursor:pointer; display:<%=HasOrder == false ? "none" : "" %>' data-toggle="modal" data-target="#buyModal" onclick="SelectProduct('<%#Eval("Id")%>','<%#Eval("Title")%>','<%#Eval("ImagePath")%>','<%#Eval("Brief").ToString().DeleteHTMLTag().Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Sale"))) : Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Price"))) : "Liên hệ"%>')" >
			        <span class="add-to-cart" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %>">
				        <span class="action-wrapper">
					        <i class="icons icon-basket-2"></i>
					        <span class="action-name">Thêm vào giỏ</span>
				        </span >
			        </span>
		        </div>
            </div>
        </ItemTemplate>
    </asp:ListView>		
</div>

<div class="row">
    <div class="paging pagination">
        <VIT:Pager ID="pager" runat="server" QueryStringField="p" CssClass="pager"/>
    </div>
</div>
        </div>
    </div>
