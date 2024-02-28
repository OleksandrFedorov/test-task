using System.Drawing;
using System.Drawing.Imaging;

namespace TestTask.Helpers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Other platforms is not supported yet")]
    public static class ImageHelper
    {
        public static string ToBase64String(this Bitmap bitmap)
        {
            if (bitmap is null)
            {
                return string.Empty;
            }

            using var memeoryStream = new MemoryStream();
            bitmap.Save(memeoryStream, ImageFormat.Jpeg);

            byte[] bytes = new byte[memeoryStream.Length];

            memeoryStream.Position = 0;
            memeoryStream.Read(bytes, 0, (int)memeoryStream.Length);
            memeoryStream.Close();

            return Convert.ToBase64String(bytes);
        }

        public static string GetThumbnailAsBase64(this Bitmap bitmap)
        {
            var maxSide = Math.Max(bitmap.Width, bitmap.Height);
            if (maxSide > 210)
            {
                var size = bitmap.Width > bitmap.Height
                    ? new Size(210, bitmap.Height / (bitmap.Width / 210))
                    : new Size(bitmap.Width / (bitmap.Height / 210), 210);
                bitmap = new Bitmap(bitmap, size);
            }

            return bitmap.ToBase64String();
        }
    }
}
