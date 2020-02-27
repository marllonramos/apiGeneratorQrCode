using System.IO;
using System.Drawing;

namespace apiQrCode.Api.Services
{
    public static class ConverterBitmapBytes
    {
        public static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}