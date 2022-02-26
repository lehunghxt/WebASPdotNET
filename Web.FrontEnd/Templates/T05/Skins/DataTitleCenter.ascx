<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>


<div class="instagram_top">
				<div class="instagram text-center">
					<h3 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>






				</div>
    
				<ul class="menu" style="color:#000">
                                    <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                    <li class="col-lg-3 col-md-3 col-sm-6" style="list-style-type:none">
                        <% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>
                        <a class="simpleimg" href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Description%>">
                                    <img src="<%=Data[i].ImagePath%>" class="img-responsive" alt="<%=Data[i].Title%>" width="<%=Width == 0 ? "100%" : Width + "px" %>" style='margin-bottom:10px;<%=Height > 0 ? "Height:" + Height + "px;": ""%>'></a>
                    </li>
								<%}%>

								</ul>
    <div class="clearfix"></div>
			</div>


