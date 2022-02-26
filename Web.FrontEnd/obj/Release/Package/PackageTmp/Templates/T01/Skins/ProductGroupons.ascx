<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductGroupons.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductGroupons" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>






<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>



<section id="recommended-section" class="section-products-carousel-tabs product-carousel">
<div class="section-products-carousel-tabs-wrap">
<header class="section-header">
                                        <h2 class="section-title" style="color:<%=this.GetValueParam<string>("FontColor")%>"><i class="fa fa-bolt"></i><strong><%=Title %></strong></h2>
                                        <ul class="nav justify-content-end" role="tablist">
                                            <li class="nav-item">
                                                <a data-toggle="tab" href="<%=HREF.LinkComponent("Groupons") %>" class="nav-link view-all">Xem tất cả </a></li>
                                        </ul>
                                    </header>
<div class="tab-content">
    <div class="tab-pane active" role="tabpanel">
        <div class="products-carousel ">

            <div class="woocommerce swiper-container">
                <div class="x-carousel swiper-wrapper" role="toolbar">
                <asp:ListView ID="rpt" runat="server">
    <ItemTemplate>
                <div class="swiper-slide single-product">
                    <div class="pro-img">
                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+ Eval("ID")+"/"+ Eval("Title").ToString().ConvertToUnSign())%>">
                            <span class="onsale">-<span><%#Math.Round((Convert.ToDouble(Eval("Price")) - Convert.ToDouble(Eval("Sale")))*100/(Convert.ToDouble(Eval("Price")) == 0 ? 1 : Convert.ToDouble(Eval("Price"))))%><span>%</span></span>
                            </span>

                            <img class="primary-img" src="<%#Eval("PathImage")%>" alt="<%#Eval("Title")%>">
                        </a>
                        <div class="yith-wcwl-add-to-wishlist">
                            <div class="yith-wcwl-add-button show" style="display:block">
                                <a href="#1" rel="nofollow" data-product-id="85" data-product-type="simple" class="add_to_wishlist" tabindex="-1"> Yêu thích</a>
                                <img src="/Templates/T01/img/icon/ajax-loader.gif" class="ajax-loading" alt="loading" width="16" height="16" style="visibility:hidden">
                            </div>
                        </div>
                    </div>
                    <div class="pro-content">
                        <div class="pro-info">

                            <%--<h4><a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+ Eval("ID")+"/"+ Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>"><%#Eval("Title")%></a></h4>--%>
                            <span class="price">
                                <ins>
                                    <span class="woocommerce-Price-amount amount">
                                        <%#String.Format("{0:0,0}", Eval("Price"))%><span>đ</span>
                                    </span>
                                </ins>
                                <del>
                                    <span><%#String.Format("{0:0,0}", Eval("Sale"))%>
                                        <span class="woocommerce-Price-currencySymbol">đ</span>
                                    </span>
                                </del>
                            </span>
                        </div>
                        <div class="pro-actions">
                            <div class="actions-primary">
                                <!-- <a href="cart.html" title="Add to Cart">Thêm vào giỏ</a> -->
                            </div>
                            <div class="actions-secondary">
                            </div>
                        </div>
                        <div class="deal-progress">
                            <div class="deal-stock">
                                <div class="stock-sold">Đã bán: <strong><%#String.Format("{0:0,0}", Eval("PayNumber"))%></strong></div>
                                <div class="stock-available">Còn lại: <strong><%#String.Format("{0:0,0}", Convert.ToInt16(Eval("Quantity")) - Convert.ToInt16(Eval("PayNumber")))%></strong></div>
                            </div>
                            <div class="progress"> <span class="progress-bar" style="width:<%#(Convert.ToInt16(Eval("PayNumber")) * 100)/Convert.ToInt16(Eval("Quantity"))%>%"><a href="#"></a></span></div>
                        </div>
                        <div class="deal-countdown-timer">
                            <div class="marketing-text1">

                            </div>
                            <br>
                            <span class="deal-time-diff" style="display:none;">29994</span>
                            <div class="deal-countdown countdown" data-sale-countdown="<%#String.Format("{0:MM/dd/yyyy HH:mm}", Eval("EndDate"))%>"></div>
                        </div>

                    </div>

                </div>
        </ItemTemplate>
</asp:ListView>  
                    </div>
                                                    <a href="#" class="arrow left desktop-only"></a>
                                                    <a href="#" class="arrow right desktop-only"></a>
     <VIT:Pager ID="pager" runat="server"/> 
            </div>
        </div>
    </div>
</div>
</div>
</section>