<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>
<%@ Import Namespace="Web.Asp.Provider"%>

    <div class="container" style="margin-bottom:50px">
                   <%if(Source == "LIN"){ %>
                  <div class="wthree-about-txt mb-lg-5 mb-md-4 mb-3">
                     <h3 style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>






                  </div>
                  <div class="about-para-txt">
                       <div class="row">
                           <div class="col-lg-12 col-md-12">
                               <ul class="menu" style="color:#000">
                                    <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
                    <li style="list-style-type:none">
                        <% var param = Source != "ATR" ? RederectSendKey + "/" + Data[i].ID
                                          : RederectSendKey + "Id/" + Data[i].CategoryId + "/" + RederectSendKey + "Vl/" + Data[i].ID; %>

                        <a class="simpleimg" href="<%=Data[i].URL != null ? Data[i].URL : HREF.LinkComponent(RederectComponent, param + "/" + Data[i].Title.ConvertToUnSign())%>" title="<%=Data[i].Description%>">
                                    <img src="<%=Data[i].ImagePath%>" class="img-responsive" alt="<%=Data[i].Title%>"  style='margin-bottom:10px;<%=Height > 0 ? "Height:" + Height + "px;": ""%>'></a>
                    </li>
								<%}%>

								</ul>
                               <div class="clearfix"></div>
                               </div>
                           </div>
                  </div>
                   <%} else if(Source == "MID"){ %>
                  <div class="wthree-about-txt mb-lg-5 mb-md-4 mb-3" style="margin-top:50px">
                     <h3 class="title text-center mb-md-4 mb-sm-3 mb-3 mb-2" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background-color:" + this.GetValueParam<string>("HeaderBackground") %><%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") == 0 ? "" : ";font-size:" + this.GetValueParam<int>("HeaderFontSize") + "px"%>"><%=Title %></h3>
                  <div class="title-wls-text text-center mb-lg-5 mb-md-4 mb-sm-4 mb-3">
               <p><%=Category.Description %></p>
            </div>
                  </div>
                  <div class="row gallery-info">
                       <%for (int i = 0; i < this.Data.Count; i++)
                           {%>
                      <div class="col-lg-3 col-md-3 col-sm-6 gallery-img-grid p-0">
                  <div class="gallery-grids">
                     <a href="#gal<%=i%>"><img src="<%=Data[i].ImagePath%>" alt="<%=Data[i].Title%>" class="img-fluid"></a>
                  </div>
               </div>

                      <div id="gal<%=i%>" class="popup-effect">
         <div class="popup">
            <img src="<%=Data[i].ImagePath%>" alt="<%=Data[i].Title%>" class="img-fluid">
            <a class="close" href="#gallery">×</a>
         </div>
      </div>
                      <%} %>
                  </div>
                   <%}%>
         </div>