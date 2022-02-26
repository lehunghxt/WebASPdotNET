<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Products.ascx.cs" Inherits="Web.FrontEnd.Modules.Products" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<section class="team py-lg-4 py-md-3 py-sm-3 py-3" id="team">
         <div class="container py-lg-5 py-md-4 py-sm-4 py-3">
              <%if (!string.IsNullOrEmpty(Search))
            { %>
        <h2 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr">Tìm thấy <%=this.pager.TotalRowCount %> kết quả cho <strong>"<%=Search.Replace('-', ' ')%>"</strong></h2>
        <%}
    else if (!string.IsNullOrEmpty(Title) && Title != ".")
    { %>
<h1 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2 clr" title="<%=Title%>" style="color:<%=this.GetValueParam<string>("FontColor") %>;font-size:<%=this.GetValueParam<string>("FontSize") %>px"><%=Title%></h1>
    
            <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3 sub-colors">
               <p><%=Category.DESCRIPTION %> </p>
            </div>
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
                     <h5>
                         <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%#Eval("Title")%></a></h5>
                       <%if (HasPrice) 
            { %>
                      <%#Convert.ToDecimal(Eval("Price")) > 0 && Convert.ToDecimal(Eval("Sale")) > 0 
                              ? "<span class='product-row-percent'>"+Math.Round(Convert.ToDouble(((Convert.ToDecimal(Eval("Price")) - Convert.ToDecimal(Eval("Sale")))*100/Convert.ToDecimal(Eval("Price")))))+"%</span>" 
                              : "<span class='product-row-percent'>"+string.Format("{0:0,0}",Math.Round(Convert.ToDouble(Convert.ToDecimal(Eval("Price"))))) +" đ</span>" %>
                      <%} %>
                  </div>               
            </div>
        </ItemTemplate>
    </asp:ListView>
            </div>
         </div>
      </section>
<VIT:Pager ID="pager" runat="server" />