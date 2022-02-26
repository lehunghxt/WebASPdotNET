using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Library.FileHelpers
{
    
    #region Enum

    public enum Dimensions
    {
        Width,
        height
    }

    public enum AnchorPosition
    {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }

    #endregion

    public class ImageUltility
    {
        #region GetImage

        public static Image GetImage(Stream stream)
        {
            try
            {
                return Image.FromStream(stream);
            }
            catch
            {
                return null;
            }
        }

        public static Image GetImage(string filePath)
        {
            try
            {
                return Image.FromFile(filePath);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Crop

        public static Image Crop(Image img, int width, int height, AnchorPosition Anchor)
        {
            if (img == null) return null;

            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)(height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)((height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)(width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)((width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            //grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.SmoothingMode = SmoothingMode.HighSpeed;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //grPhoto.TextRenderingHint = TextRenderingHint.AntiAlias;

            Rectangle destRec = new Rectangle(destX, destY, destWidth, destHeight);
            Rectangle sourceRec = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);

            grPhoto.DrawImage(img,
                              destRec,
                              sourceRec,
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();

            //Quantizer
            //OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
            //bmPhoto = quantizer.Quantize(bmPhoto);

            return bmPhoto;
        }

        public static Image Crop(Image img, int width, int height)
        {
            return Crop(img, width, height, AnchorPosition.Center);
        }

        public static bool Crop(Image img, int width, int height, string fileName)
        {
            bool ret = false;
            Image crop = null;

            try
            {
                crop = Crop(img, width, height, AnchorPosition.Center);
                if (crop != null)
                {
                    crop.Save(fileName);
                    crop.Dispose();
                    if (img != null) img.Dispose();
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (crop != null) crop.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static void Crop(Stream stream, int width, int height, AnchorPosition Anchor, ImageFormat format,
                                Stream ouput)
        {
            Image img = null;
            Image crop = null;

            try
            {
                img = GetImage(stream);
                crop = Crop(img, width, height, Anchor);
                crop.Save(ouput, format);
            }
            finally
            {
                if (crop != null) crop.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Crop(Stream stream, int width, int height, AnchorPosition Anchor, Stream ouput)
        {
            Crop(stream, width, height, Anchor, ImageFormat.Jpeg, ouput);
        }

        public static void Crop(Stream stream, int width, int height, ImageFormat format, Stream ouput)
        {
            Crop(stream, width, height, AnchorPosition.Center, format, ouput);
        }

        public static void Crop(Stream stream, int width, int height, Stream ouput)
        {
            Crop(stream, width, height, AnchorPosition.Center, ImageFormat.Jpeg, ouput);
        }

        public static bool Crop(Stream stream, int width, int height, AnchorPosition Anchor, ImageFormat format,
                                string fileName)
        {
            bool ret = false;
            Image img = null;
            Image crop = null;

            try
            {
                img = GetImage(stream);
                crop = Crop(img, width, height, Anchor);
                crop.Save(fileName, format);
                crop.Dispose();
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (crop != null) crop.Dispose();
                if (img != null) img.Dispose();
            }

            return ret;
        }

        public static bool Crop(Stream stream, int width, int height, AnchorPosition Anchor, string fileName)
        {
            return Crop(stream, width, height, Anchor, ImageFormat.Jpeg, fileName);
        }

        public static bool Crop(Stream stream, int width, int height, ImageFormat format, string fileName)
        {
            return Crop(stream, width, height, AnchorPosition.Center, format, fileName);
        }

        public static bool Crop(Stream stream, int width, int height, string fileName)
        {
            return Crop(stream, width, height, AnchorPosition.Center, ImageFormat.Jpeg, fileName);
        }

        public static void Crop(String filePath, int width, int height, AnchorPosition Anchor, ImageFormat format,
                                Stream ouput)
        {
            Image img = null;
            Image crop = null;

            try
            {
                img = GetImage(filePath);
                crop = Crop(img, width, height, Anchor);
                crop.Save(ouput, format);
            }
            finally
            {
                if (crop != null) crop.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Crop(String filePath, int width, int height, AnchorPosition Anchor, Stream ouput)
        {
            Crop(filePath, width, height, Anchor, ImageFormat.Jpeg, ouput);
        }

        public static void Crop(String filePath, int width, int height, ImageFormat format, Stream ouput)
        {
            Crop(filePath, width, height, AnchorPosition.Center, format, ouput);
        }

        public static void Crop(String filePath, int width, int height, Stream ouput)
        {
            Crop(filePath, width, height, AnchorPosition.Center, ImageFormat.Jpeg, ouput);
        }

        public static bool Crop(String filePath, int width, int height, AnchorPosition Anchor, ImageFormat format,
                                string fileName)
        {
            bool ret = false;
            Image img = null;
            Image crop = null;

            try
            {
                img = GetImage(filePath);
                crop = Crop(img, width, height, Anchor);

                crop.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (crop != null) crop.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool Crop(String filePath, int width, int height, AnchorPosition Anchor, string fileName)
        {
            return Crop(filePath, width, height, Anchor, ImageFormat.Jpeg, fileName);
        }

        public static bool Crop(String filePath, int width, int height, ImageFormat format, string fileName)
        {
            return Crop(filePath, width, height, AnchorPosition.Center, format, fileName);
        }

        public static bool Crop(String filePath, int width, int height, string fileName)
        {
            return Crop(filePath, width, height, AnchorPosition.Center, ImageFormat.Jpeg, fileName);
        }

        #endregion

        #region Resize

        public static Image ResizePercent(Image img, int percent)
        {
            if (img == null) return null;

            float nPercent = ((float)percent / 100);

            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.SmoothingMode = SmoothingMode.HighSpeed;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(img,
                              new Rectangle(destX, destY, destWidth, destHeight),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();

            //Quantizer
            //OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
            //bmPhoto = quantizer.Quantize(bmPhoto);

            return bmPhoto;
        }

        public static bool ResizePercent(Image img, int percent, string fileName)
        {
            bool ret = false;
            Image resize = null;

            try
            {
                resize = ResizePercent(img, percent);
                if (resize != null)
                {
                    resize.Save(fileName);
                    resize.Dispose();
                    if (img != null) img.Dispose();
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static void ResizePercent(Stream stream, int percent, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(stream);
                resize = ResizePercent(img, percent);

                resize.Save(ouput, format);
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void ResizePercent(Stream stream, int percent, Stream ouput)
        {
            ResizePercent(stream, percent, ImageFormat.Jpeg, ouput);
        }

        public static bool ResizePercent(Stream stream, int percent, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(stream);
                resize = ResizePercent(img, percent);

                resize.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool ResizePercent(Stream stream, int percent, string fileName)
        {
            return ResizePercent(stream, percent, ImageFormat.Jpeg, fileName);
        }

        public static void ResizePercent(String filePath, int percent, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(filePath);
                resize = ResizePercent(img, percent);

                resize.Save(ouput, format);
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void ResizePercent(String filePath, int percent, Stream ouput)
        {
            ResizePercent(filePath, percent, ImageFormat.Jpeg, ouput);
        }

        public static bool ResizePercent(String filePath, int percent, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(filePath);
                resize = ResizePercent(img, percent);

                resize.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool ResizePercent(String filePath, int percent, string fileName)
        {
            return ResizePercent(filePath, percent, ImageFormat.Jpeg, fileName);
        }

        public static Image ResizeFixed(Image img, int width, int height)
        {
            if (img == null) return null;

            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)((width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)((height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.SmoothingMode = SmoothingMode.HighSpeed;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(img,
                              new Rectangle(destX, destY, destWidth, destHeight),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();

            //Quantizer
            //OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
            //bmPhoto = quantizer.Quantize(bmPhoto);

            return bmPhoto;
        }

        public static bool ResizeFixed(Image img, int width, int height, string fileName)
        {
            bool ret = false;
            Image resize = null;

            try
            {
                resize = ResizeFixed(img, width, height);
                if (resize != null)
                {
                    resize.Save(fileName);
                    resize.Dispose();
                    if (img != null) img.Dispose();
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static void ResizeFixed(Stream stream, int width, int height, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(stream);
                resize = ResizeFixed(img, width, height);

                resize.Save(ouput, format);
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void ResizeFixed(Stream stream, int width, int height, Stream ouput)
        {
            ResizeFixed(stream, width, height, ImageFormat.Jpeg, ouput);
        }

        public static bool ResizeFixed(Stream stream, int width, int height, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(stream);
                resize = ResizeFixed(img, width, height);

                resize.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool ResizeFixed(Stream stream, int width, int height, string fileName)
        {
            return ResizeFixed(stream, width, height, ImageFormat.Jpeg, fileName);
        }

        public static void ResizeFixed(String filePath, int width, int height, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(filePath);
                resize = ResizeFixed(img, width, height);

                resize.Save(ouput, format);
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void ResizeFixed(String filePath, int width, int height, Stream ouput)
        {
            ResizeFixed(filePath, width, height, ImageFormat.Jpeg, ouput);
        }

        public static bool ResizeFixed(String filePath, int width, int height, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image resize = null;

            try
            {
                img = GetImage(filePath);
                resize = ResizeFixed(img, width, height);

                resize.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (resize != null) resize.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool ResizeFixed(String filePath, int width, int height, string fileName)
        {
            return ResizeFixed(filePath, width, height, ImageFormat.Jpeg, fileName);
        }

        #endregion

        #region Rotate

        public static Bitmap Rotate(Image img, float angle)
        {
            if (img == null) return null;

            const double pi2 = Math.PI / 2.0;

            // Why can't C# allow these to be const, or at least readonly
            // *sigh*  I'm starting to talk like Christian Graus :omg:
            double oldWidth = (double)img.Width;
            double oldHeight = (double)img.Height;

            // Convert degrees to radians
            double theta = ((double)angle) * Math.PI / 180.0;
            double locked_theta = theta;

            // Ensure theta is now [0, 2pi)
            while (locked_theta < 0.0)
                locked_theta += 2 * Math.PI;

            double newWidth, newHeight;
            int nWidth, nHeight; // The newWidth/newHeight expressed as ints

            #region Explaination of the calculations

            /*
			 * The trig involved in calculating the new width and height
			 * is fairly simple; the hard part was remembering that when 
			 * PI/2 <= theta <= PI and 3PI/2 <= theta < 2PI the width and 
			 * height are switched.
			 * 
			 * When you rotate a rectangle, r, the bounding box surrounding r
			 * contains for right-triangles of empty space.  Each of the 
			 * triangles hypotenuse's are a known length, either the width or
			 * the height of r.  Because we know the length of the hypotenuse
			 * and we have a known angle of rotation, we can use the trig
			 * function identities to find the length of the other two sides.
			 * 
			 * sine = opposite/hypotenuse
			 * cosine = adjacent/hypotenuse
			 * 
			 * solving for the unknown we get
			 * 
			 * opposite = sine * hypotenuse
			 * adjacent = cosine * hypotenuse
			 * 
			 * Another interesting point about these triangles is that there
			 * are only two different triangles. The proof for which is easy
			 * to see, but its been too long since I've written a proof that
			 * I can't explain it well enough to want to publish it.  
			 * 
			 * Just trust me when I say the triangles formed by the lengths 
			 * width are always the same (for a given theta) and the same 
			 * goes for the height of r.
			 * 
			 * Rather than associate the opposite/adjacent sides with the
			 * width and height of the original bitmap, I'll associate them
			 * based on their position.
			 * 
			 * adjacent/oppositeTop will refer to the triangles making up the 
			 * upper right and lower left corners
			 * 
			 * adjacent/oppositeBottom will refer to the triangles making up 
			 * the upper left and lower right corners
			 * 
			 * The names are based on the right side corners, because thats 
			 * where I did my work on paper (the right side).
			 * 
			 * Now if you draw this out, you will see that the width of the 
			 * bounding box is calculated by adding together adjacentTop and 
			 * oppositeBottom while the height is calculate by adding 
			 * together adjacentBottom and oppositeTop.
			 */

            #endregion

            double adjacentTop, oppositeTop;
            double adjacentBottom, oppositeBottom;

            // We need to calculate the sides of the triangles based
            // on how much rotation is being done to the bitmap.
            //   Refer to the first paragraph in the explaination above for 
            //   reasons why.
            if ((locked_theta >= 0.0 && locked_theta < pi2) ||
                (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2)))
            {
                adjacentTop = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
                oppositeTop = Math.Abs(Math.Sin(locked_theta)) * oldWidth;

                adjacentBottom = Math.Abs(Math.Cos(locked_theta)) * oldHeight;
                oppositeBottom = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
            }
            else
            {
                adjacentTop = Math.Abs(Math.Sin(locked_theta)) * oldHeight;
                oppositeTop = Math.Abs(Math.Cos(locked_theta)) * oldHeight;

                adjacentBottom = Math.Abs(Math.Sin(locked_theta)) * oldWidth;
                oppositeBottom = Math.Abs(Math.Cos(locked_theta)) * oldWidth;
            }

            newWidth = adjacentTop + oppositeBottom;
            newHeight = adjacentBottom + oppositeTop;

            nWidth = (int)Math.Ceiling(newWidth);
            nHeight = (int)Math.Ceiling(newHeight);

            Bitmap rotatedBmp = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // This array will be used to pass in the three points that 
                // make up the rotated image
                Point[] points;

                /*
                 * The values of opposite/adjacentTop/Bottom are referring to 
                 * fixed locations instead of in relation to the
                 * rotating image so I need to change which values are used
                 * based on the how much the image is rotating.
                 * 
                 * For each point, one of the coordinates will always be 0, 
                 * nWidth, or nHeight.  This because the Bitmap we are drawing on
                 * is the bounding box for the rotated bitmap.  If both of the 
                 * corrdinates for any of the given points wasn't in the set above
                 * then the bitmap we are drawing on WOULDN'T be the bounding box
                 * as required.
                 */
                if (locked_theta >= 0.0 && locked_theta < pi2)
                {
                    points = new Point[]
                        {
                            new Point((int) oppositeBottom, 0),
                            new Point(nWidth, (int) oppositeTop),
                            new Point(0, (int) adjacentBottom)
                        };
                }
                else if (locked_theta >= pi2 && locked_theta < Math.PI)
                {
                    points = new Point[]
                        {
                            new Point(nWidth, (int) oppositeTop),
                            new Point((int) adjacentTop, nHeight),
                            new Point((int) oppositeBottom, 0)
                        };
                }
                else if (locked_theta >= Math.PI && locked_theta < (Math.PI + pi2))
                {
                    points = new Point[]
                        {
                            new Point((int) adjacentTop, nHeight),
                            new Point(0, (int) adjacentBottom),
                            new Point(nWidth, (int) oppositeTop)
                        };
                }
                else
                {
                    points = new Point[]
                        {
                            new Point(0, (int) adjacentBottom),
                            new Point((int) oppositeBottom, 0),
                            new Point((int) adjacentTop, nHeight)
                        };
                }

                g.DrawImage(img, points);
            }

            //Quantizer
            //OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
            //rotatedBmp = quantizer.Quantize(rotatedBmp);

            return rotatedBmp;
        }

        public static bool Rotate(Image img, float angle, string fileName)
        {
            bool ret = false;
            Image rotate = null;
            try
            {
                rotate = Rotate(img, angle);
                if (rotate != null)
                {
                    rotate.Save(fileName);
                    rotate.Dispose();
                    if (img != null) img.Dispose();
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (img != null) img.Dispose();
                if (rotate != null) rotate.Dispose();
            }
            return ret;
        }

        public static void Rotate(Stream stream, float angle, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image rotate = null;
            try
            {
                img = GetImage(stream);
                rotate = Rotate(img, angle);

                rotate.Save(ouput, format);
            }
            finally
            {
                if (rotate != null) rotate.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Rotate(Stream stream, float angle, Stream ouput)
        {
            Rotate(stream, angle, ImageFormat.Jpeg, ouput);
        }

        public static bool Rotate(Stream stream, float angle, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image rotate = null;
            try
            {
                img = GetImage(stream);
                rotate = Rotate(img, angle);

                rotate.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (rotate != null) rotate.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool Rotate(Stream stream, float angle, string fileName)
        {
            return Rotate(stream, angle, ImageFormat.Jpeg, fileName);
        }

        public static void Rotate(String filePath, float angle, ImageFormat format, Stream ouput)
        {
            Image img = null;
            Image rotate = null;
            try
            {
                img = GetImage(filePath);
                rotate = Rotate(img, angle);

                rotate.Save(ouput, format);
            }
            finally
            {
                if (rotate != null) rotate.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Rotate(String filePath, float angle, Stream ouput)
        {
            Rotate(filePath, angle, ImageFormat.Jpeg, ouput);
        }

        public static bool Rotate(String filePath, float angle, ImageFormat format, string fileName)
        {
            bool ret = false;
            Image img = null;
            Image rotate = null;
            try
            {
                img = GetImage(filePath);
                rotate = Rotate(img, angle);

                rotate.Save(fileName, format);
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (rotate != null) rotate.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool Rotate(String filePath, float angle, string fileName)
        {
            return Rotate(filePath, angle, ImageFormat.Jpeg, fileName);
        }

        #endregion

        #region Thumbnail

        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static Image Thumbnail(Image img, int width, int height, bool keepScale)
        {
            if (img == null) return null;

            Image thumbNailImg = null;
            try
            {
                //
                Image.GetThumbnailImageAbort dummyCallBack =
                    new Image.GetThumbnailImageAbort(ThumbnailCallback);

                //img width & height
                int imageHeight = height;
                int imageWidth = width;
                int iHeight = (img.Height * imageWidth) / img.Width;
                if (keepScale) imageHeight = iHeight;

                //Create Thumbnail
                thumbNailImg =
                    img.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);

                //Quantizer
                //OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
                //thumbNailImg = quantizer.Quantize(thumbNailImg);
            }
            catch //(Exception ex)
            {
                //HttpContext.Current.Response.Write("An error occurred - " + ex.ToString());
            }

            return thumbNailImg;
        }

        public static bool Thumbnail(Image img, int width, int height, bool keepScale, string fileName)
        {
            bool ret = false;
            Image thumb = null;
            try
            {
                thumb = Thumbnail(img, width, height, keepScale);
                if (thumb != null)
                {
                    thumb.Save(fileName);
                    thumb.Dispose();
                    if (img != null) img.Dispose();
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (thumb != null) thumb.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static void Thumbnail(Stream stream, int width, int height, bool keepScale, ImageFormat format,
                                     Stream ouput)
        {
            Image img = null;
            Image thumb = null;
            try
            {
                img = GetImage(stream);
                thumb = Thumbnail(img, width, height, keepScale);
                thumb.Save(ouput, format);
            }
            finally
            {
                if (thumb != null) thumb.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Thumbnail(Stream stream, int width, int height, bool keepScale, Stream ouput)
        {
            Thumbnail(stream, width, height, keepScale, ImageFormat.Jpeg, ouput);
        }

        public static bool Thumbnail(Stream stream, int width, int height, bool keepScale, ImageFormat format,
                                     string fileName)
        {
            bool ret = false;
            Image img = null;
            Image thumb = null;
            try
            {
                img = GetImage(stream);
                thumb = Thumbnail(img, width, height, keepScale);
                thumb.Save(fileName, format);
                thumb.Dispose();
                if (img != null) img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (thumb != null) thumb.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool Thumbnail(Stream stream, int width, int height, bool keepScale, string fileName)
        {
            return Thumbnail(stream, width, height, keepScale, ImageFormat.Jpeg, fileName);
        }

        public static void Thumbnail(String filePath, int width, int height, bool keepScale, ImageFormat format,
                                     Stream ouput)
        {
            Image img = null;
            Image thumb = null;
            try
            {
                img = GetImage(filePath);
                thumb = Thumbnail(img, width, height, keepScale);
                thumb.Save(ouput, format);
            }
            finally
            {
                if (thumb != null) thumb.Dispose();
                if (img != null) img.Dispose();
            }
        }

        public static void Thumbnail(String filePath, int width, int height, bool keepScale, Stream ouput)
        {
            Thumbnail(filePath, width, height, keepScale, ImageFormat.Jpeg, ouput);
        }

        public static bool Thumbnail(String filePath, int width, int height, bool keepScale, ImageFormat format,
                                     string fileName)
        {
            bool ret = false;
            Image img = null;
            Image thumb = null;
            try
            {
                img = GetImage(filePath);
                thumb = Thumbnail(img, width, height, keepScale);
                thumb.Save(fileName, format);
                thumb.Dispose();
                img.Dispose();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (thumb != null) thumb.Dispose();
                if (img != null) img.Dispose();
            }
            return ret;
        }

        public static bool Thumbnail(String filePath, int width, int height, bool keepScale, string fileName)
        {
            return Thumbnail(filePath, width, height, keepScale, ImageFormat.Jpeg, fileName);
        }

        #endregion
    }
}
