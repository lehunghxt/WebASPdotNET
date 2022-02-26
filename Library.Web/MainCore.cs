namespace Library.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    using Library.FileHelpers;
    using Library.Images;

    public class MainCore
    {
        /// <summary>
        /// Kiểm tra dữ liệu
        /// </summary>
        /// <param name="DataValidate">Danh sách các control cần kiểm tra</param>
        /// <returns>true|false</returns>
        public static bool checkData(Dictionary<dynamic, List<object>> DataValidate)
        {
            bool isTrue;
            Validate checkError = new Validate();
            foreach (var item in DataValidate)
            {
                // kiểm tra textbox có bắt buộc nhập hay không hoặc đã có dữ liệu hau chưa
                if (item.Value[0].ToString() == "required" || !string.IsNullOrEmpty(item.Key.Text))
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        isTrue = checkError.checkValue(item.Key.Text, item.Value[i].ToString());
                        if (!isTrue) return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// kiểm tra có phải thiết mobile không
        /// </summary>
        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Getting Ip address of local machine
        /// </summary>
        public static string GetLocalIPAddress()
        {
            string strHostName = string.Empty;
            string localIP = "127.0.0.1"; //set a default value
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            foreach (IPAddress ip in addr)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        /// <summary>
        /// Getting Ip address of local machine
        /// </summary>
        public static string GetClientIpAddress()
        {
            try
            {
                var userHostAddress = HttpContext.Current.Request.UserHostAddress;

                // Attempt to parse.  If it fails, we catch below and return "0.0.0.0"
                // Could use TryParse instead, but I wanted to catch all exceptions
                IPAddress.Parse(userHostAddress);

                var xForwardedFor = HttpContext.Current.Request.ServerVariables["X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(xForwardedFor))
                    return userHostAddress;

                // Get a list of public ip addresses in the X_FORWARDED_FOR variable
                var publicForwardingIps = xForwardedFor.Split(',').Where(ip => !IsPrivateIpAddress(ip)).ToList();

                // If we found any, return the last one, otherwise return the user host address
                return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
            }
            catch (Exception)
            {
                // Always return all zeroes for any failure (my calling code expects it)
                return "0.0.0.0";
            }
        }

        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }

        public static string CreateTag(string Tag)
        {
            string[] lst = Tag.Split(',');
            for (int i = 0; i < lst.Length; i++)
            {
                lst[i] = lst[i].Trim().ConvertToUnSign().Replace("-", " ");
            }
            var tag = string.Join(",", lst);
            return tag.Length > 100 ? tag.Substring(0, 100) : tag;
        }

        // kiem tra file
        public static bool checkFile(string FileName, string Exception)
        {
            bool isTrue;
            CheckExtensionFile chkF = new CheckExtensionFile();
            var reguField = Exception.Split('|');
            foreach (string method in reguField)
            {
                isTrue = chkF.CheckValue(FileName, method);
                if (isTrue) return true;
            }
            return false;
        }

        // Capchar Image   
        public static string captChaImage(string text, HatchBrush HatchBrushAnh, HatchBrush HatchBrushText, int width = 150, int height = 20)
        {
            CaptchaImage ci = new CaptchaImage();
            ci.Text = text;
            ci.Width = width;
            ci.Height = height;
            ci.FontName = "Arial";

            // cac thong so mau sac nen
            ci.HatchBrushAnh = HatchBrushAnh; // new HatchBrush(HatchStyle.Cross, Color.Blue, Color.Yellow);
            ci.HatchBrusText = HatchBrushText; //new HatchBrush(HatchStyle.ZigZag, Color.Blue, Color.Blue);

            // tao hinh
            ci.GenerateImage();

            // luu hinh
            string strBase64 = new EncodeImage().BitmapBase64Src(ci.Image);
            ci.Dispose();

            return strBase64;
        }

        #region Excel Support
        /// <summary>
        /// Xuất 1 Datatable ra excel
        /// </summary>
        /// <param name="dtInput"></param>
        /// <param name="strFileName"></param>
        public static void ExportToExcel(DataTable dtInput, string strFileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            HttpContext.Current.Response.Charset = "utf-8";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.Html32TextWriter htmlWrite = new System.Web.UI.Html32TextWriter(stringWrite);
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

            DataGrid grvTemp = new DataGrid();
            grvTemp.Width = Unit.Percentage(100);
            grvTemp.HeaderStyle.Font.Bold = true;
            grvTemp.DataSource = dtInput;
            grvTemp.DataBind();
            grvTemp.RenderControl(htmlWrite);

            HttpContext.Current.Response.Write(stringWrite.ToString());
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// Xuất file Excel có 1 hoặc nhiều sheet
        /// dsInput: DataSet
        /// strSheetname: mãng chuỗi tên sheet (tên theo thứ tự của các sheet)
        /// strFileName: Tên file xuất ra(bao gồm cả đuôi mỡ rộng)
        /// </summary>
        /// <param name="dsInput">DataSet đưa vào</param>
        /// <param name="strSheetName">Mãng chuỗi tên các sheet</param>
        /// <param name="strFileName">Tên file cần ghi(bao gồm cả đường dẫn vật lý)</param>
        public static void ExportToExcel(DataSet dsInput, string[] strSheetName, string strFileName)
        {
            int intDataTableCount = dsInput.Tables.Count;
            if (intDataTableCount <= 0)
                return;
            StringBuilder strExcelXml = new StringBuilder();
            strExcelXml.Append(ExcelHeader());
            strExcelXml.Append(ExcelWorkSheetOptions());
            for (int i = 0; i < intDataTableCount; i++)
            {
                DataTable dtIndex = dsInput.Tables[i];
                int intdtIndexRowsCount = dtIndex.Rows.Count;
                int intdtIndexColumnCount = dtIndex.Columns.Count;

                strExcelXml.Append("<Worksheet ss:Name=\"" + strSheetName[i] + "\">");

                strExcelXml.Append("<Table>");
                strExcelXml.Append("<tr>");
                for (int j = 0; j < intdtIndexColumnCount; j++)
                    strExcelXml.Append("<td>" + dtIndex.Columns[j].ColumnName + "</td>");
                strExcelXml.Append("</tr>");
                for (int j = 0; j < intdtIndexRowsCount; j++)
                {
                    strExcelXml.Append("<tr>");
                    for (int k = 0; k < intdtIndexColumnCount; k++)
                        strExcelXml.Append("<td>" + dtIndex.Rows[j][k].ToString().Replace("<", " ").Replace(">", " ") + "</td>");
                    strExcelXml.Append("</tr>");
                }
                strExcelXml.Append("</Table>");
                strExcelXml.Append("</Worksheet>\n");
            }
            strExcelXml.Append("</Workbook>\n");
            #region "Write Into File"
            try
            {
                System.IO.File.Delete(strFileName);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(strFileName, true, System.Text.Encoding.Unicode);
                sw.Write(ConvertHTMLToExcelXML(strExcelXml.ToString()));
                sw.Close();
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
            #endregion
        }
        /// <summary>
        /// Xuất ra Header XML để xuất file Excel
        /// </summary>
        /// <returns></returns>
        public static string ExcelHeader()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\"?>\n");
            builder.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
            builder.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            builder.Append("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            builder.Append("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
            builder.Append("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            builder.Append("xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
            builder.Append("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            builder.Append("<Author>ducson@gmail.com</Author>");
            builder.Append("</DocumentProperties>");
            builder.Append("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
            builder.Append("<ProtectStructure>False</ProtectStructure>\n");
            builder.Append("<ProtectWindows>False</ProtectWindows>\n");
            builder.Append("</ExcelWorkbook>\n");
            return builder.ToString();
        }
        /// <summary>
        ///Tạo ra WorksheetOptions để xuất file Excel
        /// </summary>
        /// <returns></returns>
        public static string ExcelWorkSheetOptions()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n<Selected/>\n </WorksheetOptions>\n");
            return builder.ToString();
        }
        /// <summary>
        /// Chuyển code HTML -> XML
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ConvertHTMLToExcelXML(string strHtml)
        {
            strHtml = strHtml.Replace("<tr>", "<Row>").Replace("</tr>", "</Row>").Replace("<td>", "<Cell><Data ss:Type=\"String\">").Replace("</td>", "</Data></Cell>");
            return strHtml;
        }
        #endregion

        /// <summary>
        /// Hàm lấy số nguyên request của một key, nếu lỗi trả về giá trị defult
        /// </summary>
        /// <param name="RequestKey">Key chứa giá trị trong request</param>
        /// <param name="intDefaultValue">Giá trị mặc định nếu lỗi</param>
        /// <returns></returns>
        static public int GetIntRequest(String RequestKey, int intDefaultValue)
        {
            try
            {
                if (HttpContext.Current.Request[RequestKey] == null)
                    throw new Exception();

                return Convert.ToInt32(HttpContext.Current.Request[RequestKey].ToString());
            }
            catch
            {
                return intDefaultValue;
            }
        }

        /// <summary>
        /// Hàm lấy chuỗi request của một key, nếu lỗi trả về giá trị defult
        /// </summary>
        /// <param name="RequestKey">Key chứa giá trị trong request</param>
        /// <param name="strDefaultValue">Giá trị mặc định nếu lỗi</param>
        /// <returns></returns>
        static public String GetStrRequest(String RequestKey, String strDefaultValue)
        {
            try
            {
                return HttpContext.Current.Request[RequestKey].ToString();
            }
            catch
            {
                return strDefaultValue;
            }
        }

        /// <summary>
        /// Hàm thực hiện download một file đầy đủ đường dẫn
        /// </summary>
        /// <param name="strFilePath">Đường dẫn file đầy đủ</param>
        /// <returns></returns>
        public static bool DownloadFile(String strFilePath)
        {
            return DownloadFile(strFilePath, new System.IO.FileInfo(strFilePath).Name);
        }
        private static string FilterSpecialFilenameChar(string fileName)
        {
            return fileName.Replace(";", "_").Replace("&", "_").Replace("#", "");
        }
        public static bool DownloadFile(String strFilePath, String strOutputName)
        {
            if (!File.Exists(strFilePath))
                return false;
            //strOutputName = FilterVietkey(strOutputName).Replace(" ", "_");
            try
            {
                HttpContext.Current.Response.Clear();

                // Specify the Type of the downloadable file.
                HttpContext.Current.Response.ContentType = "application/octet-stream";

                //if (strOutputName.Length < 22)
                if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") >= 0)
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + HttpContext.Current.Server.UrlPathEncode(FilterSpecialFilenameChar(strOutputName)) + "\"");
                else
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + FilterSpecialFilenameChar(strOutputName) + "\"");
                //else
                //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + strOutputName.Substring(0, 18) + System.IO.Path.GetExtension(strFileName) + "\"");

                HttpContext.Current.Response.Flush();

                // Download the file.
                HttpContext.Current.Response.WriteFile(strFilePath);
            }
            catch (Exception objExc)
            {
                // thong bao loi
                return false;
            }
            finally
            {
                HttpContext.Current.Response.End();
            }
            return true;
        }

        /// <summary>
        /// Hàm thực hiện upload một file lên server
        /// </summary>
        /// <param name="fileUpload">Đối tượng FileUpload</param>
        /// <param name="strPath">Đường dẫn tới thư mục chứa file không có \ ở cuối</param>
        /// <param name="bolOverWrite">Cho phép ghi đè lên hay không</param>
        /// <param name="bolIncludeCreatedDate">Tên file có đính kèm định dạng ngày tháng ở đầu file không</param>
        /// <returns>Thành công: Trả về tên file thực sự được lưu xuống, thất bại trả về ""</returns>
        public static String UploadFile(FileUpload fileUpload, String strPath, bool bolOverWrite, bool bolIncludeCreatedDate)
        {
            String strFileName = "";
            try
            {
                strFileName = FilterSpecialFilenameChar(fileUpload.FileName).FilterVietkey();
                if (strFileName.Equals(""))
                    return "";

                if (bolIncludeCreatedDate)
                    strFileName = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss_") + strFileName;

                if (strPath.LastIndexOf("\\") != strPath.Length - 1)
                    strPath += "\\";

                //Nếu chọn ghi đè thì xóa file cũ đi
                if (bolOverWrite && File.Exists(strPath + strFileName))
                    File.Delete(strPath + strFileName);
                else
                    while (File.Exists(strPath + strFileName))
                        strFileName = "0" + strFileName;

                strFileName = strFileName.Replace(" ", "_");
                //Lưu file
                fileUpload.SaveAs(strPath + strFileName);
            }
            catch (Exception objExce)
            {
                // loi
                return "";
            }
            return strFileName;
        }

        // lay ra so thu tu cua ngay hien tai theo tuan/thang/nam
        public static int GetDayByTemplateConfig(string type)
        {
            switch (type)
            {
                case "Week": return Convert.ToInt16(DateTime.Now.DayOfWeek);
                case "Month": return DateTime.Now.Day;
                case "Year": return DateTime.Now.DayOfYear;
                default: return 1;
            }
        }
    }
}