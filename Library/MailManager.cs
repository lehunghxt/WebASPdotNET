using System;
using System.Net.Mail;
using log4net;
using System.IO;

namespace Library
{
    public class MailManager
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MailManager));

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Stream FileStream { get; set; }
        public string FileName { get; set; }

        public void SendEmail()
        {
            string send = string.Empty;
            string[] arr = To.Replace(',', ';').Replace('|', ';').Split(';');

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(From);
            msg.Subject = Title;
            msg.Body = Content;
            msg.IsBodyHtml = true;

            if (FileStream != null && !string.IsNullOrEmpty(FileName))
            {
                Attachment att = new Attachment(FileStream, FileName);
                msg.Attachments.Add(att);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                bool result = regex.IsMatch(arr[i]);
                if (result == false) throw new Exception("Địa chỉ email không hợp lệ.");
                else msg.To.Add(arr[i]);
            }

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host; //Sử dụng SMTP của gmail
                smtp.Port = Port;
                smtp.EnableSsl = EnableSSL;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(From, Password);
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
