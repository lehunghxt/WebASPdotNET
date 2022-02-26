<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Styles.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Styles" %>
<%@ Import Namespace="VIT.Library" %>






<div class="clear"></div>
<ul class="product_list_widget">
<asp:Repeater ID="rptData" runat="server">
    <ItemTemplate>
        <li>
            <a href="<%#HREF.LinkComponent("Product","Products", SettingsManager.Constants.SendStyle + "/"+Eval("Id")+"/"+Eval("Name").ToString().ConvertToUnSign())%>">
                <img width="65" height="65" src="<%#Eval("ImageThumbPath")%>" alt= "<%#Eval("Name")%>"></img> <%#Eval("Name")%>
            </a>
            <span class="amount"><%#Eval("Description")%></span>
        </li>
    </ItemTemplate>
</asp:Repeater>
                                </ul>