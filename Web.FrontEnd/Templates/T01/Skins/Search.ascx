<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSearch.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSearch" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>






<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<h4><img src="/Templates/T01/images/icon.png">Kết quả tìm kiếm: <strong><%=_key%></strong></h4>
<div class="products row">     
  <asp:ListView ID="rpt" runat="server">
    <ItemTemplate>
	<div class=" col-lg-<%=12/ColumnCount %> col-md-<%=12/ColumnCount %> col-sm-<%=12/ColumnCount %>" style="height:<%=HeightProduct > 0 ? HeightProduct + "px" : ""%>">
        <div class="lproduct">
	<a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+ Eval("ID")+"/"+ Eval("Title").ToString().ConvertToUnSign())%>">
	 <img src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>" class="img-service" style="height:<%=HeightImage > 0 ? HeightImage + "px" : "" %>" />
	 <span class="vaucher">&nbsp;</span> 
     <span class="xemsp">
         <span class="sanpham"><%#Eval("PayNumber").ToString() != "0" ? Eval("PayNumber") + " đã mua" : Eval("ViewNumber").ToString() != "0" ? Eval("ViewNumber") + " đã xem" : "Có " + Eval("Quantity") + " sp"%></span>
         <span class="xemngay">Xem ngay</span>
     </span>
	 <h5><%#Eval("Title")%></h5></a>
       <ul>
      <li class="gia"><b><%#Math.Round((Convert.ToDouble(Eval("Price")) - Convert.ToDouble(Eval("Sale")))*100/(Convert.ToDouble(Eval("Price")) == 0 ? 1 : Convert.ToDouble(Eval("Price"))))%>%</b></li>
      <li class="km">
        <div class="chitu"><%#String.Format("{0:0,0đ}", Eval("Price"))%></div>
        <div class="gia_l"><b><%#String.Format("{0:0,0đ}", Eval("Sale"))%></b></div>
      </li>
      <li class="dongho"></li>
      <li class="conhang" data-target="#buyModal" onclick="SelectProduct(<%#Eval("Id")%>)">Mua ngay</li>
      </ul>
            </div>
	</div>	
	</ItemTemplate>
</asp:ListView>   
     <VIT:Pager ID="pager" runat="server"/> 
</div>    

