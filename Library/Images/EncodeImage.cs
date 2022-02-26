using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Library.Images
{
    public class EncodeImage
    {
        #region Image
        /* Encode Image to Base64String JPEG*/
        public string ImageToBase64(Image image)
        {
            ImageFormat format = new ImageFormat(new Guid("b96b3cae-0728-11d3-9d7b-0000f81ef32e"));
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        
        /* Encode Image to Base64String with manual format*/
        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /* Encode Image to Base64String Source JPEG*/
        /* <img src="ImageBase64Src()"/> */
        public string ImageBase64Src(Image image)
        {
            return "data:image/jpg;base64," + ImageToBase64(image);
        }

        /* Encode Image to Base64String Source with manual format */
        /* <img src="ImageBase64Src()"/> */
        public string ImageBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            return "data:image/jpg;base64," + ImageToBase64(image, format);
        }

        /* Decode Base64String to Image */
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        #endregion

        #region Bitmap
        /* Encode Image to Base64String JPEG*/
        public string BitmapToBase64(Bitmap bitmap)
        {
            System.Drawing.Imaging.ImageFormat format = new System.Drawing.Imaging.ImageFormat(new Guid("b96b3cae-0728-11d3-9d7b-0000f81ef32e"));
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                bitmap.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /* Encode Image to Base64String with manual format*/
        public string BitmapToBase64(Bitmap image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /* Encode Image to Base64String Source JPEG*/
        /* <img src="BitmapBase64Src()"/> */
        public string BitmapBase64Src(Bitmap bitmap)
        {
            return "data:image/jpg;base64," + BitmapToBase64(bitmap);
        }

        /* Encode Image to Base64String Source with manual format */
        /* <img src="BitmapBase64Src()"/> */
        public string BitmapBase64Src(Bitmap bitmap, System.Drawing.Imaging.ImageFormat format)
        {
            return "data:image/jpg;base64," + BitmapToBase64(bitmap, format);
        }
        #endregion
    }
}
