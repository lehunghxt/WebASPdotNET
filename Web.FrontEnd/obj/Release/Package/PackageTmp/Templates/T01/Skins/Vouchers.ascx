<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Vouchers.ascx.cs" Inherits="Web.FrontEnd.Modules.Vouchers" %>


                

<section id="user-ticket" style="display:none">
    <h4><%=Title %></h4>
    <div class="tab-area mb-20">
        <ul>
            <li class="active"><a href="javascript:void(0)">Phiếu quà tặng BMG</a></li>
        </ul>






    </div>
    <%foreach (var phieu in Data)
        { %>
    <div class="item mb-10">
        <div class="row">
            <div class="ml-10 img-cont">
                <%--<a href="javascript:void(0)">
                    <img src="/Templates/T01/img/ticket/ticket.png">
                </a>--%>
            </div>
            <div class="col-md-8">

                <p class="small-black">Hạn sử dụng: Từ <b><%=string.Format("{0:dd/MM/yyyy}", phieu.EffectDate) %></b> đến <b><%=string.Format("{0:dd/MM/yyyy}", phieu.ExpirDate) %></b></p>

                <p class="small-black"> <%=phieu.IsPercent ? "Giảm: " + phieu.Value + "% giá trị đơn hàng" : "Giá trị: " + string.Format("{0:0,0}", phieu.Value) + "đ" %> </p>

                <span class="small-black">Mã quà tặng: <span class="highline-red"><%= phieu.Code %></span></span> <span><a href="javascript:void(0)" class="small-text">Dùng ngay</a></span>
                <p class="small-black">Trạng thái: <span class="highline-green"><%=phieu.EffectDate <= DateTime.Now && DateTime.Now <= phieu.ExpirDate ? "CÓ THỂ SỬ DỤNG" : "HẾT HIỆU LỰC" %> </span></p>

            </div>
        </div>
    </div>
    <%} %>
</section>