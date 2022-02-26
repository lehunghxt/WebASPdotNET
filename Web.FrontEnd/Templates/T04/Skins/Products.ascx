<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<div class="products">
                                    <%if (!string.IsNullOrEmpty(Search)){ %>
    <span style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px">Tìm thấy <%=pager.TotalRowCount %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></span>
    <%} else if (!string.IsNullOrEmpty(Title) && Title != ".")
            { %>
<div class="pro_list_title">
                                        <h1 class="cate_title" title="<%=Category.NAME %>">
                                            <%=Title %>
                                        </h1>
                                    </div>
    <%} %>
                                <div style="margin-top:20px">
                                    <div class="w3-row-padding spham">
                                        <%if (ColumnCount == 0) ColumnCount = 3;%>
                                        <asp:ListView ID="rpt" runat="server">
                                            <ItemTemplate>
                                        <div id="product<%#Eval("Id")%>"  class="w3-col m<%=12/ColumnCount %> s6 i6">
                                            <div class="pro_item">
                                                <div style="padding:5px">
                                                    <div class="product-image">
                                                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                                                            <img id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>"'></a>
                                                    </div>
                                                    <div class="pro_name">
                                                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                                                            <%#Eval("Title")%></a>
                                                    </div>
                                                    <%if (this.HasPrice)
                                                { %>
                                                    <div>
                                                        <%#Convert.ToDecimal(Eval("Price")) > 0 ? Convert.ToDecimal(Eval("Sale")) > 0 ? "<del class='reduce'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</del>" : "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Price")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Sale")) > 0 ? "<span class='price'>" + String.Format("{0:0,0}đ", Eval("Sale")) + "</span>" : ""%>
                    <%#Convert.ToDecimal(Eval("Price")) == 0 && Convert.ToDecimal(Eval("Sale")) == 0 ? "<span class='price'>"+ Language["Contact"] +"</span>" : ""%> 
			        <div class="rating readonly-rating" data-score="4"></div>
                                                    </div>
                                                    <%} %>
                                                </div>
                                            </div>
                                            <%if (this.HasOrder)
                                                { %>
                                            <div class="product-actions" style="cursor:pointer" onclick="SelectProduct('<%#Eval("Id")%>','<%#Eval("Title")%>','<%#Eval("ImagePath")%>','<%#Eval("Brief").ToString().DeleteHTMLTag().Replace(",", " ").Replace("'", "\"").Replace("\n", ".").Replace("\r", ".").Trim()%>','<%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Sale"))) : Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0}", Convert.ToDecimal(Eval("Price"))) : "Liên hệ"%>')">
                                                <span style="color:#fff">
                                                    <span>
                                                            <i style="font-size:24px" class="fa"></i>
                                                        <span><%=Language["AddToCart"] %></span>
                                                    </span>
                                                </span>
                                            </div>
                                            <%} %>
                                        </div>
                                            </ItemTemplate>
                                        </asp:ListView>	
                                        <div class="row">
    <div class="paging pagination">
        <VIT:Pager ID="pager" runat="server" QueryStringField="p" CssClass="pager"/>
    </div>
</div>
                                    </div>
                                </div>
                        </div>






