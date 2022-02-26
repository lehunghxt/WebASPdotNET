<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberPoint.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberPoint" %>

<section id="user-coupon" style="display:none">
    <h4><%=Title %></h4>
    <p>Tổng điểm hiện có: <%=this.UserContext.Point %></p>
    <div class="tab-area mb-20" id="coupon">






    </div>
    <table>
        <thead>
            <tr>
                <th class="product-code">Mã đơn hàng</th>

                <th class="product-status">Trạng thái</th>
                <th class="product-price">Giá trị mua hàng</th>
                <th class="product-coupon">Điểm sử dụng</th>
                <th class="product-coupon">Điểm tích lũy</th>

            </tr>
        </thead>
        <tbody>
            <%foreach (var don in Data)
                { %>
            <tr>
                <td class="product-code">
                    <a href="javascript:void(0)">#<%=don.Id %></a>
                </td>

                <td class="product-status">
                    <p class="product-coupon highline-green"><%=don.Status == 0 ? "Mới tạo" : don.Status == 1 ? "Đã xác nhận" : don.Status == 2 ? "Đã gửi" : don.Status == 3 ? "Hoàn thành" : "Không giao được"%></p>
                </td>
                <td class="product-price"><span class="amount">
                        <p><span class="product-coupon highline-red">đ<%=string.Format("{0:0,0}", don.Due) %></span></p>
                    </span></td>
                <td class="product-coupon highline-blue">-<%=don.Subtraction %></td>
                <td class="product-coupon highline-red"><%=don.Addition %></td>
            </tr>
            <%} %>

        </tbody>
    </table>
</section>