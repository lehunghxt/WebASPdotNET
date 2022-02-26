<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>








<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<section class="prdcategory-groups">
<h1 title="<%=Title %>" class="main-heading" >
   <%=Title %>
</h1>
    <section class="prdcategory-child list">
        <header class="panel-head"></header>
        <section class="panel-body">

    <div class="feature-product">
        <div class="row">
            <div class="main-featured-content">
    <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
            <div class="col-md-<%# ColumnCount == 1 || ColumnCount == 3 || ColumnCount == 4 || ColumnCount == 6 ? 12 / ColumnCount : 3%> col-sm-6 col-xs-12">
				<div class="product"  style="<%=WidthProduct > 0 ? "Width:" + WidthProduct + "px;": ""%><%=HeightProduct > 0 ? "Height:" + HeightProduct + "px;": ""%>">
					
                    <div class="thumb img-shine" style="<%=WidthImage > 0 ? "Width:" + WidthImage + "px;": ""%><%=HeightImage > 0 ? "Height:" + HeightImage + "px;": ""%>">
						<a class="image img-scaledown" href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>">
                            <img src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>" style="max-height:100%; max-width:100%;display: inline;"/>
                        </a>
                    </div>   
                    
                    <div class="info">
                        <h2 class="title">
                            <a class="" href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>">
                                <%#Eval("Title")%>
                                <%if (this.GetValueParam<bool>("DisplayCode")){ %> - <span class="product-row-sku"><%#Eval("Code")%><%} %></span>
                            </a>
                        </h2>
                        <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                            <%#Convert.ToDecimal(Eval("Price")) > 0 
                                    ? Convert.ToDecimal(Eval("Sale")) > 0 ? 
                                        "<span class='sale-price'>"+ String.Format("{0:0,0}", Eval("Sale")) + "<sup>đ</sup></span><br><del>" + String.Format("{0:0,0}đ", Eval("Price")) + "<sup>đ</sup></del>" 
                                        : "<span class='sale-price'>"+ String.Format("{0:0,0}", Eval("Price")) +"<sup>đ</sup></span>" 
                                    : "Liên hệ"%>
                        </div>
                        <%#Convert.ToDecimal(Eval("Price")) > 0 && Convert.ToDecimal(Eval("Sale")) > 0 ? "<div class='product-row-percent'>"+Math.Round(Convert.ToDouble(((Convert.ToDecimal(Eval("Price")) - Convert.ToDecimal(Eval("Sale")))*100/Convert.ToDecimal(Eval("Price")))))+"%</div>" : ""%>
                        <div style='display:<%=VoteNumber == 0 ? "none" : "" %>'>
                            <div id="rate<%#Eval("Id") %>" class="rate ratethis"></div>
                            <script type="text/javascript" language="javascript">
                                generate_stars(<%=VoteNumber%>, 'rate<%#Eval("Id") %>');
                                current('rate<%#Eval("Id") %>', <%#Eval("Vote") %>, <%=VoteNumber%>);
                            </script> 
                        </div>
                        <%if (HasOrder)
                            {
                                %>
                        <input data-toggle="modal" data-target="#buyModal" style='width:100%' class="addCarts btn btn-primary" onclick="SelectProduct('<%#Eval("Id")%>','<%#Eval("Title")%>','<%#Eval("ImagePath")%>','<%#Eval("SaleMin")%>','<%#Eval("Brief").ToString().DeleteHTMLTag().Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Sale"))) : Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Price"))) : "Liên hệ"%>')" value="Mua ngay"></input>
                    <%} %>
                    </div>
				</div>
			</div> 
        </ItemTemplate>
    </asp:ListView>
<div class="paging">
    <VIT:Pager ID="pager" runat="server" />
</div>
            </div>
    </div>
</div>

            </section>
    </section>
</section>