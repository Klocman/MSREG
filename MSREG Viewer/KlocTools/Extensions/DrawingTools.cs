using System.Drawing;
using System.Drawing.Drawing2D;

namespace Klocman.Extensions
{
    public static class DrawingTools
    {
        #region Methods

        public static Color ColorLerp(Color from, Color to, float ratio)
        {
            var aDiff = to.A - from.A;
            var rDiff = to.R - from.R;
            var gDiff = to.G - from.G;
            var bDiff = to.B - from.B;

            return Color.FromArgb((byte) (from.A + ratio*aDiff), (byte) (from.R + ratio*rDiff),
                (byte) (from.G + ratio*gDiff), (byte) (from.B + ratio*bDiff));
        }

        public static Bitmap ResizeBitmap(Image sourceBmp, int newWidth, int newHeight)
        {
            var result = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.DrawImage(sourceBmp, 0, 0, newWidth, newHeight);
            }
            return result;
        }

        #endregion Methods
    }
}