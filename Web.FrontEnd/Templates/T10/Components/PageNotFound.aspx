<%@ Page Language="C#" AutoEventWireup="true" Inherits="Web.Asp.UI.VITComponent" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>

<asp:content id="HeadContent" contentplaceholderid="HeadContent" runat="server">
    <title><%=HREF.Domain%> :: Thông báo lỗi - Không tìm thấy liên kết</title>
    <meta name="keywords" content="Lỗi 404, 404 error, PageNotFound"/>
    <meta name="description" content="Lỗi 404, 404 error, PageNotFound"/>
    <meta name="title" content="<%=HREF.Domain%> :: Thông báo lỗi - Không tìm thấy liên kết"/>
    <link href="<%=HREF.DomainStore%>includes/error.css" rel="stylesheet" type="text/css" media="screen" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:content>
<asp:content id="MainContent" contentplaceholderid="MainContent" runat="server">
        <div id="content_top">
            <VIT:Position runat="server" ID="ContentTop"></VIT:Position>
        </div>
        <div id="content_middle">
            <div id="content_left">
                <VIT:Position runat="server" ID="ContentLeft"></VIT:Position>
            </div>
            <div id="content_center">
                
                <div class="fof-container">
                    <i class="fof-ico"></i>
                    <p class="fof-emotion">bz.bzbz ..bz bzbz .  bz . .bz bzbz  ... .... bzbzbz ..bz .bz.. bz..  bzbz. bzbzbz   . .bz bz  ... .... .. bz</p>
                    <p class="fof-note">Không tìm thấy trang này!</p>
                    <p>
                        <span class="btn-buy">
                            <a href="<%=HREF.DomainLink %>" name="go-home">
                                <i class="left"></i>
                                <i class="cen">Đến trang chủ</i>
                                <i class="right"></i>
                            </a>
                        </span>
                    </p>
                </div>

                <VIT:Position runat="server" ID="ContentMiddle"></VIT:Position>
            </div>
            <div id="content_right">
                <VIT:Position runat="server" ID="ContentRight"></VIT:Position>
            </div>
        </div>
        <div id="content_bottom">
            <VIT:Position runat="server" ID="ContentBottom"></VIT:Position>
        </div>
</asp:content>