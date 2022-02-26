<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <VIT:Position runat="server" ID="psTop"></VIT:Position>
    <section class="f_cont min_wrap clearfix">   
    <article class="content">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </article>
        <style>
.ul_vd_sb > li > a {color: #2f2e2d;}
.ul_vd_sb > li.active > a {font-weight: 700; color:#00923f; font-weight:bold;}
</style>
        <aside class="sidebar">
            <VIT:Position runat="server" ID="psRight"></VIT:Position>
        </aside>

        </section>
</asp:Content>