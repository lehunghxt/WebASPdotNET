<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

            <div class="t_cont">
                <h1 class="h_t_cont"><%=Title%></h1>
            </div><!-- End .t_cont -->
            <div class="m_cont">
                <ul class="ul_dv clearfix">
                    <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
                                        <li>
                                            <a href="<%#HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                            <figure>
                                <img class="img_object_fit" id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>">
                            </figure>
                            <h3><%#Eval("Title")%></h3>
                        </a>
                    </li>
                            </ItemTemplate>
    </asp:ListView>            
                                    </ul><!-- End .ul_dv --> 
                <div class="page">
                    <div class="PageNum">
    <VIT:Pager ID="pager" runat="server" />
                        </div>
</div>
                <div class="clear"></div>
            </div><!-- End .m_cont -->