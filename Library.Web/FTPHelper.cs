namespace Library.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    public class FTPHelper
    {
        private static string _ftpServerIP = string.Empty;

        /// <summary>
        /// FTP Server IP
        /// </summary>
        public string FTPServerIP
        {
            get
            {
                return _ftpServerIP;
            }
            set
            {
                _ftpServerIP = value;
            }
        }

        private static string _ftpRootPath = string.Empty;

        /// <summary>
        /// FTP Server IP
        /// </summary>
        public string FTPRootPath
        {
            get
            {
                return _ftpRootPath;
            }
            set
            {
                _ftpRootPath = value;
            }
        }

        private static string _ftpUserID = string.Empty;

        /// <summary>
        /// FTP User IP
        /// </summary>
        public string FTPUserID
        {
            get
            {
                return _ftpUserID;
            }
            set
            {
                _ftpUserID = value;
            }
        }

        private static string _ftPassword = string.Empty;

        /// <summary>
        /// FTP User IP
        /// </summary>
        public string FTPassword
        {
            get
            {
                return _ftPassword;
            }
            set
            {
                _ftPassword = value;
            }
        }

        FtpWebRequest _reqFTP;

        private void Connect()
        {
            _reqFTP = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + FTPServerIP + "/" + FTPRootPath));
            _reqFTP.UseBinary = true;
            _reqFTP.Credentials = new NetworkCredential(FTPUserID, FTPassword);
        }
        private void Connect(String path)
        {
            _reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(path));
            _reqFTP.UseBinary = true;
            _reqFTP.Credentials = new NetworkCredential(FTPUserID, FTPassword);
        }

        public FTPHelper()
        {
        }

        public FTPHelper(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            this.FTPServerIP = ftpServerIP;
            this.FTPUserID = ftpUserID;
            this.FTPassword = ftpPassword;
        }

        private string[] GetFileList(string path, string wrMethods)
        {
            var result = new StringBuilder();
            try
            {
                Connect(path);
                _reqFTP.Method = wrMethods;
                var response = _reqFTP.GetResponse();
                var reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append(" ");
                    line = reader.ReadLine();
                }
                // to remove the trailing ' '
                result.Remove(result.ToString().LastIndexOf(' '), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split(' ');
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetDirectoryList()
        {
            var result = new List<string>();

            try
            {
                Connect();
                _reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                var response = _reqFTP.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Add(line);

                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
            }


            return result;
        }
        private bool IsExist(string name)
        {
            var files = GetDirectoryList();

            return files.Contains(name);
        }

        /// <summary>
        /// Lấy danh sách file từ folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFileList(string path)
        {
            return GetFileList("ftp://" + FTPServerIP + "/" + FTPRootPath + path, WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// Lấy danh sách file từ folder root
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return GetFileList("ftp://" + FTPServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// Hàm upload file
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename)
        {
            var fileInf = new FileInfo(filename);
            string uri = "ftp://" + FTPServerIP + "/" + FTPRootPath + fileInf.Name;
            Connect(uri);

            _reqFTP.KeepAlive = false;
            _reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            _reqFTP.ContentLength = fileInf.Length;

            const int buffLength = 2048;
            var buff = new byte[buffLength];

            var fs = fileInf.OpenRead();

            try
            {
                var strm = _reqFTP.GetRequestStream();
                var contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Hàm upload file
        /// </summary>
        public void Upload(string filename, string dir)
        {
            this.MakeDir(dir);

            var fileInf = new FileInfo(filename);

            // Create FtpWebRequest object from the Uri provided
            var reqFTP = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + FTPServerIP + "/" + FTPRootPath + dir + fileInf.Name));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(FTPUserID, FTPassword);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            const int buffLength = 2048;
            var buff = new byte[buffLength];

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            var fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                var strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                var contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Hàm upload file
        /// </summary>
        public void Upload(Stream file, string fileName, string dir)
        {
            var dirs = dir.Split('/');

            var needCheck = true;
            var path = "";
            foreach (var d in dirs)
            {
                if (string.IsNullOrEmpty(path)) path = d;
                else path += "/" + d;
                if (needCheck)
                {
                    if (!IsExist(path))
                    {
                        MakeDir(path);
                        needCheck = false;
                    }
                }
                else
                {
                    MakeDir(path);
                }
            }

            var uri = "ftp://" + FTPServerIP + "/" + FTPRootPath + dir + fileName;
            Connect(uri);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            _reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            _reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Notify the server about the size of the uploaded file
            _reqFTP.ContentLength = file.Length;

            // The buffer size is set to 2kb
            const int buffLength = 2048;
            var buff = new byte[buffLength];

            try
            {
                // Stream to which the file to be upload is written
                var strm = _reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                var contentLen = file.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = file.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                file.Close();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Hàm download file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="errorinfo"></param>
        /// <returns></returns>
        public bool Download(string filePath, string fileName, out string errorinfo)
        {
            try
            {
                var onlyFileName = Path.GetFileName(fileName);

                var newFileName = filePath + @"\" + onlyFileName;

                if (File.Exists(newFileName))
                {
                    errorinfo = string.Format("file không tồn tại", newFileName);
                    return false;
                }

                var url = "ftp://" + FTPServerIP + "/" + fileName;

                Connect(url);

                _reqFTP.Credentials = new NetworkCredential(FTPUserID, FTPassword);

                var response = (FtpWebResponse)_reqFTP.GetResponse();

                var ftpStream = response.GetResponseStream();

                const int bufferSize = 2048;

                var buffer = new byte[bufferSize];

                if (ftpStream != null)
                {
                    var readCount = ftpStream.Read(buffer, 0, bufferSize);

                    var outputStream = new FileStream(newFileName, FileMode.Create);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);

                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }
                    ftpStream.Close();

                    outputStream.Close();
                }

                response.Close();

                errorinfo = "";

                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("Lỗi", ex.Message);
                return false;
            }
        }

        public byte[] Download(string filePath, string fileName)
        {
            var url = "ftp://" + FTPServerIP + "/" + FTPRootPath + filePath + fileName;

            byte[] fileData;
            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(FTPUserID, FTPassword);
                fileData = request.DownloadData(url);
            }
            return fileData;
        }

        /// <summary>
        /// Hàm xóa file
        /// </summary>
        /// <param name="pathFile">Tên file muốn xóa</param>
        public void DeleteFileName(string pathFile)
        {
            try
            {
                var uri = "ftp://" + FTPServerIP + "/" + FTPRootPath + pathFile;
                Connect(uri);

                _reqFTP.KeepAlive = false;
                _reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                var response = (FtpWebResponse)_reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Hàm tạo folder
        /// </summary>
        /// <param name="dirName">Tên filder</param>
        public void MakeDir(string dirName)
        {
            try
            {
                var uri = "ftp://" + FTPServerIP + "/" + FTPRootPath + dirName;
                Connect(uri);

                _reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                var response = (FtpWebResponse)_reqFTP.GetResponse();

                response.Close();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Hàm xóa folder
        /// </summary>
        /// <param name="dirName">Tên filder muốn xóa</param>
        public void DelDir(string dirName)
        {
            try
            {
                var uri = "ftp://" + FTPServerIP + "/" + dirName;
                Connect(uri);
                _reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                var response = (FtpWebResponse)_reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Xem kích thước file
        /// </summary>
        /// <param name="filename">Tên file</param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            long fileSize = 0;
            try
            {
                var fileInf = new FileInfo(filename);
                var uri = "ftp://" + FTPServerIP + "/" + fileInf.Name;
                Connect(uri);
                _reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                var response = (FtpWebResponse)_reqFTP.GetResponse();
                fileSize = response.ContentLength;
                response.Close();
            }
            catch (Exception ex)
            {
            }
            return fileSize;
        }

        /// <summary>
        /// Đổi tên file
        /// </summary>
        /// <param name="currentFilename">Tên cũ</param>
        /// <param name="newFilename">Tên file mới</param>
        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                var fileInf = new FileInfo(currentFilename);
                var uri = "ftp://" + FTPServerIP + "/" + fileInf.Name;
                Connect(uri);
                _reqFTP.Method = WebRequestMethods.Ftp.Rename;
                _reqFTP.RenameTo = newFilename;
                var response = (FtpWebResponse)_reqFTP.GetResponse();

                //Stream ftpStream = response.GetResponseStream();
                //ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public string[] GetFilesDetailList()
        {
            return GetFileList("ftp://" + FTPServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
        }

        public string[] GetFilesDetailList(string path)
        {
            return GetFileList("ftp://" + FTPServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
    }
}
