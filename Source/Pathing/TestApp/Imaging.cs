using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pathing.Math;

namespace TestApp
{
    public static class Imaging
    {
        public static bool ShowOpenDialog(Color startColor, Color endColor, Color pathColor, out Coordinate start, out Coordinate end, out Map map)
        {
            start = default(Coordinate); end = default(Coordinate); map = default(Map);
            using (var dlg = new OpenFileDialog {
                Title = "Select an image",
                Filter = "Any|*.bmp;*.png;*.jpg;*.jpeg;*.gif|Bitmap|*.bmp|Portable Network Graphics|*.png|Jpeg|*.jpg;*.jpeg|Graphics Interchange Format|*.gif"
            }) {
                if (dlg.ShowDialog() != DialogResult.OK) return false;
                using (var img = Image.FromFile(dlg.FileName)) {
                    var pixels = GetPixels(img);
                    map = new Map(img.Width, img.Height);
                    for (var y = 0; y < img.Height; y++)
                        for (var x = 0; x < img.Width; x++)
                        {
                            var color = pixels[x, y];
                            var isStart = Matches(color, startColor);
                            var isEnd = Matches(color, endColor);
                            var isPath = Matches(color, pathColor);
                            if (isStart) start = new Coordinate(x, y);
                            if (isEnd) end = new Coordinate(x, y);
                            if (isStart || isEnd || isPath) map[x, y] = true;
                        }
                }
            }
            return true;
        } 

        private static bool Matches(Pixel pixel, Color color)
        {
            return pixel.Alpha == color.A && pixel.Red == color.R && pixel.Green == color.G && pixel.Blue == color.B;
        }

        private struct Pixel
        {
            public Byte Blue;
            public Byte Green;
            public Byte Red;
            public Byte Alpha;
        }

        private static unsafe Pixel[,] GetPixels(Image img)
        {
            var ld = ((Bitmap)img).LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                var pixels = new Pixel[ld.Width,ld.Height];
                var size = sizeof (Pixel);
                var root = (byte*)ld.Scan0.ToPointer();
                for (var y = 0; y < ld.Height; y++)
                for (var x = 0; x < ld.Width; x++) {
                    var pixel = *((Pixel*) (root + y*ld.Stride + x*size));
                    pixels[x, y] = pixel;
                }
                return pixels;
            } finally { ((Bitmap)img).UnlockBits(ld); }
        }

        public static Point ToPoint(this Coordinate coord) { return new Point((int)coord.X, (int)coord.Y); }
    }
}
