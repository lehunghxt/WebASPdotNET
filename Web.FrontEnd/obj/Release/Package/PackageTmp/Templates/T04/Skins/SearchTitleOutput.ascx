<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SearchTitleOutput.ascx.cs" Inherits="Web.FrontEnd.Modules.SearchTitleOutput" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="row">
	<asp:ListView ID="ListView" runat="server">
        <ItemTemplate>
            <div class="product col-lg-3 col-md-3 col-sm-3" >
                <div class="product-image" >
			        <a href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
                        <img id="bay<%#Eval("Id") %>" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>"></a>






		        </div>
											
		        <div class="product-info">
			        <h5><a href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>"><%#Eval("Title")%></a></h5>			        
		        </div>
            </div>
        </ItemTemplate>
    </asp:ListView>		
</div>


    <div class="row">
        <div class="paging pagination">
          <VIT:Pager ID="pager" runat="server" OnPagerCommand="pager_PagerCommand"/>
        </div>
    </div>