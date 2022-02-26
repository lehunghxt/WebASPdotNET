<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Web.Model"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<div class="shop-top-bar">
    <div class="select-shoing-wrap">
        <%--<div class="shop-select">
            <select class="form-control">
                <option value="">Mới nhất</option>
                <option value="">Giá từ thấp đến cao</option>
                <option value="">Giá từ cao đến thấp</option>
                <option value="">Đánh giá</option>
                <option value="">A đến Z</option>
                <option value=""> Z đến A</option>
                <option value="">Còn hàng</option>
            </select>
        </div>--%>
        <p>Hiển thị <%=this.pager.StartRowIndex + 1 %>–<%= this.pager.PageSize > this.pager.TotalRowCount ? this.pager.TotalRowCount : this.pager.StartRowIndex + this.pager.PageSize %> của <%=this.pager.TotalRowCount %> kết quả</p>
    </div> 
    <div class="shop-tab nav">
        <a class="active" href="#shop-1" data-toggle="tab">
            <i class="sli sli-grid"></i>
        </a>
        <a href="#shop-2" data-toggle="tab">
            <i class="sli sli-menu"></i>
        </a>
    </div>
</div>

<div class="shop-bottom-area mt-35">
    <div class="tab-content jump">
        <div id="shop-1" class="tab-pane active">
            <div class="row woocommerce col-product-5">
                <asp:ListView ID="rpt" runat="server">
                    <ItemTemplate>
                <div class="single-product">
                    <div class="pro-img">
                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+ Eval("ID")+"/"+ Eval("Title").ToString().ConvertToUnSign())%>">
                            <span class="onsale">-<span><%#Math.Round((Convert.ToDouble(Eval("Price")) - Convert.ToDouble(Eval("Sale")))*100/(Convert.ToDouble(Eval("Price")) == 0 ? 1 : Convert.ToDouble(Eval("Price"))))%><span>%</span></span>
                            </span>

                            <img class="primary-img" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>">
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

                            <h4><a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+ Eval("ID")+"/"+ Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>"><%#Eval("Title").ToString().Length > 40 ? Eval("Title").ToString().Substring(0, 40) + "..." : Eval("Title").ToString()%></a></h4>
                            <span class="price">
                                <ins>
                                    <span class="woocommerce-Price-amount amount">
                                        <%#String.Format("{0:0,0}", Eval("Sale"))%><span>đ</span>
                                    </span>
                                </ins>
                                <del>
                                    <span><%#String.Format("{0:0,0}", Eval("Price"))%>
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

                    </div>

                </div>
                    </ItemTemplate>
            </asp:ListView>   
            </div>
        </div>
        <div id="shop-2" class="tab-pane ">
            <%var Data = this.rpt.DataSource as List<ProductWebModel>;
                if (Data != null) {%>
            <%foreach(var product in Data) {%>
            <div class="shop-list-wrap shop-list-mrg2 shop-list-mrg-none mb-30">
                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <div class="product-list-img">
                            <a href="<%=HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+ product.ID +"/"+ product.Title.ConvertToUnSign())%>">
                                <img src="<%=product.ImagePath %>" alt="<%=product.Title %>">
                            </a>
                            <!--
                            <div class="product-quickview">
                                <a href="#" title="Xem nhanh" data-toggle="modal" data-target="#exampleModal"><i class="sli sli-magnifier-add"></i></a>
                            </div>
-->
                        </div>
                    </div>
                    <div class="col-lg-8 col-md-8 align-self-center">
                        <div class="shop-list-content">
                            <div class="product-details-content">
                                <h2 title="<%=product.Title %>"><%=product.Title %></h2>
                                <div class="product-details-price">
                                    <span class="price_red"><%=String.Format("{0:0,0}", product.Sale)%> <div class="symbol">đ</div> </span>
                                    <span class="old"><%=String.Format("{0:0,0}", product.Price)%><div class="symbol">đ</div> </span>
                                    <div class="rating-share">
                                        <div class="pro-details-rating ratethis" id="rate<%=product.ID %>"></div>
                                        <script type="text/javascript" language="javascript">
                                            generate_stars(<%=VoteNumber%>, 'rate<%=product.ID%>');
                                            current('rate<%=product.ID %>', <%=product.Vote %>, <%=VoteNumber%>);
                                        </script>

                                        <span class="pro-details-share">
                                            <a title="Chia sẽ" href="#"><i class="lnr lnr-link"></i></a>
                                        </span>
                                        <span class="pro-details-wishlist">
                                            <a title="Yêu thích" href="#"><i class="lnr lnr-heart"></i></a>
                                        </span>
                                    </div>

                                </div>


                                <%var productProperties = Properties.Where(e => e.Type == product.ID.ToString()); %>
                                <div class="product-brand">
                                    <%var thuonghieu = productProperties.FirstOrDefault(e => e.Name == "Thương hiệu");%>
                                <%if (thuonghieu != null && !string.IsNullOrEmpty(thuonghieu.Value))
                                    { %>
                                                        <span>
                                                            <span class="product-brand-name">Thương hiệu:
                                                            </span>
                                                            <a class="product-brand-link" target="_self" href=""><%=thuonghieu.ValueName %></a>
                                                        </span>
                            <%} %>
                             <%var diem = productProperties.FirstOrDefault(e => e.Name == "Điểm");%>
                                <%if (diem != null && !string.IsNullOrEmpty(diem.Value))
                                    { %>
                                                         <span class="sku">
                                                            <span class="product-sku">Điểm tích lũy:
                                                            </span>
                                                            <a class="product-sku-link" target="_self" href=""><%=diem.Value %></a>

                                                        </span>
                            <%} %>
                                </div>

                              
                                <div class="pro-details-size-color">
                                    <input name ="<%=product.ID %>Property_0_" id ="<%=product.ID %>Property_0" type="hidden" value=""/>
                                     <%var colors = Colors.Where(e => e.ProductId == product.ID).ToList();
                                         if (colors.Count > 0)
                                         { %>
                                    <div class="pro-details-color-wrap">
                                        <span>Màu sắc</span>
                                        <div class="pro-details-color-content">
                                            <ul>
                                                <%foreach (var color in colors)
                                            { %>
                                        <li title="<%=color.Name %>" style="background-color: <%=color.Value%>; border: <%=color.Value%>" onclick="SetProperty('0','<%=color.Name %>')"></li>
                                        <%} %>
                                            </ul>
                                        </div>
                                    </div>
                        <%} %>
                                    

                                    <%var orderProperties = OrderProperties.Where(e => e.Type == product.ID.ToString()); %>
                                    <%foreach (var property in orderProperties)
                            {
                                if (property.Value == null && property.ValueName == null) continue;
                                var value = string.IsNullOrEmpty(property.ValueName) ? property.Value : property.ValueName;
                                var values = value.Split(','); %>
                                    <div class="pro-details-size">
                                        <span><%=property.Name %></span>
                                        <div class="pro-details-size-content">
                                            <ul>
                                                <%foreach (var val in values)
                                                                        { %>
                                                                    <li class="conga"><a style="cursor:pointer" id="a_<%=property.ID %>" onclick="SetProperty('<%=property.ID %>','<%=val %>')"><%=val %></a></li>
                                                                    <%} %>
                                            </ul>
                                        </div>
                                    </div>
                        <input name ="<%=product.ID %>Property_<%=property.ID %>" id ="<%=product.ID %>Property_<%=property.ID %>" type="hidden" value=""/>
                        <%} %>
                        <script>
                            function SetProperty(id, value) {
                                $("#Property_" + id).val(value);
                            }
                        </script>
                                </div>

                                <div class="pro-details-quality">
                                    <div class="pro-details-quality-wrap">
                                        <span>Số lượng</span>
                                        <div class="cart-plus-minus"><div class="dec qtybutton">-</div>
                                            <input class="cart-plus-minus-box" type="text" name="qtybutton" value="<%=product.SaleMin %>" id="txtQuantity<%=product.ID%>">
                                        <div class="inc qtybutton">+</div></div>
                                    </div>
                                    <div class="cart-concern">

                                        <a class="add-to-cart-btn  button button-xl button_theme_bluedaraz" href="#" onclick="AddProductToCart<%=product.ID%>(false)">
                                            <span class="button-text">
                                                    <i class="lnr lnr-cart"></i>Thêm vào giỏ hàng
                                            </span>
                                        </a>
                                        <a class="buy-now-btn  pdp-button button button-xl button_theme_orange" href="#" onclick="AddProductToCart<%=product.ID%>(true)">
                                            <span class="button-text">
                                                MUA NGAY
                                            </span>
                                        </a>
                                    </div>
                                </div>
                                   <script type="text/javascript">
                                       // Send the rating information somewhere using Ajax or something like that.
                                       function AddProductToCart<%=product.ID%>(go) {
                                           var sendproperties = $('#<%=product.ID%>Property_0').val();
        <%foreach (var property in orderProperties)
    {%>
                                           var temp = $('#<%=product.ID%>Property_<%=property.ID%>').val();
                                       if (temp) {
                                           if (sendproperties) sendproperties = sendproperties + ", " + temp;
                                           else sendproperties = temp;
                                       }
        <%}%>
                                       var productQuantity = $("#txtQuantity<%=product.ID%>").val();
                                       if (parseInt(productQuantity, 10) < parseInt(<%=product.SaleMin%>, 10)) {
                                           alert('Số lượng mua ít nhất là <%=product.SaleMin%>, bạn không thể mua ' + productQuantity + ' sản phẩm');
    }
    else {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
                                       data: JSON.stringify({ productId: <%=product.ID%>, quantity: productQuantity, properties: sendproperties }),
                                       contentType: "application/json; charset=utf-8",
                                       dataType: "json",
                                       success: function (data) {
                                           if (data != "") {
                                               if (go == true) location.href = '<%=HREF.BaseUrl %>vit-order';
                        else location.href = '<%=HREF.BaseUrl %>vit-carts';
                                           }
                                       }
                                           });
                                       }
                                       }
</script> 


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>
             <%} %>
        </div>
    </div>
    <div class="pro-pagination-style text-center mt-30">
        <VIT:Pager ID="pager" runat="server" QueryStringField="spp"/> 
        <%--<ul>
            <li><a class="prev" href="#"><i class="sli sli-arrow-left"></i></a></li>
            <li><a class="active" href="#">1</a></li>
            <li><a href="#">2</a></li>
            <li><a class="next" href="#"><i class="sli sli-arrow-right"></i></a></li>
        </ul>--%>
    </div>
</div>

<style>
    .pro-pagination-style a {
        display:inline-block;
        padding: 10px 15px;
        font-size:16px;
}
    .pro-pagination-style a:hover,.pro-pagination-style .current {
    background-color: #64b1f4;
    color: #fff;
}
        
</style>

<script type="text/javascript">

                            // Send the rating information somewhere using Ajax or something like that.
                            function sendRate(sel) {
                                var rid = sel.id.split('_');
                                var id = rid[0].replace("rate", '');

                                $.ajax({
                                    type: "POST",
                                    url: "<%=HREF.BaseUrl %>JsonPost.aspx/UpdateVote",
            data: JSON.stringify({ Id: id, Rate: rid[1] }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    current(rid[0], data.d.VoteRate, <%=VoteNumber/2%>);
                }
            }

        })
                            }
</script>
