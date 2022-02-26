<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Products" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>

<link rel="stylesheet" type="text/css" href="/templates/t0160/styles/sagscroller.css" />






<script src="/templates/t0160/scripts/sagscroller.js"></script>

<style type="text/css">
    div#mysagscroller{
        width: 270px; /*width of scroller*/
        height: 600px;
        border:0px solid #666666;
    }

    div#mysagscroller ul li{
        height:100%;
    }

    div#mysagscroller ul li p{ text-align:center; font-weight:bold; color:#BCB23D;}

    div#mysagscroller ul li img{
        width:100%;
        display:block;
        border: none;
        padding: 0; margin:0;
        box-shadow: none; 
    }
</style>

<script>
    jQuery(document).ready(function ($) {
        var sagscroller2 = new sagscroller({
            id: 'mysagscroller',
            mode: 'auto',
            pause: 2500,
            animatespeed: 400 //<--no comma following last option
        })
    });
</script>

<div id="mysagscroller" class="sagscroller">
    <ul>    
        <asp:ListView ID="rpt" runat="server">
            <ItemTemplate>
                <li>
                    <a href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title") %>">
                        <img src='<%#Eval("ImageThumdPath")%>' alt='<%#Eval("Title") %>' style='<%#(Eval("ImageThumdPath") != null) && Eval("ImageThumdPath").ToString().Length > 0 ? "" : "display:none"%>'>
                        <p><%#Eval("Title")%></p>
                    </a>
                
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</div>




 <p class="paging">
    <VIT:Pager ID="pager" runat="server" QueryStringField="p" Title="Page" Visible="false"/> 
</p>