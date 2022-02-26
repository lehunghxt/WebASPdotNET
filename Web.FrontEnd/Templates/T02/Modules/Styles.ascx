<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Styles.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Styles" %>
<%@ Import Namespace="VIT.Library" %>

<div class="field-groups" style="padding-left:20px;<%=string.IsNullOrEmpty(this.BackgroundColor) ? "" : ";background-color:" + this.BackgroundColor %><%=string.IsNullOrEmpty(this.FontColor) ? "" : ";color:" + this.FontColor %>">
<asp:Repeater ID="rptData" runat="server">
    <ItemTemplate>
        <div class="form-row">
            <%if(this.GetValueParam<bool>("DisplayImage")) {%>
                <img class="styles" src="<%#Eval("ImageThumbPath")%>" alt="<%#Eval("Name")%>"/>
            <%} %>
            <a href="<%#this.GetURL(Convert.ToInt32(Eval("Id")), Eval("Name").ToString())%>" title="<%#Eval("Description")%>">
                <input type="radio" id="<%#Eval("id")%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%#Convert.ToInt32(Eval("Id")) == this.CurrentId ? "checked='checked'" : "" %>/>
                <label class="form-label"><%#Eval("Name")%></label>
            </a>






        </div>
    </ItemTemplate>
</asp:Repeater> 
</div>