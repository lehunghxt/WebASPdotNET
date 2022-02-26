<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="Web.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="Library" %>

<div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="single-footer mb-sm-40">
                                <h3 class="footer-title"><%=Title %></h3>
                                <div class="footer-content">
                                    <ul class="footer-list">
                                        <%foreach(var item in this.Data) 
  {%>  	
<li>
                                        <a href="<%=item.URL != null ? item.URL : HREF.LinkComponent(RederectComponent, RederectSendKey + "/"+item.ID+"/"+ item.Title.ConvertToUnSign())%> " title="<%=item.Title%>">
            <%=item.Title%>
        </a>
                                    </li>    
  <%} %>
                                    </ul>






                                </div>
                            </div>
                        </div>

  

                

