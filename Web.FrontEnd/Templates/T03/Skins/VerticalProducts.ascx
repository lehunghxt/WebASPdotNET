<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
			<h4 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h4>






		</div>
							
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">

<table class="bestsellers-table">
															
	<asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
            <tr>
		        <td class="product-thumbnail">
                    <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                        <img src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>"/></a></td>
		        <td class="product-info">
			        <p><a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                        <%#Eval("Title")%></a></p>
			        <%#Convert.ToDecimal(Eval("Price")) > 0 ? Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='reduce'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Sale")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Price")) == 0 && Convert.ToDecimal(Eval("Sale")) == 0 ? "<span class='price'>Liên hệ</span>" : ""%> 
		        </td>
	        </tr>
        </ItemTemplate>
    </asp:ListView>
									
</table>


<div class="row" style="display:<%=this._hasPaging ? "" : "none" %>">
    <div class="paging pagination">
        <VIT:Pager ID="pager" runat="server" QueryStringField="p" CssClass="pager"/>
    </div>
</div>
        </div>
    </div>