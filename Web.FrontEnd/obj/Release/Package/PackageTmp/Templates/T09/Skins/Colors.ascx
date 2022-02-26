<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Colors.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Colors" %>
<%@ Import Namespace="VIT.Library" %>

<ul class="module_colors">
<asp:Repeater ID="rptData" runat="server">
    <ItemTemplate>
        <li>






            <div style="background-color:#<%# Convert.ToInt64(Eval("Value")).ToString("X6")%>"></div>
            <a href="<%#HREF.LinkComponent("Product","Products", SettingsManager.Constants.SendColor + "/"+Eval("Id")+"/"+Eval("Name").ToString().ConvertToUnSign())%>" title="<%#Eval("Description")%>"><%#Eval("Name")%></a>
        </li>
    </ItemTemplate>
</asp:Repeater> 
</ul>