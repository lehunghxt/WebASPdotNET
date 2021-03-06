using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Library.Images
{
    public class AddTextToImage
    {
        private string text;
        private string fontName;
        private float fontSize = 8;
        private Bitmap image = null;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public string FontName
        {
            set
            {
                try
                {
                    Font font = new Font(value, 12F);
                    this.fontName = value;
                    font.Dispose();
                }
                catch
                {
                    this.fontName = System.Drawing.FontFamily.GenericSerif.Name;
                }
            }
        }
        public float FontSize
        {
            get { return this.fontSize; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("fontSize", value, "Đối số ra khỏi phạm vi, phải lớn hơn số không");
                this.fontSize = value;
            }
        }
        public AddTextToImage(){}
        public AddTextToImage(string img, string s)
        {
            this.image = (Bitmap)Bitmap.FromFile(img);
            this.Text = s;
        }
        public AddTextToImage(string img, string s, string Name)
        {
            this.image = (Bitmap) Bitmap.FromFile(img);
            this.Text = s;
            this.FontName = Name;
        }
        public AddTextToImage(string img, string s, string Name, int Size)
        {
            this.image = (Bitmap)Bitmap.FromFile(img);
            this.Text = s;
            this.FontName = Name;
            this.FontSize = Size;
        }

        ~AddTextToImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.image.Dispose();
        }

        public void GenerateImage(int x, int y, int w, int h)
        {
            // Tạo một đối tượng đồ hoạ cho bản vẽ.
            Graphics g = Graphics.FromImage(image);
            //Point p = new Point(0, 0);
            Rectangle rec = new Rectangle(x,y,w,h);

            // Tạo Font
            Font font = new Font(this.fontName, this.fontSize);

            // Thiết lập các định dạng văn bản.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;

            // Tạo hình text
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, 3, this.fontSize, rec, format);
            
            // Phan hinh cua text
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.Blue);
            g.FillPath(hatchBrush, path);

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
        }
    }
}
