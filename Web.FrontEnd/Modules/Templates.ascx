<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Templates.ascx.cs" Inherits="Web.FrontEnd.Modules.Templates" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

 <div class="row">
        <%foreach (var template in DataTemplates)
            { %>
                        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6 temItem">
                            <div class="row">
                                <div class="col-xs-12 center temImage">
                                    <div style='background-image:url(<%=template.ImageThumbPath%>);'></div>
                                </div>
                                <div class="col-xs-12 center">
                                    <div class="temText"> 
                                         <div class="row"> 
                                              <div class="col-xs-12 center">
                                        <a class="temName" href="http://<%=template.TemplateName %>.vdoni.com" title="Website hiệu quả nhất"><%=template.TemplateName %></a>
                                        <a class="temSelect" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendTemplate + "/"+ template.TemplateName + (this.GetValueRequest<int>(SettingsManager.Constants.SendCompany) > 0 ? "/" + SettingsManager.Constants.SendCompany + "/" + this.GetValueRequest<int>(SettingsManager.Constants.SendCompany) : ""))%>" title="Đăng ký sử dụng website miễn phí">Chọn</a>
                                        <div class="dropdown">
                                        <a class="dropbtn temDemo" href="http://<%=template.IsPublic ? template.TemplateName + ".vdoni.com" : GetDomainByTemplate(template.TemplateName)%>" title="Xem giao diện mẫu <%#Eval("TemplateName") %>">Xem</a>
                                        <div class="dropdown-content">
                                            <div>
                                                <a href="http://<%=template.IsPublic ? Eval("TemplateName") + ".vdoni.com" : GetDomainByTemplate(template.TemplateName)%>" title="Xem giao diện mẫu <%=template.TemplateName %>">Xem mẫu</a>
                                                <a style="float:right" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendTemplate + "/"+template.TemplateName + (this.GetValueRequest<int>(SettingsManager.Constants.SendCompany) > 0 ? "/" + SettingsManager.Constants.SendCompany + "/" + this.GetValueRequest<int>(SettingsManager.Constants.SendCompany) : ""))%>" title="Đăng ký sử dụng website miễn phí với giao diện <%=template.TemplateName %>">(Chọn)</a>
                                            </div>
                                            <%--<%#GetDemos(Container) %>--%>
                                          </div>
                                            </div>
                                                  </div>
                                              </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <%} %>
</div>

<script>
$(document).ready(function() {
    $(".temImage img").hover(
    function () {
        $(this).stop().animate({
            top: '0px'
        }, 'slow');
    },
    function () {
        $(this).stop().animate({
            top: '-100'
        }, 'slow');
    });
});
</script>