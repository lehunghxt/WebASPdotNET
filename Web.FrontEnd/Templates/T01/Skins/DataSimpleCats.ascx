<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<div class="row" style="background:white">
    <%for(int i = 0; i < this.Data.Count; i++) 
            {%>
    <div class="col-md-6 col-sm-6" style="text-align:center;width:50%">
                    <div class="single-product">
                        <!-- Product Image Start -->
                        <div class="pro-img">
                             <a title="<%=this.Data[i].Title%>" href="<%=this.Data[i].URL != null ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+ this.Data[i].Title.ConvertToUnSign())%>">
                                <img class="border-img" src="<%=this.Data[i].ImagePath%>" alt="<%=this.Data[i].Title%>">
                            </a>
                            <a title="<%=this.Data[i].Title%>" href="<%=this.Data[i].URL != null ? this.Data[i].URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+this.Data[i].ID+"/"+ this.Data[i].Title.ConvertToUnSign())%>">
                                <%=this.Data[i].Title%>
                            </a>
                        </div>
                        <!-- Product Image End -->
                    </div>
                </div>
            <%} %>	
                
               

            </div>
                                                

                

