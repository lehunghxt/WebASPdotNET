<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>









<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<div class="t_cont">
                <p class="h_t_cont"><%=Title%></p>
</div>
<div class="m_cont">

                <ul class="ul_dv clearfix">
                    <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
                    
                    <li>

                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title")%>">

                            <figure>

                                <img class="img_object_fit" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>">

                            </figure>

                            <h3><%#Eval("Title")%></h3>

                        </a>

                    </li>

                    </ItemTemplate>
    </asp:ListView>
                </ul><!-- End .ul_dv -->

            </div>
    <VIT:Pager ID="pager" runat="server" />

