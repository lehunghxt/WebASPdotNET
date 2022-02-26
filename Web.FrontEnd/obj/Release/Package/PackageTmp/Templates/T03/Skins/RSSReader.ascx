<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/RSSReader.ascx.cs" Inherits="Web.FrontEnd.Modules.RSSReader" %>



<div class="products-row row">
    <div class="col-lg-12 col-md-12 col-sm-12">
							
		<div class="carousel-heading" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("BackgroundColor")) ? "" : ";background-color:" + this.GetValueParam<string>("BackgroundColor") %>">
			<h4 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("FontColor")) ? "" : ";color:" + this.GetValueParam<string>("FontColor") %><%=this.GetValueParam<int>("FontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("FontSize") + "px"%>"><%=Title %></h4>
		</div>
	</div>
    <div class="col-lg-12 col-md-12 col-sm-12 box12titlecontent" style="height:300px; overflow: scroll;">
        <ul class="row module_colors">
            <%if (Feed != null && Feed.Items != null && Feed.Items.Count() > 0)
                    {%>
        <%foreach (var item in Feed.Items)
                    {%>
             <li>
                        <a class="simpletitle" href="<%=item.Links[0].Uri.AbsoluteUri %>" target="_blank" title="<%=item.Title.Text %>">
                        <i class="icons icon-right-dir"></i> <%=item.Title.Text %></a>
                 <div style="text-align:right; font-style: italic;font-size:small"><%=item.PublishDate.ToString("dd/MM/yyyy hh:mm:ss") %></div>
                 <div style="margin-top:10px">
                     <%=item.Summary.Text.Replace("\n","<br />") %>
                 </div>
                 <hr />
            </li>
        
  <%}
                    }%>
            </ul>
        </div>
    </div>