<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ArticleOrther.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.ArticleOrther" %>
<%@ Import Namespace="VIT.Library"%>
<%@ Import Namespace="VIT.Library.Web" %>

<div class="OrtherArticle">
<asp:Repeater ID="rptfrist" runat="server">
    <ItemTemplate>
         <p title="<%#Eval("Description").ToString().DeleteHTMLTag().Trim()%>">
            <a href="<%#HREF.LinkComponent("Article","Article","sArt/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                <%#Eval("Title")%>
            </a>
        </p>
    </ItemTemplate>






</asp:Repeater>

<asp:Repeater ID="rptlast" runat="server">
   <ItemTemplate>
         <p title="<%#Eval("Description").ToString().DeleteHTMLTag().Trim()%>">
            <a href="<%#HREF.LinkComponent("Article","Article","sArt/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                <%#Eval("Title")%>
            </a>
        </p>
    </ItemTemplate>
</asp:Repeater>
</div>
