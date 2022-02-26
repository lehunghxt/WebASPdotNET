using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;

namespace Library.Images
{
    public class ResizeImage
    {
        public void Resize(string ImagePath, int withNew, int HeightNews)
        {
            Stream Buffer = new WebClient().OpenRead(ImagePath);
            Image imgInput = Image.FromStream(Buffer);

            //Determine image format 
            ImageFormat fmtImageFormat = imgInput.RawFormat;

            //create new bitmap 
            Bitmap bmpResized = new Bitmap(imgInput, withNew, HeightNews);

            //save bitmap to disk 
            bmpResized.Save(ImagePath, fmtImageFormat);

            //release used resources 
            imgInput.Dispose();
            bmpResized.Dispose();
            Buffer.Close();
        }


        public void Resize(string OldImage, string NewImage, int MaxSideSize)
        {
            int intNewWidth;
            int intNewHeight;
            Stream Buffer = new WebClient().OpenRead(OldImage);
            Image imgInput = Image.FromStream(Buffer);

            //Determine image format 
            ImageFormat fmtImageFormat = imgInput.RawFormat;

            //get image original width and height 
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            //determine if landscape or portrait 
            int intMaxSide;

            if (intOldWidth >= intOldHeight)
            {
                intMaxSide = intOldWidth;
            }
            else
            {
                intMaxSide = intOldHeight;
            }


            if (intMaxSide > MaxSideSize)
            {
                //set new width and height 
                double dblCoef = MaxSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }
            //create new bitmap 
            Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);

            //save bitmap to disk 
            bmpResized.Save(NewImage, fmtImageFormat);

            //release used resources 
            imgInput.Dispose();
            bmpResized.Dispose();
            Buffer.Close();
        } 
    }
}
