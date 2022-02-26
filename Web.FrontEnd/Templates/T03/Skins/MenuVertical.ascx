<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataComplex.ascx.cs" Inherits="Web.FrontEnd.Modules.DataComplex" %>

<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
			<h4 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize ") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h4>






		</div>
							
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent">
<div class="sidebar-box-content">
<ul class="vmnu">
     <%for (int i = 0; i < this.Model.Count; i++)
     {%>
        <%if (this.Model[i] != null && this.Model[i].Childs.Count > 0)
        {%>
            <li><a href="<%=CreateLink(this.Model[i].Type, false, this.Model[i].ID, this.Model[i].CategoryName)%>" title="<%=this.Model[i].CategoryName%>"><%=this.Model[i].CategoryName%> <i class="icons icon-right-dir"></i></a>
            <ul class="sidebar-dropdown" style="display: none; width: 250px;<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
            <%foreach (var dto in this.Model[i].Childs)
                {%>
                <li style="padding: 5px 10px">
                    <a href="<%=CreateLink(this.Model[i].Type, false, dto.ID, dto.CategoryName)%>" title="<%=dto.CategoryName%>"><%=dto.CategoryName%></a>
                </li>
            <%} %>
            </ul>
                </li>
        <%}
            else
            {%>
            <li><a href="<%=CreateLink(this.Model[i].Type, false, this.Model[i].ID, this.Model[i].CategoryName)%>" title="<%=this.Model[i].CategoryName%>"><%=this.Model[i].CategoryName%> <i class="icons icon-right-dir"></i></a></li>
        <%} %>
    <%} %>
</ul>
</div></div>
    </div>