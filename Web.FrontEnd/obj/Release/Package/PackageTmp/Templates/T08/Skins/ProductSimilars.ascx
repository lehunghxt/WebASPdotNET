<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>








<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<section class="team py-lg-4 py-md-3 py-sm-3 py-3" id="team">
         <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
             <%if (!string.IsNullOrEmpty(Title) && Title != ".")
                                                { %>
<h2 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr" title="<%=Title%>" style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px"><%=Title%></h2>
    
             <%} %>
            <div class="row ">
    <asp:ListView ID="rpt" runat="server">
        <ItemTemplate>
           <%if (ColumnCount == 0) ColumnCount = 4; %>
            <div class="team-grid-colum text-center simpleCart_shelfItem col-lg-<%=12/ColumnCount %> col-md-<%=12/ColumnCount %> col-sm-<%=12/ColumnCount %>" style='<%=WidthProduct > 0 ? "Width:" + WidthProduct + "px;": ""%><%=HeightProduct > 0 ? "Height:" + HeightProduct + "px;": ""%>;text-align:center;overflow:hidden;margin-bottom:20px'>
                <div style='<%=WidthImage > 0? "Width:" + WidthImage + "px;": ""%><%=HeightImage > 0 ? "Height:" + HeightImage + "px;": ""%>'>
			        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                        <img id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>" style="max-height:100%; max-width:100%" alt="<%#Eval("Title")%>" class="img-fluid">
                    </a>
		        </div>
                  <div class="text-grid-gried">
                     <h4>
                         <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%#Eval("Title")%></a></h4>
                  </div>               
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
         </div>
      </section>
    <VIT:Pager ID="pager" runat="server" />
