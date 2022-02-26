<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>


<section class="products_container clearfix m_bottom_25 m_sm_bottom_15">
  <div id="exTab1"> 
    <div class="tab-content clearfix">
      <div class="tab-pane active" id="1a">
          <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
        <div class="product_item col-lg-<%=12/ColumnCount %> col-md-<%=12/ColumnCount %> col-sm-<%=12/ColumnCount %> col-xs-6">
          <figure class="photoframe">
            <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" class="d_block relative pp_wrap">
              <img class="mx-auto" height="200px" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>">
            </a>
            <figcaption>
              <h5 class="m_bottom_10">
                  <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" class="color_dark" title="<%#Eval("Title")%>"><%#Eval("Title")%></a>
              </h5>
              <div class="clearfix">
                <p class="scheme_color f_size_large m_bottom_15">
              <%#Convert.ToDecimal(Eval("Price")) > 0 ? Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='reduce'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : ""%>
              <%#Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Sale")) + "</span>" : ""%>
              <%#Convert.ToDecimal(Eval("Price")) == 0 && Convert.ToDecimal(Eval("Sale")) == 0 ? "<span class='price'>Liên hệ</span>" : ""%> </p>
            </div>
            <input type="button" class="btn" data-toggle="modal" data-target="#buyModal" onclick="SelectProduct('<%#Eval("Id")%>','<%#Eval("Title")%>','<%#Eval("ImagePath")%>','<%#Eval("SaleMin")%>','<%#Eval("Brief").ToString().DeleteHTMLTag().Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Sale"))) : Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Price"))) : "Liên hệ"%>')" value="Thêm vào giỏ"></input>
            </figcaption>
          </figure>
          </div>
          </ItemTemplate>
    </asp:ListView>	
            </div>
          </div>
        </div>
      
    </section>

<div class="row">
    <div class="paging pagination">
        <VIT:Pager ID="pager" runat="server" QueryStringField="p" CssClass="pager"/>
    </div>
</div>