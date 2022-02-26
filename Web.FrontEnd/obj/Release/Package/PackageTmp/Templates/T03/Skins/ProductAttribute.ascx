<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductAttribute.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductAttribute" %>

<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %>">
			<h4 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h4>






		</div>
							
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
<ul class="row module_colors <%=this.ComponentProducts%>">
    <%foreach (var item in this.Data)
        {%>
    <li>
        <a href="<%=HREF.LinkComponent(ComponentProducts, SettingsManager.Constants.SendAttributeId + "/" + item.CategoryId + "/" + SettingsManager.Constants.SendAttributeValue + "/" + item.Id + "/" + item.Value.ConvertToUnSign())%>" title="<%=item.Value%>">
                <input type="radio" id="<%=item.Id%>" name="attr[quy-cach-san-pham]" value="60" onclick="return false;" class="input-checkbox filter" <%=item.Id == this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue) ? "checked='checked'" : "" %>/>
                <label class="form-label"><%=item.Value%></label>
            </a>
        </li>
    <%} %>
</ul>
    <div class="clear"></div>
    </div>
</div>



                


                

