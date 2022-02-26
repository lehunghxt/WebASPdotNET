<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <section style="margin: 50px 0px;line-height: 30px;">
    <div class="container">
      <div class="row clearfix">
          <div class="col-lg-9 col-md-9 col-sm-8 col-xs-12">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
              </div>
          <aside class="col-lg-3 col-md-3 col-sm-4 col-xs-12">
              <VIT:Position runat="server" ID="psRight"></VIT:Position>
          </aside>
        </div>
    </div>
  </section>
</asp:Content>