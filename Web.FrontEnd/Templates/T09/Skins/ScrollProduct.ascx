<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Products" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>

    <ul class="product_list_widget">
    
        <asp:ListView ID="rpt" runat="server">
            <ItemTemplate>
                <li>
                    <a class="simpleimg" href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title") %>">
                        <img src='<%#Eval("ImageThumdPath")%>' alt='<%#Eval("Title") %>' style='<%#(Eval("ImageThumdPath") != null) && Eval("ImageThumdPath").ToString().Length > 0 ? "" : "display:none"%>' width="65" height="65"/>
                    </a>
                    <a class="simpletitle" href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title") %>"><%#Eval("Title")%></a>
                    <span class="amount"><%=Language["Price"] %>: <b><%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0} đ", Eval("Sale")) : (Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0} đ", Eval("Price")) : Language["Contact"])%></b></span>
                </li>
            </ItemTemplate>






        </asp:ListView>
    </ul>

 <p class="paging">
    <VIT:Pager ID="pager" runat="server" QueryStringField="p" Title="Page" Visible="false"/> 
</p>