<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CompanyInfo.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.CompanyInfo" %>

<div style='margin: 10px;<%=WidthModule > 0 ? "Width:" + WidthModule + "px;": ""%><%=HeightModule > 0 ? "Height:" + HeightModule + "px;": ""%>'>






    <div style='display:<%=DisplaySlogan ? "" : "none"%>'><strong><%=_dto.Slogan%></strong></div>
    <div style='float:left;display:<%=DisplayImage ? "" : "none"%>'><img src="<%=_dto.ImagePath%>" alt="<%=_dto.FullName%>" style='<%=WidthImage > 0 ? "Width:" + WidthImage + "px;": ""%><%=HeightImage > 0 ? "Height:" + HeightImage + "px;": ""%>'/></div>
    <div style="text-align:left">
        <div style='display:<%=DisplayShortName ? "" : "none"%>'><strong><%=_dto.DisplayName%></strong></div>
        <div style='display:<%=DisplayFullName ? "" : "none"%>'><strong><%=_dto.FullName%></strong></div>
        <div style='display:<%=DisplayAddress ? "" : "none"%>'>Địa chỉ: <strong><%=_dto.Address%></strong></div>
        <div style='display:<%=DisplayPhone ? "" : "none"%>'>Điện thoại: <strong><%=_dto.Phone%></strong></div>
        <div style='display:<%=DisplayFax ? "" : "none"%>'>Fax: <strong><%=_dto.Fax%></strong></div>
        <div style='display:<%=DisplayEmail ? "" : "none"%>'>Email: <strong><%=_dto.Email%></strong></div>
    </div>
    <div class="space"></div>
    <div style='text-align:justify;display:<%=DisplayAboutUs ? "" : "none"%>'><%=_dto.AboutUs%></div>
</div>

