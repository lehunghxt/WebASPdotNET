<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Web.FrontEnd.Error" %>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Cảnh báo</title>
	<link rel="stylesheet" href="Includes/Error.css" type="text/css" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</head>
<body>
 <form id="MainForm" runat="server">
	<div align="center">
		<div id="outline">
		    <div id="errorboxoutline">
			    <div id="errorboxheader">
                    Thông báo lỗi
                </div>
			    <div id="errorboxbody">
                <p class="mainerror" id="error_content" runat="server"></p>
			    <p><strong>Bạn không thể truy cập trang này vì:</strong></p>
				    <ol>
					    <li>Bạn không có quyền truy cập trang này.</li>
					    <li>Địa chỉ không đúng.</li>
					    <li>Không tìm thấy tài nguyên được yêu cầu.</li>
					    <li>Một lỗi đã xảy ra khi thực hiện yêu cầu của bạn.</li>
				    </ol>
			    <p><strong>Xin hãy thử một trong những cách sau:</strong></p>
			    <ul>
				    <li><a href="/" title="Đến trang chủ">Về trang chủ</a></li>
                    <li><a href="http://admin.vdoni.com" title="Đến trang chủ">Trang đăng nhập</a></li>
			    </ul>
                <p>Vui lòng liên hệ với người quản trị hệ thống của trang web để biết thông tin chính xác.</p>
			    </div>
		    </div>
		</div>
	</div>
</form>
</body>
</html>
