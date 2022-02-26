<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Type.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Type" %>

<div class="field-groups" style="padding-left:20px;<%=string.IsNullOrEmpty(this.BackgroundColor) ? "" : ";background-color:" + this.BackgroundColor %><%=string.IsNullOrEmpty(this.FontColor) ? "" : ";color:" + this.FontColor %>">
<asp:Repeater ID="rptData" runat="server">
    <ItemTemplate>
        <div class="form-row">
            <a href="<%#CreateLink(Eval("Code").ToString(), Eval("Name").ToString())%>" title="<%#Eval("Name")%>">
                <input type="radio" id="<%#Eval("Code")%>" name="attr[loai-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%#Eval("Code").ToString() == this.CurrentId ? "checked='checked'" : "" %>/>
                <label class="form-label"><%#Eval("Name")%></label>
            </a>






        </div>
    </ItemTemplate>
</asp:Repeater> 
</div>