namespace Library.FileHelpers
{
    using System.Collections.Generic;
    using System.IO;

    public class CheckExtensionFile
    {
        public bool CheckValue(string value, string methodName)
        {
            var type = this.GetType();
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                //object[] args = new object[] { value };

                var isTrue = (bool)method.Invoke(this, new object[] { value });
                return isTrue;
            }

            return false;
        }

        public static bool isVideo(string FileName)
        {
            var check = new List<string> { ".flv" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }

        public static bool isAudio(string FileName)
        {
            var check = new List<string> { ".mp3" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }

        public static bool isIcon(string FileName)
        {
            var check = new List<string> { ".ico" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }

        public static bool isImage(string FileName)
        {
            var check = new List<string> { ".gif", ".jpg", ".bmp", ".png", ".jpeg" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }

        public static bool isFlash(string FileName)
        {
            var check = new List<string> { ".swf" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }

        public static bool isDocument(string FileName)
        {
            var check = new List<string> { ".doc", ".pdf", ".doc", ".docx", ".xls", ".ppt", ".chm", ".prc" };
            var ex = Path.GetExtension(FileName).ToLower();
            return check.Contains(ex);
        }
    }
}
