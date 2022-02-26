<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CategoryBackLink.ascx.cs" Inherits="Web.FrontEnd.Modules.CategoryBackLink" %>
<%@ Import Namespace="Library"%>

<div class="breadcrumb-area mt-30">
                                <ul>
                                    <li class="active"><a href="/" target="_blank"><span><i class="fa fa-fw fa-home"></i> Trang
                                                chủ</span></a></li>
                                    <%for (int i = 1; i< Categories.Count; i++)
    { %>
                                    <li><a href="<%=HREF.LinkComponent(GetComponentByType(Categories[i].TargetTag), "sCat/" + Categories[i].ID + "/" + Categories[i].Title.ConvertToUnSign()) %>"><span><i class="fa fa-user"></i> <%=Categories[i].Title %></span></a></li>
    <%} %>
                                    
                                </ul>

                                <!-- Container End -->






                            </div>
