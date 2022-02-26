<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberOrders.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberOrders" %>


<section id="checkout-manager" style="display:none">
    <h4><%=Title %></h4>
    <div class="tab-area mb-20" id="checkout-mgd">
        <ul>
            <li class="active" data-rel="waiting"><a href="javascript:void(0)">Chờ xác nhận (<%=Data.Count(e => e.Status == 0) %>)</a></li>
            <li data-rel="accept"><a href="javascript:void(0)">Đã xác nhận (<%=Data.Count(e => e.Status == 1) %>)</a></li>
            <li data-rel="delivering"><a href="javascript:void(0)">Đang vận chuyển (<%=Data.Count(e => e.Status == 2) %>)</a></li>
            <li data-rel="delivered"><a href="javascript:void(0)">Đã giao hàng (<%=Data.Count(e => e.Status == 3) %>)</a></li>
            <li data-rel="cancel"><a href="javascript:void(0)">Đã hủy (<%=Data.Count(e => e.Status == 4) %>)</a></li>
        </ul>






    </div>

    <div id="waiting" class="state">
        <%foreach (var order in Data.Where(e => e.Status == 0))
            {%>
        <div class="row black-border pda-10 mt-10">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                        <p class="order-code">Mã đơn hàng
                            <a href="javascript:void(0)">#<%=order.Id %></a>
                            <span>|</span>
                            <a href="javascript:void(0)">Chi tiết</a>
                        </p>
                        <p>Đặt ngày: <b><%=string.Format("{0:dd/MM/yyyy}", order.CreateDate)%></b></p>

                    </div>
                    <div class="col-md-6">
                        <p>Người nhận:</p>
                        <p><%=order.CustomerName %></p>

                    </div>
                    <div class="col-md-2">
                        <p>Tổng tiền</p>
                        <p><b><%=string.Format("{0:0,0}", order.TotalDue)%>đ</b></p>
                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-1 img-cont">
                        <%--<a href="javascript:void(0)">
                            <img src="img/icon/user_circle.png" height="64px">
                        </a>--%>
                    </div>
                    <div class="col-md-3">
                        <%--<p>Ốp lưng in hình cô gái đỏ</p>

                        <p class="status-wait">Chờ shop xác nhận đơn hàng</p>--%>
                    </div>
                    <div class="col-md-8">
                        <div class="container">
                            <ul class="progressbar">
                                <li class="active">Chờ xác nhận</li>
                                <li>Đang xác nhận</li>
                                <li>Đang vận chuyển</li>
                                <li>Đã giao hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row mt-10 mb-10">
                    <div class="col-md-12 pb-10">
                        <%--<button class="btn btn-primary">Tiếp tục mua hàng</button>--%>
                        <button type="button" class="btn btn-secondary fr" data-toggle="modal" data-target="#cancel-reason" onclick="CancleOrder(<%=order.Id %>)">Hủy</button>
                    </div>

                </div>

            </div>
        </div>
        <%} %>
    </div>
    <div id="accept" class="state">
        <%foreach (var order in Data.Where(e => e.Status == 1))
            {%>
        <div class="row black-border pda-10 mt-10">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                        <p class="order-code">Mã đơn hàng
                            <a href="javascript:void(0)">#<%=order.Id %></a>
                            <span>|</span>
                            <a href="javascript:void(0)">Chi tiết</a>
                        </p>
                        <p>Đặt ngày: <b><%=string.Format("{0:dd/MM/yyyy}", order.CreateDate)%></b></p>

                    </div>
                    <div class="col-md-6">
                        <p>Người nhận:</p>
                        <p><%=order.CustomerName %></p>

                    </div>
                    <div class="col-md-2">
                        <p>Tổng tiền</p>
                        <p><b><%=string.Format("{0:0,0}", order.TotalDue)%>đ</b></p>

                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-1 img-cont">
                        <%--<a href="javascript:void(0)">
                            <img src="img/icon/user_circle.png" height="64px">
                        </a>--%>
                    </div>
                    <div class="col-md-3">
                        <%--<p>Ốp lưng in hình cô gái đỏ</p>

                        <p class="status-wait">Shop đã xác nhận đơn hàng</p>--%>
                    </div>
                    <div class="col-md-8">
                        <div class="container">
                            <ul class="progressbar">
                                <li class="active">Chờ xác nhận</li>
                                <li class="active">Đang xác nhận</li>
                                <li>Đang vận chuyển</li>
                                <li>Đã giao hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row mt-10 mb-10">
                    <div class="col-md-12 pb-10">
                        <%--<button class="btn btn-primary">Tiếp tục mua hàng</button>--%>
                        <button type="button" class="btn btn-secondary fr" data-toggle="modal" data-target="#cancel-reason" onclick="CancleOrder(<%=order.Id %>)">Hủy</button>
                    </div>
                </div>

            </div>
        </div>
        <%} %>
    </div>
    <div id="delivering" class="state">
        <%foreach (var order in Data.Where(e => e.Status == 2))
            {%>
        <div class="row black-border pda-10 mt-10">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                        <p class="order-code">Mã đơn hàng
                            <a href="javascript:void(0)">#<%=order.Id %></a>
                            <span>|</span>
                            <a href="javascript:void(0)">Chi tiết</a>
                        </p>
                        <p>Đặt ngày: <b><%=string.Format("{0:dd/MM/yyyy}", order.CreateDate)%></b></p>

                    </div>
                    <div class="col-md-6">
                        <p>Người nhận:</p>
                        <p><%=order.CustomerName %></p>

                    </div>
                    <div class="col-md-2">
                        <p>Tổng tiền</p>
                        <p><b><%=string.Format("{0:0,0}", order.TotalDue)%>đ</b></p>

                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-1 img-cont">
                        <%--<a href="javascript:void(0)">
                            <img src="img/icon/user_circle.png" height="64px">
                        </a>--%>
                    </div>
                    <div class="col-md-3">
                        <%--<p>Ốp lưng in hình cô gái đỏ</p>

                        <p class="status-wait">Dự kiến giao hàng 11/04 - 15/04</p>--%>
                    </div>
                    <div class="col-md-8">
                        <div class="container">
                            <ul class="progressbar">
                                <li class="active">Chờ xác nhận</li>
                                <li class="active">Đang xác nhận</li>
                                <li class="active">Đang vận chuyển</li>
                                <li>Đã giao hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-12 pb-10">
                       <%-- <button class="btn btn-primary">Tiếp tục mua hàng</button>--%>
                        <button class="btn btn-danger fr">Theo dõi đơn hàng</button>
                    </div>
                </div>

            </div>
        </div>
        <%} %>
    </div>
    <div id="delivered" class="state">
        <%foreach (var order in Data.Where(e => e.Status == 3))
            {%>
        <div class="row black-border pda-10 mt-10">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                        <p class="order-code">Mã đơn hàng
                            <a href="javascript:void(0)">#<%=order.Id %></a>
                            <span>|</span>
                            <a href="javascript:void(0)">Chi tiết</a>
                        </p>
                        <p>Đặt ngày: <b><%=string.Format("{0:dd/MM/yyyy}", order.CreateDate)%></b></p>

                    </div>
                    <div class="col-md-6">
                        <p>Người nhận:</p>
                        <p><%=order.CustomerName %></p>

                    </div>
                    <div class="col-md-2">
                        <p>Tổng tiền</p>
                        <p><b><%=string.Format("{0:0,0}", order.TotalDue)%>đ</b></p>

                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-1 img-cont">
                        <%--<a href="javascript:void(0)">
                            <img src="img/icon/user_circle.png" height="64px">
                        </a>--%>
                    </div>
                    <div class="col-md-3">
                        <%--<p>Ốp lưng in hình cô gái đỏ</p>

                        <p class="status-wait">Đã giao hàng thành công</p>--%>
                    </div>
                    <div class="col-md-8">
                        <div class="container">
                            <ul class="progressbar">
                                <li class="active">Chờ xác nhận</li>
                                <li class="active">Đang xác nhận</li>
                                <li class="active">Đang vận chuyển</li>
                                <li class="active">Đã giao hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row mt-10 mb-10">
                    <div class="col-md-12 pb-10">
                        <%--<button class="btn btn-primary">Tiếp tục mua hàng</button>--%>
                        <%--<button class="btn btn-danger fr">Theo dõi đơn hàng</button>--%>
                    </div>
                </div>

            </div>
        </div>
        <%} %>
    </div>
    <div id="cancel" class="state">
        <%foreach (var order in Data.Where(e => e.Status == 4))
            {%>
        <div class="row black-border pda-10 mt-10">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-4">
                        <p class="order-code">Mã đơn hàng
                            <a href="javascript:void(0)">#<%=order.Id %></a>
                            <span>|</span>
                            <a href="javascript:void(0)">Chi tiết</a>
                        </p>
                        <p>Đặt ngày: <b><%=string.Format("{0:dd/MM/yyyy}", order.CreateDate)%></b></p>

                    </div>
                    <div class="col-md-6">
                        <p>Người nhận:</p>
                        <p><%=order.CustomerName %></p>

                    </div>
                    <div class="col-md-2">
                        <p>Tổng tiền</p>
                        <p><b><%=string.Format("{0:0,0}", order.TotalDue)%>đ</b></p>
                            
                    </div>
                </div>
                <div class="row mt-10">
                    <div class="col-md-1 img-cont">
                        Lý do: 
                    </div>
                    <div class="col-md-3">
                        <%=order.CustomerNote%>
                        <%=order.Note%>
                    </div>
                    <div class="col-md-8">
                        <div class="container">
                            <ul class="progressbar">
                                <li>Chờ xác nhận</li>
                                <li>Đang xác nhận</li>
                                <li>Đang vận chuyển</li>
                                <li>Đã giao hàng</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row mt-10 mb-10">
                    <div class="col-md-12 pb-10">
                        <%--<button class="btn btn-primary">Tiếp tục mua hàng</button>--%>
                        <%--<button class="btn btn-danger fr">Theo dõi đơn hàng</button>--%>
                    </div>
                </div>

            </div>
        </div>
        <%} %>
    </div>

    <input type="hidden" value ="0" id="OrderCancleId" name="OrderCancleId" />
    <script>
        function CancleOrder(id) {
            $("#OrderCancleId").val(id);
        }
    </script>
    <div class="modal fade" id="cancel-reason" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;" aria-hidden="true">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center" style="max-width: 500px;">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="align-center">
                                    <h4 class="modal-title"><b>HỦY ĐƠN HÀNG</b></h4>
                                    <p>Bạn muốn hủy đơn hàng từ <b class="blue-text">BMG</b>? Vui lòng cho <b class="blue-text">BMG</b> biết lý do:</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                    <div class="form-check">
                                        <label for="opt1" class="radio">
                                            <input type="radio" name="rdo" id="opt1" class="hidden">
                                            <span class="label"></span>Thay đổi chi tiết đơn hàng (màu sắc, kích thước, số lượng...)
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label for="opt2" class="radio">
                                            <input type="radio" name="rdo" id="opt2" class="hidden">
                                            <span class="label"></span>Thay đổi địa chỉ nhận hàng
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label for="opt3" class="radio">
                                            <input type="radio" name="rdo" id="opt3" class="hidden">
                                            <span class="label"></span>Đặt nhầm/trùng
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label for="opt4" class="radio">
                                            <input type="radio" name="rdo" id="opt4" class="hidden">
                                            <span class="label"></span>Khác
                                        </label>
                                    </div>

                                    <div class="form-group">
                                        <textarea class="form-control" name="CustomerNote" id="CustomerNote" rows="3" placeholder="Vui lòng cho biết lý do"></textarea>
                                    </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="align-center">
                                    <asp:Button ID="btnHuy" runat="server" CssClass="btn btn-primary" OnClick="btnHuy_Click" Text="Xác nhận hủy"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
