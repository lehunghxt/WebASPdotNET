<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<section class="prdcategory-groups">
    <%if (!string.IsNullOrEmpty(Search)){ %>
    <span style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px">Tìm thấy <%=pager.TotalRowCount %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></span>
    <%} else if (!string.IsNullOrEmpty(Title) && Title != ".")
            { %>
<h1 class="main-heading" title="<%=Title%>" style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px"><%=Title%></h1>
    <%} %>

    <div class="feature-product">
        <div class="row">
            <div class="main-featured-content">
    <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
            <%if (ColumnCount == 0) ColumnCount = 3; %>
            <div class="simpleCart_shelfItem col-lg-<%=12/ColumnCount %> col-md-<%=12/ColumnCount %> col-sm-<%=12/ColumnCount %> col-xs-6" style='<%=WidthProduct > 0 ? "Width:" + WidthProduct + "px;": ""%><%=HeightProduct > 0 ? "Height:" + HeightProduct + "px;": ""%>'>
                <div class="inner_content clearfix"><div class="product_image">
                
                <div style='<%=WidthImage > 0? "Width:" + WidthImage + "px;": ""%><%=HeightImage > 0 ? "Height:" + HeightImage + "px;": ""%>'>
			        <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                        <img id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>" style="max-height:100%; max-width:100%"></a>
		        </div>
                <a class="button item_add item_1" style='cursor:pointer; display:<%=HasOrder == false ? "none" : "" %>' onclick="AddToCart(<%#Eval("Id") %>);"> </a>
                <div class="product_container">
								   <div class="cart-left">
									 <p class="title"><a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>"><%#Eval("Title")%></a></p>
								   </div>
                    <%if (this.HasPrice)
                                                { %>
                    <%#Convert.ToDecimal(Eval("Price")) > 0 ? Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='amount item_price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : "<span class='amount item_price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='amount item_price'>" + String.Format("{0:0,0}đ", Eval("Sale")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Price")) == 0 && Convert.ToDecimal(Eval("Sale")) == 0 ? "<span class='amount item_price'>Liên hệ</span>" : ""%> 
								   <%} %><div class="clearfix"></div>
							     </div>
                    </div></div>
            </div>
        </ItemTemplate>
    </asp:ListView>
<div class="paging">
    <VIT:Pager ID="pager" runat="server" QueryStringField="sxthubong"/>
</div>
            </div>
    </div>
</div>
    </section>