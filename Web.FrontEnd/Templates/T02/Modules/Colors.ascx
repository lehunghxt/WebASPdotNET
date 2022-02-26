<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Colors.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Colors" %>

    <ul class="module_colors" style="<%=string.IsNullOrEmpty(this.BackgroundColor) ? "" : ";background-color:" + this.BackgroundColor %><%=string.IsNullOrEmpty(this.FontColor) ? "" : ";color:" + this.FontColor %>">
    <asp:Repeater ID="rptData" runat="server">
        <ItemTemplate>
            <li style="<%#Convert.ToInt32(Eval("Id")) == this.CurrentId ? "border: dotted 2px blue;" : "" %>">






                <div class="color" style="background-color:#<%# Convert.ToInt64(Eval("Value")).ToString("X6")%>"></div>
                <a href="<%#this.GetURL(Convert.ToInt32(Eval("Id")), Eval("Name").ToString())%>" title="<%#Eval("Description")%>"><%#Eval("Name")%></a>
            </li>
        </ItemTemplate> 
    </asp:Repeater> 
    </ul>