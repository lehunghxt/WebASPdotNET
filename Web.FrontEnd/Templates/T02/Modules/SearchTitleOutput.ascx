<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SearchTitleOutput.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.SearchTitleOutput" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>
<%@ Import Namespace="VIT.Library.Web"%>

<div class='row margin0'>
    <asp:ListView ID="ListView" runat="server">
        <ItemTemplate>
            <div class="col-sm-2">
							    <div class="product-image-wrapper">
								    <div class="single-products">
										    <div class="productinfo text-center">
											    <img src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>" />
											    <h2><%#Eval("Title")%></h2>
											    <a href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" class="btn btn-default add-to-cart"><i class="glyphicon glyphicon-globe"></i>
                                                    Xem chi tiết</a>






										    </div>
								    </div>
							    </div>
						    </div> 
        </ItemTemplate>
    </asp:ListView>
<VIT:Pager ID="pager" runat="server" OnPagerCommand="pager_PagerCommand"/>
    </div>